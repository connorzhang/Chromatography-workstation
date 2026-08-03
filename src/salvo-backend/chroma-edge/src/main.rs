mod hal_tcd;
mod hal_temp;
mod hal_voltage;
mod hal_epc;
mod analyzer;
mod grpc_server;
mod api;

use std::sync::Arc;
use tokio::sync::Mutex;
use tonic::transport::Server;
use analyzer::AnalyzerController;
use salvo::prelude::*;
use salvo::serve_static::StaticDir;

use grpc_server::{TcdService, TempService, ValveService};
use grpc_server::pb::detector::thermal_conductivity_detector_service_server::ThermalConductivityDetectorServiceServer;
use grpc_server::pb::temperature::temperature_controller_service_server::TemperatureControllerServiceServer;
use grpc_server::pb::valve::valve_controller_service_server::ValveControllerServiceServer;

lazy_static::lazy_static! {
    static ref ANALYZER: Arc<Mutex<AnalyzerController>> = Arc::new(Mutex::new(AnalyzerController::new()));
}


#[handler]
async fn debug_static(req: &mut Request) {
    let path = req.params().tail().unwrap_or("").to_string();
    let log = format!("Request path: {}\nParams: {:?}", path, req.params());
    let _ = std::fs::write("C:\\Users\\trae\\Desktop\\chroma-edge\\req_debug.txt", log);
}

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    println!("Chroma Edge v0.3.125 starting...");
    let grpc_addr = "0.0.0.0:50051".parse()?;
    let web_addr = "0.0.0.0:8000";

    // Initialize Hardware from Config
    {
        let mut ctrl = ANALYZER.lock().await;
        let config_path = "serial_config.json";
        let mut tcd_port = String::new();
        let mut temp_port = String::new();
        let mut voltage_port = String::new();

        if let Ok(content) = std::fs::read_to_string(config_path) {
            if let Ok(config) = serde_json::from_str::<api::SerialConfig>(&content) {
                if let Some(tcd) = config.tcd_port {
                    tcd_port = tcd;
                }
                if let Some(temp) = config.temp_port {
                    temp_port = temp.clone();
                    voltage_port = temp; // Same port usually
                }
            }
        }

        if !tcd_port.is_empty() || !temp_port.is_empty() {
            println!("Connecting Hardware... TCD: {}, Temp: {}, Voltage: {}", tcd_port, temp_port, voltage_port);
            ctrl.connect_hardware(tcd_port, temp_port, voltage_port);
        } else {
            println!("Hardware ports not configured. Skipping auto-connect.");
        }
    }

    // Salvo Web Router
    let exe_dir = std::env::current_exe().unwrap().parent().unwrap().to_path_buf();
    let static_dir_path = if exe_dir.join("static").exists() {
        exe_dir.join("static")
    } else if std::env::current_dir().unwrap().join("static").exists() {
        std::env::current_dir().unwrap().join("static")
    } else {
        std::env::current_dir().unwrap().join("../edge/cmd/collector/static")
    };
    let static_dir = static_dir_path.to_string_lossy().to_string();
    println!("static_dir resolved to: {}", static_dir);
    std::fs::write("C:\\Users\\trae\\Desktop\\chroma-edge\\static_path_debug.txt", format!("exe_dir: {:?}\nstatic_dir: {}", exe_dir, static_dir)).ok();

    let router = Router::new()
        .push(Router::with_path("api/v1").push(api::api_router()))
        .push(
            Router::with_path("{**path}").hoop(debug_static).get(
                StaticDir::new([static_dir])
                    .defaults("index.html")
            )
        );

    let analyzer_clone = ANALYZER.clone();
    let web_server = tokio::spawn(async move {
        println!("Edge Web Server (Salvo) v0.3.125 running on http://{}", web_addr);
        let acceptor = TcpListener::new(web_addr).bind().await;
        let service = Service::new(router).hoop(salvo::affix_state::inject(analyzer_clone));
        salvo::server::Server::new(acceptor).serve(service).await;
    });

    let tcd_service = ThermalConductivityDetectorServiceServer::new(TcdService {
        analyzer: ANALYZER.clone(),
    });
    let temp_service = TemperatureControllerServiceServer::new(TempService {
        analyzer: ANALYZER.clone(),
    });
    let valve_service = ValveControllerServiceServer::new(ValveService {
        analyzer: ANALYZER.clone(),
    });

    let grpc_server = tokio::spawn(async move {
        println!("Edge HAL gRPC (SiLA 2 Standard) running on {}", grpc_addr);
        tonic::transport::Server::builder()
            .add_service(tcd_service)
            .add_service(temp_service)
            .add_service(valve_service)
            .serve(grpc_addr)
            .await
            .unwrap();
    });

    let _ = tokio::join!(web_server, grpc_server);

    Ok(())
}
