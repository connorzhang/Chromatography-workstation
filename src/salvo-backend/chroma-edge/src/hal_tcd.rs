use std::sync::Arc;
use std::time::Duration;
use tokio::sync::Mutex;
use tokio::io::{AsyncReadExt, AsyncWriteExt};
use tokio_serial::{SerialPortBuilderExt, SerialStream};
use serde::{Serialize, Deserialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct TcdState {
    pub connected: bool,
    pub bridge_current: u8,
    pub values: [f64; 20],
    pub frame_count: u64,
    #[serde(skip, default = "std::time::Instant::now")]
    pub last_update: std::time::Instant,
}

impl Default for TcdState {
    fn default() -> Self {
        Self {
            connected: false,
            bridge_current: 0,
            values: [0.0; 20],
            frame_count: 0,
            last_update: std::time::Instant::now(),
        }
    }
}

pub struct TcdController {
    port_name: String,
    state: Arc<Mutex<TcdState>>,
    tx_cmd: tokio::sync::mpsc::Sender<Vec<u8>>,
    stop_signal: tokio::sync::mpsc::Sender<()>,
}

impl TcdController {
    pub fn new(port_name: String) -> Self {
        let (tx_cmd, rx_cmd) = tokio::sync::mpsc::channel(10);
        let (stop_signal, mut rx_stop) = tokio::sync::mpsc::channel(1);
        let state = Arc::new(Mutex::new(TcdState::default()));

        let controller = Self {
            port_name: port_name.clone(),
            state: state.clone(),
            tx_cmd,
            stop_signal,
        };

        let state_clone = state.clone();
        std::thread::spawn(move || {
            let rt = tokio::runtime::Builder::new_current_thread().enable_all().build().unwrap();
            rt.block_on(async move {
                let mut port = match tokio_serial::new(port_name, 38400)
                    .data_bits(tokio_serial::DataBits::Eight)
                    .stop_bits(tokio_serial::StopBits::One)
                    .parity(tokio_serial::Parity::None)
                    .open_native_async()
                {
                    Ok(p) => p,
                    Err(e) => {
                        eprintln!("Failed to open TCD port: {}", e);
                        return;
                    }
                };

                {
                    let mut st = state_clone.lock().await;
                    st.connected = true;
                    st.last_update = std::time::Instant::now();
                }

                let mut buf = vec![0u8; 1024];
                let mut frame_buf = Vec::new();
                let mut rx_cmd = rx_cmd;

                loop {
                    tokio::select! {
                        _ = rx_stop.recv() => {
                            break;
                        }
                        cmd = rx_cmd.recv() => {
                            if let Some(c) = cmd {
                                let _ = port.write_all(&c).await;
                            }
                        }
                        res = port.read(&mut buf) => {
                            match res {
                                Ok(0) => {
                                    break;
                                }
                                Ok(n) => {
                                    frame_buf.extend_from_slice(&buf[..n]);
                                    Self::process_frames(&mut frame_buf, &state_clone).await;
                                }
                                Err(_) => {
                                    tokio::time::sleep(Duration::from_millis(100)).await;
                                }
                            }
                        }
                    }
                }

                {
                    let mut st = state_clone.lock().await;
                    st.connected = false;
                }
            });
        });

        controller
    }

    async fn process_frames(frame_buf: &mut Vec<u8>, state: &Arc<Mutex<TcdState>>) {
        while frame_buf.len() >= 87 {
            let mut idx = None;
            for i in 0..=(frame_buf.len() - 87) {
                if frame_buf[i] == 0x45 && frame_buf[i+1] == 0x45 && frame_buf[i+2] == 0xFF && frame_buf[i+3] == 0x01 {
                    idx = Some(i);
                    break;
                }
            }

            if let Some(i) = idx {
                let frame = &frame_buf[i..i+87];
                if frame[85] == 0x0D && frame[86] == 0x0A {
                    Self::parse_frame(frame, state).await;
                } else {
                    Self::parse_frame(frame, state).await;
                }
                *frame_buf = frame_buf[i+87..].to_vec();
            } else {
                *frame_buf = frame_buf[frame_buf.len() - 86..].to_vec();
                break;
            }
        }
    }

    async fn parse_frame(frame: &[u8], state: &Arc<Mutex<TcdState>>) {
        let bridge_current = frame[84];
        let mut values = [0.0; 20];
        
        let data_offset = 4;
        for i in 0..20 {
            let idx = data_offset + (i * 4);
            let nibbles = [
                frame[idx] >> 4,
                frame[idx] & 0x0F,
                frame[idx+1] >> 4,
                frame[idx+1] & 0x0F,
                frame[idx+2] >> 4,
                frame[idx+2] & 0x0F,
                frame[idx+3] >> 4,
                frame[idx+3] & 0x0F,
            ];

            let sign = if nibbles[0] == 1 { -1.0 } else { 1.0 };
            
            let mut uv_value: i64 = 0;
            for j in 1..8 {
                uv_value = uv_value * 10 + (nibbles[j] as i64);
            }

            values[i] = sign * (uv_value as f64) / 1000.0;
        }

        let mut st = state.lock().await;
        st.bridge_current = bridge_current;
        st.values = values;
        st.frame_count += 1;
        st.last_update = std::time::Instant::now();
    }

    pub async fn get_state(&self) -> TcdState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(3) {
            st.connected = false;
        }
        st
    }

    pub async fn set_bridge_current(&self, val: u8) -> Result<(), String> {
        let cmd = vec![0x47, 0x45, 0x45, 0x02, 0x0E, val];
        self.tx_cmd.send(cmd).await.map_err(|e| e.to_string())
    }

    pub async fn zeroing(&self) -> Result<(), String> {
        let cmd = vec![0x47, 0x45, 0x45, 0x02, 0x0B, 0x00];
        self.tx_cmd.send(cmd).await.map_err(|e| e.to_string())
    }

    pub async fn read_bridge_current(&self) -> Result<(), String> {
        let cmd = vec![0x47, 0x45, 0x45, 0x02, 0x08, 0x50];
        self.tx_cmd.send(cmd).await.map_err(|e| e.to_string())
    }

    pub async fn close(&self) {
        let _ = self.stop_signal.send(()).await;
    }
}


