mod hal_tcd;
mod hal_temp;
mod hal_voltage;
mod analyzer;
mod grpc_server;

use std::sync::Arc;
use tokio::sync::Mutex;
use tonic::transport::Server;
use analyzer::AnalyzerController;

use grpc_server::{TcdService, TempService, ValveService};
use grpc_server::pb::detector::thermal_conductivity_detector_service_server::ThermalConductivityDetectorServiceServer;
use grpc_server::pb::temperature::temperature_controller_service_server::TemperatureControllerServiceServer;
use grpc_server::pb::valve::valve_controller_service_server::ValveControllerServiceServer;

lazy_static::lazy_static! {
    static ref ANALYZER: Arc<Mutex<AnalyzerController>> = Arc::new(Mutex::new(AnalyzerController::new()));
}

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error>> {
    let addr = "0.0.0.0:50051".parse()?;

    // In a real production scenario, hardware ports would be read from a .env or config file
    // For demonstration of HAL startup, we connect dummy ports here or wait for a SiLA init command.
    {
        // let mut ctrl = ANALYZER.lock().await;
        // ctrl.connect_hardware("COM1".into(), "COM2".into(), "COM3".into());
    }

    let tcd_service = ThermalConductivityDetectorServiceServer::new(TcdService {
        analyzer: ANALYZER.clone(),
    });
    let temp_service = TemperatureControllerServiceServer::new(TempService {
        analyzer: ANALYZER.clone(),
    });
    let valve_service = ValveControllerServiceServer::new(ValveService {
        analyzer: ANALYZER.clone(),
    });

    println!("Edge HAL gRPC (SiLA 2 Standard) running on {}", addr);

    Server::builder()
        .add_service(tcd_service)
        .add_service(temp_service)
        .add_service(valve_service)
        .serve(addr)
        .await?;

    Ok(())
}
