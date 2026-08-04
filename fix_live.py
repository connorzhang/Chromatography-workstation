import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\live.js'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

old_str = '''            if (parsed.type === 'samples' && parsed.values && (parsed.channel === 0 || parsed.channel === undefined)) {
                const baseT = parsed.t0S || 0;
                const dtS = parsed.dtS || 0.05;

                if (baseT === 0 || dataPoints.length > 50000000000) {
                    dataPoints = [];
                    latestPollutants = null;
                    lastCycleResetTime = Date.now();
                }'''

new_str = '''            if (parsed.type === 'samples' && parsed.values && (parsed.channel === 0 || parsed.channel === undefined)) {
                const baseT = parsed.t0S || 0;
                const dtS = parsed.dtS || 0.05;

                let sessionChanged = false;
                if (parsed.sessionToken) {
                    if (window.currentSessionToken !== undefined && window.currentSessionToken !== parsed.sessionToken) {
                        sessionChanged = true;
                    }
                    window.currentSessionToken = parsed.sessionToken;
                }

                if (baseT === 0 || sessionChanged || dataPoints.length > 50000000000) {
                    dataPoints = [];
                    latestPollutants = null;
                    lastCycleResetTime = Date.now();
                }'''

if old_str in content:
    content = content.replace(old_str, new_str)
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)
    print('live.js successfully updated')
else:
    print('old_str not found in live.js')
