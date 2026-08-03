use std::sync::Arc;
use std::time::Duration;
use tokio::sync::Mutex;
use serde::{Serialize, Deserialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct EpcState {
    pub connected: bool,
    pub real_pressure: f32,
    pub real_flow: f32,
    pub valve_open: u16,
    pub status: u16,
    pub temp: i16,
    #[serde(skip, default = "std::time::Instant::now")]
    pub last_update: std::time::Instant,
}

impl Default for EpcState {
    fn default() -> Self {
        Self {
            connected: false,
            real_pressure: 0.0,
            real_flow: 0.0,
            valve_open: 0,
            status: 0,
            temp: 0,
            last_update: std::time::Instant::now(),
        }
    }
}

pub enum EpcRequest {
    SetMode(u16),
    SetPressure(f32),
    SetFlow(f32),
    SetGasType(u16),
    SetUnits(u16),
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct EpcRamp {
    pub id: String,
    pub rate: f64,
    pub final_value: f64,
    pub hold_time: f64,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct EpcProgram {
    pub mode: String,
    pub initial_value: f64,
    pub initial_time: f64,
    pub ramps: Vec<EpcRamp>,
}

pub struct SequenceEngine {
    run_start_time: Option<std::time::Instant>,
    program: Option<EpcProgram>,
}

pub struct EpcController {
    pub state: Arc<Mutex<EpcState>>,
    pub req_tx: tokio::sync::mpsc::Sender<EpcRequest>,
    pub engine: Arc<Mutex<SequenceEngine>>,
}

impl EpcController {
    pub fn new(req_tx: tokio::sync::mpsc::Sender<EpcRequest>) -> Self {
        let req_tx_clone = req_tx.clone();
        let engine = Arc::new(Mutex::new(SequenceEngine {
            run_start_time: None,
            program: None,
        }));
        let engine_clone = engine.clone();

        // Sequence Engine Background Task
        tokio::spawn(async move {
            loop {
                tokio::time::sleep(Duration::from_secs(1)).await;
                
                let mut eng = engine_clone.lock().await;
                if let (Some(start_time), Some(prog)) = (eng.run_start_time, &eng.program) {
                    let elapsed_min = start_time.elapsed().as_secs_f64() / 60.0;
                    
                    let mut current_target = prog.initial_value;
                    let mut time_accum = prog.initial_time;
                    let mut ramp_active = false;

                    if elapsed_min < time_accum {
                        // In initial hold
                        ramp_active = true;
                    } else {
                        // Check ramps
                        for ramp in &prog.ramps {
                            let ramp_time = if ramp.rate > 0.0 {
                                (ramp.final_value - current_target).abs() / ramp.rate
                            } else {
                                0.0
                            };
                            
                            if elapsed_min < time_accum + ramp_time {
                                // Currently ramping
                                current_target += ramp.rate * (elapsed_min - time_accum);
                                ramp_active = true;
                                break;
                            }
                            time_accum += ramp_time;
                            current_target = ramp.final_value;

                            if elapsed_min < time_accum + ramp.hold_time {
                                // Currently holding
                                ramp_active = true;
                                break;
                            }
                            time_accum += ramp.hold_time;
                        }
                    }

                    // Send command to hardware
                    if prog.mode.contains("Pressure") {
                        let _ = req_tx_clone.send(EpcRequest::SetPressure(current_target as f32)).await;
                    } else {
                        let _ = req_tx_clone.send(EpcRequest::SetFlow(current_target as f32)).await;
                    }

                    // Standard GC behavior: after all ramps, we HOLD the last final_value.
                    // We don't stop the hardware automatically.
                }
            }
        });

        Self {
            state: Arc::new(Mutex::new(EpcState::default())),
            req_tx,
            engine,
        }
    }

    pub async fn get_state(&self) -> EpcState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(10) {
            st.connected = false;
        }
        st
    }

    pub async fn start_sequence(&self, program: EpcProgram) {
        let mut eng = self.engine.lock().await;
        eng.program = Some(program);
        eng.run_start_time = Some(std::time::Instant::now());
    }

    pub async fn stop_sequence(&self) {
        let mut eng = self.engine.lock().await;
        eng.run_start_time = None;
    }

    pub async fn set_mode(&self, mode: u16) -> Result<(), String> {
        self.req_tx.send(EpcRequest::SetMode(mode)).await.map_err(|e| e.to_string())
    }

    pub async fn set_pressure(&self, pressure: f32) -> Result<(), String> {
        self.req_tx.send(EpcRequest::SetPressure(pressure)).await.map_err(|e| e.to_string())
    }

    pub async fn set_flow(&self, flow: f32) -> Result<(), String> {
        self.req_tx.send(EpcRequest::SetFlow(flow)).await.map_err(|e| e.to_string())
    }

    pub async fn set_gas_type(&self, gas_type: u16) -> Result<(), String> {
        self.req_tx.send(EpcRequest::SetGasType(gas_type)).await.map_err(|e| e.to_string())
    }

    pub async fn set_units(&self, units: u16) -> Result<(), String> {
        self.req_tx.send(EpcRequest::SetUnits(units)).await.map_err(|e| e.to_string())
    }

    pub async fn close(&self) {
        // Nothing to close
    }
}
