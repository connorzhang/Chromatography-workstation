pub mod pb {
    pub mod detector {
        tonic::include_proto!("sila2.custom.detectorservice.v1");
    }
    pub mod temperature {
        tonic::include_proto!("sila2.org.silastandard.core.temperaturecontrollerservice.v1");
    }
    pub mod valve {
        tonic::include_proto!("sila2.org.silastandard.core.valvecontrollerservice.v1");
    }
}

use std::sync::Arc;
use tokio::sync::Mutex;
use tonic::{Request, Response, Status};

use crate::analyzer::AnalyzerController;

use pb::detector::thermal_conductivity_detector_service_server::ThermalConductivityDetectorService;
use pb::detector::{SetBridgeRequest, SetBridgeResponse, ZeroingRequest, ZeroingResponse, StateRequest, StateResponse};

use pb::temperature::temperature_controller_service_server::TemperatureControllerService;
use pb::temperature::{SetTargetTemperatureRequest, SetTargetTemperatureResponse, GetTemperatureRequest, GetTemperatureResponse, SetHeatingStateRequest, SetHeatingStateResponse};

use pb::valve::valve_controller_service_server::ValveControllerService;
use pb::valve::{SwitchValveRequest, SwitchValveResponse, GetValveStateRequest, GetValveStateResponse};

pub struct TcdService {
    pub analyzer: Arc<Mutex<AnalyzerController>>,
}

#[tonic::async_trait]
impl ThermalConductivityDetectorService for TcdService {
    async fn set_bridge(&self, request: Request<SetBridgeRequest>) -> Result<Response<SetBridgeResponse>, Status> {
        let req = request.into_inner();
        let ctrl = self.analyzer.lock().await;
        match ctrl.tcd_set_bridge(req.current as u8).await {
            Ok(_) => Ok(Response::new(SetBridgeResponse { success: true, message: "OK".into() })),
            Err(e) => Ok(Response::new(SetBridgeResponse { success: false, message: e })),
        }
    }

    async fn zeroing(&self, _request: Request<ZeroingRequest>) -> Result<Response<ZeroingResponse>, Status> {
        let ctrl = self.analyzer.lock().await;
        match ctrl.tcd_zeroing().await {
            Ok(_) => Ok(Response::new(ZeroingResponse { success: true, message: "OK".into() })),
            Err(e) => Ok(Response::new(ZeroingResponse { success: false, message: e })),
        }
    }

    async fn state(&self, _request: Request<StateRequest>) -> Result<Response<StateResponse>, Status> {
        let ctrl = self.analyzer.lock().await;
        let st = ctrl.get_state().await;
        let tcd_val = st.signals.iter().find(|s| s.id == 0).map(|s| s.value).unwrap_or(0.0);
        let is_ready = ctrl.tcd.is_some();
        Ok(Response::new(StateResponse {
            is_ready,
            current_value: tcd_val,
        }))
    }
}

pub struct TempService {
    pub analyzer: Arc<Mutex<AnalyzerController>>,
}

#[tonic::async_trait]
impl TemperatureControllerService for TempService {
    async fn set_target_temperature(&self, request: Request<SetTargetTemperatureRequest>) -> Result<Response<SetTargetTemperatureResponse>, Status> {
        let req = request.into_inner();
        let id: u8 = req.zone_id.parse().unwrap_or(0);
        let ctrl = self.analyzer.lock().await;
        match ctrl.control_temp(id, req.target_temperature as f32).await {
            Ok(_) => Ok(Response::new(SetTargetTemperatureResponse { success: true, message: "OK".into() })),
            Err(e) => Ok(Response::new(SetTargetTemperatureResponse { success: false, message: e })),
        }
    }

    async fn get_temperature(&self, request: Request<GetTemperatureRequest>) -> Result<Response<GetTemperatureResponse>, Status> {
        let req = request.into_inner();
        let id: u8 = req.zone_id.parse().unwrap_or(0);
        let ctrl = self.analyzer.lock().await;
        let st = ctrl.get_state().await;
        if let Some(zone) = st.temp_zones.iter().find(|z| z.id == id) {
            Ok(Response::new(GetTemperatureResponse {
                actual_temperature: zone.current as f64,
                target_temperature: zone.target as f64,
                protect_temperature: 400.0,
                heating_enabled: zone.connected,
            }))
        } else {
            Err(Status::not_found("Zone not found"))
        }
    }

    async fn set_heating_state(&self, _request: Request<SetHeatingStateRequest>) -> Result<Response<SetHeatingStateResponse>, Status> {
        Ok(Response::new(SetHeatingStateResponse { success: true, message: "OK".into() }))
    }
}

pub struct ValveService {
    pub analyzer: Arc<Mutex<AnalyzerController>>,
}

#[tonic::async_trait]
impl ValveControllerService for ValveService {
    async fn switch_valve(&self, request: Request<SwitchValveRequest>) -> Result<Response<SwitchValveResponse>, Status> {
        let req = request.into_inner();
        let id: u8 = req.valve_id.parse().unwrap_or(0);
        let is_on = req.position > 0;
        let ctrl = self.analyzer.lock().await;
        match ctrl.control_switch(id, is_on).await {
            Ok(_) => Ok(Response::new(SwitchValveResponse { success: true, message: "OK".into() })),
            Err(e) => Ok(Response::new(SwitchValveResponse { success: false, message: e })),
        }
    }

    async fn get_valve_state(&self, request: Request<GetValveStateRequest>) -> Result<Response<GetValveStateResponse>, Status> {
        let req = request.into_inner();
        let id: u8 = req.valve_id.parse().unwrap_or(0);
        let ctrl = self.analyzer.lock().await;
        let st = ctrl.get_state().await;
        if let Some(sw) = st.switches.iter().find(|s| s.id == id) {
            Ok(Response::new(GetValveStateResponse {
                current_position: if sw.is_on { 1 } else { 0 },
            }))
        } else {
            Err(Status::not_found("Valve not found"))
        }
    }
}
