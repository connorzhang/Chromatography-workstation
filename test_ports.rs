use tokio_serial::available_ports;
fn main() {
    if let Ok(ports) = available_ports() {
        for p in ports {
            println!("PORT: '{}'", p.port_name);
        }
    }
}
