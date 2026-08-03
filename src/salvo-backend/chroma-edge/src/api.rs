use salvo::prelude::*;
use std::sync::Arc;
use tokio::sync::Mutex;
use crate::analyzer::AnalyzerController;
use serde_json::json;
use serde::Deserialize;

pub fn api_router() -> Router {
    Router::new()
        .push(Router::with_path("tcd/state").get(tcd_state))
        .push(Router::with_path("tcd/zeroing").post(tcd_zeroing))
        .push(Router::with_path("tcd/set_bridge").post(tcd_set_bridge))
        .push(Router::with_path("modbus_temp/state").get(modbus_temp_state))
        .push(Router::with_path("modbus_temp/set").post(modbus_temp_set))
        .push(Router::with_path("modbus_temp/set_io").post(modbus_temp_set_io))
        .push(Router::with_path("modbus_temp/set_mode").post(modbus_temp_set_mode))
        .push(Router::with_path("voltage/state").get(voltage_state))
        .push(Router::with_path("epc/state").get(epc_state))
        .push(Router::with_path("epc/config").post(epc_config))
        .push(Router::with_path("epc/program").get(get_epc_program).post(set_epc_program))
        .push(Router::with_path("sequence/start").post(sequence_start))
        .push(Router::with_path("sequence/stop").post(sequence_stop))
        .push(Router::with_path("valve/program").get(get_valve_program).post(set_valve_program))
        .push(Router::with_path("serial/ports").get(serial_ports))
        .push(Router::with_path("serial/config").get(get_serial_config).post(set_serial_config))
}

#[handler]
async fn tcd_state(depot: &mut Depot, res: &mut Response) {
    let start = std::time::Instant::now();
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let lock_time = start.elapsed();
    let tcd_ctrl = {
        let ctrl = analyzer.lock().await;
        ctrl.tcd.clone()
    };
    let after_lock_time = start.elapsed();
    
    if let Some(tcd) = tcd_ctrl {
        let state = tcd.get_state().await;
        let get_state_time = start.elapsed();
        // Mock the timeout logic
        let mut connected = state.connected;
        if state.last_update.elapsed().as_secs() > 3 {
            connected = false;
        }
        res.render(Json(json!({
            "connected": connected,
            "bridge_current": state.bridge_current,
            "values": state.values,
            "frame_count": state.frame_count,
            "debug": format!("lock: {:?}, after_lock: {:?}, get_state: {:?}", lock_time, after_lock_time, get_state_time)
        })));
    } else {
        res.render(Json(json!({ "connected": false })));
    }
}

#[handler]
async fn tcd_zeroing(depot: &mut Depot, res: &mut Response) {
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let ctrl = analyzer.lock().await;
    
    if let Some(tcd) = &ctrl.tcd {
        match tcd.zeroing().await {
            Ok(_) => res.render(Json(json!({ "success": true }))),
            Err(e) => res.render(Json(json!({ "success": false, "error": e }))),
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "TCD not connected" })));
    }
}

#[derive(Deserialize)]
struct TcdSetBridgeReq {
    val: u8,
}

#[handler]
async fn tcd_set_bridge(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<TcdSetBridgeReq>().await {
        let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
        let ctrl = analyzer.lock().await;
        if let Some(tcd) = &ctrl.tcd {
            match tcd.set_bridge_current(body.val).await {
                Ok(_) => res.render(Json(json!({ "success": true }))),
                Err(e) => res.render(Json(json!({ "success": false, "error": e }))),
            }
        } else {
            res.render(Json(json!({ "success": false, "error": "TCD not connected" })));
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "Invalid request" })));
    }
}

#[handler]
async fn modbus_temp_state(depot: &mut Depot, res: &mut Response) {
    let start = std::time::Instant::now();
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let lock_start = start.elapsed();
    let temp_ctrl = {
        let ctrl = analyzer.lock().await;
        ctrl.temp.clone()
    };
    let after_lock = start.elapsed();
    
    if let Some(temp) = temp_ctrl {
        let state = temp.get_state().await;
        let after_state = start.elapsed();
        res.render(Json(json!({
            "connected": state.connected,
            "temperatures": state.temperatures,
            "set_temperatures": state.set_temperatures,
            "disconnected_status": state.disconnected_status,
            "switch_states": state.switch_states,
            "debug": format!("lock_start: {:?}, after_lock: {:?}, after_state: {:?}", lock_start, after_lock, after_state)
        })));
    } else {
        res.render(Json(json!({ "connected": false })));
    }
}

#[derive(Deserialize)]
struct TempSetReq {
    channel: u16,
    target_temp: i16,
}

#[handler]
async fn modbus_temp_set(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<TempSetReq>().await {
        let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
        let ctrl = analyzer.lock().await;
        if let Some(temp) = &ctrl.temp {
            match temp.set_temperature(body.channel, body.target_temp).await {
                Ok(_) => res.render(Json(json!({ "success": true }))),
                Err(e) => res.render(Json(json!({ "success": false, "error": e }))),
            }
        } else {
            res.render(Json(json!({ "success": false, "error": "Temp not connected" })));
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "Invalid request" })));
    }
}

#[derive(Deserialize)]
struct TempSetIoReq {
    channel: u16,
    state: bool,
}

#[handler]
async fn modbus_temp_set_io(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<TempSetIoReq>().await {
        let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
        let ctrl = analyzer.lock().await;
        if let Some(temp) = &ctrl.temp {
            match temp.set_switch(body.channel, body.state).await {
                Ok(_) => res.render(Json(json!({ "success": true }))),
                Err(e) => res.render(Json(json!({ "success": false, "error": e }))),
            }
        } else {
            res.render(Json(json!({ "success": false, "error": "Temp not connected" })));
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "Invalid request" })));
    }
}

#[derive(Deserialize)]
struct TempSetModeReq {
    channel: u16,
    mode: i16,
}

#[handler]
async fn modbus_temp_set_mode(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<TempSetModeReq>().await {
        let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
        let ctrl = analyzer.lock().await;
        if let Some(temp) = &ctrl.temp {
            match temp.set_mode(body.channel, body.mode).await {
                Ok(_) => res.render(Json(json!({ "success": true }))),
                Err(e) => res.render(Json(json!({ "success": false, "error": e }))),
            }
        } else {
            res.render(Json(json!({ "success": false, "error": "Temp not connected" })));
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "Invalid request" })));
    }
}

#[handler]
async fn voltage_state(depot: &mut Depot, res: &mut Response) {
    let start = std::time::Instant::now();
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let volt_ctrl = {
        let ctrl = analyzer.lock().await;
        ctrl.voltage.clone()
    };
    
    if let Some(voltage) = volt_ctrl {
        let state = voltage.get_state().await;
        res.render(Json(json!({
            "connected": state.connected,
            "voltage": state.voltage,
            "debug": format!("time: {:?}", start.elapsed())
        })));
    } else {
        res.render(Json(json!({ "connected": false })));
    }
}

#[handler]
async fn epc_state(depot: &mut Depot, res: &mut Response) {
    let start = std::time::Instant::now();
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let epc_ctrl = {
        let ctrl = analyzer.lock().await;
        ctrl.epc.clone()
    };
    
    if let Some(epc) = epc_ctrl {
        let state = epc.get_state().await;
        res.render(Json(json!({
            "connected": state.connected,
            "real_pressure": state.real_pressure,
            "real_flow": state.real_flow,
            "valve_open": state.valve_open,
            "status": state.status,
            "temp": state.temp,
            "debug": format!("time: {:?}", start.elapsed())
        })));
    } else {
        res.render(Json(json!({ "connected": false })));
    }
}

#[derive(Deserialize)]
struct EpcConfigReq {
    mode: Option<u16>,
    pressure: Option<f32>,
    flow: Option<f32>,
    gas_type: Option<u16>,
    units: Option<u16>,
}

#[handler]
async fn epc_config(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<EpcConfigReq>().await {
        let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
        let ctrl = analyzer.lock().await;
        if let Some(epc) = &ctrl.epc {
            if let Some(m) = body.mode { let _ = epc.set_mode(m).await; }
            if let Some(p) = body.pressure { let _ = epc.set_pressure(p).await; }
            if let Some(f) = body.flow { let _ = epc.set_flow(f).await; }
            if let Some(g) = body.gas_type { let _ = epc.set_gas_type(g).await; }
            if let Some(u) = body.units { let _ = epc.set_units(u).await; }
            res.render(Json(json!({ "success": true })));
        } else {
            res.render(Json(json!({ "success": false, "error": "EPC not connected" })));
        }
    } else {
        res.render(Json(json!({ "success": false, "error": "Invalid request" })));
    }
}

#[handler]
async fn get_epc_program(res: &mut Response) {
    let config_path = "epc_program.json";
    if let Ok(content) = std::fs::read_to_string(config_path) {
        if let Ok(config) = serde_json::from_str::<serde_json::Value>(&content) {
            res.render(Json(config));
            return;
        }
    }
    res.render(Json(json!({
        "mode": "Constant Flow",
        "initial_value": 1.0,
        "initial_time": 0.0,
        "ramps": []
    })));
}

#[handler]
async fn set_epc_program(req: &mut Request, res: &mut Response) {
    if let Ok(body) = req.parse_json::<serde_json::Value>().await {
        let config_path = "epc_program.json";
        if let Ok(content) = serde_json::to_string(&body) {
            let _ = std::fs::write(config_path, content);
            res.render(Json(json!({ "success": true })));
            return;
        }
    }
    res.render(Json(json!({ "success": false, "error": "Invalid request" })));
}

#[handler]
async fn sequence_start(depot: &mut Depot, res: &mut Response) {
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let ctrl = analyzer.lock().await;
    let mut success = true;
    let mut err_msg = String::new();

    if let Some(epc) = &ctrl.epc {
        let config_path = "epc_program.json";
        if let Ok(content) = std::fs::read_to_string(config_path) {
            if let Ok(prog) = serde_json::from_str::<crate::hal_epc::EpcProgram>(&content) {
                epc.start_sequence(prog).await;
            } else {
                success = false;
                err_msg.push_str("Invalid epc_program.json. ");
            }
        }
    } else {
        success = false;
        err_msg.push_str("EPC not connected. ");
    }

    if let Some(temp) = &ctrl.temp {
        let config_path = "valve_program.json";
        if let Ok(content) = std::fs::read_to_string(config_path) {
            if let Ok(prog) = serde_json::from_str::<Vec<crate::hal_temp::ValveEvent>>(&content) {
                temp.start_sequence(prog).await;
            }
        }
    }

    if success {
        res.render(Json(json!({ "success": true })));
    } else {
        res.render(Json(json!({ "success": false, "error": err_msg })));
    }
}

#[handler]
async fn sequence_stop(depot: &mut Depot, res: &mut Response) {
    let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
    let ctrl = analyzer.lock().await;
    if let Some(epc) = &ctrl.epc {
        epc.stop_sequence().await;
    }
    if let Some(temp) = &ctrl.temp {
        temp.stop_sequence().await;
    }
    res.render(Json(json!({ "success": true })));
}

#[handler]
async fn get_valve_program(res: &mut Response) {
    let config_path = "valve_program.json";
    if let Ok(content) = std::fs::read_to_string(config_path) {
        if let Ok(config) = serde_json::from_str::<serde_json::Value>(&content) {
            res.render(Json(config));
            return;
        }
    }
    res.render(Json(json!([])));
}

#[handler]
async fn set_valve_program(req: &mut Request, res: &mut Response) {
    if let Ok(body) = req.parse_json::<serde_json::Value>().await {
        let config_path = "valve_program.json";
        if let Ok(content) = serde_json::to_string(&body) {
            let _ = std::fs::write(config_path, content);
            res.render(Json(json!({ "success": true })));
            return;
        }
    }
    res.render(Json(json!({ "success": false, "error": "Invalid request" })));
}

#[handler]
async fn serial_ports(res: &mut Response) {
    let mut ports = Vec::new();
    if let Ok(available_ports) = tokio_serial::available_ports() {
        for port in available_ports {
            ports.push(port.port_name);
        }
    }
    res.render(Json(ports));
}

#[derive(Deserialize, serde::Serialize)]
pub struct SerialConfig {
    pub tcd_port: Option<String>,
    pub temp_port: Option<String>,
}

#[handler]
async fn get_serial_config(res: &mut Response) {
    let config_path = "serial_config.json";
    if let Ok(content) = std::fs::read_to_string(config_path) {
        if let Ok(config) = serde_json::from_str::<SerialConfig>(&content) {
            res.render(Json(config));
            return;
        }
    }
    res.render(Json(json!({ "tcd_port": null, "temp_port": null })));
}

#[handler]
async fn set_serial_config(req: &mut Request, depot: &mut Depot, res: &mut Response) {
    if let Ok(body) = req.parse_json::<SerialConfig>().await {
        let config_path = "serial_config.json";
        if let Ok(content) = serde_json::to_string(&body) {
            let _ = std::fs::write(config_path, content);
            
            // Reconnect hardware
            let analyzer = depot.obtain::<Arc<Mutex<AnalyzerController>>>().unwrap();
            let mut ctrl = analyzer.lock().await;
            
            // Disconnect old hardware (you may need to implement a clean disconnect, 
            // for now connect_hardware usually overwrites or we can just call it)
            let tcd_port = body.tcd_port.unwrap_or_default();
            let temp_port = body.temp_port.clone().unwrap_or_default();
            let voltage_port = body.temp_port.unwrap_or_default();
            
            if !tcd_port.is_empty() || !temp_port.is_empty() {
                ctrl.connect_hardware(tcd_port, temp_port, voltage_port);
            }
            
            res.render(Json(json!({ "success": true })));
            return;
        }
    }
    res.render(Json(json!({ "success": false, "error": "Invalid request" })));
}

