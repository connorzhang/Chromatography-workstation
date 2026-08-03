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

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct ValveEvent {
    pub time: f64,
    pub event_id: u16,
    pub state: bool,
}

pub struct SequenceEngine {
    run_start_time: Option<std::time::Instant>,
    program: Option<Vec<ValveEvent>>,
    executed_events: Vec<usize>,
}

pub struct TempController {
    state: Arc<Mutex<TempState>>,
    voltage_state: Arc<Mutex<crate::hal_voltage::VoltageState>>,
    stop_signal: tokio::sync::mpsc::Sender<()>,
    req_tx: tokio::sync::mpsc::Sender<TempRequest>,
    engine: Arc<Mutex<SequenceEngine>>,
}

enum TempRequest {
    SetTemp(u16, i16), // channel(0-7), value
    SetSwitch(u16, bool), // channel(0-7), state
    SetMode(u16, i16), // channel(0-7), mode
}

impl TempController {
    pub fn new(
        port_name: String, 
        voltage_state: Arc<Mutex<crate::hal_voltage::VoltageState>>,
        epc_state: Arc<Mutex<crate::hal_epc::EpcState>>,
        mut epc_req_rx: tokio::sync::mpsc::Receiver<crate::hal_epc::EpcRequest>
    ) -> Self {
        let state = Arc::new(Mutex::new(TempState::default()));
        let (stop_signal, mut rx_stop) = tokio::sync::mpsc::channel(1);
        let (req_tx, mut req_rx) = tokio::sync::mpsc::channel(10);
        let req_tx_clone = req_tx.clone();

        let engine = Arc::new(Mutex::new(SequenceEngine {
            run_start_time: None,
            program: None,
            executed_events: Vec::new(),
        }));
        let engine_clone = engine.clone();

        // Sequence Engine Background Task
        tokio::spawn(async move {
            loop {
                tokio::time::sleep(Duration::from_millis(500)).await;
                
                let mut executed_now = Vec::new();
                {
                    let eng = engine_clone.lock().await;
                    if let (Some(start_time), Some(prog)) = (eng.run_start_time, &eng.program) {
                        let elapsed_min = start_time.elapsed().as_secs_f64() / 60.0;
                        
                        for (i, event) in prog.iter().enumerate() {
                            if !eng.executed_events.contains(&i) && elapsed_min >= event.time {
                                executed_now.push((i, event.clone()));
                            }
                        }
                    }
                }
                
                if !executed_now.is_empty() {
                    let mut eng = engine_clone.lock().await;
                    for (i, event) in executed_now {
                        let ch = (event.event_id - 1) + 4;
                        let _ = req_tx_clone.send(TempRequest::SetSwitch(ch, event.state)).await;
                        eng.executed_events.push(i);
                    }
                }
            }
        });

        let controller = Self {
            state: state.clone(),
            voltage_state: voltage_state.clone(),
            stop_signal,
            req_tx,
            engine,
        };

        let state_clone = state.clone();
        let voltage_state_clone = voltage_state.clone();
        let epc_state_clone = epc_state.clone();
        std::thread::spawn(move || {
            let rt = tokio::runtime::Builder::new_current_thread().enable_all().build().unwrap();
            rt.block_on(async move {
                let builder = tokio_serial::new(port_name, 9600)
                    .data_bits(tokio_serial::DataBits::Eight)
                    .stop_bits(tokio_serial::StopBits::One)
                    .parity(tokio_serial::Parity::None);

                let port = match tokio_serial::SerialStream::open(&builder) {
                    Ok(p) => p,
                    Err(e) => {
                        eprintln!("Failed to open RS485 (Temp/Voltage) port: {}", e);
                        return;
                    }
                };
                
                println!("RS485 port opened successfully");

                let mut ctx = tokio_modbus::prelude::rtu::attach_slave(port, Slave(20));

                loop {
                    tokio::select! {
                        _ = rx_stop.recv() => {
                            break;
                        }
                        req = req_rx.recv() => {
                            if let Some(r) = req {
                                ctx.set_slave(Slave(20));
                                match r {
                                    TempRequest::SetTemp(ch, val) => {
                                        let _ = ctx.write_single_register(42 + ch, val as u16).await;
                                    }
                                    TempRequest::SetSwitch(ch, val) => {
                                        let _ = ctx.write_single_register(78 + ch, 1).await;
                                        let _ = ctx.write_single_coil(32 + ch, val).await;
                                    }
                                    TempRequest::SetMode(ch, val) => {
                                        let _ = ctx.write_single_register(78 + ch, val as u16).await;
                                    }
                                }
                            }
                        }
                        epc_req = epc_req_rx.recv() => {
                            if let Some(r) = epc_req {
                                ctx.set_slave(Slave(21));
                                match r {
                                    crate::hal_epc::EpcRequest::SetMode(m) => {
                                        let _ = ctx.write_single_register(0x0014, m).await;
                                    }
                                    crate::hal_epc::EpcRequest::SetPressure(p) => {
                                        let bits = p.to_bits();
                                        let regs = [(bits >> 16) as u16, (bits & 0xFFFF) as u16];
                                        let _ = ctx.write_multiple_registers(0x0015, &regs).await;
                                    }
                                    crate::hal_epc::EpcRequest::SetFlow(f) => {
                                        let bits = f.to_bits();
                                        let regs = [(bits >> 16) as u16, (bits & 0xFFFF) as u16];
                                        let _ = ctx.write_multiple_registers(0x0017, &regs).await;
                                    }
                                    crate::hal_epc::EpcRequest::SetGasType(g) => {
                                        let _ = ctx.write_single_register(0x0019, g).await;
                                    }
                                    crate::hal_epc::EpcRequest::SetUnits(u) => {
                                        let _ = ctx.write_single_register(0x001A, u).await;
                                    }
                                }
                            }
                        }
                        _ = tokio::time::sleep(Duration::from_millis(500)) => {
                            // 1. Read Voltage (Slave 1)
                            ctx.set_slave(Slave(1));
                            let mut v_ok = false;
                            let mut voltage = 0.0;
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_holding_registers(0x0020, 2)).await {
                                if res.len() == 2 {
                                    let reg1 = res[0];
                                    let reg2 = res[1];
                                    let packed = ((reg2 as u32) << 16) | (reg1 as u32);
                                    voltage = f32::from_bits(packed);
                                    v_ok = true;
                                }
                            } else {
                                if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_input_registers(0x0020, 2)).await {
                                    if res.len() == 2 {
                                        let reg1 = res[0];
                                        let reg2 = res[1];
                                        let packed = ((reg2 as u32) << 16) | (reg1 as u32);
                                        voltage = f32::from_bits(packed);
                                        v_ok = true;
                                    }
                                }
                            }
                            
                            let mut vst = voltage_state_clone.lock().await;
                            vst.connected = v_ok;
                            if v_ok {
                                vst.voltage = voltage;
                                vst.last_update = std::time::Instant::now();
                            }

                            // 2. Read Temperatures (Slave 20)
                            ctx.set_slave(Slave(20));
                            let mut ok = true;
                            let mut temps = [0.0; 8];
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_holding_registers(360, 16)).await {
                                for i in 0..8 {
                                    let reg1 = res[i * 2];
                                    let reg2 = res[i * 2 + 1];
                                    let packed = ((reg1 as u32) << 16) | (reg2 as u32);
                                    temps[i] = f32::from_bits(packed);
                                }
                            } else {
                                ok = false;
                            }

                            let mut set_temps = [0; 8];
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_holding_registers(42, 8)).await {
                                for i in 0..8 {
                                    set_temps[i] = res[i] as i16;
                                }
                            } else {
                                ok = false;
                            }

                            let mut disconnected = [false; 8];
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_coils(192, 8)).await {
                                for i in 0..8 {
                                    disconnected[i] = res[i];
                                }
                            } else {
                                ok = false;
                            }

                            let mut switches = [false; 8];
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_coils(32, 8)).await {
                                if res.len() >= 8 {
                                    for i in 0..8 {
                                        switches[i] = res[i];
                                    }
                                } else {
                                    ok = false;
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

                            // 3. Read EPC (Slave 21)
                            ctx.set_slave(Slave(21));
                            if let Ok(Ok(Ok(res))) = tokio::time::timeout(Duration::from_millis(1000), ctx.read_holding_registers(0, 7)).await {
                                if res.len() >= 7 {
                                    let mut epc_st = epc_state_clone.lock().await;
                                    epc_st.connected = true;
                                    
                                    let reg1 = res[0];
                                    let reg2 = res[1];
                                    epc_st.real_pressure = f32::from_bits(((reg1 as u32) << 16) | (reg2 as u32));
                                    
                                    let reg3 = res[2];
                                    let reg4 = res[3];
                                    epc_st.real_flow = f32::from_bits(((reg3 as u32) << 16) | (reg4 as u32));
                                    
                                    epc_st.valve_open = res[4];
                                    epc_st.status = res[5];
                                    epc_st.temp = res[6] as i16;
                                    epc_st.last_update = std::time::Instant::now();
                                }
                            }
                        }
                    }
                }
            });
        });

        controller
    }

    pub async fn get_state(&self) -> TempState {
        let mut st = self.state.lock().await.clone();
        if st.last_update.elapsed() > Duration::from_secs(10) {
            st.connected = false;
        }
        st
    }

    pub async fn start_sequence(&self, program: Vec<ValveEvent>) {
        let mut eng = self.engine.lock().await;
        eng.program = Some(program);
        eng.executed_events.clear();
        eng.run_start_time = Some(std::time::Instant::now());
    }

    pub async fn stop_sequence(&self) {
        let mut eng = self.engine.lock().await;
        eng.run_start_time = None;
    }

    pub async fn set_temperature(&self, channel: u16, value: i16) -> Result<(), String> {
        self.req_tx.send(TempRequest::SetTemp(channel, value)).await.map_err(|e| e.to_string())
    }

    pub async fn set_switch(&self, channel: u16, state: bool) -> Result<(), String> {
        self.req_tx.send(TempRequest::SetSwitch(channel, state)).await.map_err(|e| e.to_string())
    }

    pub async fn set_mode(&self, channel: u16, mode: i16) -> Result<(), String> {
        self.req_tx.send(TempRequest::SetMode(channel, mode)).await.map_err(|e| e.to_string())
    }

    pub async fn close(&self) {
        let _ = self.stop_signal.send(()).await;
    }
}





