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
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 15px; background: rgba(0,0,0,0.2); padding: 5px 12px; border-radius: 6px; border: 1px solid #334155;">
                    <span style="color: #94a3b8; font-size: 12px;">电压:</span>
                    <span id="tcd-voltage" style="color: #facc15; font-weight: bold; font-size: 13px; font-family: monospace;">-- V</span>
                    <span style="color: #475569;">|</span>
                    <span style="color: #94a3b8; font-size: 12px;">阻值:</span>
                    <span id="tcd-resistance" style="color: #38bdf8; font-weight: bold; font-size: 13px; font-family: monospace;">-- Ω</span>
                    <span style="color: #475569;">|</span>
                    <span style="color: #94a3b8; font-size: 12px;">温度:</span>
                    <span id="tcd-filament-temp" style="color: #ef4444; font-weight: bold; font-size: 13px; font-family: monospace;">-- ℃</span>
                </div>
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
                <div style="flex: 1; display: flex; flex-direction: column;">
                    <div style="display: flex; gap: 10px; align-items: center; padding: 8px; background: rgba(0,0,0,0.15); border-radius: 4px; margin-bottom: 5px;">
                        <label style="color: #94a3b8; font-size: 12px;">
                            <input type="checkbox" id="tcd-auto-scale" checked> 自适应
                        </label>
                        <span style="color: #94a3b8; font-size: 12px;">Y上限:</span>
                        <input type="number" id="tcd-y-max" class="input" style="width: 70px; font-size: 12px;" step="0.01">
                        <span style="color: #94a3b8; font-size: 12px;">Y下限:</span>
                        <input type="number" id="tcd-y-min" class="input" style="width: 70px; font-size: 12px;" step="0.01">
                        <span style="color: #94a3b8; font-size: 12px;">满屏(秒):</span>
                        <input type="number" id="tcd-full-screen-sec" class="input" style="width: 60px; font-size: 12px;" value="120">
                        <span style="color: #94a3b8; font-size: 12px;">拖放:</span>
                        <select id="tcd-drag-mode" class="input" style="width: 60px; font-size: 12px;">
                            <option value="y">仅Y轴</option>
                            <option value="xy">XY轴</option>
                            <option value="none">禁用</option>
                        </select>
                        <span style="color: #64748b; font-size: 11px; margin-left: auto;">双击重置 | 滚轮缩放 | 拖放选区放大</span>
                    </div>
                    <div style="flex: 1; border: 1px solid #334155; border-radius: 6px; position: relative; background: #0f172a;">
                        <canvas id="tcd-canvas" style="position: absolute; top:0; left:0; width:100%; height:100%;"></canvas>
                    </div>
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

    // 最大存储点数：4分钟数据，0.5秒一个点 = 480
    const maxPoints = 480;

    // Savitzky-Golay 滤波系数（窗口5，2阶多项式）
    const SG_COEFFS = [-0.08571429, 0.34285714, 0.48571429, 0.34285714, -0.08571429];

    // 交互状态
    let zoomState = null; // { minIdx, maxIdx, minY, maxY } 用户拖放选区放大后的状态
    let isDragging = false;
    let dragStartX = 0;
    let dragStartY = 0;
    let dragCurrentX = 0;
    let dragCurrentY = 0;
    let plotLayout = { padLeft: 80, padRight: 30, padTop: 30, padBottom: 30 };

    const canvas = document.getElementById('tcd-canvas');

    // 控件引用
    const autoScaleChk = document.getElementById('tcd-auto-scale');
    const yMaxInput = document.getElementById('tcd-y-max');
    const yMinInput = document.getElementById('tcd-y-min');
    const fullScreenSecInput = document.getElementById('tcd-full-screen-sec');
    const dragModeSelect = document.getElementById('tcd-drag-mode');

    // 实时输入响应：输入变化立即重绘
    function onControlChange() {
        // 手动模式下，如果用户清空了输入，不重绘（避免 NaN）
        requestAnimationFrame(drawTCDCanvas);
    }
    autoScaleChk.addEventListener('change', onControlChange);
    yMaxInput.addEventListener('input', onControlChange);
    yMinInput.addEventListener('input', onControlChange);
    fullScreenSecInput.addEventListener('input', onControlChange);
    dragModeSelect.addEventListener('change', onControlChange);

    if (canvas) {
        canvas.addEventListener('mousedown', (e) => {
            const dragMode = dragModeSelect.value;
            if (dragMode === 'none') return;
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
                requestAnimationFrame(drawTCDCanvas);
            }
        });

        canvas.addEventListener('mouseup', (e) => {
            if (isDragging) {
                isDragging = false;
                const rect = canvas.getBoundingClientRect();
                dragCurrentX = e.clientX - rect.left;
                dragCurrentY = e.clientY - rect.top;

                const dragMode = dragModeSelect.value;
                if (dragMode === 'none') return;

                // 根据拖放模式判断是否需要X方向变化
                const requireX = (dragMode === 'xy');
                const requireY = true; // y 和 xy 都需要Y方向

                const dxAbs = Math.abs(dragCurrentX - dragStartX);
                const dyAbs = Math.abs(dragCurrentY - dragStartY);

                // 判断是否构成有效选区
                const xValid = requireX ? dxAbs > 10 : true;
                const yValid = requireY ? dyAbs > 10 : true;
                if (!(xValid && yValid) || tcdDataPoints.length === 0) return;

                // 获取当前可视范围
                const view = computeView();
                if (!view) return;
                let currentMinIdx = view.startIdx;
                let currentMaxIdx = view.endIdx;
                let currentMinY = view.minY;
                let currentMaxY = view.maxY;

                let px1 = Math.max(plotLayout.padLeft, Math.min(dragStartX, dragCurrentX));
                let px2 = Math.min(canvas.width - plotLayout.padRight, Math.max(dragStartX, dragCurrentX));
                let py1 = Math.max(plotLayout.padTop, Math.min(dragStartY, dragCurrentY));
                let py2 = Math.min(canvas.height - plotLayout.padBottom, Math.max(dragStartY, dragCurrentY));

                const plotW = canvas.width - plotLayout.padLeft - plotLayout.padRight;
                const plotH = canvas.height - plotLayout.padTop - plotLayout.padBottom;
                if (plotW <= 0 || plotH <= 0) return;

                const newZoom = {};
                if (requireX) {
                    const newMinIdx = currentMinIdx + ((px1 - plotLayout.padLeft) / plotW) * (currentMaxIdx - currentMinIdx);
                    const newMaxIdx = currentMinIdx + ((px2 - plotLayout.padLeft) / plotW) * (currentMaxIdx - currentMinIdx);
                    newZoom.minIdx = Math.max(0, Math.floor(newMinIdx));
                    newZoom.maxIdx = Math.min(tcdDataPoints.length - 1, Math.ceil(newMaxIdx));
                } else {
                    // Y模式：X轴保持原样
                    newZoom.minIdx = Math.floor(currentMinIdx);
                    newZoom.maxIdx = Math.ceil(currentMaxIdx);
                }

                const newMaxY = currentMaxY - ((py1 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);
                const newMinY = currentMaxY - ((py2 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);
                newZoom.minY = newMinY;
                newZoom.maxY = newMaxY;

                zoomState = newZoom;
                requestAnimationFrame(drawTCDCanvas);
            }
        });

        canvas.addEventListener('dblclick', () => {
            zoomState = null;
            requestAnimationFrame(drawTCDCanvas);
        });

        canvas.addEventListener('wheel', (e) => {
            e.preventDefault();
            const dragMode = dragModeSelect.value;
            if (dragMode === 'none') return;

            const rect = canvas.getBoundingClientRect();
            const mx = e.clientX - rect.left;
            const my = e.clientY - rect.top;
            if (mx < plotLayout.padLeft || mx > canvas.width - plotLayout.padRight ||
                my < plotLayout.padTop || my > canvas.height - plotLayout.padBottom) return;

            const view = computeView();
            if (!view) return;

            // 滚轮缩放Y轴，以鼠标Y位置为中心
            const deltaY = e.deltaY > 0 ? 1.1 : 0.9; // 向上滚放大，向下滚缩小
            const plotH = canvas.height - plotLayout.padTop - plotLayout.padBottom;
            // 鼠标位置对应的Y值
            const mouseVal = view.maxY - ((my - plotLayout.padTop) / plotH) * (view.maxY - view.minY);
            const newSpan = (view.maxY - view.minY) * deltaY;
            const newMinY = mouseVal - (mouseVal - view.minY) * deltaY;
            const newMaxY = mouseVal + (view.maxY - mouseVal) * deltaY;

            let newMinIdx = view.startIdx;
            let newMaxIdx = view.endIdx;

            // 如果是xy模式，也缩放X轴
            if (dragMode === 'xy') {
                const plotW = canvas.width - plotLayout.padLeft - plotLayout.padRight;
                const mouseIdx = view.startIdx + ((mx - plotLayout.padLeft) / plotW) * (view.endIdx - view.startIdx);
                const idxSpan = (view.endIdx - view.startIdx) * deltaY;
                newMinIdx = Math.max(0, Math.floor(mouseIdx - (mouseIdx - view.startIdx) * deltaY));
                newMaxIdx = Math.min(tcdDataPoints.length - 1, Math.ceil(mouseIdx + (view.endIdx - mouseIdx) * deltaY));
            }

            zoomState = {
                minIdx: newMinIdx,
                maxIdx: newMaxIdx,
                minY: newMinY,
                maxY: newMaxY
            };
            requestAnimationFrame(drawTCDCanvas);
        }, { passive: false });
    }

    // Savitzky-Golay 滤波：对数组应用窗口5、2阶多项式平滑
    // 返回与输入等长的平滑后数组；边缘点使用缩减窗口
    function savitzkyGolay(arr) {
        const n = arr.length;
        if (n < 5) {
            // 数据太少，直接返回副本
            return arr.slice();
        }
        const out = new Array(n);
        for (let i = 0; i < n; i++) {
            if (i < 2 || i >= n - 2) {
                // 边缘点：使用缩减窗口（直接复制原值）
                out[i] = arr[i];
            } else {
                out[i] = SG_COEFFS[0] * arr[i-2] + SG_COEFFS[1] * arr[i-1] + SG_COEFFS[2] * arr[i] + SG_COEFFS[3] * arr[i+1] + SG_COEFFS[4] * arr[i+2];
            }
        }
        return out;
    }

    // 计算当前可视范围（索引范围和Y范围）
    function computeView() {
        if (tcdDataPoints.length === 0) return null;
        const total = tcdDataPoints.length;

        let startIdx, endIdx;
        if (zoomState) {
            startIdx = zoomState.minIdx;
            endIdx = zoomState.maxIdx;
        } else {
            // 根据 fullScreenSec 计算可见点数 N = fullScreenSec / 0.5
            const fsSec = parseFloat(fullScreenSecInput.value);
            const N = Math.max(1, Math.floor((isNaN(fsSec) ? 120 : fsSec) / 0.5));
            endIdx = total - 1;
            startIdx = Math.max(0, endIdx - N + 1);
        }

        let minY, maxY;
        if (autoScaleChk.checked) {
            // 自适应：从可见范围内原始数据计算min/max
            minY = Infinity; maxY = -Infinity;
            for (let i = startIdx; i <= endIdx; i++) {
                const v = tcdDataPoints[i];
                if (v < minY) minY = v;
                if (v > maxY) maxY = v;
            }
            if (minY === Infinity || maxY === -Infinity) return null;
            minY = Math.min(minY, 0);
            maxY = Math.max(maxY, 0);
            if (minY === maxY) { minY -= 10; maxY += 10; }
            const span = maxY - minY;
            minY -= span * 0.1;
            maxY += span * 0.1;
        } else {
            // 手动模式
            minY = parseFloat(yMinInput.value);
            maxY = parseFloat(yMaxInput.value);
            if (isNaN(minY)) minY = -100;
            if (isNaN(maxY)) maxY = 100;
            if (minY === maxY) { minY -= 10; maxY += 10; }
        }
        return { startIdx, endIdx, minY, maxY };
    }

    // 自动开始轮询状态
    tcdPollInterval = setInterval(pollTCDState, 500);

    // 电压/阻值/温度轮询 (1秒一次，与TCD数据轮询独立)
    setInterval(pollVoltage, 1000);

    async function pollVoltage() {
        try {
            const res = await fetch('/api/v1/voltage/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('tcd-voltage').innerText = '-- V';
                    document.getElementById('tcd-resistance').innerText = '-- kΩ';
                    document.getElementById('tcd-filament-temp').innerText = '-- ℃';
                    return;
                }
                const voltage = data.voltage; // 浮点电压值 (V)
                document.getElementById('tcd-voltage').innerText = voltage.toFixed(4) + ' V';

                // 获取当前桥流(mA)，用于计算电阻
                const bridgeText = document.getElementById('tcd-current-bridge').innerText;
                const bridgeCurrent = parseFloat(bridgeText);
                if (bridgeCurrent > 0 && voltage > 0) {
                    // R = V / I, 电压V，电流mA → 电阻Ω = (V / mA) * 1000
                    const resistance = (voltage / bridgeCurrent) * 1000; // Ω
                    document.getElementById('tcd-resistance').innerText = resistance.toFixed(2) + ' Ω';

                    // 温度公式: T = 2.5458 * R - 285.5878 (R单位为Ω)
                    const temp = 2.5458 * resistance - 285.5878;
                    document.getElementById('tcd-filament-temp').innerText = temp.toFixed(2) + ' ℃';
                } else {
                    document.getElementById('tcd-resistance').innerText = '-- Ω';
                    document.getElementById('tcd-filament-temp').innerText = '-- ℃';
                }
            }
        } catch (e) {}
    }

    // 加载配置的桥流值
    async function loadTCDBridgeConfig() {
        try {
            const deviceId = window.currentDeviceId || 'GC-MODULAR';
            const res = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
            if (res.ok) {
                const data = await res.json();
                if (data.tcdBridgeCurrent !== undefined && data.tcdBridgeCurrent > 0) {
                    document.getElementById('tcd-set-bridge-val').value = data.tcdBridgeCurrent;
                }
            }
        } catch (e) {
            console.error('Failed to load TCD config', e);
        }
    }
    loadTCDBridgeConfig();

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

                // 只取 CH1 作为主曲线数据（与谱图界面一致的连续画法）
                tcdDataPoints.push(data.values[0]);
                // 最大存储 maxPoints = 480（4分钟数据，0.5秒一个点）
                if(tcdDataPoints.length > maxPoints) {
                    const overLimit = tcdDataPoints.length - maxPoints;
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
                
                // Calculate Baseline Noise & Drift (基于全量窗口数据)
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

        // 计算可视范围
        const view = computeView();
        if (!view) return;
        let startIdx = view.startIdx;
        let endIdx = view.endIdx;
        let min = view.minY;
        let max = view.maxY;

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

        // --- 绘制Y轴网格和刻度（6等分） ---
        ctx.fillStyle = '#94a3b8';
        ctx.textAlign = 'right';
        ctx.textBaseline = 'middle';
        
        ctx.strokeStyle = '#1e293b';
        ctx.lineWidth = 1;
        ctx.beginPath();
        for(let i=0; i<=6; i++) {
            const y = padTop + (i/6) * plotH;
            const val = max - (i/6) * (max - min);
            
            ctx.moveTo(padLeft, y);
            ctx.lineTo(canvas.width - padRight, y);
            ctx.fillText(val.toFixed(1), padLeft - 10, y);
        }
        ctx.stroke();

        // 绘制 "mV" 单位标签
        ctx.textAlign = 'left';
        ctx.textBaseline = 'bottom';
        ctx.fillStyle = '#94a3b8';
        ctx.fillText('mV', 10, padTop - 5);

        // --- 绘制X轴网格和时间刻度 ---
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        ctx.beginPath();
        for(let i=0; i<=10; i++) {
            const x = padLeft + (i/10) * plotW;
            const idx = startIdx + (i/10) * (endIdx - startIdx);
            const timeSec = idx * 0.5; // 每个点代表0.5秒
            
            ctx.moveTo(x, padTop);
            ctx.lineTo(x, canvas.height - padBottom);
            ctx.fillText(timeSec.toFixed(0) + 's', x, canvas.height - padBottom + 10);
        }
        ctx.stroke();

        // 绘制0基线
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

        // --- 对可见数据点应用 Savitzky-Golay 滤波 ---
        const visibleRaw = tcdDataPoints.slice(startIdx, endIdx + 1);
        const visibleSmoothed = savitzkyGolay(visibleRaw);

        // 绘制平滑后的曲线
        ctx.strokeStyle = '#38bdf8'; 
        ctx.lineWidth = 2;
        ctx.lineJoin = 'round';
        ctx.lineCap = 'round';
        ctx.beginPath();
        const visibleCount = visibleSmoothed.length;
        for(let i = 0; i < visibleCount; i++) {
            const x = padLeft + (i / (visibleCount - 1 || 1)) * plotW;
            const val = visibleSmoothed[i];
            let y = padTop + plotH - ((val - min) / (max - min)) * plotH;
            // 视觉裁剪到绘图区
            if(y < padTop) y = padTop;
            if(y > padTop + plotH) y = padTop + plotH;

            if(i === 0) ctx.moveTo(x, y);
            else ctx.lineTo(x, y);
        }
        ctx.stroke();

        // 绘制拖放选区框
        if (isDragging) {
            const dragMode = dragModeSelect.value;
            ctx.fillStyle = 'rgba(56, 189, 248, 0.2)';
            ctx.strokeStyle = '#38bdf8';
            ctx.lineWidth = 1;
            
            let dx1 = Math.max(padLeft, Math.min(canvas.width - padRight, dragStartX));
            let dx2 = Math.max(padLeft, Math.min(canvas.width - padRight, dragCurrentX));
            let dy1 = Math.max(padTop, Math.min(canvas.height - padBottom, dragStartY));
            let dy2 = Math.max(padTop, Math.min(canvas.height - padBottom, dragCurrentY));

            // 根据拖放模式限制选区形状
            if (dragMode === 'y') {
                // 仅Y轴：选区横向铺满整个绘图区
                dx1 = padLeft;
                dx2 = canvas.width - padRight;
            }

            const w = dx2 - dx1;
            const h = dy2 - dy1;
            ctx.fillRect(dx1, dy1, w, h);
            ctx.strokeRect(dx1, dy1, w, h);
            
            // 显示选区数值
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
        
        // 显示已放大状态
        if (zoomState) {
            ctx.fillStyle = '#facc15';
            ctx.textAlign = 'right';
            ctx.textBaseline = 'top';
            ctx.fillText('🔍 已放大 (双击还原)', canvas.width - 10, 20);
        }
    }
}