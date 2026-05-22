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
    
    function draw() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        // Draw grid
        ctx.strokeStyle = '#334155';
        ctx.lineWidth = 1;
        for (let i = 0; i < canvas.width; i += 50) {
            ctx.beginPath(); ctx.moveTo(i, 0); ctx.lineTo(i, canvas.height); ctx.stroke();
        }
        for (let i = 0; i < canvas.height; i += 50) {
            ctx.beginPath(); ctx.moveTo(0, i); ctx.lineTo(canvas.width, i); ctx.stroke();
        }

        // Draw line
        if (dataPoints.length > 1) {
            ctx.strokeStyle = '#3b82f6';
            ctx.lineWidth = 2;
            ctx.beginPath();
            
            // Normalize values for rendering (assuming pA range, adjust as needed)
            const maxValue = Math.max(...dataPoints, 100);
            const scaleY = canvas.height / maxValue;
            const scaleX = canvas.width / Math.max(dataPoints.length, 1000);

            ctx.moveTo(0, canvas.height - (dataPoints[0] * scaleY));
            for (let i = 1; i < dataPoints.length; i++) {
                ctx.lineTo(i * scaleX, canvas.height - (dataPoints[i] * scaleY));
            }
            ctx.stroke();
        }
    }
    
    // Resize handler
    window.addEventListener('resize', () => {
        const rect = canvas.parentElement.getBoundingClientRect();
        canvas.width = rect.width;
        canvas.height = rect.height;
        draw();
    });

    // WebSocket/SSE integration
    const evtSource = new EventSource('/events');
    evtSource.onmessage = function(event) {
        try {
            const parsed = JSON.parse(event.data);
            if (parsed.type === 'data' && parsed.values) {
                // If it's a new session or start of data, reset points
                if (parsed.t0S === 0 || dataPoints.length > 10000) {
                    dataPoints = [];
                }
                dataPoints.push(...parsed.values);
                
                // Keep the last N points to avoid memory overflow (e.g. 10000 points)
                if (dataPoints.length > 10000) {
                    dataPoints = dataPoints.slice(dataPoints.length - 10000);
                }
                
                requestAnimationFrame(draw);
            } else if (parsed.type === 'result') {
                // Update live results table
                // TODO: Update results
            }
        } catch (e) {
            console.error('SSE parse error:', e);
        }
    };
    
    draw();
}
