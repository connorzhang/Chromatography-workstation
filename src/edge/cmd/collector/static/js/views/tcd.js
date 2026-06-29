export function initTCD() {
    const container = document.getElementById('view-tcd');
    if (!container) return;

    container.innerHTML = `
        <div class="card" style="margin-bottom: 20px; text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">TCD 放大器测试</h3>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <span style="color: #94a3b8;">状态:</span>
                    <span id="tcd-status" style="font-weight: bold; color: #94a3b8;">未连接</span>
                </div>
            </div>

            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap; padding-top: 15px; border-top: 1px dashed #334155;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">桥流 (0-127):</label>
                    <input type="number" id="tcd-set-bridge-val" value="80" class="input" style="width: 80px; margin-right: 0;">
                </div>
                <button class="btn" id="btn-tcd-set-bridge">设置桥流</button>
                <button class="btn btn-danger" id="btn-tcd-zeroing">设备调零</button>
                <div style="margin-left: auto; display: flex; align-items: center; gap: 15px; background: rgba(0,0,0,0.2); padding: 5px 15px; border-radius: 6px; border: 1px solid #334155;">
                    <div style="display: flex; flex-direction: column; align-items: flex-end; gap: 2px;">
                        <div style="font-size: 12px; color: #94a3b8;" title="最近2分钟内的最大值减去最小值 (浮动差)">2分钟基线噪声(Noise): <span id="tcd-stat-noise" style="color: #facc15; font-weight: bold;">--</span></div>
                        <div style="font-size: 12px; color: #94a3b8;" title="浮动差与基线均值的比值 (百分比)">基线漂移度(Noise/Mean): <span id="tcd-stat-drift" style="color: #38bdf8; font-weight: bold;">--</span></div>
                    </div>
                    <div style="height: 30px; width: 1px; background: #334155; margin: 0 5px;"></div>
                    <div style="display: flex; align-items: center; gap: 8px;">
                        <span style="color: #94a3b8;">当前桥流:</span>
                        <span id="tcd-current-bridge" style="font-weight: bold; color: var(--text); font-size: 16px;">--</span>
                    </div>
                </div>
            </div>

            <div style="display: flex; gap: 20px; margin-top: 20px; height: 350px;">
                <div style="flex: 1; border: 1px solid #334155; border-radius: 6px; position: relative; background: #0f172a;">
                    <canvas id="tcd-canvas" style="position: absolute; top:0; left:0; width:100%; height:100%;"></canvas>
                </div>
                <div style="flex: 0 0 220px; border: 1px solid #334155; border-radius: 6px; background: #0f172a; padding: 10px; overflow-y: auto;">
                    <h4 style="margin-top: 0; color: #94a3b8; font-size: 13px; text-align: center; border-bottom: 1px solid #334155; padding-bottom: 5px;">20组实时数据</h4>
                    <div id="tcd-values-list" style="display: grid; grid-template-columns: 1fr 1fr; gap: 4px; font-size: 12px; font-family: monospace;">
                        <!-- data goes here -->
                    </div>
                </div>
            </div>
        </div>
    `;

    let tcdPollInterval = null;
    let tcdDataPoints = []; // sliding window

    // Zoom and Pan states
    let zoomState = null; // { minIdx, maxIdx, minY, maxY }
    let isDragging = false;
    let dragStartX = 0;
    let dragStartY = 0;
    let dragCurrentX = 0;
    let dragCurrentY = 0;

    const canvas = document.getElementById('tcd-canvas');
    if (canvas) {
        canvas.addEventListener('mousedown', (e) => {
            const rect = canvas.getBoundingClientRect();
            dragStartX = e.clientX - rect.left;
            dragStartY = e.clientY - rect.top;
            isDragging = true;
        });

        canvas.addEventListener('mousemove', (e) => {
            if (isDragging) {
                const rect = canvas.getBoundingClientRect();
                dragCurrentX = e.clientX - rect.left;
                dragCurrentY = e.clientY - rect.top;
                // Avoid drawing the box here directly to prevent flickering, let requestAnimationFrame handle it
            }
        });

        canvas.addEventListener('mouseup', (e) => {
            if (isDragging) {
                isDragging = false;
                const rect = canvas.getBoundingClientRect();
                dragCurrentX = e.clientX - rect.left;
                dragCurrentY = e.clientY - rect.top;

                if (Math.abs(dragCurrentX - dragStartX) > 10 && tcdDataPoints.length > 0) {
                    // Apply zoom
                    let min = Infinity, max = -Infinity;
                    for(let v of tcdDataPoints) {
                        if(v < min) min = v;
                        if(v > max) max = v;
                    }
                    min = Math.min(min, 0);
                    max = Math.max(max, 0);
                    if(min === max) { min -= 10; max += 10; }
                    const spanY = max - min;
                    min -= spanY * 0.2;
                    max += spanY * 0.2;

                    let px1 = Math.min(dragStartX, dragCurrentX);
                    let px2 = Math.max(dragStartX, dragCurrentX);
                    let py1 = Math.min(dragStartY, dragCurrentY);
                    let py2 = Math.max(dragStartY, dragCurrentY);

                    // Map pixels to data indices and values
                    const len = tcdDataPoints.length - 1 || 1;
                    const zMinIdx = Math.floor((px1 / canvas.width) * len);
                    const zMaxIdx = Math.ceil((px2 / canvas.width) * len);

                    let zMaxY = max - (py1 / canvas.height) * (max - min);
                    let zMinY = max - (py2 / canvas.height) * (max - min);

                    zoomState = {
                        minIdx: Math.max(0, zMinIdx),
                        maxIdx: Math.min(tcdDataPoints.length - 1, zMaxIdx),
                        minY: zMinY,
                        maxY: zMaxY
                    };
                }
            }
        });

        canvas.addEventListener('dblclick', () => {
            zoomState = null;
        });
    }

    // 自动开始轮询状态
    tcdPollInterval = setInterval(pollTCDState, 500);

    document.getElementById('btn-tcd-set-bridge').addEventListener('click', async () => {
        const val = parseInt(document.getElementById('tcd-set-bridge-val').value);
        try {
            const res = await fetch('/api/v1/tcd/set_bridge', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ value: val })
            });
            if (res.ok) {
                window.showToast('设置桥流指令已下发');
            } else {
                const data = await res.json();
                window.showToast('设置失败: ' + data.error, true);
            }
        } catch (e) {
            window.showToast('请求异常', true);
        }
    });

    document.getElementById('btn-tcd-zeroing').addEventListener('click', async () => {
        try {
            const res = await fetch('/api/v1/tcd/zeroing', { method: 'POST' });
            if (res.ok) {
                window.showToast('调零指令已下发');
            } else {
                const data = await res.json();
                window.showToast('调零失败: ' + data.error, true);
            }
        } catch (e) {}
    });

    async function pollTCDState() {
        try {
            const res = await fetch('/api/v1/tcd/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('tcd-status').innerText = '连接已断开';
                    document.getElementById('tcd-status').style.color = 'var(--danger)';
                    return;
                }
                document.getElementById('tcd-status').innerText = '已连接 (通信中)';
                document.getElementById('tcd-status').style.color = 'var(--success)';
                document.getElementById('tcd-current-bridge').innerText = data.bridge_current;

                let html = '';
                for (let i = 0; i < 20; i++) {
                    const color = data.values[i] >= 0 ? '#38bdf8' : '#ef4444';
                    html += `<div><span style="color:#94a3b8">CH${(i+1).toString().padStart(2,'0')}</span> <span style="color:${color}">${data.values[i]}</span></div>`;
                }
                document.getElementById('tcd-values-list').innerHTML = html;

                for(let i=0; i<20; i++) {
                    tcdDataPoints.push(data.values[i]);
                }
                // Keep 2 minutes of data: 120s / 0.5s = 240 polls * 20 points = 4800 points
                if(tcdDataPoints.length > 4800) {
                    tcdDataPoints = tcdDataPoints.slice(tcdDataPoints.length - 4800);
                }
                
                // Calculate Baseline Noise & Drift
                if (tcdDataPoints.length > 0) {
                    let minVal = Infinity, maxVal = -Infinity;
                    let sum = 0;
                    for (let v of tcdDataPoints) {
                        if (v < minVal) minVal = v;
                        if (v > maxVal) maxVal = v;
                        sum += v;
                    }
                    const noise = maxVal - minVal;
                    const mean = sum / tcdDataPoints.length;
                    
                    document.getElementById('tcd-stat-noise').innerText = noise.toFixed(2);
                    if (mean === 0) {
                        document.getElementById('tcd-stat-drift').innerText = '0.0000';
                    } else {
                        const driftRatio = noise / Math.abs(mean);
                        document.getElementById('tcd-stat-drift').innerText = driftRatio.toFixed(4);
                    }
                }

                requestAnimationFrame(drawTCDCanvas);
            }
        } catch (e) {}
    }

    function drawTCDCanvas() {
        const canvas = document.getElementById('tcd-canvas');
        if(!canvas) return;
        const rect = canvas.parentElement.getBoundingClientRect();
        if (rect.width === 0 || rect.height === 0) return;

        if (canvas.width !== rect.width || canvas.height !== rect.height) {
            canvas.width = rect.width;
            canvas.height = rect.height;
        }

        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        if(tcdDataPoints.length === 0) return;

        let min = Infinity, max = -Infinity;
        let startIdx = 0, endIdx = tcdDataPoints.length - 1;

        if (zoomState) {
            startIdx = zoomState.minIdx;
            endIdx = zoomState.maxIdx;
            min = zoomState.minY;
            max = zoomState.maxY;
            
            // Adjust bounds if data shifted significantly
            if (endIdx >= tcdDataPoints.length) {
                const shift = endIdx - (tcdDataPoints.length - 1);
                startIdx = Math.max(0, startIdx - shift);
                endIdx = tcdDataPoints.length - 1;
            }
        } else {
            for(let v of tcdDataPoints) {
                if(v < min) min = v;
                if(v > max) max = v;
            }
            min = Math.min(min, 0);
            max = Math.max(max, 0);
            if(min === max) { min -= 10; max += 10; }
            const span = max - min;
            min -= span * 0.2;
            max += span * 0.2;
        }

        // --- Draw Y-axis grid and ticks ---
        ctx.fillStyle = '#94a3b8';
        ctx.font = '12px monospace'; // 增大字体，提高可读性
        ctx.textAlign = 'right';
        ctx.textBaseline = 'middle';
        
        ctx.strokeStyle = '#1e293b';
        ctx.lineWidth = 1;
        ctx.beginPath();
        for(let i=1; i<=10; i++) {
            const y = (i/10) * canvas.height;
            const val = max - (i/10) * (max - min);
            
            ctx.moveTo(50, y); // 增加左侧留白给大字体
            ctx.lineTo(canvas.width, y);
            ctx.fillText(val.toFixed(1) + ' mV', 45, y); // 增加留白
        }
        ctx.stroke();

        // --- Draw X-axis grid and ticks ---
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        ctx.beginPath();
        for(let i=1; i<=10; i++) {
            const x = 50 + (i/10) * (canvas.width - 50); // 配合左侧留白
            const idx = startIdx + (i/10) * (endIdx - startIdx);
            const timeSec = idx * 0.5; // assuming 0.5s per point
            
            ctx.moveTo(x, 0);
            ctx.lineTo(x, canvas.height - 20);
            ctx.fillText(timeSec.toFixed(0) + 's', x, canvas.height - 15);
        }
        ctx.stroke();

        // Draw 0 baseline
        const zeroY = canvas.height - 20 - ((0 - min) / (max - min)) * (canvas.height - 20);
        if (zeroY >= 0 && zeroY <= canvas.height - 20) {
            ctx.strokeStyle = '#64748b'; 
            ctx.setLineDash([5, 5]);
            ctx.lineWidth = 1.5;
            ctx.beginPath();
            ctx.moveTo(50, zeroY); // 配合左侧留白
            ctx.lineTo(canvas.width, zeroY);
            ctx.stroke();
            ctx.setLineDash([]); 
        }

        // Draw data curve
        ctx.strokeStyle = '#38bdf8'; 
        ctx.lineWidth = 2;
        ctx.beginPath();
        const plotW = canvas.width - 50; // 配合左侧留白
        const plotH = canvas.height - 20;
        for(let i = startIdx; i <= endIdx; i++) {
            const x = 50 + ((i - startIdx) / (endIdx - startIdx || 1)) * plotW;
            const y = plotH - ((tcdDataPoints[i] - min) / (max - min)) * plotH;
            if(i === startIdx) ctx.moveTo(x, y);
            else ctx.lineTo(x, y);
        }
        ctx.stroke();

        // Draw drag box
        if (isDragging) {
            ctx.fillStyle = 'rgba(56, 189, 248, 0.2)';
            ctx.strokeStyle = '#38bdf8';
            ctx.lineWidth = 1;
            const w = dragCurrentX - dragStartX;
            const h = dragCurrentY - dragStartY;
            ctx.fillRect(dragStartX, dragStartY, w, h);
            ctx.strokeRect(dragStartX, dragStartY, w, h);
            
            // Show calculation
            ctx.fillStyle = '#fff';
            ctx.textAlign = 'left';
            const dx = Math.abs(dragCurrentX - dragStartX);
            const dy = Math.abs(dragCurrentY - dragStartY);
            const valSpan = max - min;
            const timeSpan = (endIdx - startIdx) * 0.5;
            const dVal = (dy / plotH) * valSpan;
            const dTime = (dx / plotW) * timeSpan;
            
            ctx.fillText(`ΔX: ${dTime.toFixed(1)}s, ΔY: ${dVal.toFixed(2)}mV`, Math.max(45, Math.min(dragStartX, dragCurrentX)), Math.max(15, Math.min(dragStartY, dragCurrentY) - 5));
        }
        
        // Indicate zoom state
        if (zoomState) {
            ctx.fillStyle = '#facc15';
            ctx.textAlign = 'right';
            ctx.fillText('🔍 已放大 (双击还原)', canvas.width - 10, 20);
        }
    }
}
