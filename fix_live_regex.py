import re

filepath = 'src/edge/cmd/collector/static/js/views/live.js'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

# Replace HTML
content = re.sub(
    r'<span style="color: #94a3b8;">通道1:</span>\s*<span id="live-current-time"[^>]*>0.000</span>\s*<span[^>]*>min</span>\s*<span id="live-current-signal"[^>]*>0.000</span>\s*<span[^>]*>pA</span>\s*<span style="color: #94a3b8; margin-left: 10px;">信号1:</span>',
    '<span style="color: #94a3b8;">时间:</span>\n<span id="live-current-time" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">min</span>\n<span style="color: #3b82f6; margin-left: 10px;">FID(CH0):</span>\n<span id="live-current-signal" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">pA</span>\n<span style="color: #ef4444; margin-left: 10px;">TCD(CH1):</span>\n<span id="live-current-signal-tcd" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">mV</span>\n<span style="color: #94a3b8; margin-left: 10px;">显示:</span>',
    content
)

# Auto Y
content = re.sub(
    r'if \(autoYEl && autoYEl\.checked && dataPoints\.length > 0\) \{',
    'if (autoYEl && autoYEl.checked && (dataPoints.length > 0 || dataPoints1.length > 0)) {',
    content
)

content = re.sub(
    r'(for \(let i = 0; i < dataPoints\.length; i\+\+\) \{.*?if \(v > yMax\) yMax = v;\s*\})',
    r'\1\n            for (let i = 0; i < dataPoints1.length; i++) {\n                const v = dataPoints1[i][1];\n                if (v < yMin) yMin = v;\n                if (v > yMax) yMax = v;\n            }',
    content,
    flags=re.DOTALL
)

# Draw curve
draw_old = r'(if \(dataPoints\.length > 1\) \{.*?ctx\.stroke\(\);\s*\}) else \{'
draw_new = r'''let hasData = false;
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
content = re.sub(draw_old, draw_new, content, flags=re.DOTALL)

# Data reception logic
recv_old = r"if \(parsed\.type === 'samples' && parsed\.values && \(parsed\.channel === 0 \|\| parsed\.channel === undefined\)\) \{.*?requestAnimationFrame\(draw\);\s*\}"
recv_new = r'''if (parsed.type === 'samples' && parsed.values) {
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
content = re.sub(recv_old, recv_new, content, flags=re.DOTALL)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
print('live.js regex replace done.')
