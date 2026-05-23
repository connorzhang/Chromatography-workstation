export function initLiveChromatogram() {
    const container = document.getElementById('view-live');
    container.innerHTML = `
        <div style="display: flex; height: 100%; gap: 1rem;">
            <div style="flex: 3; background: var(--panel); border-radius: 8px; border: 1px solid #334155; position: relative; display: flex; flex-direction: column;">
                <div style="padding: 10px; border-bottom: 1px solid #334155; display: flex; gap: 10px; align-items: center;">
                    <span style="color: #94a3b8;">进样类型:</span>
                    <label><input type="radio" name="injType" value="normal" checked> 正常</label>
                    <label><input type="radio" name="injType" value="zero"> 零气</label>
                    <label><input type="radio" name="injType" value="span"> 标气</label>
                    
                    <div style="margin-left: auto; display: flex; gap: 10px;">
                        <button class="btn" onclick="window.sendCmd('startAll')">▶ 开始分析</button>
                        <button class="btn btn-danger" onclick="window.sendCmd('stopAll')">⏹ 停止分析</button>
                    </div>
                </div>
                <div style="flex: 1; position: relative;">
                    <canvas id="chromatogram-canvas" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></canvas>
                </div>
            </div>
            <div style="flex: 1; display: flex; flex-direction: column; gap: 1rem;">
                <div class="control-group" style="flex: 1; margin: 0; overflow-y: auto;">
                    <h3 style="margin-top:0">硬件状态</h3>
                    <table>
                        <tr><td>FID1点火</td><td style="color:var(--success)">已点燃</td></tr>
                        <tr><td>进样口温</td><td>120.0 ℃</td></tr>
                        <tr><td>柱温</td><td>80.0 ℃</td></tr>
                        <tr><td>检测器温</td><td>150.0 ℃</td></tr>
                    </table>
                </div>
                <div class="control-group" style="flex: 1; margin: 0; overflow-y: auto;">
                    <h3 style="margin-top:0">实时结果</h3>
                    <table id="live-results-table">
                        <thead>
                            <tr><th>组分</th><th>浓度(mg/m³)</th></tr>
                        </thead>
                        <tbody>
                            <tr><td colspan="2" style="text-align:center; color:#94a3b8">等待分析...</td></tr>
                        </tbody>
                    </table>
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
        let xEndMin = 2; // Default 2 minutes
        if (dataPoints.length > 0) {
            const maxT = dataPoints[dataPoints.length - 1][0];
            if (maxT / 60 > 2) {
                xEndMin = Math.ceil(maxT / 60);
            }
        }
        const xSpanMin = xEndMin - xBegMin;
        
        let yBeg = 0;
        let yEnd = 40;
        
        // Auto Y
        if (dataPoints.length > 0) {
            let yMin = Infinity;
            let yMax = -Infinity;
            for (let i = 0; i < dataPoints.length; i++) {
                const v = dataPoints[i][1];
                if (v < yMin) yMin = v;
                if (v > yMax) yMax = v;
            }
            if (yMin === Infinity) { yMin = 0; yMax = 1; }

            let span = yMax - yMin;
            const minSpan = 0.5;
            if (span < minSpan) {
                const c = (yMin + yMax) / 2;
                yMin = c - minSpan / 2;
                yMax = c + minSpan / 2;
                span = minSpan;
            }
            
            // 下面预留 5%，上面预留 40%，所以数据占 55%
            const V = span / 0.55;
            yBeg = yMin - 0.05 * V;
            yEnd = yMax + 0.40 * V;
        }
        
        // Smooth Y transition
        if (lastMin !== null && lastMax !== null) {
            yBeg = lastMin + (yBeg - lastMin) * 0.2;
            yEnd = lastMax + (yEnd - lastMax) * 0.2;
        }
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
                    const inj = parsed.tempInj1 || 0;
                    const col = parsed.tempCol || 0;
                    const det = parsed.tempDet1 || 0;
                    // Update DOM element if exists
                    const table = document.querySelector('#view-live .control-group table');
                    if (table) {
                        table.innerHTML = `
                            <tr><td>FID1点火</td><td style="color:var(--success)">监控中</td></tr>
                            <tr><td>进样口温</td><td>${inj.toFixed(1)} ℃</td></tr>
                            <tr><td>柱温</td><td>${col.toFixed(1)} ℃</td></tr>
                            <tr><td>检测器温</td><td>${det.toFixed(1)} ℃</td></tr>
                        `;
                    }
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
                    // if (p.status === 'calculated') return; // We actually want to show NMHC in the result table, but not in the graph
                    html += `<tr><td>${p.code || p.name}</td><td style="color:var(--success)">${p.amount ? p.amount.toFixed(2) : '0.00'}</td></tr>`;
                });
            }
            if (resultObj && resultObj.groups) {
                resultObj.groups.forEach(g => {
                    html += `<tr><td style="font-weight:bold">${g.code || g.name}</td><td style="font-weight:bold;color:var(--accent)">${g.amount ? g.amount.toFixed(2) : '0.00'}</td></tr>`;
                });
            }
            tbody.innerHTML = html || '<tr><td colspan="2" style="text-align:center; color:#94a3b8">暂无组分数据</td></tr>';
        }
    }
    
    draw();
}
