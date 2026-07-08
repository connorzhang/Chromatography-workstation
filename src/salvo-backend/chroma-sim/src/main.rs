use serde::Serialize;
use std::sync::Arc;
use std::time::Duration;
use tokio::io::{AsyncBufReadExt, AsyncWriteExt, BufReader};
use tokio::net::TcpListener;
use tokio::sync::Mutex;
use tokio::time;

#[derive(Serialize, Clone)]
struct SimData {
    msg_type: String,
    state: String,
    time: f64,
    signal: f64,
    pressure: f64,
    temperature: f64,
    tcd_bridge_current: String,
    tcd_temp: f64,
    tcd_polarity: String,
    fid_flame: String,
    fid_temp: f64,
    ms_vacuum: String,
    prep_valve: String,
}

struct HardwareState {
    sys_state: String, // IDLE, RUNNING, ERROR
    run_time: f64,
    pump_on: bool,
    flow_setpoint: f64,
    oven_on: bool,
    oven_setpoint: f64,
    current_temp: f64,
    current_pressure: f64,
}

#[tokio::main]
async fn main() {
    let hw_state = Arc::new(Mutex::new(HardwareState {
        sys_state: "IDLE".to_string(),
        run_time: 0.0,
        pump_on: true,
        flow_setpoint: 1.0,
        oven_on: true,
        oven_setpoint: 35.0,
        current_temp: 25.0,
        current_pressure: 0.0,
    }));

    // Start physics simulation loop
    let hw_state_clone = Arc::clone(&hw_state);
    let (tx, _rx) = tokio::sync::broadcast::channel::<String>(100);
    let tx_clone = tx.clone();

    tokio::spawn(async move {
        let mut interval = time::interval(Duration::from_millis(100)); // 10Hz
        loop {
            interval.tick().await;
            let mut state = hw_state_clone.lock().await;

            // Physics model
            if state.oven_on {
                // simple newton cooling/heating
                state.current_temp += (state.oven_setpoint - state.current_temp) * 0.05;
            } else {
                state.current_temp += (25.0 - state.current_temp) * 0.01;
            }

            if state.pump_on {
                let target_p = state.flow_setpoint * 120.0;
                state.current_pressure += (target_p - state.current_pressure) * 0.1;
            } else {
                state.current_pressure += (0.0 - state.current_pressure) * 0.1;
            }

            let mut sig = 10.0;
            let mut prep_valve = "WASTE";

            if state.sys_state == "RUNNING" {
                let t = state.run_time;
                sig += (t * 5.0).sin() * 0.2; // noise
                
                // periodic peaks for simulation
                let cycle_t = t % 30.0;
                if (cycle_t - 5.0).abs() < 1.0 {
                    sig += 50.0 * (-(cycle_t - 5.0).powi(2) / 0.1).exp();
                }
                if (cycle_t - 12.0).abs() < 1.5 {
                    sig += 120.0 * (-(cycle_t - 12.0).powi(2) / 0.2).exp();
                }
                if (cycle_t - 18.0).abs() < 1.0 {
                    sig += 80.0 * (-(cycle_t - 18.0).powi(2) / 0.1).exp();
                }

                if (cycle_t - 5.0).abs() < 1.0 || (cycle_t - 12.0).abs() < 1.5 {
                    prep_valve = "COLLECT";
                }

                state.run_time += 0.1;
            } else {
                sig += (std::time::SystemTime::now().duration_since(std::time::UNIX_EPOCH).unwrap().as_secs_f64() * 5.0).sin() * 0.2; // just noise
            }

            let data = SimData {
                msg_type: "DATA".to_string(),
                state: state.sys_state.clone(),
                time: state.run_time,
                signal: sig,
                pressure: state.current_pressure + (state.run_time * 0.1).sin() * 2.0,
                temperature: state.current_temp,
                tcd_bridge_current: "ON (150 mA)".to_string(),
                tcd_temp: 250.0,
                tcd_polarity: "Positive".to_string(),
                fid_flame: "ON".to_string(),
                fid_temp: 300.0,
                ms_vacuum: "1.2e-5 Torr".to_string(),
                prep_valve: prep_valve.to_string(),
            };

            if let Ok(mut json) = serde_json::to_string(&data) {
                json.push('\n');
                let _ = tx_clone.send(json);
            }
        }
    });

    // Start SCPI Server
    let listener = TcpListener::bind("127.0.0.1:8081").await.unwrap();
    println!("SCPI Hardware Simulator running on 127.0.0.1:8081");

    loop {
        let (mut socket, _) = listener.accept().await.unwrap();
        let hw_state_client = Arc::clone(&hw_state);
        let mut rx = tx.subscribe();

        tokio::spawn(async move {
            let (reader, mut writer) = socket.split();
            let mut buf_reader = BufReader::new(reader);
            
            // Send initial state message (for backwards compatibility with old edge client)
            let _ = writer.write_all(b"{\"msg_type\": \"STATE\", \"state\": \"READY\"}\n").await;

            let mut line = String::new();
            loop {
                tokio::select! {
                    // Read commands from client
                    result = buf_reader.read_line(&mut line) => {
                        match result {
                            Ok(0) => break, // EOF
                            Ok(_) => {
                                let cmd = line.trim().to_uppercase();
                                line.clear();
                                if cmd.is_empty() { continue; }
                                
                                println!("SCPI Command Received: {}", cmd);

                                let mut st = hw_state_client.lock().await;
                                let mut response = None;

                                if cmd == "*IDN?" {
                                    response = Some("Agilent-Clone, LC-1260-Sim, SN12345, V1.0.0\n".to_string());
                                } else if cmd == "SYST:STAT?" || cmd == "SYST:STAT" {
                                    response = Some(format!("{}\n", st.sys_state));
                                } else if cmd == "INJ:START" {
                                    st.sys_state = "RUNNING".to_string();
                                    st.run_time = 0.0;
                                    response = Some("OK\n".to_string());
                                } else if cmd == "INJ:STOP" {
                                    st.sys_state = "IDLE".to_string();
                                    response = Some("OK\n".to_string());
                                } else if cmd.starts_with("PUMP:FLOW ") {
                                    if let Ok(val) = cmd.replace("PUMP:FLOW ", "").parse::<f64>() {
                                        st.flow_setpoint = val;
                                        response = Some("OK\n".to_string());
                                    } else {
                                        response = Some("ERROR\n".to_string());
                                    }
                                } else if cmd == "PUMP:FLOW?" {
                                    response = Some(format!("{:.3}\n", st.flow_setpoint));
                                } else if cmd.starts_with("OVEN:TEMP ") {
                                    if let Ok(val) = cmd.replace("OVEN:TEMP ", "").parse::<f64>() {
                                        st.oven_setpoint = val;
                                        response = Some("OK\n".to_string());
                                    } else {
                                        response = Some("ERROR\n".to_string());
                                    }
                                } else if cmd == "OVEN:TEMP?" {
                                    response = Some(format!("{:.2}\n", st.current_temp));
                                } else {
                                    response = Some("ERROR: Unknown Command\n".to_string());
                                }

                                if let Some(resp) = response {
                                    if writer.write_all(resp.as_bytes()).await.is_err() {
                                        break;
                                    }
                                }
                            }
                            Err(_) => break,
                        }
                    }
                    // Broadcast telemetry JSON to client
                    msg = rx.recv() => {
                        match msg {
                            Ok(json_str) => {
                                if writer.write_all(json_str.as_bytes()).await.is_err() {
                                    break;
                                }
                            }
                            Err(_) => break,
                        }
                    }
                }
            }
        });
    }
}