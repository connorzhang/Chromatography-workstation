fn main() {
    let root = std::path::PathBuf::from(r"I:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static");
    let mut p = std::path::PathBuf::new();
    p.push(&root);
    p.push("index.html");
    let raw_path = p.to_string_lossy().replace('\\', "/");
    println!("raw_path: {}", raw_path);
    let meta = std::fs::symlink_metadata(&raw_path);
    println!("meta: {:?}", meta);
}