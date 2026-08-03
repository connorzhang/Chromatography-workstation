use serde::{Deserialize, Serialize};
use std::sync::Arc;
use crate::hal_tcd::{TcdController, TcdState};
use crate::hal_temp::{TempController, TempState};
use crate::hal_voltage::{VoltageController, VoltageState};
use crate::hal_epc::{EpcController, EpcState};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TempZoneInfo {
    pub id: u8,
    pub name: String,
    pub max_temp: f32,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct SwitchInfo {
    pub id: u8,
    pub name: String,
    pub switch_type: String,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct SignalInfo {
    pub id: u8,
    pub name: String,
    pub unit: String,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct AnalyzerCapabilities {
    pub device_type: String,
    pub version: String,
    pub temp_zones: Vec<TempZoneInfo>,
    pub switches: Vec<SwitchInfo>,
    pub signals: Vec<SignalInfo>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TempZoneState {
    pub id: u8,
    pub current: f32,
    pub target: f32,
    pub connected: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct SwitchState {
    pub id: u8,
    pub is_on: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct SignalState {
    pub id: u8,
    pub value: f64,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct UnifiedState {
    pub status: String,
    pub temp_zones: Vec<TempZoneState>,
    pub switches: Vec<SwitchState>,
    pub signals: Vec<SignalState>,
}

pub struct AnalyzerController {
    pub capabilities: AnalyzerCapabilities,
    pub tcd: Option<Arc<TcdController>>,
    pub temp: Option<Arc<TempController>>,
    pub voltage: Option<Arc<VoltageController>>,
    pub epc: Option<Arc<EpcController>>,
}

impl AnalyzerController {
    pub fn new() -> Self {
        let capabilities = AnalyzerCapabilities {
            device_type: "TCD_ANALYZER".to_string(),
            version: "1.0.0".to_string(),
            temp_zones: vec![
                TempZoneInfo { id: 0, name: "Valve Box".to_string(), max_temp: 250.0 },
                TempZoneInfo { id: 1, name: "Column".to_string(), max_temp: 400.0 },
                TempZoneInfo { id: 2, name: "Detector".to_string(), max_temp: 300.0 },
                TempZoneInfo { id: 3, name: "Valve Head".to_string(), max_temp: 200.0 },
            ],
            switches: vec![
                SwitchInfo { id: 0, name: "Valve Switch".to_string(), switch_type: "valve".to_string() },
                SwitchInfo { id: 1, name: "Injection".to_string(), switch_type: "relay".to_string() },
                SwitchInfo { id: 2, name: "Cooling Fan".to_string(), switch_type: "fan".to_string() },
                SwitchInfo { id: 3, name: "Reserved".to_string(), switch_type: "relay".to_string() },
            ],
            signals: vec![
                SignalInfo { id: 0, name: "TCD Signal".to_string(), unit: "mV".to_string() },
                SignalInfo { id: 1, name: "Aux Voltage".to_string(), unit: "V".to_string() },
            ],
        };

        Self {
            capabilities,
            tcd: None,
            temp: None,
            voltage: None,
            epc: None,
        }
    }

    pub fn connect_hardware(&mut self, tcd_port: String, temp_port: String, voltage_port: String) {
        if let Some(c) = self.tcd.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.temp.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.voltage.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.epc.take() { tokio::spawn(async move { c.close().await; }); }

        self.tcd = Some(Arc::new(TcdController::new(tcd_port)));
        
        let vc = Arc::new(VoltageController::new());
        let (epc_req_tx, epc_req_rx) = tokio::sync::mpsc::channel(10);
        let epc_ctrl = Arc::new(EpcController::new(epc_req_tx));
        
        self.temp = Some(Arc::new(TempController::new(temp_port, vc.state.clone(), epc_ctrl.state.clone(), epc_req_rx)));
        self.voltage = Some(vc);
        self.epc = Some(epc_ctrl);
    }

    pub fn disconnect_hardware(&mut self) {
        if let Some(c) = self.tcd.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.temp.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.voltage.take() { tokio::spawn(async move { c.close().await; }); }
        if let Some(c) = self.epc.take() { tokio::spawn(async move { c.close().await; }); }
    }

    pub async fn get_state(&self) -> UnifiedState {
        let mut temp_zones = Vec::new();
        let mut switches = Vec::new();
        let mut signals = Vec::new();

        if let Some(temp) = &self.temp {
            let st = temp.get_state().await;
            for i in 0..4 {
                temp_zones.push(TempZoneState {
                    id: i as u8,
                    current: st.temperatures[i],
                    target: st.set_temperatures[i] as f32,
                    connected: st.connected && !st.disconnected_status[i],
                });
                switches.push(SwitchState {
                    id: i as u8,
                    is_on: st.switch_states[i],
                });
            }
        } else {
            for i in 0..4 {
                temp_zones.push(TempZoneState { id: i as u8, current: 0.0, target: 0.0, connected: false });
                switches.push(SwitchState { id: i as u8, is_on: false });
            }
        }

        if let Some(tcd) = &self.tcd {
            let st = tcd.get_state().await;
            signals.push(SignalState { id: 0, value: st.values[0] });
        } else {
            signals.push(SignalState { id: 0, value: 0.0 });
        }

        if let Some(vol) = &self.voltage {
            let st = vol.get_state().await;
            signals.push(SignalState { id: 1, value: st.voltage as f64 });
        } else {
            signals.push(SignalState { id: 1, value: 0.0 });
        }

        let status = if self.tcd.is_some() || self.temp.is_some() || self.voltage.is_some() {
            "running".to_string()
        } else {
            "offline".to_string()
        };

        UnifiedState {
            status,
            temp_zones,
            switches,
            signals,
        }
    }

    pub async fn control_temp(&self, id: u8, target: f32) -> Result<(), String> {
        if let Some(temp) = &self.temp {
            temp.set_temperature(id as u16, target as i16).await
        } else {
            Err("Temp controller offline".to_string())
        }
    }

    pub async fn control_switch(&self, id: u8, is_on: bool) -> Result<(), String> {
        if let Some(temp) = &self.temp {
            temp.set_switch(id as u16, is_on).await
        } else {
            Err("Switch controller offline".to_string())
        }
    }

    pub async fn tcd_set_bridge(&self, val: u8) -> Result<(), String> {
        if let Some(tcd) = &self.tcd {
            tcd.set_bridge_current(val).await
        } else {
            Err("TCD controller offline".to_string())
        }
    }

    pub async fn tcd_zeroing(&self) -> Result<(), String> {
        if let Some(tcd) = &self.tcd {
            tcd.zeroing().await
        } else {
            Err("TCD controller offline".to_string())
        }
    }
}


