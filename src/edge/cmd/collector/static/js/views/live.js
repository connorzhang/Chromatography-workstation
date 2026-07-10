export function initLiveChromatogram() {
    const container = document.getElementById('view-live');
    container.innerHTML = `
        <div style="display: flex; height: calc(100vh - 80px); gap: 1rem;">
            <div style="flex: 1; min-width: 0; background: var(--panel); border-radius: 8px; border: 1px solid #334155; position: relative; display: flex; flex-direction: column;">
                <!-- 绗竴琛岋細杩涙牱绫诲瀷銆佹帶鍒舵寜閽€佺偣鐏浘鏍?-->
                <div style="padding: 6px 10px; border-bottom: 1px solid #334155; display: flex; gap: 10px; align-items: center; justify-content: space-between;">
                    <div style="display: flex; gap: 10px; align-items: center;">
                        <span style="color: #94a3b8;">杩涙牱绫诲瀷:</span>
                        <label><input type="radio" name="injType" value="normal" checked> 姝ｅ父</label>
                        <label><input type="radio" name="injType" value="zero"> 闆舵皵</label>
                        <label><input type="radio" name="injType" value="span"> 鏍囨皵</label>
                        
                        <div style="margin-left: 20px; display: flex; gap: 10px;">
                            <button class="btn" onclick="window.sendCmd('startAll')">鈻?寮€濮嬪垎鏋?/button>
                            <button class="btn btn-danger" onclick="window.sendCmd('stopAll')">鈴?鍋滄鍒嗘瀽</button>
                        </div>
                    </div>
                    
                    <!-- 鐐圭伀鍥炬爣 -->
                    <div id="live-ignite-icon" style="font-size: 28px; cursor: pointer; color: #64748b; transition: color 0.3s;" title="鐐瑰嚮鍙戦€佺偣鐏寚浠?>
                        馃敟
                    </div>
                </div>

                <!-- 绗簩琛岋細鍥捐〃閰嶇疆椤?-->
                <div style="padding: 6px 10px; border-bottom: 1px solid #334155; display: flex; gap: 15px; align-items: center; background: rgba(0,0,0,0.2);">
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">涓嬮檺:</span>
                        <input type="number" id="live-y-low" class="input" value="0" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">涓婇檺:</span>
                        <input type="number" id="live-y-high" class="input" value="40" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">閲囬泦鏃堕棿:</span>
                        <input type="number" id="live-acq-min" class="input" value="2" step="0.1" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <div style="display: flex; align-items: center; gap: 5px;">
                        <span style="color: #94a3b8;">婊″睆鏃堕棿:</span>
                        <input type="number" id="live-full-min" class="input" value="2" style="width: 60px; padding: 2px 5px;">
                    </div>
                    <button class="btn" id="btn-apply-live-settings" style="padding: 2px 10px; font-size: 12px;">搴旂敤</button>
                </div>

                <!-- 绗笁琛岋細瀹炴椂鐘舵€佷笌鑷€傚簲閰嶇疆 -->
                <div style="padding: 5px 10px; border-bottom: 1px solid #334155; display: flex; gap: 15px; align-items: center; font-size: 13px; background: rgba(0,0,0,0.1);">
                    <span style="color: #94a3b8;">閫氶亾1:</span>
                    <span id="live-current-time" style="font-family: monospace; font-weight: bold;">0.000</span> <span style="color: #94a3b8;">min</span>
                    <span id="live-current-signal" style="font-family: monospace; font-weight: bold; margin-left: 10px;">0.000</span> <span style="color: #94a3b8;">pA</span>
                    
                    <span style="color: #94a3b8; margin-left: 10px;">淇″彿1:</span>
                    <label style="display: flex; align-items: center; gap: 5px; color: #10b981; cursor: pointer;">
                        <input type="checkbox" id="live-auto-y"> 宄伴珮鑷€傚簲
                    </label>
                </div>

                <!-- 鍥捐〃鍖?-->
                <div style="flex: 1; position: relative; min-height: 0;">
                    <canvas id="chromatogram-canvas" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></canvas>
                </div>
            </div>
            <div style="flex: 0 0 350px; display: flex; flex-direction: column; gap: 0.5rem; min-height: 0;">
                <div class="control-group" style="flex: 1; margin: 0; padding: 0.8rem; overflow-y: auto;">
                    <h3 style="margin-top:0; margin-bottom:0.5rem;">瀹炴椂缁撴灉</h3>
                    <table id="live-results-table" style="margin-top: 0;">
                        <thead>
                            <tr><th>鍚嶇О</th><th style="text-align:right">鍚噺(mg/m鲁)</th></tr>
                        </thead>
                        <tbody>
                            <tr><td colspan="2" style="text-align:center; color:#94a3b8">绛夊緟鍒嗘瀽...</td></tr>
                        </tbody>
                    </table>
                </div>
                <div style="flex: 1; display: flex; gap: 0.5rem; min-height: 0;">
                    <div class="control-group" style="flex: 1; margin: 0; padding: 0.6rem; overflow-y: auto;">
                        <table id="live-pressure-table" style="font-size: 13px; margin-top: 0; width: 100%;">
                            <thead>
                                <tr><th>鍚嶇О</th><th style="text-align:right">瀹炴祴(psi)</th></tr>
                            </thead>
                            <tbody>
                                <tr><td>杞芥皵1</td><td id="val-carrier1" style="text-align:right">0.00</td></tr>
                                <tr><td>杞芥皵3</td><td id="val-carrier3" style="text-align:right">0.00</td></tr>
                                <tr><td>鏍锋皵</td><td id="val-sample" style="text-align:right">0.00</td></tr>
                                <tr><td>杞芥皵2</td><td id="val-carrier2" style="text-align:right">0.00</td></tr>
                                <tr><td>姘㈡皵1</td><td id="val-h2-1" style="text-align:right">0.00</td></tr>
                                <tr><td>绌烘皵1</td><td id="val-air-1" style="text-align:right">0.00</td></tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="control-group" style="flex: 1; margin: 0; padding: 0.6rem; overflow-y: auto;">
                        <table id="live-temp-table" style="font-size: 13px; margin-top: 0; width: 100%;">
                            <thead>
                                <tr><th>鍚嶇О</th><th style="text-align:right">瀹炴祴(鈩?</th></tr>
                            </thead>
                            <tbody>
                                <tr><td>鏌辩</td><td id="val-col" style="text-align:right">0.0</td></tr>
                                <tr><td>闃€娓?/td><td id="val-valve" style="text-align:right">0.0</td></tr>
                                <tr><td>妫€娴?</td><td id="val-det1" style="text-align:right">0.0</td></tr>
                                <tr><td>杩涙牱1</td><td id="val-inj1" style="text-align:right">0.0</td></tr>
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
                            uiSettings = ui; // 淇濆瓨瀹屾暣鐨?ui 瀵硅薄锛岄槻姝?POST 瑕嗙洊涓㈠け瀛楁
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
                
                const activeDev = devices.find(d => d.deviceId === deviceId);
                if (activeDev && activeDev.capabilities && activeDev.capabilities.has_ignition === false) {
                    const igniteIcon = document.getElementById('live-ignite-icon');
                    if (igniteIcon) igniteIcon.style.display = 'none';
                }
            }

            uiSettings.deviceId = deviceId;

            const res = await fetch('/api/v1/ui', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(uiSettings)
            });
            if(res.ok) {
                window.showToast('鍥捐〃閰嶇疆宸插簲鐢?');
                draw();
            } else {
                window.showToast('搴旂敤澶辫触', true);
            }
        } catch(e) {
            window.showToast('寮傚父: ' + e.message, true);
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
                    window.showToast('FID 鐐圭伀鎸囦护宸插彂閫?);
                    isIgnited = true;
                    igniteIcon.style.color = '#ef4444'; // Red color when ignited
                    igniteIcon.title = "宸茬偣鐏?;
                } else {
                    window.showToast('鐐圭伀鎸囦护鍙戦€佸け璐?, true);
                }
            } catch(e) {
                window.showToast('鐐圭伀寮傚父: ' + e.message, true);
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
        
        // 瑕嗙洊鑷姩寤堕暱鐨勯€昏緫锛屼互 UI 閰嶇疆鐨勬弧灞忔椂闂翠负鍑?
        if (dataPoints.length > 0) {
            const maxT = dataPoints[dataPoints.length - 1][0];
            if (maxT / 60 > xEndMin) {
                // 濡傛灉瀹為檯鏁版嵁瓒呰繃浜嗘弧灞忔椂闂达紝鑷姩寰€鍙虫粴鍔ㄦ垨鑰呰嚜鍔ㄦ墿灞曘€?
                // 浼犵粺鑹茶氨閫氬父浼氬浐瀹?xEndMin锛岃秴鍑虹殑閮ㄥ垎琚鍓紝鎴栬€呰嚜鍔ㄥ鍔犳弧灞忔椂闂淬€?
                // 杩欓噷鎴戜滑鏆備笖鍏佽瀹冭嚜鍔ㄥ悜鍚庢嫇灞曪紝淇濊瘉鑳界湅鍒版尝褰€?
                xEndMin = Math.ceil(maxT / 60);
            }
        }
        const xSpanMin = xEndMin - xBegMin;
        
        let yBeg = uiSettings.yLow;
        let yEnd = uiSettings.yHigh;

        // 宄伴珮鑷€傚簲閫昏緫
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
            // 鎻愰珮鏈€灏忛噺绋嬮槇鍊硷紝閬垮厤绾熀绾垮井灏忔紓绉昏褰撲綔宄拌€岃Е鍙戠暀鐧芥斁澶?
            const minSpan = 10.0; 
            if (span < minSpan) {
                span = minSpan;
            }
            
            // 涓嬮潰鐣?%锛屼笂闈㈢暀60%锛屾墍浠ュ疄闄呮尝褰紙鎴?minSpan锛夊崰鎹腑闂寸殑 35%
            const V = span / 0.35;
            yBeg = yMin - 0.05 * V;
            // 濮嬬粓鍩轰簬 yMin 鍜?span 璁＄畻 yEnd锛岀‘淇濆湪 span 琚攣瀹氭椂锛寉End 涓嶄細闅忕潃 yMax 鐨勫井灏忓鍔犺€屽線涓嬪帇娉㈠舰
            yEnd = yMin + 0.95 * V;
        }

        if (yEnd <= yBeg) yEnd = yBeg + 1; // 闃叉鏃犳晥鍖洪棿
        
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
        ctx.fillText('鏃堕棿 (min)', padL + w / 2, padT + h + 24);
        
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
        ctx.fillText('淇″彿 (pA)', 0, 0);
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
            ctx.fillText('绛夊緟鑹茶氨浠笅鍙戞尝褰㈡暟鎹?(璁惧鍙兘姝ｅ湪鍗囨俯鎴栧氨缁腑)...', padL + w / 2, padT + h / 2);
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
    if (window.liveResizeObserver) window.liveResizeObserver.disconnect();
    window.liveResizeObserver = new ResizeObserver(entries => {
        for (let entry of entries) {
            if (entry.contentRect.width > 0 && entry.contentRect.height > 0) {
                canvas.width = entry.contentRect.width;
                canvas.height = entry.contentRect.height;
                draw();
            }
        }
    });
    window.liveResizeObserver.observe(canvas.parentElement);

    // WebSocket/SSE integration
    if (window.liveEvtSource) window.liveEvtSource.close();
    window.liveEvtSource = new EventSource('/events');
    const evtSource = window.liveEvtSource;
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
                            // 杩欓噷瑕佽€冭檻鍒板鏋滃悗绔瓨鐨勪笉鏄叏閲忔暟缁勮€屾槸涓€閮ㄥ垎锛屾垜浠渶瑕佸姞涓婃纭殑鍋忕Щ
                            // 浣嗘槸 /api/v1/session/active 鎺ュ彛杩斿洖鐨?values 灏辨槸浠?0 寮€濮嬬殑鏁翠釜鍛ㄦ湡鐨勫揩鐓?
                            for (let i = 0; i < sess.values.length; i++) {
                                restored.push([i * dtS, sess.values[i]]);
                            }
                            // Merge and deduplicate by time
                            const uniqueMap = new Map();
                            for (const p of restored.concat(dataPoints)) {
                                uniqueMap.set(p[0].toFixed(3), p);
                            }
                            dataPoints = Array.from(uniqueMap.values()).sort((a, b) => a[0] - b[0]);
                            
                            // 鎭㈠涓婁竴缁勭殑瀹炴椂缁撴灉
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
                    if (elValve) elValve.innerText = (parsed.tempInj2 || 0).toFixed(1); // 闃€娓╂殏鍊熺敤 tempInj2
                }
                if (parsed.epc && parsed.epc.length > 0) {
                    const elC1 = document.getElementById('val-carrier1');
                    const elC3 = document.getElementById('val-carrier3');
                    const elSample = document.getElementById('val-sample');
                    const elC2 = document.getElementById('val-carrier2');
                    const elH2_1 = document.getElementById('val-h2-1');
                    const elAir_1 = document.getElementById('val-air-1');
                    
                    if (elC1 && parsed.epc.length > 0) elC1.innerText = (parsed.epc[0].psi || 0).toFixed(4);
                    if (elC3 && parsed.epc.length > 1) elC3.innerText = (parsed.epc[1].psi || 0).toFixed(4);
                    if (elSample && parsed.epc.length > 2) elSample.innerText = (parsed.epc[2].psi || 0).toFixed(4);
                    if (elC2 && parsed.epc.length > 3) elC2.innerText = (parsed.epc[3].psi || 0).toFixed(4);
                    if (elH2_1 && parsed.epc.length > 9) elH2_1.innerText = (parsed.epc[9].psi || 0).toFixed(4);
                    if (elAir_1 && parsed.epc.length > 10) elAir_1.innerText = (parsed.epc[10].psi || 0).toFixed(4);
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
            tbody.innerHTML = html || '<tr><td colspan="2" style="text-align:center; color:#94a3b8">鏆傛棤缁勫垎鏁版嵁</td></tr>';
        }
    }
    
    draw();
}

