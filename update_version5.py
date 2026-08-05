filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'

# Read as binary, do byte-level replacement to avoid encoding issues
with open(filepath, 'rb') as f:
    data = f.read()

old_ver = b'const AppVersion = "v0.3.145"'
new_ver = b'const AppVersion = "v0.3.146"'

if old_ver in data:
    data = data.replace(old_ver, new_ver)
    with open(filepath, 'wb') as f:
        f.write(data)
    print("Version updated to v0.3.146 (binary mode)")
else:
    print("Version string not found!")