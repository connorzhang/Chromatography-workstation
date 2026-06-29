export function initLiveChromatogram() {
    const container = document.getElementById('view-live');
    container.innerHTML = `
        <div style="display: flex; height: calc(100vh - 80px); gap: 1rem;">
            <div style="flex: 1; min-width: 0; background: var(--panel); border-radius: 8px; border: 1px solid #334155; position: relative; display: flex; flex-direction: column;">
                <!-- 第一行：进样类型、控制按钮、点火图标 -->
                <div style="padding: 6px 10px; border-bottom: 1px solid #334155; display: flex; gap: 10px; align-items: center; justify-content: space-between;">
                    <div style="display: flex; gap: 10px; align-items: center;">
                        <span style="color: #94a3b8;">进样类型:</span>
                        <label><input type="radio" name="injType" value="normal" checked> 正常</label>
                        <label><input type="radio" name="injType" value="zero"> 零气</label>
                        <label><input type="radio" name="injType" value="span"> 标气</label>
                        
                        <div style="margin-left: 20px; display: flex; gap: 10px;">
                            <button class="btn" onclick="window.sendCmd('startAll')">▶ 开始分析</button>
                            <button class="btn btn-danger" onclick="window.sendCmd('stopAll')">⏹ 停止分析</button>
                        </div>
                    </div>
                    
                    <!-- 点火图标 -->
                    <div id="live-ignite-icon" style="font-size: 28px; cursor: pointer; color: #64748b; transition: color 0.3s;" title="点击发送点火指令">
                        🔥
                    </div>
                </div>

                <!-- 第二行：图表配置项 -->
                <div style="padding: 6px 10px; border-bottom: 1px solid #334155; display: flex; gap: 15px; align-items: center; background: rgba(0,0,0,0.2);">
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">下限:</span>
                        <input type="number" id="live-y-low" class="input" value="0" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">上限:</span>
                        <input type="number" id="live-y-high" class="input" value="40" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">采集时间:</span>
                        <input type="number" id="live-acq-min" class="input" value="2" step="0.1" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">满屏时间:</span>
                        <input type="number" id="live-full-min" class="input" value="2" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <button class="btn" id="btn-apply-live-settings" style="padding: 2px 10px; font-size: 12px;">应用</button>
                </div>

                <!-- 第三行：实时状态与自适应配置 -->
                <div style="padding: 5px 10px; border-bottom: 1px solid #334155; display: flex; gap: 15px; align-items: center; font-size: 13px; background: rgba(0,0,0,0.1);">
                    <span style="color: #94a3b8;">通道1:</span>
                    <span id="live-current-time" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">min</span>
                    <span id="live-current-signal" style="font-family: monospace; font-weight: bold; margin-left: 10px;">0.000</span> <span style="color: #94a3b8;">pA</span>
                    
                    <span style="color: #94a3b8; margin-left: 10px;">信号1:</span>
                    <label style="display: flex; align-items: center; gap: 5px; color: #10b981; cursor: pointer;">
                        <input type="checkbox" id="live-auto-y"> 峰高自适应
                    </label>
                </div>

                <!-- 图表区 -->
                <div style="flex: 1; position: relative; min-height: 0;">
                    <canvas id="chromatogram-canvas" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></canvas>
                </div>
            </div>
            <div style="flex: 0 0 350px; display: flex; flex-direction: column; gap: 0.5rem; min-height: 0;">
                <div class="control-group" style="flex: 1; margin: 0; padding: 0.8rem; overflow-y: auto;">
                    <h3 style="margin-top:0; margin-bottom:0.5rem;">实时结果</h3>
                    <table id="live-results-table" style="margin-top: 0;">
                        <thead>
                            <tr><th>名称</th><th style="text-align:right">含量(mg/m³)</th></tr>
                        </thead>
                        <tbody>
                            <tr><td colspan="2" style="text-align:center; color:#94a3b8">等待分析...</td></tr>
                        </tbody>
                    </table>
                </div>
                <div style="flex: 1; display: flex; gap: 0.5rem; min-height: 0;">
                    <div class="control-group" style="flex: 1; margin: 0; padding: 0.6rem; overflow-y: auto;">
                        <table id="live-pressure-table" style="font-size: 13px; margin-top: 0; width: 100%;">
                            <thead>
                                <tr><th>名称</th><th style="text-align:right">实测(psi)</th></tr>
                            </thead>
                            <tbody>
                                <tr><td>载气1</td><td id="val-carrier1" style="text-align:right">0.00</td></tr>
                                <tr><td>载气3</td><td id="val-carrier3" style="text-align:right">0.00</td></tr>
                                <tr><td>样气</td><td id="val-sample" style="text-align:right">0.00</td></tr>
                                <tr><td>载气2</td><td id="val-carrier2" style="text-align:right">0.00</td></tr>
                                <tr><td>氢气1</td><td id="val-h2-1" style="text-align:right">0.00</td></tr>
                                <tr><td>空气1</td><td id="val-air-1" style="text-align:right">0.00</td></tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="control-group" style="flex: 1; margin: 0; padding: 0.6rem; overflow-y: auto;">
                        <table id="live-temp-table" style="font-size: 13px; margin-top: 0; width: 100%;">
                            <thead>
                                <tr><th>名称</th><th style="text-align:right">实测(℃)</th></tr>
                            </thead>
                            <tbody>
                                <tr><td>柱箱</td><td id="val-col" style="text-align:right">0.0</td></tr>
                                <tr><td>阀温</td><td id="val-valve" style="text-align:right">0.0</td></tr>
                                <tr><td>检测1</td><td id="val-det1" style="text-align:right">0.0</td></tr>
                                <tr><td>进样1</td><td id="val-inj1" style="text-align:right">0.0</td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    `;

    // Initialize Canvas after rendering DOM
    setTimeout(setupCanvas, 0);
}

function setupCanvas() {
    const canvas = document.getElementById('chromatogram-canvas');
    if (!canvas) return;
    
    // Resize canvas to match display size
    const rect = canvas.parentElement.getBoundingClientRect();
    canvas.width = rect.width;
    canvas.height = rect.height;
    
    const ctx = canvas.getContext('2d');
    let dataPoints = [];
    let latestPollutants = null;
    let sessionRestored = false;
    let lastCycleResetTime = 0;

    // For auto Y smoothing
    let lastMin = null;
    let lastMax = null;

    // Local UI settings
    let uiSettings = {
        yLow: 0,
        yHigh: 40,
        acqMin: 2,
        fullMin: 2,
        autoY: true
    };

    let isIgnited = false;

    // Load initial UI settings
    fetch('/api/v1/devices')
        .then(res => res.json())
        .then(data => {
            if(data && data.length > 0) {
                const devId = data[0].deviceId;
                fetch('/api/v1/ui?deviceId=' + encodeURIComponent(devId))
                    .then(r => r.json())
                    .then(ui => {
                        if (ui) {
                            uiSettings = ui; // 保存完整的 ui 对象，防止 POST 覆盖丢失字段
                            if (ui.yLow !== undefined) uiSettings.yLow = ui.yLow;
                            if (ui.yHigh !== undefined) uiSettings.yHigh = ui.yHigh;
                            if (ui.acqMin !== undefined) uiSettings.acqMin = ui.acqMin;
                            if (ui.fullMin !== undefined) uiSettings.fullMin = ui.fullMin;
                            if (ui.autoY !== undefined) uiSettings.autoY = ui.autoY;
                            
                            document.getElementById('live-y-low').value = uiSettings.yLow;
                            document.getElementById('live-y-high').value = uiSettings.yHigh;
                            document.getElementById('live-acq-min').value = uiSettings.acqMin;
                            document.getElementById('live-full-min').value = uiSettings.fullMin;
                            const autoYEl = document.getElementById('live-auto-y');
                            if (autoYEl) autoYEl.checked = uiSettings.autoY;
                            draw();
                        }
                    }).catch(e => console.error(e));
            }
        }).catch(e => console.error(e));

    // Handle Apply button
    document.getElementById('btn-apply-live-settings').addEventListener('click', async () => {
        const yLow = parseFloat(document.getElementById('live-y-low').value) || 0;
        const yHigh = parseFloat(document.getElementById('live-y-high').value) || 40;
        const acqMin = parseFloat(document.getElementById('live-acq-min').value) || 0;
        const fullMin = parseFloat(document.getElementById('live-full-min').value) || 2;

        uiSettings.yLow = yLow;
        uiSettings.yHigh = yHigh;
        uiSettings.acqMin = acqMin;
        uiSettings.fullMin = fullMin;
        const autoYEl = document.getElementById('live-auto-y');
        if (autoYEl) uiSettings.autoY = autoYEl.checked;

        try {
            const devRes = await fetch('/api/v1/devices');
            const devices = await devRes.json();
            let deviceId = "GC-MODULAR";
            if (devices && devices.length > 0) {
                deviceId = devices[0].deviceId;
                const gcDev = devices.find(d => String(d.deviceId).startsWith('GC-MODULAR'));
                if (gcDev) deviceId = gcDev.deviceId;
            }

            uiSettings.deviceId = deviceId;

            const res = await fetch('/api/v1/ui', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(uiSettings)
            });
            if(res.ok) {
                window.showToast('图表配置已应用!');
                draw();
            } else {
                window.showToast('应用失败', true);
            }
        } catch(e) {
            window.showToast('异常: ' + e.message, true);
        }
    });

    // Handle Auto Y toggle
    const autoYEl = document.getElementById('live-auto-y');
    if (autoYEl) {
        autoYEl.addEventListener('change', async (e) => {
            uiSettings.autoY = e.target.checked;
            try {
                const devRes = await fetch('/api/v1/devices');
                const devices = await devRes.json();
                let devId = "GC-MODULAR";
                if (devices && devices.length > 0) {
                    devId = devices[0].deviceId;
                    const gcDev = devices.find(d => String(d.deviceId).startsWith('GC-MODULAR'));
                    if (gcDev) devId = gcDev.deviceId;
                }
                uiSettings.deviceId = devId;
                await fetch('/api/v1/ui', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(uiSettings)
                });
            } catch(err) {
                console.error('Failed to save autoY', err);
            }
            draw();
        });
    }

    // Handle Ignite icon click
    const igniteIcon = document.getElementById('live-ignite-icon');
    if (igniteIcon) {
        igniteIcon.addEventListener('click', async () => {
            try {
                const res = await fetch('/api/control/ignite', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ action: 'start', detector: 'FID1' })
                });
                if(res.ok) {
                    window.showToast('FID 点火指令已发送');
                    isIgnited = true;
                    igniteIcon.style.color = '#ef4444'; // Red color when ignited
                    igniteIcon.title = "已点火";
                } else {
                    window.showToast('点火指令发送失败', true);
                }
            } catch(e) {
                window.showToast('点火异常: ' + e.message, true);
            }
        });
    }

    function draw() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        const padL = 60;
        const padR = 20;
        const padT = 20;
        const padB = 40;
        
        const w = canvas.width - padL - padR;
        const h = canvas.height - padT - padB;
        
        if (w <= 0 || h <= 0) return;
        
        // Background grid and axis
        ctx.strokeStyle = '#334155';
        ctx.lineWidth = 1;
        
        let xBegMin = 0;
        let xEndMin = uiSettings.fullMin || 2; 
        
        // 覆盖自动延长的逻辑，以 UI 配置的满屏时间为准
        if (dataPoints.length > 0) {
            const maxT = dataPoints[dataPoints.length - 1][0];
            if (maxT / 60 > xEndMin) {
                // 如果实际数据超过了满屏时间，自动往右滚动或者自动扩展。
                // 传统色谱通常会固定 xEndMin，超出的部分被裁剪，或者自动增加满屏时间。
                // 这里我们暂且允许它自动向后拓展，保证能看到波形。
                xEndMin = Math.ceil(maxT / 60);
            }
        }
        const xSpanMin = xEndMin - xBegMin;
        
        let yBeg = uiSettings.yLow;
        let yEnd = uiSettings.yHigh;

        // 峰高自适应逻辑
        const autoYEl = document.getElementById('live-auto-y');
        if (autoYEl && autoYEl.checked && dataPoints.length > 0) {
            let yMin = Infinity;
            let yMax = -Infinity;
            for (let i = 0; i < dataPoints.length; i++) {
                const v = dataPoints[i][1];
                if (v < yMin) yMin = v;
                if (v > yMax) yMax = v;
            }
            if (yMin === Infinity) { yMin = 0; yMax = 1; }

            let span = yMax - yMin;
            // 提高最小量程阈值，避免纯基线微小漂移被当作峰而触发留白放大
            const minSpan = 10.0; 
            if (span < minSpan) {
                span = minSpan;
            }
            
            // 下面留5%，上面留60%，所以实际波形（或 minSpan）占据中间的 35%
            const V = span / 0.35;
            yBeg = yMin - 0.05 * V;
            // 始终基于 yMin 和 span 计算 yEnd，确保在 span 被锁定时，yEnd 不会随着 yMax 的微小增加而往下压波形
            yEnd = yMin + 0.95 * V;
        }

        if (yEnd <= yBeg) yEnd = yBeg + 1; // 防止无效区间
        
        lastMin = yBeg;
        lastMax = yEnd;
        
        function niceStep(range, targetTicks) {
            const raw = range / targetTicks;
            const pow = Math.pow(10, Math.floor(Math.log10(raw)));
            const n = raw / pow;
            let step;
            if (n <= 1.5) step = 1;
            else if (n <= 2.5) step = 2;
            else if (n <= 4) step = 3;
            else if (n <= 7) step = 5;
            else step = 10;
            return step * pow;
        }
        
        const xStep = niceStep(xSpanMin, 8);
        const yStep = niceStep(yEnd - yBeg, 6);
        
        // Draw Grid
        ctx.strokeStyle = '#334155';
        ctx.beginPath();
        for (let x = Math.ceil(xBegMin / xStep) * xStep; x <= xEndMin + 1e-9; x += xStep) {
            const sx = padL + ((x - xBegMin) / xSpanMin) * w;
            ctx.moveTo(sx, padT);
            ctx.lineTo(sx, padT + h);
        }
        for (let y = Math.ceil(yBeg / yStep) * yStep; y <= yEnd + 1e-9; y += yStep) {
            const sy = padT + (1 - (y - yBeg) / (yEnd - yBeg)) * h;
            ctx.moveTo(padL, sy);
            ctx.lineTo(padL + w, sy);
        }
        ctx.stroke();
        
        // Draw Axis Lines
        ctx.strokeStyle = '#94a3b8';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(padL, padT);
        ctx.lineTo(padL, padT + h);
        ctx.lineTo(padL + w, padT + h);
        ctx.stroke();
        
        // Draw Labels
        ctx.fillStyle = '#94a3b8';
        ctx.font = '12px system-ui';
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        for (let x = Math.ceil(xBegMin / xStep) * xStep; x <= xEndMin + 1e-9; x += xStep) {
            const sx = padL + ((x - xBegMin) / xSpanMin) * w;
            ctx.fillText((Math.round(x * 1000) / 1000).toString(), sx, padT + h + 8);
        }
        ctx.fillText('时间 (min)', padL + w / 2, padT + h + 24);
        
        ctx.textAlign = 'right';
        ctx.textBaseline = 'middle';
        for (let y = Math.ceil(yBeg / yStep) * yStep; y <= yEnd + 1e-9; y += yStep) {
            const sy = padT + (1 - (y - yBeg) / (yEnd - yBeg)) * h;
            ctx.fillText(y.toFixed(1), padL - 8, sy);
        }
        
        ctx.save();
        ctx.translate(16, padT + h / 2);
        ctx.rotate(-Math.PI / 2);
        ctx.textAlign = 'center';
        ctx.fillText('信号 (pA)', 0, 0);
        ctx.restore();
        
        // Draw Curve
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
        } else {
            ctx.fillStyle = '#94a3b8';
            ctx.font = '14px system-ui';
            ctx.textAlign = 'center';
            ctx.textBaseline = 'middle';
            ctx.fillText('等待色谱仪下发波形数据 (设备可能正在升温或就绪中)...', padL + w / 2, padT + h / 2);
        }

        // Draw peak labels
          if (latestPollutants && dataPoints.length > 0) {
              latestPollutants.forEach((p, idx) => {
                  if (p.status === 'calculated') return; // Do not draw calculated peaks like NMHC on graph
                  
                  let xMin = p.retain_time;
                if (xMin === undefined && p.rtS !== undefined) xMin = p.rtS / 60.0;
                if (xMin === undefined) return;
                
                if (xMin >= xBegMin && xMin <= xEndMin) {
                    const x = padL + ((xMin - xBegMin) / xSpanMin) * w;
                    
                    // Find closest point to get Y
                    let closestY = padT + h;
                    let minDist = Infinity;
                    for(let i = 0; i < dataPoints.length; i++) {
                        const dist = Math.abs(dataPoints[i][0]/60 - xMin);
                        if (dist < minDist) {
                            minDist = dist;
                            const yn = (dataPoints[i][1] - yBeg) / (yEnd - yBeg);
                            closestY = padT + (1 - yn) * h;
                        }
                    }

                    // Vertical dashed line
                    ctx.strokeStyle = '#10b981';
                    ctx.lineWidth = 1;
                    ctx.setLineDash([4, 4]);
                    ctx.beginPath();
                    const boxY = padT + 10 + (idx % 3) * 30;
                    ctx.moveTo(x, boxY + 20);
                    ctx.lineTo(x, closestY);
                    ctx.stroke();
                    ctx.setLineDash([]);

                    // Label Box
                    const text = `${p.code || p.name}: ${p.amount ? p.amount.toFixed(2) : '0.00'}`;
                    ctx.font = '12px system-ui';
                    const textW = ctx.measureText(text).width;
                    
                    ctx.fillStyle = 'rgba(15, 23, 42, 0.8)';
                    ctx.fillRect(x - textW/2 - 4, boxY - 2, textW + 8, 20);
                    
                    ctx.strokeStyle = '#10b981';
                    ctx.strokeRect(x - textW/2 - 4, boxY - 2, textW + 8, 20);

                    ctx.fillStyle = '#10b981';
                    ctx.textAlign = 'center';
                    ctx.textBaseline = 'top';
                    ctx.fillText(text, x, boxY + 2);
                }
            });
        }
    }
    
    // Resize handler using ResizeObserver (handles tab switching properly)
    const resizeObserver = new ResizeObserver(entries => {
        for (let entry of entries) {
            if (entry.contentRect.width > 0 && entry.contentRect.height > 0) {
                canvas.width = entry.contentRect.width;
                canvas.height = entry.contentRect.height;
                draw();
            }
        }
    });
    resizeObserver.observe(canvas.parentElement);

    // WebSocket/SSE integration
    const evtSource = new EventSource('/events');
    evtSource.onmessage = function(event) {
        try {
            const parsed = JSON.parse(event.data);

            // Auto restore session for the first seen device to recover history of the current cycle
            if (parsed.deviceId && !sessionRestored) {
                sessionRestored = true;
                const fetchTime = Date.now();
                fetch('/api/v1/session/active?deviceId=' + encodeURIComponent(parsed.deviceId) + '&channel=0')
                    .then(r => r.json())
                    .then(sess => {
                        if (lastCycleResetTime > fetchTime) {
                            // A new cycle started while we were fetching! Ignore the fetched data.
                            return;
                        }
                        if (sess && sess.values && sess.dtS) {
                            const dtS = sess.dtS;
                            const restored = [];
                            // 这里要考虑到如果后端存的不是全量数组而是一部分，我们需要加上正确的偏移
                            // 但是 /api/v1/session/active 接口返回的 values 就是从 0 开始的整个周期的快照
                            for (let i = 0; i < sess.values.length; i++) {
                                restored.push([i * dtS, sess.values[i]]);
                            }
                            // Merge and deduplicate by time
                            const uniqueMap = new Map();
                            for (const p of restored.concat(dataPoints)) {
                                uniqueMap.set(p[0].toFixed(3), p);
                            }
                            dataPoints = Array.from(uniqueMap.values()).sort((a, b) => a[0] - b[0]);
                            
                            // 恢复上一组的实时结果
                            if (sess.result) {
                                latestPollutants = sess.result.pollutants;
                                updateLiveResultsTable(sess.result);
                            }
                            
                            if (dataPoints.length > 0) {
                                const lastPoint = dataPoints[dataPoints.length - 1];
                                const timeEl = document.getElementById('live-current-time');
                                const sigEl = document.getElementById('live-current-signal');
                                if (timeEl) timeEl.innerText = (lastPoint[0] / 60.0).toFixed(3);
                                if (sigEl) sigEl.innerText = lastPoint[1].toFixed(3);
                            }
                            
                            requestAnimationFrame(draw);
                        }
                    }).catch(e => {
                        console.error('Session restore failed:', e);
                        sessionRestored = false; // allow retry
                    });
            }

            if (parsed.type === 'samples' && parsed.values) {
                const baseT = parsed.t0S || 0;
                const dtS = parsed.dtS || 0.05;

                if (baseT === 0 || dataPoints.length > 50000) {
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
            } else if (parsed.type === 'result') {
                // Update live results table
                if (parsed.result && parsed.result.pollutants) {
                    latestPollutants = parsed.result.pollutants;
                    requestAnimationFrame(draw);
                }
                updateLiveResultsTable(parsed.result);
            } else if (parsed.type === 'telemetry') {
                // Update hardware states if available
                if (parsed.tempInj1 !== undefined) {
                    const elInj1 = document.getElementById('val-inj1');
                    const elCol = document.getElementById('val-col');
                    const elDet1 = document.getElementById('val-det1');
                    const elValve = document.getElementById('val-valve');
                    
                    if (elInj1) elInj1.innerText = (parsed.tempInj1 || 0).toFixed(1);
                    if (elCol) elCol.innerText = (parsed.tempCol || 0).toFixed(1);
                    if (elDet1) elDet1.innerText = (parsed.tempDet1 || 0).toFixed(1);
                    if (elValve) elValve.innerText = (parsed.tempInj2 || 0).toFixed(1); // 阀温暂借用 tempInj2
                }
                if (parsed.epc && parsed.epc.length > 0) {
                    const elC1 = document.getElementById('val-carrier1');
                    const elC3 = document.getElementById('val-carrier3');
                    const elSample = document.getElementById('val-sample');
                    const elC2 = document.getElementById('val-carrier2');
                    const elH2_1 = document.getElementById('val-h2-1');
                    const elAir_1 = document.getElementById('val-air-1');
                    
                    if (elC1 && parsed.epc.length > 0) elC1.innerText = (parsed.epc[0].psi || 0).toFixed(2);
                    if (elC3 && parsed.epc.length > 1) elC3.innerText = (parsed.epc[1].psi || 0).toFixed(2);
                    if (elSample && parsed.epc.length > 2) elSample.innerText = (parsed.epc[2].psi || 0).toFixed(2);
                    if (elC2 && parsed.epc.length > 3) elC2.innerText = (parsed.epc[3].psi || 0).toFixed(2);
                    if (elH2_1 && parsed.epc.length > 9) elH2_1.innerText = (parsed.epc[9].psi || 0).toFixed(2);
                    if (elAir_1 && parsed.epc.length > 10) elAir_1.innerText = (parsed.epc[10].psi || 0).toFixed(2);
                }
            }
        } catch (e) {
            console.error('SSE parse error:', e);
        }
    };

    function updateLiveResultsTable(resultObj) {
        const tbody = document.querySelector('#live-results-table tbody');
        if (tbody) {
            let html = '';
            if (resultObj && resultObj.pollutants) {
                        resultObj.pollutants.forEach(p => {
                            html += `<tr><td>${p.code || p.name}</td><td style="color:var(--success); text-align:right;">${p.amount ? p.amount.toFixed(2) : '0.00'}</td></tr>`;
                        });
                    }
                    if (resultObj && resultObj.groups) {
                        resultObj.groups.forEach(g => {
                            html += `<tr><td style="font-weight:bold">${g.code || g.name}</td><td style="font-weight:bold;color:var(--accent); text-align:right;">${g.amount ? g.amount.toFixed(2) : '0.00'}</td></tr>`;
                        });
                    }
            tbody.innerHTML = html || '<tr><td colspan="2" style="text-align:center; color:#94a3b8">暂无组分数据</td></tr>';
        }
    }
    
    draw();
}
