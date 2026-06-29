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
                    <input type="number" id="tcd-set-bridge-val" value="12" class="input" style="width: 80px; margin-right: 0;">
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
    let plotLayout = { padLeft: 80, padRight: 30, padTop: 30, padBottom: 30 };

    const canvas = document.getElementById('tcd-canvas');
    if (canvas) {
        canvas.addEventListener('mousedown', (e) => {
            const rect = canvas.getBoundingClientRect();
            dragStartX = e.clientX - rect.left;
            dragStartY = e.clientY - rect.top;
            
            if (dragStartX >= plotLayout.padLeft && dragStartX <= canvas.width - plotLayout.padRight &&
                dragStartY >= plotLayout.padTop && dragStartY <= canvas.height - plotLayout.padBottom) {
                isDragging = true;
                dragCurrentX = dragStartX;
                dragCurrentY = dragStartY;
            }
        });

        canvas.addEventListener('mousemove', (e) => {
            if (isDragging) {
                const rect = canvas.getBoundingClientRect();
                dragCurrentX = e.clientX - rect.left;
                dragCurrentY = e.clientY - rect.top;
                // requestAnimationFrame will handle drawing
            }
        });

        canvas.addEventListener('mouseup', (e) => {
            if (isDragging) {
                isDragging = false;
                const rect = canvas.getBoundingClientRect();
                dragCurrentX = e.clientX - rect.left;
                dragCurrentY = e.clientY - rect.top;

                if (Math.abs(dragCurrentX - dragStartX) > 10 && Math.abs(dragCurrentY - dragStartY) > 10 && tcdDataPoints.length > 0) {
                    let currentMinIdx = 0, currentMaxIdx = tcdDataPoints.length - 1;
                    let currentMinY = Infinity, currentMaxY = -Infinity;

                    if (zoomState) {
                        currentMinIdx = zoomState.minIdx;
                        currentMaxIdx = zoomState.maxIdx;
                        currentMinY = zoomState.minY;
                        currentMaxY = zoomState.maxY;
                    } else {
                        for(let v of tcdDataPoints) {
                            if(v < currentMinY) currentMinY = v;
                            if(v > currentMaxY) currentMaxY = v;
                        }
                        currentMinY = Math.min(currentMinY, 0);
                        currentMaxY = Math.max(currentMaxY, 0);
                        if(currentMinY === currentMaxY) { currentMinY -= 10; currentMaxY += 10; }
                        const spanY = currentMaxY - currentMinY;
                        currentMinY -= spanY * 0.2;
                        currentMaxY += spanY * 0.2;
                    }

                    let px1 = Math.max(plotLayout.padLeft, Math.min(dragStartX, dragCurrentX));
                    let px2 = Math.min(canvas.width - plotLayout.padRight, Math.max(dragStartX, dragCurrentX));
                    let py1 = Math.max(plotLayout.padTop, Math.min(dragStartY, dragCurrentY));
                    let py2 = Math.min(canvas.height - plotLayout.padBottom, Math.max(dragStartY, dragCurrentY));

                    const plotW = canvas.width - plotLayout.padLeft - plotLayout.padRight;
                    const plotH = canvas.height - plotLayout.padTop - plotLayout.padBottom;

                    if (plotW > 0 && plotH > 0) {
                        const newMinIdx = currentMinIdx + ((px1 - plotLayout.padLeft) / plotW) * (currentMaxIdx - currentMinIdx);
                        const newMaxIdx = currentMinIdx + ((px2 - plotLayout.padLeft) / plotW) * (currentMaxIdx - currentMinIdx);
                        
                        const newMaxY = currentMaxY - ((py1 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);
                        const newMinY = currentMaxY - ((py2 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);

                        zoomState = {
                            minIdx: Math.max(0, Math.floor(newMinIdx)),
                            maxIdx: Math.min(tcdDataPoints.length - 1, Math.ceil(newMaxIdx)),
                            minY: newMinY,
                            maxY: newMaxY
                        };
                    }
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
                    const overLimit = tcdDataPoints.length - 4800;
                    tcdDataPoints = tcdDataPoints.slice(overLimit);
                    if (zoomState) {
                        zoomState.minIdx -= overLimit;
                        zoomState.maxIdx -= overLimit;
                        if (zoomState.maxIdx < 0) {
                            zoomState = null;
                        } else {
                            if (zoomState.minIdx < 0) zoomState.minIdx = 0;
                        }
                    }
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

        ctx.font = '12px monospace';
        const wMax = ctx.measureText(max.toFixed(1)).width;
        const wMin = ctx.measureText(min.toFixed(1)).width;
        plotLayout.padLeft = Math.max(wMax, wMin) + 20;

        const padLeft = plotLayout.padLeft;
        const padRight = plotLayout.padRight;
        const padBottom = plotLayout.padBottom;
        const padTop = plotLayout.padTop;

        const plotW = canvas.width - padLeft - padRight;
        const plotH = canvas.height - padTop - padBottom;

        if (plotW <= 0 || plotH <= 0) return;

        // --- Draw Y-axis grid and ticks ---
        ctx.fillStyle = '#94a3b8';
        ctx.textAlign = 'right';
        ctx.textBaseline = 'middle';
        
        ctx.strokeStyle = '#1e293b';
        ctx.lineWidth = 1;
        ctx.beginPath();
        for(let i=0; i<=10; i++) {
            const y = padTop + (i/10) * plotH;
            const val = max - (i/10) * (max - min);
            
            ctx.moveTo(padLeft, y);
            ctx.lineTo(canvas.width - padRight, y);
            ctx.fillText(val.toFixed(1), padLeft - 10, y);
        }
        ctx.stroke();

        // Draw "mV" unit label
        ctx.textAlign = 'left';
        ctx.textBaseline = 'bottom';
        ctx.fillText('mV', 10, padTop - 5);

        // --- Draw X-axis grid and ticks ---
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        ctx.beginPath();
        for(let i=0; i<=10; i++) {
            const x = padLeft + (i/10) * plotW;
            const idx = startIdx + (i/10) * (endIdx - startIdx);
            const timeSec = idx * 0.5; // assuming 0.5s per point
            
            ctx.moveTo(x, padTop);
            ctx.lineTo(x, canvas.height - padBottom);
            ctx.fillText(timeSec.toFixed(0) + 's', x, canvas.height - padBottom + 10);
        }
        ctx.stroke();

        // Draw 0 baseline
        const zeroY = padTop + plotH - ((0 - min) / (max - min)) * plotH;
        if (zeroY >= padTop && zeroY <= padTop + plotH) {
            ctx.strokeStyle = '#64748b'; 
            ctx.setLineDash([5, 5]);
            ctx.lineWidth = 1.5;
            ctx.beginPath();
            ctx.moveTo(padLeft, zeroY);
            ctx.lineTo(canvas.width - padRight, zeroY);
            ctx.stroke();
            ctx.setLineDash([]); 
        }

        // Draw data curve
        ctx.strokeStyle = '#38bdf8'; 
        ctx.lineWidth = 2;
        ctx.beginPath();
        for(let i = startIdx; i <= endIdx; i++) {
            const x = padLeft + ((i - startIdx) / (endIdx - startIdx || 1)) * plotW;
            const y = padTop + plotH - ((tcdDataPoints[i] - min) / (max - min)) * plotH;
            
            // Clip line to plot area visually
            if(y < padTop) y = padTop;
            if(y > padTop + plotH) y = padTop + plotH;

            if(i === startIdx) ctx.moveTo(x, y);
            else ctx.lineTo(x, y);
        }
        ctx.stroke();

        // Draw drag box
        if (isDragging) {
            ctx.fillStyle = 'rgba(56, 189, 248, 0.2)';
            ctx.strokeStyle = '#38bdf8';
            ctx.lineWidth = 1;
            
            // Constrain drag box to plot area
            let dx1 = Math.max(padLeft, Math.min(canvas.width - padRight, dragStartX));
            let dx2 = Math.max(padLeft, Math.min(canvas.width - padRight, dragCurrentX));
            let dy1 = Math.max(padTop, Math.min(canvas.height - padBottom, dragStartY));
            let dy2 = Math.max(padTop, Math.min(canvas.height - padBottom, dragCurrentY));

            const w = dx2 - dx1;
            const h = dy2 - dy1;
            ctx.fillRect(dx1, dy1, w, h);
            ctx.strokeRect(dx1, dy1, w, h);
            
            // Show calculation
            ctx.fillStyle = '#fff';
            ctx.textAlign = 'left';
            const dx = Math.abs(dx2 - dx1);
            const dy = Math.abs(dy2 - dy1);
            const valSpan = max - min;
            const timeSpan = (endIdx - startIdx) * 0.5;
            const dVal = (dy / plotH) * valSpan;
            const dTime = (dx / plotW) * timeSpan;
            
            ctx.fillText(`ΔX: ${dTime.toFixed(1)}s, ΔY: ${dVal.toFixed(2)}mV`, Math.max(padLeft + 5, Math.min(dx1, dx2)), Math.max(padTop + 15, Math.min(dy1, dy2) - 5));
        }
        
        // Indicate zoom state
        if (zoomState) {
            ctx.fillStyle = '#facc15';
            ctx.textAlign = 'right';
            ctx.fillText('🔍 已放大 (双击还原)', canvas.width - 10, 20);
        }
    }
}
