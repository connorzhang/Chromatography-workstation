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
    pub state: Arc<Mutex<VoltageState>>,
}

impl VoltageController {
    pub fn new() -> Self {
        Self {
            state: Arc::new(Mutex::new(VoltageState::default())),
        }
    }

    pub async fn get_state(&self) -> VoltageState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(10) {
            st.connected = false;
        }
        st
    }

    pub async fn close(&self) {
        // Nothing to close
    }
}





