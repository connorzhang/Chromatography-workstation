import re
import os

def update_version():
    main_go_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
    html_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\index.html'
    
    # Update main.go
    with open(main_go_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    match = re.search(r'const AppVersion = "v0\.3\.(\d+)"', content)
    if not match:
        print("Could not find AppVersion in main.go")
        return
        
    current_build = int(match.group(1))
    new_build = current_build + 1
    old_version = f'v0.3.{current_build}'
    new_version = f'v0.3.{new_build}'
    
    content = content.replace(f'const AppVersion = "{old_version}"', f'const AppVersion = "{new_version}"')
    
    with open(main_go_path, 'w', encoding='utf-8') as f:
        f.write(content)
        
    # Update index.html
    with open(html_path, 'rb') as f:
        html_content = f.read()
        
    old_version_bytes = old_version.encode('utf-8')
    new_version_bytes = new_version.encode('utf-8')
    html_content = html_content.replace(old_version_bytes, new_version_bytes)
    
    with open(html_path, 'wb') as f:
        f.write(html_content)
        
    print(f"Version updated to {new_version}")

if __name__ == '__main__':
    update_version()
