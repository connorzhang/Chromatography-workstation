fn main() -> Result<(), Box<dyn std::error::Error>> {
    unsafe {
        std::env::set_var("PROTOC", protoc_bin_vendored::protoc_bin_path().unwrap());
    }
    tonic_build::configure()
        .build_server(true)
        .compile(
            &[
                "../../../src/edge/internal/sila2/proto/detector.proto",
                "../../../src/edge/internal/sila2/proto/temperature.proto",
                "../../../src/edge/internal/sila2/proto/valve.proto",
            ],
            &["../../../src/edge/internal/sila2/proto/"]
        )?;
    Ok(())
}
