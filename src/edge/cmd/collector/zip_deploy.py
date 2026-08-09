import zipfile, os
z = zipfile.ZipFile('deploy_new.zip', 'w')
z.write('collector.exe')
z.write('.env')
z.write(r'..\..\..\..\collector-service.exe', 'collector-service.exe')
z.write(r'..\..\..\..\collector-service.xml', 'collector-service.xml')
for r, d, fs in os.walk('static'):
    for f in fs:
        z.write(os.path.join(r, f))
z.close()