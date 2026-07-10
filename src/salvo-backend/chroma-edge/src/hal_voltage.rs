use std::sync::Arc;
use std::time::Duration;
use tokio::sync::Mutex;
use tokio_serial::SerialPortBuilderExt;
use tokio_modbus::prelude::*;
use serde::{Serialize, Deserialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct VoltageState {
    pub connected: bool,
    pub voltage: f32, // Raw voltage (V)
    #[serde(skip, default = "std::time::Instant::now")]
    pub last_update: std::time::Instant,
}

impl Default for VoltageState {
    fn default() -> Self {
        Self {
            connected: false,
            voltage: 0.0,
            last_update: std::time::Instant::now(),
        }
    }
}

pub struct VoltageController {
    state: Arc<Mutex<VoltageState>>,
    stop_signal: tokio::sync::mpsc::Sender<()>,
}

impl VoltageController {
    pub fn new(port_name: String) -> Self {
        let state = Arc::new(Mutex::new(VoltageState::default()));
        let (stop_signal, mut rx_stop) = tokio::sync::mpsc::channel(1);

        let controller = Self {
            state: state.clone(),
            stop_signal,
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
                    eprintln!("Failed to open Voltage port: {}", e);
                    return;
                }
            };

            let mut ctx = tokio_modbus::prelude::rtu::attach_slave(port, Slave(1));

            loop {
                tokio::select! {
                    _ = rx_stop.recv() => {
                        break;
                    }
                    _ = tokio::time::sleep(Duration::from_millis(100)) => {
                        // Read Holding Registers 0x0020 (2 regs)
                        let mut ok = false;
                        let mut voltage = 0.0;

                        if let Ok(Ok(res)) = ctx.read_holding_registers(0x0020, 2).await {
                            if res.len() == 2 {
                                // CDAB byte order
                                // reg1 is CD, reg2 is AB -> AB CD
                                let reg1 = res[0];
                                let reg2 = res[1];
                                let packed = ((reg2 as u32) << 16) | (reg1 as u32);
                                voltage = f32::from_bits(packed);
                                ok = true;
                            }
                        }

                        let mut st = state_clone.lock().await;
                        st.connected = ok;
                        if ok {
                            st.voltage = voltage;
                            st.last_update = std::time::Instant::now();
                        }
                    }
                }
            }
        });

        controller
    }

    pub async fn get_state(&self) -> VoltageState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(3) {
            st.connected = false;
        }
        st
    }

    pub async fn close(&self) {
        let _ = self.stop_signal.send(()).await;
    }
}





