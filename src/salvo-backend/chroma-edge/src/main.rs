use salvo::prelude::*;
use salvo::cors::{Cors, Any};
use salvo::http::Method;
use salvo::websocket::{Message, WebSocketUpgrade};
use serde::{Deserialize, Serialize};
use tokio::net::TcpStream;
use tokio::io::{AsyncBufReadExt, BufReader};

use chroma_core::{
    DataPoint, downsample_lttb, process_chromatogram, IntegrationEvents, IntegrationReport, 
    SequenceRequest, SequenceStatus, CalibrationRequest, calculate_calibration_curve,
    GpcSlice, calculate_gpc_distribution,
    MassPeak, match_mass_spectrum
};
use std::sync::Arc;
use tokio::sync::Mutex;
use std::fs;
use std::path::Path;

lazy_static::lazy_static! {
    static ref SEQ_STATUS: Arc<Mutex<SequenceStatus>> = Arc::new(Mutex::new(SequenceStatus {
        status: "IDLE".to_string(),
        current_line: 0,
        current_inj: 0,
        message: "Ready".to_string(),
    }));

    static ref BROADCAST_TX: tokio::sync::broadcast::Sender<String> = {
        let (tx, _) = tokio::sync::broadcast::channel(100);
        tx
    };
}

#[derive(Serialize, Deserialize, Extractible, Debug)]
#[salvo(extract(default_source(from = "body")))]
struct AnalyzeRequest {
    file_name: Option<String>,
    events: IntegrationEvents,
}

#[derive(Serialize)]
struct AnalyzeResponse {
    status: String,
    report: IntegrationReport,
    trace: TraceData,
}

#[derive(Serialize)]
struct TraceData {
    times: Vec<f64>,
    values: Vec<f64>,
}

#[handler]
async fn analyze_handler(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<AnalyzeRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    // Get raw data: either from file or generate mock
    let mut raw_data = Vec::new();
    if let Some(ref fname) = payload.file_name {
        let file_path = Path::new("../../data/Sequence_001").join(fname).join("signal.json");
        if let Ok(content) = fs::read_to_string(file_path) {
            if let Ok(data) = serde_json::from_str::<Vec<DataPoint>>(&content) {
                raw_data = data;
            }
        }
    }

    if raw_data.is_empty() {
        // Fallback to mock data if no file specified or file load failed
        for i in 0..100_000 {
            let t = i as f64 * 0.0001; // 0 to 10 seconds
            let mut v = 10.0 + (t * 10.0).sin() * 0.5; // baseline noise
            
            // Add peaks
            if (t - 2.5).abs() < 0.5 {
                v += 100.0 * (-(t - 2.5).powi(2) / 0.05).exp();
            }
            if (t - 5.75).abs() < 0.75 {
                v += 200.0 * (-(t - 5.75).powi(2) / 0.1).exp();
            }
            
            raw_data.push(DataPoint { time: t, value: v });
        }
    }

    // Pass logic to chroma-core: integration using the events provided by React
    let report = process_chromatogram(&raw_data, &payload.events);

    // Pass logic to chroma-core: Downsample to 2000 points for the Web UI
    let display_data = downsample_lttb(&raw_data, 2000);

    let mut times = Vec::with_capacity(display_data.len());
    let mut values = Vec::with_capacity(display_data.len());
    for dp in display_data {
        times.push(dp.time);
        values.push(dp.value);
    }

    let response = AnalyzeResponse {
        status: "success".to_string(),
        report,
        trace: TraceData { times, values },
    };

    res.render(Json(response));
}

#[handler]
async fn realtime_ws(req: &mut Request, res: &mut Response) -> Result<(), StatusError> {
    WebSocketUpgrade::new().upgrade(req, res, |mut ws| async move {
        let mut rx = BROADCAST_TX.subscribe();
        loop {
            match rx.recv().await {
                Ok(msg) => {
                    if ws.send(Message::text(msg)).await.is_err() {
                        break;
                    }
                }
                Err(_) => {
                    let _ = ws.send(Message::text(r#"{"msg_type": "ERROR", "message": "Simulator disconnected"}"#)).await;
                    break;
                }
            }
        }
    }).await
}

#[handler]
async fn sequence_start_handler(req: &mut Request, res: &mut Response) {
    println!("--- sequence_start_handler called ---");
    let payload = match req.parse_json::<SequenceRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    let mut status = SEQ_STATUS.lock().await;
    if status.status == "RUNNING" {
        res.status_code(StatusCode::BAD_REQUEST);
        res.render(Text::Plain("Sequence is already running".to_string()));
        return;
    }

    status.status = "RUNNING".to_string();
    status.current_line = 1;
    status.current_inj = 1;
    status.message = format!("Starting sequence, line 1: {}", payload.rows[0].sample_name);

    // In a real application, we would spawn a tokio::task to run the sequence state machine
    // interacting with the hardware simulator. For now, we just update the status.
    let cloned_payload = payload.rows.clone();
    tokio::spawn(async move {
        // Send SCPI command to simulator to start injection
        if let Ok(mut stream) = tokio::net::TcpStream::connect("127.0.0.1:8081").await {
            use tokio::io::{AsyncWriteExt, AsyncBufReadExt};
            let (reader, mut writer) = stream.split();
            let mut buf_reader = tokio::io::BufReader::new(reader);
            let mut line = String::new();
            // Read initial state message
            let _ = buf_reader.read_line(&mut line).await;
            
            // Send INJ:START
            let _ = writer.write_all(b"INJ:START\n").await;
            
            // Wait for response
            line.clear();
            if let Ok(_) = buf_reader.read_line(&mut line).await {
                println!("Simulator response to INJ:START: {}", line.trim());
            }
        } else {
            println!("Failed to connect to simulator for INJ:START");
        }

        // Create base data directory
        let base_dir = Path::new("../../data/Sequence_001");
        let _ = fs::create_dir_all(base_dir);

        for row in cloned_payload {
            for inj in 1..=row.inj_per_loc {
                {
                    let mut st = SEQ_STATUS.lock().await;
                    if st.status == "STOPPING" {
                        st.status = "IDLE".to_string();
                        st.message = "Sequence stopped".to_string();
                        return;
                    }
                    st.current_line = row.line;
                    st.current_inj = inj;
                    st.message = format!("Running line {}, inj {}/{} - Vial {}", row.line, inj, row.inj_per_loc, row.location);
                }
                
                // Start collecting data
                let mut rx = BROADCAST_TX.subscribe();
                let mut collected_points = Vec::new();
                let run_time = 35.0; // 35 seconds for simulation to show all peaks
                let start_time = std::time::Instant::now();

                while start_time.elapsed().as_secs_f64() < run_time {
                    if let Ok(msg) = tokio::time::timeout(std::time::Duration::from_millis(500), rx.recv()).await {
                        if let Ok(json_str) = msg {
                            if let Ok(v) = serde_json::from_str::<serde_json::Value>(&json_str) {
                                if v["msg_type"] == "DATA" {
                                    if let (Some(t), Some(val)) = (v["time"].as_f64(), v["signal"].as_f64()) {
                                        collected_points.push(DataPoint { time: t, value: val });
                                    }
                                }
                            }
                        }
                    }
                }

                // Save to .D folder
                let folder_name = format!("{}F{:02}{:02}.D", row.location, row.line, inj);
                let d_path = base_dir.join(&folder_name);
                let _ = fs::create_dir_all(&d_path);
                
                // Write signal data
                if let Ok(json) = serde_json::to_string(&collected_points) {
                    let _ = fs::write(d_path.join("signal.json"), json);
                }

                // Write acq metadata
                let acq_meta = serde_json::json!({
                    "sample_name": row.sample_name,
                    "method": row.method_name,
                    "vial": row.location,
                    "injection": inj,
                    "date": format!("{:?}", std::time::SystemTime::now())
                });
                let _ = fs::write(d_path.join("acq.json"), acq_meta.to_string());
            }
        }
        let mut st = SEQ_STATUS.lock().await;
        st.status = "IDLE".to_string();
        st.message = "Sequence completed successfully".to_string();

        // Send SCPI command to simulator to stop
        if let Ok(mut stream) = tokio::net::TcpStream::connect("127.0.0.1:8081").await {
            use tokio::io::{AsyncWriteExt, AsyncBufReadExt};
            let (reader, mut writer) = stream.split();
            let mut buf_reader = tokio::io::BufReader::new(reader);
            let mut line = String::new();
            let _ = buf_reader.read_line(&mut line).await;
            
            let _ = writer.write_all(b"INJ:STOP\n").await;
            line.clear();
            let _ = buf_reader.read_line(&mut line).await;
        }
    });

    res.render(Json(&*status));
}

#[handler]
async fn sequence_stop_handler(res: &mut Response) {
    let mut status = SEQ_STATUS.lock().await;
    if status.status == "RUNNING" {
        status.status = "STOPPING".to_string();
        status.message = "Stopping sequence...".to_string();
        
        // Send SCPI command to simulator to stop
        tokio::spawn(async move {
            if let Ok(mut stream) = tokio::net::TcpStream::connect("127.0.0.1:8081").await {
                use tokio::io::{AsyncWriteExt, AsyncBufReadExt};
                let (reader, mut writer) = stream.split();
                let mut buf_reader = tokio::io::BufReader::new(reader);
                let mut line = String::new();
                let _ = buf_reader.read_line(&mut line).await;
                
                let _ = writer.write_all(b"INJ:STOP\n").await;
                line.clear();
                let _ = buf_reader.read_line(&mut line).await;
            }
        });
    }
    res.render(Json(&*status));
}

#[handler]
async fn sequence_status_handler(res: &mut Response) {
    let status = SEQ_STATUS.lock().await;
    res.render(Json(&*status));
}

#[handler]
async fn sequence_save_handler(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<SequenceRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    
    let base_dir = Path::new("../../data/sequences");
    let _ = fs::create_dir_all(base_dir);
    let file_path = base_dir.join("current_sequence.json");
    if let Ok(json) = serde_json::to_string_pretty(&payload) {
        let _ = fs::write(file_path, json);
    }
    
    res.render(Json(serde_json::json!({"status": "ok"})));
}

#[handler]
async fn sequence_load_handler(res: &mut Response) {
    let file_path = Path::new("../../data/sequences/current_sequence.json");
    if let Ok(content) = fs::read_to_string(file_path) {
        if let Ok(data) = serde_json::from_str::<SequenceRequest>(&content) {
            res.render(Json(&data));
            return;
        }
    }
    
    // Return empty or default if not found
    let empty = SequenceRequest { rows: vec![] };
    res.render(Json(&empty));
}

#[handler]
async fn list_data_files(res: &mut Response) {
    let base_dir = Path::new("../../data/Sequence_001");
    let mut files = Vec::new();
    if let Ok(entries) = fs::read_dir(base_dir) {
        for entry in entries.flatten() {
            if entry.path().is_dir() && entry.path().extension().map_or(false, |ext| ext == "D") {
                if let Some(name) = entry.path().file_name().and_then(|n| n.to_str()) {
                    files.push(name.to_string());
                }
            }
        }
    }
    files.sort();
    res.render(Json(files));
}

#[handler]
async fn calibration_handler(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<CalibrationRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    let curve = calculate_calibration_curve(&payload);
    res.render(Json(curve));
}

#[derive(Serialize, Deserialize, Extractible, Clone, Debug)]
#[salvo(extract(default_source(from = "body")))]
struct AuditLog {
    id: Option<String>,
    time: Option<String>,
    user: String,
    module: String,
    action: String,
    details: String,
}

#[derive(Serialize, Deserialize, Extractible, Debug)]
#[salvo(extract(default_source(from = "body")))]
struct GpcRequest {
    slices: Vec<GpcSlice>,
    slope: f64,
    intercept: f64,
}

#[handler]
async fn gpc_calculate_handler(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<GpcRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    let result = calculate_gpc_distribution(&payload.slices, payload.slope, payload.intercept);
    res.render(Json(result));
}

#[derive(Serialize, Deserialize, Extractible, Debug)]
#[salvo(extract(default_source(from = "body")))]
struct MsDeconvRequest {
    unknown: Vec<MassPeak>,
    library: Vec<MassPeak>,
}

#[handler]
async fn ms_deconv_handler(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<MsDeconvRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    let result = match_mass_spectrum(&payload.unknown, &payload.library);
    res.render(Json(result));
}

#[derive(Serialize)]
struct DadSpectrumPoint {
    wavelength: f64,
    absorbance: f64,
}

#[handler]
async fn get_dad_spectrum(res: &mut Response) {
    // Generate simulated DAD spectrum (e.g., at a specific retention time)
    let mut spectral_data = Vec::new();
    for i in 0..=200 {
        let wl = 200.0 + i as f64;
        let mut abs = 10.0;
        if (wl - 254.0).abs() < 15.0 {
            abs += 200.0 * (-(wl - 254.0).powi(2) / 100.0).exp();
        }
        if (wl - 273.0).abs() < 20.0 {
            abs += 150.0 * (-(wl - 273.0).powi(2) / 200.0).exp();
        }
        abs += (i % 5) as f64 * 1.0; // some noise
        spectral_data.push(DadSpectrumPoint {
            wavelength: wl,
            absorbance: abs,
        });
    }
    res.render(Json(spectral_data));
}

#[derive(Serialize, Deserialize, Clone)]
struct PrepSettings {
    trigger_mode: String,
    slope_up: f64,
    slope_down: f64,
    max_volume: f64,
}

lazy_static::lazy_static! {
    static ref PREP_SETTINGS: Arc<Mutex<PrepSettings>> = Arc::new(Mutex::new(PrepSettings {
        trigger_mode: "Slope".to_string(),
        slope_up: 5.0,
        slope_down: -2.0,
        max_volume: 15.0,
    }));
}

#[handler]
async fn get_prep_settings(res: &mut Response) {
    let settings = PREP_SETTINGS.lock().await;
    res.render(Json(&*settings));
}

#[handler]
async fn save_prep_settings(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<PrepSettings>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    let mut settings = PREP_SETTINGS.lock().await;
    *settings = payload.clone();
    res.render(Json(payload));
}

#[derive(Serialize, Deserialize, Clone)]
struct ValveEvent {
    id: String,
    time: f64,
    valve: String,
    position: String,
}

#[derive(Serialize, Deserialize, Clone)]
struct EpcRamp {
    id: String,
    rate: f64,
    final_value: f64,
    hold_time: f64,
}

#[derive(Serialize, Deserialize, Clone)]
struct EpcProgram {
    mode: String,
    initial_value: f64,
    initial_time: f64,
    ramps: Vec<EpcRamp>,
}

lazy_static::lazy_static! {
    static ref VALVE_PROGRAM: Arc<Mutex<Vec<ValveEvent>>> = Arc::new(Mutex::new(vec![
        ValveEvent { id: "1".to_string(), time: 0.0, valve: "Valve 1".to_string(), position: "OFF".to_string() },
    ]));
    static ref EPC_PROGRAM: Arc<Mutex<EpcProgram>> = Arc::new(Mutex::new(EpcProgram {
        mode: "Constant Flow".to_string(),
        initial_value: 1.0,
        initial_time: 0.0,
        ramps: vec![],
    }));
}

#[handler]
async fn get_valve_program(res: &mut Response) {
    let program = VALVE_PROGRAM.lock().await;
    res.render(Json(&*program));
}

#[handler]
async fn save_valve_program(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<Vec<ValveEvent>>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    let mut program = VALVE_PROGRAM.lock().await;
    *program = payload.clone();
    res.render(Json(payload));
}

#[handler]
async fn get_epc_program(res: &mut Response) {
    let program = EPC_PROGRAM.lock().await;
    res.render(Json(&*program));
}

#[handler]
async fn save_epc_program(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<EpcProgram>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    let mut program = EPC_PROGRAM.lock().await;
    *program = payload.clone();
    res.render(Json(payload));
}

#[derive(Serialize, Deserialize, Clone)]
struct PrepStep {
    id: String,
    action: String,
    volume: Option<f64>,
    location: Option<String>,
    speed: Option<f64>,
    duration: Option<f64>,
}

lazy_static::lazy_static! {
    static ref INJECTOR_PROGRAM: Arc<Mutex<Vec<PrepStep>>> = Arc::new(Mutex::new(vec![
        PrepStep { id: "1".to_string(), action: "Wash".to_string(), location: Some("WashA".to_string()), volume: Some(10.0), speed: None, duration: None },
        PrepStep { id: "2".to_string(), action: "Draw".to_string(), location: Some("Vial1".to_string()), volume: Some(2.0), speed: Some(100.0), duration: None },
        PrepStep { id: "3".to_string(), action: "Draw".to_string(), location: Some("ISTD".to_string()), volume: Some(1.0), speed: Some(100.0), duration: None },
        PrepStep { id: "4".to_string(), action: "Mix".to_string(), location: None, volume: Some(3.0), speed: None, duration: Some(5.0) },
        PrepStep { id: "5".to_string(), action: "Dispense".to_string(), location: Some("Seat".to_string()), volume: Some(3.0), speed: Some(100.0), duration: None },
    ]));
}

#[handler]
async fn get_injector_program(res: &mut Response) {
    let program = INJECTOR_PROGRAM.lock().await;
    res.render(Json(&*program));
}

#[handler]
async fn save_injector_program(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<Vec<PrepStep>>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    let mut program = INJECTOR_PROGRAM.lock().await;
    *program = payload.clone();
    res.render(Json(payload));
}
#[derive(Serialize)]
struct SstTrendData {
    run: i32,
    rt: f64,
    limit_upper: f64,
    limit_lower: f64,
    mean: f64,
}

#[handler]
async fn get_sst_trends(res: &mut Response) {
    let mut trends = Vec::new();
    let mean = 4.10;
    let limit_upper = 4.20;
    let limit_lower = 4.00;
    
    // Simulate trend data
    let base_rts = [4.12, 4.13, 4.11, 4.15, 4.18, 4.21, 4.16, 4.14, 4.09, 4.10];
    for (i, &rt) in base_rts.iter().enumerate() {
        trends.push(SstTrendData {
            run: (i + 1) as i32,
            rt,
            limit_upper,
            limit_lower,
            mean,
        });
    }
    res.render(Json(trends));
}

#[handler]
async fn get_ms_data(res: &mut Response) {
    let data = serde_json::json!({
        "tic": [
            { "time": 1.0, "intensity": 100 },
            { "time": 2.0, "intensity": 150 },
            { "time": 3.1, "intensity": 8000 },
            { "time": 4.0, "intensity": 200 },
            { "time": 5.5, "intensity": 12000 },
            { "time": 6.0, "intensity": 180 }
        ],
        "spectrum": [
            { "mz": 50, "abundance": 10 },
            { "mz": 77, "abundance": 45 },
            { "mz": 91, "abundance": 100 },
            { "mz": 105, "abundance": 20 },
            { "mz": 150, "abundance": 5 }
        ]
    });
    res.render(Json(data));
}

#[handler]
async fn get_gpc_data(res: &mut Response) {
    let mut slices = Vec::new();
    for i in 0..50 {
        let rt = 6.0 + i as f64 * 0.1;
        let height = 100.0 * (-(rt - 8.5).powi(2) / 2.0).exp();
        slices.push(serde_json::json!({ "retention_time": rt, "height": height }));
    }
    
    let data = serde_json::json!({
        "calibration": [
            { "logM": 3.0, "rt": 12.5 },
            { "logM": 4.0, "rt": 10.2 },
            { "logM": 5.0, "rt": 8.5 },
            { "logM": 6.0, "rt": 6.8 },
            { "logM": 7.0, "rt": 5.2 }
        ],
        "sample_slices": slices
    });
    res.render(Json(data));
}

#[handler]
async fn get_audit_logs(res: &mut Response) {
    let path = Path::new("../../data/audit_trail.json");
    let mut logs: Vec<AuditLog> = Vec::new();
    if let Ok(content) = fs::read_to_string(path) {
        if let Ok(data) = serde_json::from_str(&content) {
            logs = data;
        }
    } else {
        // Mock data if file doesn't exist
        logs.push(AuditLog {
            id: Some("1".to_string()),
            time: Some("2026-06-20 09:15:22".to_string()),
            user: "Admin".to_string(),
            module: "System".to_string(),
            action: "System Started".to_string(),
            details: "Salvo Backend Initialized".to_string(),
        });
    }
    // Return newest first
    logs.reverse();
    res.render(Json(logs));
}

#[handler]
async fn add_audit_log(req: &mut Request, res: &mut Response) {
    let mut payload = match req.parse_json::<AuditLog>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };

    payload.id = Some(uuid::Uuid::new_v4().to_string());
    payload.time = Some(chrono::Local::now().format("%Y-%m-%d %H:%M:%S").to_string());

    let path = Path::new("../../data/audit_trail.json");
    let _ = fs::create_dir_all(path.parent().unwrap());
    
    let mut logs: Vec<AuditLog> = Vec::new();
    if let Ok(content) = fs::read_to_string(path) {
        if let Ok(data) = serde_json::from_str(&content) {
            logs = data;
        }
    }

    logs.push(payload.clone());
    
    if let Ok(json) = serde_json::to_string_pretty(&logs) {
        let _ = fs::write(path, json);
    }

    res.render(Json(payload));
}

#[handler]
async fn options_handler(res: &mut Response) {
    res.status_code(StatusCode::OK);
}

use rust_embed::RustEmbed;

#[derive(RustEmbed)]
#[folder = "../../../src/ui/apps/workstation/dist"]
struct Assets;

#[handler]
async fn serve_assets_embedded(req: &mut Request, res: &mut Response) {
    let mut path = req.param::<String>("path").unwrap_or_default();
    if path.is_empty() || path == "/" {
        path = "index.html".to_string();
    }
    let clean_path = path.trim_start_matches('/');

    if let Some(content) = Assets::get(clean_path) {
        let mime = mime_guess::from_path(clean_path).first_or_octet_stream();
        res.body(salvo::http::response::ResBody::Once(content.data.into_owned().into()));
        res.headers_mut().insert("content-type", mime.as_ref().parse().unwrap());
    } else {
        // Fallback to index.html for SPA routing
        if let Some(index) = Assets::get("index.html") {
            let mime = mime_guess::from_path("index.html").first_or_octet_stream();
            res.body(salvo::http::response::ResBody::Once(index.data.into_owned().into()));
            res.headers_mut().insert("content-type", mime.as_ref().parse().unwrap());
        } else {
            res.status_code(StatusCode::NOT_FOUND);
        }
    }
}

#[handler]
async fn serve_assets(req: &mut Request, res: &mut Response) {
    let path = req.param::<String>("path").unwrap_or_default();
    let clean_path = path.trim_start_matches('/');
    let file_path = std::path::Path::new("/opt/chromatography-workstation/dist/assets").join(clean_path);

    if file_path.exists() && file_path.is_file() {
        let content = std::fs::read(&file_path).unwrap();
        let ext = file_path.extension().and_then(|s| s.to_str()).unwrap_or("");
        let mime_type = match ext {
            "js" => "application/javascript",
            "css" => "text/css",
            "svg" => "image/svg+xml",
            "png" => "image/png",
            _ => "application/octet-stream",
        };
        res.body(salvo::http::response::ResBody::Once(content.into()));
        res.headers_mut().insert("content-type", mime_type.parse().unwrap());
    } else {
        res.status_code(StatusCode::NOT_FOUND);
    }
}

#[derive(Serialize, Deserialize, Extractible, Debug, Clone)]
#[salvo(extract(default_source(from = "body")))]
struct SelectSerialRequest {
    tcd_port: Option<String>,
    temp_port: Option<String>,
}

lazy_static::lazy_static! {
    static ref SERIAL_CONFIG: Arc<Mutex<SelectSerialRequest>> = Arc::new(Mutex::new(SelectSerialRequest {
        tcd_port: None,
        temp_port: None,
    }));
}

#[handler]
async fn list_serial_ports(res: &mut Response) {
    let mut port_names = Vec::new();
    if let Ok(ports) = serialport::available_ports() {
        for p in ports {
            port_names.push(p.port_name);
        }
    }
    res.render(Json(port_names));
}

#[handler]
async fn get_serial_config(res: &mut Response) {
    let config = SERIAL_CONFIG.lock().await;
    res.render(Json(&*config));
}

#[handler]
async fn save_serial_config(req: &mut Request, res: &mut Response) {
    let payload = match req.parse_json::<SelectSerialRequest>().await {
        Ok(data) => data,
        Err(e) => {
            res.status_code(StatusCode::BAD_REQUEST);
            res.render(Text::Plain(format!("Invalid request body: {}", e)));
            return;
        }
    };
    
    let mut config = SERIAL_CONFIG.lock().await;
    *config = payload.clone();
    res.render(Json(payload));
}

#[tokio::main]
async fn main() {
    // Start background task to connect to Hardware Simulator
    tokio::spawn(async move {
        loop {
            match TcpStream::connect("127.0.0.1:8081").await {
                Ok(stream) => {
                    println!("Connected to Hardware Simulator");
                    let mut reader = BufReader::new(stream);
                    let mut line = String::new();
                    loop {
                        line.clear();
                        match reader.read_line(&mut line).await {
                            Ok(0) => {
                                println!("Simulator disconnected");
                                break;
                            }
                            Ok(_) => {
                                let _ = BROADCAST_TX.send(line.trim().to_string());
                            }
                            Err(e) => {
                                println!("Error reading from simulator: {}", e);
                                break;
                            }
                        }
                    }
                }
                Err(_) => {
                    tokio::time::sleep(tokio::time::Duration::from_secs(2)).await;
                }
            }
        }
    });

    // Allow React Dev Server to connect
    let cors = Cors::new()
        .allow_origin(Any)
        .allow_methods(vec![Method::GET, Method::POST, Method::OPTIONS])
        .allow_headers(Any)
        .into_handler();

    let router = Router::new()
        .hoop(cors)
        .push(Router::with_path("api/v1/analyze").post(analyze_handler).options(options_handler))
        .push(Router::with_path("api/v1/sequence/start").post(sequence_start_handler).options(options_handler))
        .push(Router::with_path("api/v1/sequence/stop").post(sequence_stop_handler).options(options_handler))
        .push(Router::with_path("api/v1/sequence/status").get(sequence_status_handler).options(options_handler))
        .push(Router::with_path("api/v1/sequence/save").post(sequence_save_handler).options(options_handler))
        .push(Router::with_path("api/v1/sequence/load").get(sequence_load_handler).options(options_handler))
        .push(Router::with_path("api/v1/data/files").get(list_data_files).options(options_handler))
        .push(Router::with_path("api/v1/calibration/calculate").post(calibration_handler).options(options_handler))    
        .push(Router::with_path("api/v1/audit/logs").get(get_audit_logs).options(options_handler))
        .push(Router::with_path("api/v1/audit/log").post(add_audit_log).options(options_handler))
        .push(Router::with_path("api/v1/gpc/calculate").post(gpc_calculate_handler).options(options_handler))
        .push(Router::with_path("api/v1/ms/deconvolute").post(ms_deconv_handler).options(options_handler))
        .push(Router::with_path("api/v1/dad/spectrum").get(get_dad_spectrum).options(options_handler))
        .push(Router::with_path("api/v1/prep/settings").get(get_prep_settings).options(options_handler))
        .push(Router::with_path("api/v1/prep/settings").post(save_prep_settings).options(options_handler))
        .push(Router::with_path("api/v1/prep/injector").get(get_injector_program).options(options_handler))
        .push(Router::with_path("api/v1/prep/injector").post(save_injector_program).options(options_handler))
        .push(Router::with_path("api/v1/valve/program").get(get_valve_program).options(options_handler))
        .push(Router::with_path("api/v1/valve/program").post(save_valve_program).options(options_handler))
        .push(Router::with_path("api/v1/epc/program").get(get_epc_program).options(options_handler))
        .push(Router::with_path("api/v1/epc/program").post(save_epc_program).options(options_handler))
        .push(Router::with_path("api/v1/sst/trends").get(get_sst_trends).options(options_handler))
        .push(Router::with_path("api/v1/ms/data").get(get_ms_data).options(options_handler))
        .push(Router::with_path("api/v1/gpc/data").get(get_gpc_data).options(options_handler))
        .push(Router::with_path("api/v1/serial/ports").get(list_serial_ports).options(options_handler))
        .push(Router::with_path("api/v1/serial/config").get(get_serial_config).options(options_handler))
        .push(Router::with_path("api/v1/serial/config").post(save_serial_config).options(options_handler))
        .push(Router::with_path("ws/v1/realtime").goal(realtime_ws))
        .push(Router::with_path("<**path>").get(serve_assets_embedded));

    println!("Starting Salvo Edge API on http://0.0.0.0:8082");
    let acceptor = TcpListener::new("0.0.0.0:8082").bind().await;
    Server::new(acceptor).serve(router).await;
}