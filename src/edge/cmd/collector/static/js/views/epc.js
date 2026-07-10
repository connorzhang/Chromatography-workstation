export function initEPC() {
    const container = document.getElementById('view-epc');
    if (!container) return;

    container.innerHTML = `
        <div class="card" style="margin-bottom: 20px; text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">EPC 调试与数据分析</h3>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <span style="color: #94a3b8;">状态:</span>
                    <span id="epc-status" style="font-weight: bold; color: #94a3b8;">未连接</span>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <span style="color: #94a3b8;">环境温度:</span>
                    <span id="epc-env-temp" style="font-weight: bold; color: #facc15;">-- ℃</span>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <span style="color: #94a3b8;">阀门开度:</span>
                    <span id="epc-valve" style="font-weight: bold; color: #38bdf8;">-- %</span>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <span style="color: #94a3b8;">硬件报警:</span>
                    <span id="epc-hw-status" style="font-weight: bold; color: #ef4444;">--</span>
                </div>
            </div>

            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap; padding-top: 15px; border-top: 1px dashed #334155;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">控制模式:</label>
                    <select id="epc-set-mode" class="input" style="width: 100px;">
                        <option value="0">待机 (关阀)</option>
                        <option value="1">恒压控制</option>
                        <option value="2">恒流控制</option>
                        <option value="3">吹扫 (全开)</option>
                    </select>
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">目标压力:</label>
                    <input type="number" id="epc-set-press" value="0.0000" step="0.0001" class="input" style="width: 80px;">
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">目标流量:</label>
                    <input type="number" id="epc-set-flow" value="0.0000" step="0.0001" class="input" style="width: 80px;">
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">载气类型:</label>
                    <select id="epc-set-gas" class="input" style="width: 80px;">
                        <option value="0">N2 (氮气)</option>
                        <option value="1">He (氦气)</option>
                        <option value="2">H2 (氢气)</option>
                        <option value="3">Ar (氩气)</option>
                        <option value="4">Air (空气)</option>
                    </select>
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">单位:</label>
                    <select id="epc-set-unit" class="input" style="width: 100px;">
                        <option value="0">kPa & mL/min</option>
                        <option value="1">psi & sccm</option>
                        <option value="2">bar & L/min</option>
                    </select>
                </div>
                <button class="btn" id="btn-epc-apply">下发配置</button>
            </div>

            <div style="display: flex; gap: 20px; margin-top: 20px;">
                <div style="flex: 1; display: flex; flex-direction: column; gap: 15px;">
                    <div style="height: 200px; border: 1px solid #334155; border-radius: 6px; position: relative; background: #0f172a;">
                        <div style="position: absolute; top: 10px; right: 10px; color: #94a3b8; font-size: 12px; font-weight: bold; z-index: 10;">压力曲线 (实时: <span id="epc-val-press" style="color: #38bdf8;">0.0000</span>)</div>
                        <canvas id="epc-canvas-press" style="position: absolute; top:0; left:0; width:100%; height:100%;"></canvas>
                    </div>
                    <div style="height: 200px; border: 1px solid #334155; border-radius: 6px; position: relative; background: #0f172a;">
                        <div style="position: absolute; top: 10px; right: 10px; color: #94a3b8; font-size: 12px; font-weight: bold; z-index: 10;">流量曲线 (实时: <span id="epc-val-flow" style="color: #facc15;">0.0000</span>)</div>
                        <canvas id="epc-canvas-flow" style="position: absolute; top:0; left:0; width:100%; height:100%;"></canvas>
                    </div>
                </div>
            </div>
        </div>
    `;

    let epcPollInterval = null;
    let pressDataPoints = []; 
    let flowDataPoints = [];
    const maxPoints = 240; // 2分钟的数据，假设500ms一轮

    // 自动开始轮询状态
    epcPollInterval = setInterval(pollEPCState, 500);

    document.getElementById('btn-epc-apply').addEventListener('click', async () => {
        const mode = parseInt(document.getElementById('epc-set-mode').value);
        const press = parseFloat(document.getElementById('epc-set-press').value);
        const flow = parseFloat(document.getElementById('epc-set-flow').value);
        const gasType = parseInt(document.getElementById('epc-set-gas').value);
        const units = parseInt(document.getElementById('epc-set-unit').value);

        try {
            const res = await fetch('/api/v1/epc/config', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ 
                    mode: mode,
                    pressure: press,
                    flow: flow,
                    gasType: gasType,
                    units: units
                })
            });
            if (res.ok) {
                window.showToast('EPC 配置指令已下发');
            } else {
                const data = await res.json();
                window.showToast('设置失败: ' + data.error, true);
            }
        } catch (e) {
            window.showToast('请求异常', true);
        }
    });

    const statusMap = {
        0: '正常',
        1: '传感器异常',
        2: '压力超限',
        3: '目标未达',
        4: '阀门卡死'
    };

    async function pollEPCState() {
        try {
            const res = await fetch('/api/v1/epc/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('epc-status').innerText = '连接已断开/超时';
                    document.getElementById('epc-status').style.color = 'var(--danger)';
                    return;
                }
                document.getElementById('epc-status').innerText = '已连接 (通信中)';
                document.getElementById('epc-status').style.color = 'var(--success)';

                document.getElementById('epc-env-temp').innerText = (data.temp / 10.0).toFixed(1) + ' ℃';
                document.getElementById('epc-valve').innerText = (data.valve_open / 100.0).toFixed(2) + ' %';
                
                const stText = statusMap[data.status] || ('未知:' + data.status);
                document.getElementById('epc-hw-status').innerText = stText;
                document.getElementById('epc-hw-status').style.color = data.status === 0 ? 'var(--success)' : 'var(--danger)';

                document.getElementById('epc-val-press').innerText = data.real_pressure.toFixed(4);
                document.getElementById('epc-val-flow').innerText = data.real_flow.toFixed(4);

                pressDataPoints.push(data.real_pressure);
                flowDataPoints.push(data.real_flow);

                if(pressDataPoints.length > maxPoints) {
                    pressDataPoints.shift();
                    flowDataPoints.shift();
                }

                requestAnimationFrame(drawEPCCanvas);
            }
        } catch (e) {}
    }

    function drawEPCCanvas() {
        drawCanvas('epc-canvas-press', pressDataPoints, '#38bdf8');
        drawCanvas('epc-canvas-flow', flowDataPoints, '#facc15');
    }

    function drawCanvas(canvasId, dataPoints, color) {
        const canvas = document.getElementById(canvasId);
        if(!canvas) return;
        const rect = canvas.parentElement.getBoundingClientRect();
        if (rect.width === 0 || rect.height === 0) return;

        if (canvas.width !== rect.width || canvas.height !== rect.height) {
            canvas.width = rect.width;
            canvas.height = rect.height;
        }

        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        if(dataPoints.length === 0) return;

        let min = Infinity, max = -Infinity;
        for(let v of dataPoints) {
            if(v < min) min = v;
            if(v > max) max = v;
        }
        if(min === max) { min -= 1; max += 1; }
        const span = max - min;
        min -= span * 0.2;
        max += span * 0.2;

        const padLeft = 60;
        const padRight = 10;
        const padBottom = 20;
        const padTop = 20;

        const plotW = canvas.width - padLeft - padRight;
        const plotH = canvas.height - padTop - padBottom;

        if (plotW <= 0 || plotH <= 0) return;

        // Draw Y-axis grid
        ctx.fillStyle = '#94a3b8';
        ctx.textAlign = 'right';
        ctx.textBaseline = 'middle';
        ctx.strokeStyle = '#1e293b';
        ctx.lineWidth = 1;
        ctx.beginPath();
        for(let i=0; i<=5; i++) {
            const y = padTop + (i/5) * plotH;
            const val = max - (i/5) * (max - min);
            ctx.moveTo(padLeft, y);
            ctx.lineTo(canvas.width - padRight, y);
            ctx.fillText(val.toFixed(4), padLeft - 5, y);
        }
        ctx.stroke();

        // Draw data curve
        ctx.strokeStyle = color; 
        ctx.lineWidth = 2;
        ctx.beginPath();
        for(let i = 0; i < dataPoints.length; i++) {
            const x = padLeft + (i / Math.max(1, maxPoints - 1)) * plotW;
            const y = padTop + plotH - ((dataPoints[i] - min) / (max - min)) * plotH;
            
            let cy = y;
            if(cy < padTop) cy = padTop;
            if(cy > padTop + plotH) cy = padTop + plotH;

            if(i === 0) ctx.moveTo(x, cy);
            else ctx.lineTo(x, cy);
        }
        ctx.stroke();
    }
}
