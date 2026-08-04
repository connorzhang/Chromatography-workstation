import re

file_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\live.js'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

# 1. HTML change
html_old = '''                    <span style="color: #94a3b8;">通道1:</span>
                    <span id="live-current-time" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">min</span>
                    <span id="live-current-signal" style="font-family: monospace; font-weight: bold; margin-left: 10px;">0.000</span> <span style="color: #94a3b8;">pA</span>

                    <span style="color: #94a3b8; margin-left: 10px;">信号1:</span>'''

html_new = '''                    <span style="color: #94a3b8;">时间:</span>
                    <span id="live-current-time" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">min</span>
                    <span style="color: #3b82f6; margin-left: 10px;">FID(CH0):</span>
                    <span id="live-current-signal" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">pA</span>
                    <span style="color: #ef4444; margin-left: 10px;">TCD(CH1):</span>
                    <span id="live-current-signal-tcd" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">mV</span>

                    <span style="color: #94a3b8; margin-left: 10px;">显示:</span>'''
content = content.replace(html_old, html_new)

# 2. Variable declarations
content = content.replace('let dataPoints = [];', 'let dataPoints = [];\n    let dataPoints1 = [];')

# 3. Y axis auto scale
auto_y_old = '''        if (autoYEl && autoYEl.checked && dataPoints.length > 0) {
            let yMin = Infinity; 
            let yMax = -Infinity;
            for (let i = 0; i < dataPoints.length; i++) {
                const v = dataPoints[i][1];
                if (v < yMin) yMin = v;
                if (v > yMax) yMax = v;
            }'''
auto_y_new = '''        if (autoYEl && autoYEl.checked && (dataPoints.length > 0 || dataPoints1.length > 0)) {
            let yMin = Infinity; 
            let yMax = -Infinity;
            for (let i = 0; i < dataPoints.length; i++) {
                const v = dataPoints[i][1];
                if (v < yMin) yMin = v;
                if (v > yMax) yMax = v;
            }
            for (let i = 0; i < dataPoints1.length; i++) {
                const v = dataPoints1[i][1];
                if (v < yMin) yMin = v;
                if (v > yMax) yMax = v;
            }'''
content = content.replace(auto_y_old, auto_y_new)

# 4. Draw curve
draw_old = '''        // Draw Curve
        if (dataPoints.length > 1) {
            ctx.strokeStyle = '#3b82f6';
            ctx.lineWidth = 1.5; 
            ctx.beginPath();     

            let started = false; 
            for (let i = 0; i < dataPoints.length; i++) {
                const tS = dataPoints[i][0];
                const v = dataPoints[i][1];
                const xMin = tS / 60;

                const x = padL + ((xMin - xBegMin) / xSpanMin) * w;
                const yn = (v - yBeg) / (yEnd - yBeg);
                const y = padT + (1 - yn) * h;

                if (!started) {  
                    ctx.moveTo(x, y);
                    started = true;
                } else {
                    ctx.lineTo(x, y);
                }
            }
            ctx.stroke();        
        } else {'''
draw_new = '''        // Draw Curve
        let hasData = false;
        if (dataPoints.length > 1) {
            hasData = true;
            ctx.strokeStyle = '#3b82f6';
            ctx.lineWidth = 1.5; 
            ctx.beginPath();     
            let started = false; 
            for (let i = 0; i < dataPoints.length; i++) {
                const xMin = dataPoints[i][0] / 60;
                const x = padL + ((xMin - xBegMin) / xSpanMin) * w;
                const y = padT + (1 - (dataPoints[i][1] - yBeg) / (yEnd - yBeg)) * h;
                if (!started) { ctx.moveTo(x, y); started = true; } else { ctx.lineTo(x, y); }
            }
            ctx.stroke();        
        }
        if (dataPoints1.length > 1) {
            hasData = true;
            ctx.strokeStyle = '#ef4444';
            ctx.lineWidth = 1.5; 
            ctx.beginPath();     
            let started = false; 
            for (let i = 0; i < dataPoints1.length; i++) {
                const xMin = dataPoints1[i][0] / 60;
                const x = padL + ((xMin - xBegMin) / xSpanMin) * w;
                const y = padT + (1 - (dataPoints1[i][1] - yBeg) / (yEnd - yBeg)) * h;
                if (!started) { ctx.moveTo(x, y); started = true; } else { ctx.lineTo(x, y); }
            }
            ctx.stroke();        
        }
        if (!hasData) {'''
content = content.replace(draw_old, draw_new)

# 5. Data reception logic
recv_old = '''            if (parsed.type === 'samples' && parsed.values && (parsed.channel === 0 || parsed.channel === undefined)) {
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
                }

                for (let i = 0; i < parsed.values.length; i++) {  
                    dataPoints.push([baseT + i * dtS, parsed.values[i]]);
                }

                // Ensure it stays sorted if merged out of order  
                dataPoints.sort((a, b) => a[0] - b[0]);

                if (dataPoints.length > 0) {
                    const lastPoint = dataPoints[dataPoints.length - 1];
                    const timeEl = document.getElementById('live-current-time');
                    const sigEl = document.getElementById('live-current-signal');
                    if (timeEl) timeEl.innerText = (lastPoint[0] / 60.0).toFixed(3);
                    if (sigEl) sigEl.innerText = lastPoint[1].toFixed(3);
                }

                requestAnimationFrame(draw);
            }'''
recv_new = '''            if (parsed.type === 'samples' && parsed.values) {
                const baseT = parsed.t0S || 0;
                const dtS = parsed.dtS || 0.05;
                const ch = parsed.channel || 0;

                let sessionChanged = false;
                if (parsed.sessionToken) {
                    if (window.currentSessionToken !== undefined && window.currentSessionToken !== parsed.sessionToken) {
                        sessionChanged = true;
                    }
                    window.currentSessionToken = parsed.sessionToken;
                }

                if (baseT === 0 || sessionChanged || dataPoints.length > 50000000000) {
                    dataPoints = [];
                    dataPoints1 = [];
                    latestPollutants = null;
                    lastCycleResetTime = Date.now();
                }

                if (ch === 0) {
                    for (let i = 0; i < parsed.values.length; i++) {  
                        dataPoints.push([baseT + i * dtS, parsed.values[i]]);
                    }
                    dataPoints.sort((a, b) => a[0] - b[0]);
                    if (dataPoints.length > 0) {
                        const lastPoint = dataPoints[dataPoints.length - 1];
                        const timeEl = document.getElementById('live-current-time');
                        const sigEl = document.getElementById('live-current-signal');
                        if (timeEl) timeEl.innerText = (lastPoint[0] / 60.0).toFixed(3);
                        if (sigEl) sigEl.innerText = lastPoint[1].toFixed(3);
                    }
                } else if (ch === 1) {
                    for (let i = 0; i < parsed.values.length; i++) {  
                        dataPoints1.push([baseT + i * dtS, parsed.values[i]]);
                    }
                    dataPoints1.sort((a, b) => a[0] - b[0]);
                    if (dataPoints1.length > 0) {
                        const lastPoint = dataPoints1[dataPoints1.length - 1];
                        const timeEl = document.getElementById('live-current-time');
                        const sigEl1 = document.getElementById('live-current-signal-tcd');
                        if (timeEl) timeEl.innerText = (lastPoint[0] / 60.0).toFixed(3);
                        if (sigEl1) sigEl1.innerText = lastPoint[1].toFixed(3);
                    }
                }

                requestAnimationFrame(draw);
            }'''
content = content.replace(recv_old, recv_new)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)
print('live.js channels isolated and fixed')
