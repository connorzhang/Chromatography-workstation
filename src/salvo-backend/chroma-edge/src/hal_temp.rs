use std::sync::Arc;
use std::time::Duration;
use tokio::sync::Mutex;
use tokio_serial::SerialPortBuilderExt;
use tokio_modbus::prelude::*;
use serde::{Serialize, Deserialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TempState {
    pub connected: bool,
    pub temperatures: [f32; 8],
    pub set_temperatures: [i16; 8],
    pub disconnected_status: [bool; 8],
    pub switch_states: [bool; 8], // Address 32-39
    #[serde(skip, default = "std::time::Instant::now")]
    pub last_update: std::time::Instant,
}

impl Default for TempState {
    fn default() -> Self {
        Self {
            connected: false,
            temperatures: [0.0; 8],
            set_temperatures: [0; 8],
            disconnected_status: [false; 8],
            switch_states: [false; 8],
            last_update: std::time::Instant::now(),
        }
    }
}

pub struct TempController {
    state: Arc<Mutex<TempState>>,
    stop_signal: tokio::sync::mpsc::Sender<()>,
    req_tx: tokio::sync::mpsc::Sender<TempRequest>,
}

enum TempRequest {
    SetTemp(u16, i16), // channel(0-7), value
    SetSwitch(u16, bool), // channel(0-7), state
}

impl TempController {
    pub fn new(port_name: String) -> Self {
        let state = Arc::new(Mutex::new(TempState::default()));
        let (stop_signal, mut rx_stop) = tokio::sync::mpsc::channel(1);
        let (req_tx, mut req_rx) = tokio::sync::mpsc::channel(10);

        let controller = Self {
            state: state.clone(),
            stop_signal,
            req_tx,
        };

        let state_clone = state.clone();
        tokio::spawn(async move {
            let builder = tokio_serial::new(port_name, 9600)
                .data_bits(tokio_serial::DataBits::Eight)
                .stop_bits(tokio_serial::StopBits::One)
                .parity(tokio_serial::Parity::None);

            let port = match tokio_serial::SerialStream::open(&builder) {
                Ok(p) => p,
                Err(e) => {
                    eprintln!("Failed to open Temp port: {}", e);
                    return;
                }
            };

            let mut ctx = tokio_modbus::prelude::rtu::attach_slave(port, Slave(20));

            loop {
                tokio::select! {
                    _ = rx_stop.recv() => {
                        break;
                    }
                    req = req_rx.recv() => {
                        if let Some(r) = req {
                            match r {
                                TempRequest::SetTemp(ch, val) => {
                                    let _ = ctx.write_single_register(42 + ch, val as u16).await;
                                }
                                TempRequest::SetSwitch(ch, val) => {
                                    // Make sure mode is IO (Address 78+ch = 1)
                                    let _ = ctx.write_single_register(78 + ch, 1).await;
                                    let _ = ctx.write_single_coil(32 + ch, val).await;
                                }
                            }
                        }
                    }
                    _ = tokio::time::sleep(Duration::from_millis(500)) => {
                        // Read Temperatures (Addr 360, 16 regs)
                        let mut ok = true;
                        let mut temps = [0.0; 8];
                        if let Ok(Ok(res)) = ctx.read_holding_registers(360, 16).await {
                            for i in 0..8 {
                                let reg1 = res[i * 2];
                                let reg2 = res[i * 2 + 1];
                                // ABCD order: reg1 is high word, reg2 is low word
                                let packed = ((reg1 as u32) << 16) | (reg2 as u32);
                                temps[i] = f32::from_bits(packed);
                            }
                        } else {
                            ok = false;
                        }

                        // Read Set Temperatures (Addr 42, 8 regs)
                        let mut set_temps = [0; 8];
                        if let Ok(Ok(res)) = ctx.read_holding_registers(42, 8).await {
                            for i in 0..8 {
                                set_temps[i] = res[i] as i16;
                            }
                        } else {
                            ok = false;
                        }

                        // Read Disconnect Status (Addr 192, 8 coils)
                        let mut disconnected = [false; 8];
                        if let Ok(Ok(res)) = ctx.read_coils(192, 8).await {
                            for i in 0..8 {
                                disconnected[i] = res[i];
                            }
                        } else {
                            ok = false;
                        }

                        // Read Switch Status (Addr 32, 8 coils)
                        let mut switches = [false; 8];
                        if let Ok(Ok(res)) = ctx.read_coils(32, 8).await {
                            for i in 0..8 {
                                switches[i] = res[i];
                            }
                        } else {
                            ok = false;
                        }

                        let mut st = state_clone.lock().await;
                        st.connected = ok;
                        if ok {
                            st.temperatures = temps;
                            st.set_temperatures = set_temps;
                            st.disconnected_status = disconnected;
                            st.switch_states = switches;
                            st.last_update = std::time::Instant::now();
                        }
                    }
                }
            }
        });

        controller
    }

    pub async fn get_state(&self) -> TempState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(3) {
            st.connected = false;
        }
        st
    }

    pub async fn set_temperature(&self, channel: u16, value: i16) -> Result<(), String> {
        self.req_tx.send(TempRequest::SetTemp(channel, value)).await.map_err(|e| e.to_string())
    }

    pub async fn set_switch(&self, channel: u16, state: bool) -> Result<(), String> {
        self.req_tx.send(TempRequest::SetSwitch(channel, state)).await.map_err(|e| e.to_string())
    }

    pub async fn close(&self) {
        let _ = self.stop_signal.send(()).await;
    }
}





