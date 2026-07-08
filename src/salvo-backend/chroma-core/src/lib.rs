use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Copy, Serialize, Deserialize)]
pub struct DataPoint {
    pub time: f64,
    pub value: f64,
}

/// Largest Triangle Three Buckets (LTTB) algorithm implementation
/// Downsamples a large chromatogram to `threshold` points while preserving visual peaks and valleys.
pub fn downsample_lttb(data: &[DataPoint], threshold: usize) -> Vec<DataPoint> {
    let data_len = data.len();
    if threshold >= data_len || threshold == 0 {
        return data.to_vec();
    }

    let mut sampled = Vec::with_capacity(threshold);
    
    // Bucket size. Leave room for start and end data points
    let every = (data_len - 2) as f64 / (threshold - 2) as f64;
    
    let mut a = 0;
    let mut max_area_point = (0, 0.0);
    let mut next_a = 0;

    sampled.push(data[a]); // Always add the first point

    for i in 0..(threshold - 2) {
        // Calculate point average for next bucket (center of mass)
        let mut avg_x = 0.0;
        let mut avg_y = 0.0;
        let avg_range_start = ((i + 1) as f64 * every).floor() as usize + 1;
        let mut avg_range_end = ((i + 2) as f64 * every).floor() as usize + 1;
        
        if avg_range_end >= data_len {
            avg_range_end = data_len;
        }
        
        let avg_range_length = (avg_range_end - avg_range_start) as f64;
        
        for j in avg_range_start..avg_range_end {
            avg_x += data[j].time;
            avg_y += data[j].value;
        }
        avg_x /= avg_range_length;
        avg_y /= avg_range_length;

        // Get the range for this bucket
        let range_offs = (i as f64 * every).floor() as usize + 1;
        let range_to = ((i + 1) as f64 * every).floor() as usize + 1;

        // Point a
        let point_a_x = data[a].time;
        let point_a_y = data[a].value;

        let mut max_area = -1.0;

        for j in range_offs..range_to {
            // Calculate triangle area over three buckets
            let area = ((point_a_x - avg_x) * (data[j].value - point_a_y) - 
                       (point_a_x - data[j].time) * (avg_y - point_a_y)).abs() * 0.5;
            
            if area > max_area {
                max_area = area;
                max_area_point = (j, area);
                next_a = j;
            }
        }
        
        sampled.push(data[max_area_point.0]); // Pick this point from the bucket
        a = next_a; // This a is the next a (prev)
    }

    sampled.push(data[data_len - 1]); // Always add last

    sampled
}

// ==========================================
// Calibration Engine
// ==========================================

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct CalibrationPoint {
    pub level: usize,
    pub amount: f64,
    pub response: f64,
    pub used: bool,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct CalibrationCurve {
    pub slope: f64,
    pub intercept: f64,
    pub r_squared: f64,
    pub points: Vec<CalibrationPoint>,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct CalibrationRequest {
    pub points: Vec<CalibrationPoint>,
    pub fit_type: String, // "Linear", "Quadratic"
    pub origin_treatment: String, // "Ignore Origin", "Force Origin", "Include Origin"
}

pub fn calculate_calibration_curve(req: &CalibrationRequest) -> CalibrationCurve {
    let mut sum_x = 0.0;
    let mut sum_y = 0.0;
    let mut sum_xy = 0.0;
    let mut sum_x2 = 0.0;
    let mut sum_y2 = 0.0;
    let mut n = 0.0;

    let mut points = req.points.clone();
    
    // Include Origin logic
    if req.origin_treatment == "Include Origin" {
        points.push(CalibrationPoint {
            level: 0,
            amount: 0.0,
            response: 0.0,
            used: true,
        });
    }

    for pt in &points {
        if pt.used {
            let x = pt.amount;
            let y = pt.response;
            sum_x += x;
            sum_y += y;
            sum_xy += x * y;
            sum_x2 += x * x;
            sum_y2 += y * y;
            n += 1.0;
        }
    }

    if req.origin_treatment == "Force Origin" {
        // y = mx
        // m = sum(x*y) / sum(x^2)
        let slope = if sum_x2 != 0.0 { sum_xy / sum_x2 } else { 0.0 };
        let intercept = 0.0;
        
        let mut ss_tot = 0.0;
        let mut ss_res = 0.0;
        let y_mean = sum_y / n;
        for pt in &points {
            if pt.used {
                let y_pred = slope * pt.amount;
                ss_tot += (pt.response - y_mean).powi(2);
                ss_res += (pt.response - y_pred).powi(2);
            }
        }
        let r_squared = if ss_tot != 0.0 { 1.0 - (ss_res / ss_tot) } else { 0.0 };

        CalibrationCurve {
            slope,
            intercept,
            r_squared,
            points: req.points.clone(), // Return original points without the injected origin
        }
    } else {
        // Standard Linear Regression
        let denominator = n * sum_x2 - sum_x * sum_x;
        if denominator == 0.0 || n < 2.0 {
            return CalibrationCurve { slope: 0.0, intercept: 0.0, r_squared: 0.0, points: req.points.clone() };
        }

        let slope = (n * sum_xy - sum_x * sum_y) / denominator;
        let intercept = (sum_y - slope * sum_x) / n;

        // Calculate R^2
        let r_num = n * sum_xy - sum_x * sum_y;
        let r_den = ((n * sum_x2 - sum_x * sum_x) * (n * sum_y2 - sum_y * sum_y)).sqrt();
        let r = if r_den != 0.0 { r_num / r_den } else { 0.0 };
        let r_squared = r * r;

        CalibrationCurve {
            slope,
            intercept,
            r_squared,
            points: req.points.clone(),
        }
    }
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct SequenceRow {
    pub line: usize,
    pub location: String,
    pub sample_name: String,
    pub method_name: String,
    pub inj_vol: String,
    pub inj_per_loc: usize,
    pub sample_type: String,
    pub multiplier: f64,
    pub dilution: f64,
    pub data_file: String,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct SequenceRequest {
    pub rows: Vec<SequenceRow>,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct SequenceStatus {
    pub status: String,
    pub current_line: usize,
    pub current_inj: usize,
    pub message: String,
}

#[derive(Debug, Serialize, Deserialize, Clone)]
pub struct TimeEvent {
    pub time: f64,
    pub event_type: String, // e.g., "Integration Off", "Tangent Skim", "Drop Baseline"
    pub value: f64,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct IntegrationEvents {
    pub initial_area_reject: f64,
    pub initial_peak_width: f64,
    pub tangent_skim_mode: bool,
    pub drop_baseline: bool,
    #[serde(default)]
    pub time_events: Vec<TimeEvent>,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct PeakResult {
    pub num: usize,
    pub rt_min: f64,
    pub area: f64,
    pub height: f64,
    pub width: f64,
    pub baseline_type: String,
    pub area_percent: f64,
    pub theoretical_plates: f64,
    pub resolution: f64,
    pub tailing_factor: f64,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct IntegrationReport {
    pub peaks: Vec<PeakResult>,
    pub total_area: f64,
    pub total_height: f64,
}

/// A simplified representation of a chromatographic integration engine
/// In a real world, this would use first derivative thresholding.
pub fn process_chromatogram(data: &[DataPoint], events: &IntegrationEvents) -> IntegrationReport {
    let mut peaks = Vec::new();
    
    // Very naive threshold-based peak picking for demonstration
    let mut in_peak = false;
    let mut _peak_start_idx = 0;
    let mut peak_start_time = 0.0;
    let mut peak_max_val = 0.0;
    let mut peak_max_time = 0.0;
    let mut current_area = 0.0;
    
    // Simulate drop baseline by adjusting the threshold
    let threshold = 15.0; // Assume baseline is around 10.0
    
    for i in 1..data.len() {
        let pt = data[i];
        let prev = data[i-1];
        
        // Evaluate time events for the current time
        let mut integration_active = true;
        let mut current_tangent = events.tangent_skim_mode;
        let mut current_drop = events.drop_baseline;

        for evt in &events.time_events {
            if pt.time >= evt.time {
                match evt.event_type.as_str() {
                    "Integration" => integration_active = evt.value > 0.0,
                    "Tangent Skim" => current_tangent = evt.value > 0.0,
                    "Drop Baseline" => current_drop = evt.value > 0.0,
                    _ => {}
                }
            }
        }

        if !integration_active {
            if in_peak {
                in_peak = false; // abort current peak if integration turned off
            }
            continue;
        }

        if pt.value > threshold && !in_peak {
            in_peak = true;
            _peak_start_idx = i;
            peak_start_time = pt.time;
            peak_max_val = pt.value;
            peak_max_time = pt.time;
            current_area = 0.0;
        } else if pt.value > threshold && in_peak {
            if pt.value > peak_max_val {
                peak_max_val = pt.value;
                peak_max_time = pt.time;
            }
            // Trapezoidal rule for area
            let dt = pt.time - prev.time;
            current_area += (pt.value + prev.value) * 0.5 * dt;
        } else if pt.value <= threshold && in_peak {
            in_peak = false;
            
            // Apply Initial Area Reject event
            if current_area >= events.initial_area_reject {
                let rt_min = (peak_max_time * 100.0).round() / 100.0;
                let width = ((pt.time - peak_start_time) * 1000.0).round() / 1000.0;
                
                // Calculate SST parameters (USP/EP algorithms)
                // 1. Theoretical Plates (USP): N = 16 * (t_r / W)^2
                let mut plates = 0.0;
                if width > 0.0 {
                    plates = 16.0 * (rt_min / width).powi(2);
                }

                // 2. Tailing Factor (USP): Tf = W0.05 / 2f
                // Mock calculation based on width for demo purposes
                let tailing_factor = 1.0 + (width * 0.1); 

                peaks.push(PeakResult {
                    num: 0, // Will be filled later
                    rt_min, // Assume input time is min
                    area: (current_area * 100.0).round() / 100.0,
                    height: ((peak_max_val - 10.0) * 100.0).round() / 100.0, // Baseline sub
                    width,
                    baseline_type: if current_drop { "Drop".to_string() } else if current_tangent { "Tangent".to_string() } else { "BB".to_string() },
                    area_percent: 0.0,
                    theoretical_plates: plates.round(),
                    resolution: 0.0, // Calculated after all peaks are found
                    tailing_factor: (tailing_factor * 100.0).round() / 100.0,
                });
            }
        }
    }

    let total_area: f64 = peaks.iter().map(|p| p.area).sum();
    let total_height: f64 = peaks.iter().map(|p| p.height).sum();

    for i in 0..peaks.len() {
        peaks[i].num = i + 1;
        if total_area > 0.0 {
            peaks[i].area_percent = ((peaks[i].area / total_area) * 10000.0).round() / 100.0;
        }

        // Calculate Resolution (Rs) between adjacent peaks
        // Rs = 2 * (tr2 - tr1) / (W1 + W2)
        if i > 0 {
            let tr1 = peaks[i-1].rt_min;
            let w1 = peaks[i-1].width;
            let tr2 = peaks[i].rt_min;
            let w2 = peaks[i].width;
            
            if w1 + w2 > 0.0 {
                peaks[i].resolution = (2.0 * (tr2 - tr1) / (w1 + w2) * 100.0).round() / 100.0;
            }
        }
    }

    IntegrationReport {
        peaks,
        total_area: (total_area * 100.0).round() / 100.0,
        total_height: (total_height * 100.0).round() / 100.0,
    }
}

// ==========================================
// GPC/SEC Analysis Engine (Gel Permeation Chromatography)
// ==========================================

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct GpcSlice {
    pub retention_time: f64,
    pub height: f64,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct GpcResult {
    pub mn: f64, // Number-average molecular weight
    pub mw: f64, // Weight-average molecular weight
    pub mz: f64, // Z-average molecular weight
    pub pdi: f64, // Polydispersity index (Mw/Mn)
}

/// Calculate GPC molecular weight distribution parameters
/// slope and intercept are derived from a log M vs RT calibration curve: log M = slope * RT + intercept
pub fn calculate_gpc_distribution(slices: &[GpcSlice], slope: f64, intercept: f64) -> GpcResult {
    let mut sum_h = 0.0;
    let mut sum_h_over_m = 0.0;
    let mut sum_h_times_m = 0.0;
    let mut sum_h_times_m2 = 0.0;

    for slice in slices {
        if slice.height <= 0.0 {
            continue;
        }
        
        // M_i = 10^(slope * RT + intercept)
        let log_m = slope * slice.retention_time + intercept;
        let m_i = 10_f64.powf(log_m);
        let h_i = slice.height;

        sum_h += h_i;
        sum_h_over_m += h_i / m_i;
        sum_h_times_m += h_i * m_i;
        sum_h_times_m2 += h_i * m_i * m_i;
    }

    if sum_h == 0.0 || sum_h_over_m == 0.0 || sum_h_times_m == 0.0 {
        return GpcResult { mn: 0.0, mw: 0.0, mz: 0.0, pdi: 0.0 };
    }

    let mn = sum_h / sum_h_over_m;
    let mw = sum_h_times_m / sum_h;
    let mz = sum_h_times_m2 / sum_h_times_m;
    let pdi = if mn > 0.0 { mw / mn } else { 0.0 };

    GpcResult {
        mn: mn.round(),
        mw: mw.round(),
        mz: mz.round(),
        pdi: (pdi * 1000.0).round() / 1000.0,
    }
}

// ==========================================
// MS Deconvolution Engine (Mass Spectrometry AMDIS core)
// ==========================================

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct MassPeak {
    pub mz: f64,
    pub intensity: f64,
}

#[derive(Debug, Serialize, Deserialize)]
pub struct LibraryMatch {
    pub compound_name: String,
    pub match_factor: f64, // 0.0 - 1000.0 (like NIST)
    pub cas_number: String,
}

/// A very basic representation of mass spectral library matching
/// Uses dot-product (cosine similarity) of spectra
pub fn match_mass_spectrum(unknown: &[MassPeak], library_spectrum: &[MassPeak]) -> LibraryMatch {
    let mut dot_product = 0.0;
    let mut norm_unknown = 0.0;
    let mut norm_library = 0.0;

    // Simple nested loop for matching mz (in reality, requires binning or precise mapping)
    for u in unknown {
        norm_unknown += u.intensity * u.intensity;
        for l in library_spectrum {
            // If m/z matches within a small tolerance (e.g. 0.5 Da)
            if (u.mz - l.mz).abs() < 0.5 {
                dot_product += u.intensity * l.intensity;
            }
        }
    }

    for l in library_spectrum {
        norm_library += l.intensity * l.intensity;
    }

    let match_factor = if norm_unknown > 0.0 && norm_library > 0.0 {
        let cosine = dot_product / (norm_unknown.sqrt() * norm_library.sqrt());
        // Scale to NIST-like 0-1000 score
        (cosine * 1000.0).max(0.0).min(1000.0)
    } else {
        0.0
    };

    LibraryMatch {
        compound_name: "Matched Compound".to_string(), // Placeholder
        match_factor: match_factor.round(),
        cas_number: "00-00-0".to_string(),
    }
}
