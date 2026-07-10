export function initTCD() {
    const container = document.getElementById('view-tcd');
    if (!container) return;

    container.innerHTML = `
        <div class="card" style="margin-bottom: 20px; text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">TCD 鏀惧ぇ鍣ㄦ祴璇?/h3>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <span style="color: #94a3b8;">鐘舵€?</span>
                    <span id="tcd-status" style="font-weight: bold; color: #94a3b8;">鏈繛鎺?/span>
                </div>
            </div>

            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap; padding-top: 15px; border-top: 1px dashed #334155;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">妗ユ祦 (0-127):</label>
                    <input type="number" id="tcd-set-bridge-val" value="12" class="input" style="width: 80px; margin-right: 0;">
                </div>
                <button class="btn" id="btn-tcd-set-bridge">璁剧疆妗ユ祦</button>
                <button class="btn btn-danger" id="btn-tcd-zeroing">璁惧璋冮浂</button>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 15px; background: rgba(0,0,0,0.2); padding: 5px 12px; border-radius: 6px; border: 1px solid #334155;">
                    <span style="color: #94a3b8; font-size: 12px;">鐢靛帇:</span>
                    <span id="tcd-voltage" style="color: #facc15; font-weight: bold; font-size: 13px; font-family: monospace;">-- V</span>
                    <span style="color: #475569;">|</span>
                    <span style="color: #94a3b8; font-size: 12px;">闃诲€?</span>
                    <span id="tcd-resistance" style="color: #38bdf8; font-weight: bold; font-size: 13px; font-family: monospace;">-- 惟</span>
                    <span style="color: #475569;">|</span>
                    <span style="color: #94a3b8; font-size: 12px;">娓╁害:</span>
                    <span id="tcd-filament-temp" style="color: #ef4444; font-weight: bold; font-size: 13px; font-family: monospace;">-- 鈩?/span>
                </div>
                <div style="margin-left: auto; display: flex; align-items: center; gap: 15px; background: rgba(0,0,0,0.2); padding: 5px 15px; border-radius: 6px; border: 1px solid #334155;">
                    <div style="display: flex; flex-direction: column; align-items: flex-end; gap: 2px;">
                        <div style="font-size: 12px; color: #94a3b8;" title="鏈€杩?鍒嗛挓鍐呯殑鏈€澶у€煎噺鍘绘渶灏忓€?(娴姩宸?">2鍒嗛挓鍩虹嚎鍣０(Noise): <span id="tcd-stat-noise" style="color: #facc15; font-weight: bold;">--</span></div>
                        <div style="font-size: 12px; color: #94a3b8;" title="娴姩宸笌鍩虹嚎鍧囧€肩殑姣斿€?(鐧惧垎姣?">鍩虹嚎婕傜Щ搴?Noise/Mean): <span id="tcd-stat-drift" style="color: #38bdf8; font-weight: bold;">--</span></div>
                    </div>
                    <div style="height: 30px; width: 1px; background: #334155; margin: 0 5px;"></div>
                    <div style="display: flex; align-items: center; gap: 8px;">
                        <span style="color: #94a3b8;">褰撳墠妗ユ祦:</span>
                        <span id="tcd-current-bridge" style="font-weight: bold; color: var(--text); font-size: 16px;">--</span>
                    </div>
                </div>
            </div>

            <div style="display: flex; gap: 20px; margin-top: 20px; height: 350px;">
                <div style="flex: 1; display: flex; flex-direction: column;">
                    <div style="display: flex; gap: 10px; align-items: center; padding: 8px; background: rgba(0,0,0,0.15); border-radius: 4px; margin-bottom: 5px;">
                        <label style="color: #94a3b8; font-size: 12px;">
                            <input type="checkbox" id="tcd-auto-scale" checked> 鑷€傚簲
                        </label>
                        <span style="color: #94a3b8; font-size: 12px;">Y涓婇檺:</span>
                        <input type="number" id="tcd-y-max" class="input" style="width: 70px; font-size: 12px;" step="0.01">
                        <span style="color: #94a3b8; font-size: 12px;">Y涓嬮檺:</span>
                        <input type="number" id="tcd-y-min" class="input" style="width: 70px; font-size: 12px;" step="0.01">
                        <span style="color: #94a3b8; font-size: 12px;">婊″睆(绉?:</span>
                        <input type="number" id="tcd-full-screen-sec" class="input" style="width: 60px; font-size: 12px;" value="120">
                        <span style="color: #94a3b8; font-size: 12px;">鎷栨斁:</span>
                        <select id="tcd-drag-mode" class="input" style="width: 60px; font-size: 12px;">
                            <option value="y">浠匶杞?/option>
                            <option value="xy">XY杞?/option>
                            <option value="none">绂佺敤</option>
                        </select>
                        <span style="color: #64748b; font-size: 11px; margin-left: auto;">鍙屽嚮閲嶇疆 | 婊氳疆缂╂斁 | 鎷栨斁閫夊尯鏀惧ぇ</span>
                    </div>
                    <div style="flex: 1; border: 1px solid #334155; border-radius: 6px; position: relative; background: #0f172a;">
                        <canvas id="tcd-canvas" style="position: absolute; top:0; left:0; width:100%; height:100%;"></canvas>
                    </div>
                </div>
                <div style="flex: 0 0 220px; border: 1px solid #334155; border-radius: 6px; background: #0f172a; padding: 10px; overflow-y: auto;">
                    <h4 style="margin-top: 0; color: #94a3b8; font-size: 13px; text-align: center; border-bottom: 1px solid #334155; padding-bottom: 5px;">20缁勫疄鏃舵暟鎹?/h4>
                    <div id="tcd-values-list" style="display: grid; grid-template-columns: 1fr 1fr; gap: 4px; font-size: 12px; font-family: monospace;">
                        <!-- data goes here -->
                    </div>
                </div>
            </div>
        </div>
    `;


    let tcdDataPoints = []; // sliding window

    // 鏈€澶у瓨鍌ㄧ偣鏁帮細4鍒嗛挓鏁版嵁锛屾瘡绉?0涓偣 = 9600
    const maxPoints = 9600;

    // Savitzky-Golay 婊ゆ尝绯绘暟锛堢獥鍙?锛?闃跺椤瑰紡锛?
    const SG_COEFFS = [-0.08571429, 0.34285714, 0.48571429, 0.34285714, -0.08571429];

    // 浜や簰鐘舵€?
    let zoomState = null; // { minIdx, maxIdx, minY, maxY } 鐢ㄦ埛鎷栨斁閫夊尯鏀惧ぇ鍚庣殑鐘舵€?
    let isDragging = false;
    let dragStartX = 0;
    let dragStartY = 0;
    let dragCurrentX = 0;
    let dragCurrentY = 0;
    let plotLayout = { padLeft: 80, padRight: 30, padTop: 30, padBottom: 30 };

    const canvas = document.getElementById('tcd-canvas');

    // 鎺т欢寮曠敤
    const autoScaleChk = document.getElementById('tcd-auto-scale');
    const yMaxInput = document.getElementById('tcd-y-max');
    const yMinInput = document.getElementById('tcd-y-min');
    const fullScreenSecInput = document.getElementById('tcd-full-screen-sec');
    const dragModeSelect = document.getElementById('tcd-drag-mode');

    // 瀹炴椂杈撳叆鍝嶅簲锛氳緭鍏ュ彉鍖栫珛鍗抽噸缁?
    function onControlChange() {
        // 鎵嬪姩妯″紡涓嬶紝濡傛灉鐢ㄦ埛娓呯┖浜嗚緭鍏ワ紝涓嶉噸缁橈紙閬垮厤 NaN锛?
        scheduleDraw();
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
                scheduleDraw();
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

                // 鏍规嵁鎷栨斁妯″紡鍒ゆ柇鏄惁闇€瑕乆鏂瑰悜鍙樺寲
                const requireX = (dragMode === 'xy');
                const requireY = true; // y 鍜?xy 閮介渶瑕乊鏂瑰悜

                const dxAbs = Math.abs(dragCurrentX - dragStartX);
                const dyAbs = Math.abs(dragCurrentY - dragStartY);

                // 鍒ゆ柇鏄惁鏋勬垚鏈夋晥閫夊尯
                const xValid = requireX ? dxAbs > 10 : true;
                const yValid = requireY ? dyAbs > 10 : true;
                if (!(xValid && yValid) || tcdDataPoints.length === 0) return;

                // 鑾峰彇褰撳墠鍙鑼冨洿
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
                    // Y妯″紡锛歑杞翠繚鎸佸師鏍?
                    newZoom.minIdx = Math.floor(currentMinIdx);
                    newZoom.maxIdx = Math.ceil(currentMaxIdx);
                }

                const newMaxY = currentMaxY - ((py1 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);
                const newMinY = currentMaxY - ((py2 - plotLayout.padTop) / plotH) * (currentMaxY - currentMinY);
                newZoom.minY = newMinY;
                newZoom.maxY = newMaxY;

                zoomState = newZoom;
                scheduleDraw();
            }
        });

        canvas.addEventListener('dblclick', () => {
            zoomState = null;
            scheduleDraw();
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

            // 婊氳疆缂╂斁Y杞达紝浠ラ紶鏍嘫浣嶇疆涓轰腑蹇?
            const deltaY = e.deltaY > 0 ? 1.1 : 0.9; // 鍚戜笂婊氭斁澶э紝鍚戜笅婊氱缉灏?
            const plotH = canvas.height - plotLayout.padTop - plotLayout.padBottom;
            // 榧犳爣浣嶇疆瀵瑰簲鐨刌鍊?
            const mouseVal = view.maxY - ((my - plotLayout.padTop) / plotH) * (view.maxY - view.minY);
            const newSpan = (view.maxY - view.minY) * deltaY;
            const newMinY = mouseVal - (mouseVal - view.minY) * deltaY;
            const newMaxY = mouseVal + (view.maxY - mouseVal) * deltaY;

            let newMinIdx = view.startIdx;
            let newMaxIdx = view.endIdx;

            // 濡傛灉鏄痻y妯″紡锛屼篃缂╂斁X杞?
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
            scheduleDraw();
        }, { passive: false });
    }

    // Savitzky-Golay 婊ゆ尝锛氬鏁扮粍搴旂敤绐楀彛5銆?闃跺椤瑰紡骞虫粦
    // 杩斿洖涓庤緭鍏ョ瓑闀跨殑骞虫粦鍚庢暟缁勶紱杈圭紭鐐逛娇鐢ㄧ缉鍑忕獥鍙?
    function savitzkyGolay(arr) {
        const n = arr.length;
        if (n < 5) {
            // 鏁版嵁澶皯锛岀洿鎺ヨ繑鍥炲壇鏈?
            return arr.slice();
        }
        const out = new Array(n);
        for (let i = 0; i < n; i++) {
            if (i < 2 || i >= n - 2) {
                // 杈圭紭鐐癸細浣跨敤缂╁噺绐楀彛锛堢洿鎺ュ鍒跺師鍊硷級
                out[i] = arr[i];
            } else {
                out[i] = SG_COEFFS[0] * arr[i-2] + SG_COEFFS[1] * arr[i-1] + SG_COEFFS[2] * arr[i] + SG_COEFFS[3] * arr[i+1] + SG_COEFFS[4] * arr[i+2];
            }
        }
        return out;
    }

    // 璁＄畻褰撳墠鍙鑼冨洿锛堢储寮曡寖鍥村拰Y鑼冨洿锛?
    function computeView() {
        if (tcdDataPoints.length === 0) return null;
        const total = tcdDataPoints.length;

        let startIdx, endIdx;
        if (zoomState) {
            startIdx = zoomState.minIdx;
            endIdx = zoomState.maxIdx;
        } else {
            // 鏍规嵁 fullScreenSec 璁＄畻鍙鐐规暟 N = fullScreenSec * 40 (姣忕40涓偣)
            const fsSec = parseFloat(fullScreenSecInput.value);
            const N = Math.max(1, Math.floor((isNaN(fsSec) ? 120 : fsSec) * 40));
            endIdx = total - 1;
            startIdx = Math.max(0, endIdx - N + 1);
        }

        let minY, maxY;
        if (autoScaleChk.checked) {
            // 鑷€傚簲锛氫粠鍙鑼冨洿鍐呭師濮嬫暟鎹绠梞in/max
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
            // 鎵嬪姩妯″紡
            minY = parseFloat(yMinInput.value);
            maxY = parseFloat(yMaxInput.value);
            if (isNaN(minY)) minY = -100;
            if (isNaN(maxY)) maxY = 100;
            if (minY === maxY) { minY -= 10; maxY += 10; }
        }
        return { startIdx, endIdx, minY, maxY };
    }

    // 鑷姩寮€濮嬭疆璇㈢姸鎬?
    tcdPollInterval = setInterval(pollTCDState, 500);

    // 鐢靛帇/闃诲€?娓╁害杞 (1绉掍竴娆★紝涓嶵CD鏁版嵁杞鐙珛)
    setInterval(pollVoltage, 1000);

    async function pollVoltage() {
        try {
            const res = await fetch('/api/v1/voltage/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('tcd-voltage').innerText = '-- V';
                    document.getElementById('tcd-resistance').innerText = '-- k惟';
                    document.getElementById('tcd-filament-temp').innerText = '-- 鈩?;
                    return;
                }
                const voltage = data.voltage; // 娴偣鐢靛帇鍊?(V)
                document.getElementById('tcd-voltage').innerText = voltage.toFixed(4) + ' V';

                // 鑾峰彇褰撳墠妗ユ祦(mA)锛岀敤浜庤绠楃數闃?
                const bridgeText = document.getElementById('tcd-current-bridge').innerText;
                const bridgeCurrent = parseFloat(bridgeText);
                if (bridgeCurrent > 0 && voltage > 0) {
                    // R = V / I, 鐢靛帇V锛岀數娴乵A 鈫?鐢甸樆惟 = (V / mA) * 1000
                    const resistance = (voltage / bridgeCurrent) * 1000; // 惟
                    document.getElementById('tcd-resistance').innerText = resistance.toFixed(2) + ' 惟';

                    // 娓╁害鍏紡: T = 2.5458 * R - 285.5878 (R鍗曚綅涓何?
                    const temp = 2.5458 * resistance - 285.5878;
                    document.getElementById('tcd-filament-temp').innerText = temp.toFixed(2) + ' 鈩?;
                } else {
                    document.getElementById('tcd-resistance').innerText = '-- 惟';
                    document.getElementById('tcd-filament-temp').innerText = '-- 鈩?;
                }
            }
        } catch (e) {}
    }

    // 鍔犺浇閰嶇疆鐨勬ˉ娴佸€?
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
                window.showToast('璁剧疆妗ユ祦鎸囦护宸蹭笅鍙?);
            } else {
                const data = await res.json();
                window.showToast('璁剧疆澶辫触: ' + data.error, true);
            }
        } catch (e) {
            window.showToast('璇锋眰寮傚父', true);
        }
    });

    document.getElementById('btn-tcd-zeroing').addEventListener('click', async () => {
        try {
            const res = await fetch('/api/v1/tcd/zeroing', { method: 'POST' });
            if (res.ok) {
                window.showToast('璋冮浂鎸囦护宸蹭笅鍙?);
            } else {
                const data = await res.json();
                window.showToast('璋冮浂澶辫触: ' + data.error, true);
            }
        } catch (e) {}
    });

    async function pollTCDState() {
        if (!document.getElementById('tcd-status')) return; // DOM销毁时自动停止轮询
        try {
            const res = await fetch('/api/v1/tcd/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('tcd-status').innerText = '杩炴帴宸叉柇寮€';
                    document.getElementById('tcd-status').style.color = 'var(--danger)';
                    return;
                }
                document.getElementById('tcd-status').innerText = '宸茶繛鎺?(閫氫俊涓?';
                document.getElementById('tcd-status').style.color = 'var(--success)';
                document.getElementById('tcd-current-bridge').innerText = data.bridge_current;

                let html = '';
                for (let i = 0; i < 20; i++) {
                    const color = data.values[i] >= 0 ? '#38bdf8' : '#ef4444';
                    html += `<div><span style="color:#94a3b8">CH${(i+1).toString().padStart(2,'0')}</span> <span style="color:${color}">${data.values[i]}</span></div>`;
                }
                document.getElementById('tcd-values-list').innerHTML = html;

                // 灏?0涓師濮嬫暟鎹偣鎸夐『搴忎竴娆℃€у叏閮ㄦ帹鍏ワ紙40Hz 閲囨牱鐜囷級
                tcdDataPoints.push(...data.values);
                // 鏈€澶у瓨鍌?maxPoints = 9600锛?鍒嗛挓鏁版嵁锛?
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
                
                // Calculate Baseline Noise & Drift (鍩轰簬鍏ㄩ噺绐楀彛鏁版嵁)
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

                scheduleDraw();
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

        // 璁＄畻鍙鑼冨洿
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

        // --- 缁樺埗Y杞寸綉鏍煎拰鍒诲害锛?绛夊垎锛?---
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

        // 缁樺埗 "mV" 鍗曚綅鏍囩
        ctx.textAlign = 'left';
        ctx.textBaseline = 'bottom';
        ctx.fillStyle = '#94a3b8';
        ctx.fillText('mV', 10, padTop - 5);

        // --- 缁樺埗X杞寸綉鏍煎拰鏃堕棿鍒诲害 ---
        ctx.textAlign = 'center';
        ctx.textBaseline = 'top';
        ctx.beginPath();
        for(let i=0; i<=10; i++) {
            const x = padLeft + (i/10) * plotW;
            const idx = startIdx + (i/10) * (endIdx - startIdx);
            const timeSec = idx / 40; // 姣忎釜鐐逛唬琛?1/40 绉?(0.025s)
            
            ctx.moveTo(x, padTop);
            ctx.lineTo(x, canvas.height - padBottom);
            ctx.fillText(timeSec.toFixed(0) + 's', x, canvas.height - padBottom + 10);
        }
        ctx.stroke();

        // 缁樺埗0鍩虹嚎
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

        // --- 瀵瑰彲瑙佹暟鎹偣搴旂敤 Savitzky-Golay 婊ゆ尝 ---
        const visibleRaw = tcdDataPoints.slice(startIdx, endIdx + 1);
        const visibleSmoothed = savitzkyGolay(visibleRaw);

        // 缁樺埗骞虫粦鍚庣殑鏇茬嚎
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
            // 瑙嗚瑁佸壀鍒扮粯鍥惧尯
            if(y < padTop) y = padTop;
            if(y > padTop + plotH) y = padTop + plotH;

            if(i === 0) ctx.moveTo(x, y);
            else ctx.lineTo(x, y);
        }
        ctx.stroke();

        // 缁樺埗鎷栨斁閫夊尯妗?
        if (isDragging) {
            const dragMode = dragModeSelect.value;
            ctx.fillStyle = 'rgba(56, 189, 248, 0.2)';
            ctx.strokeStyle = '#38bdf8';
            ctx.lineWidth = 1;
            
            let dx1 = Math.max(padLeft, Math.min(canvas.width - padRight, dragStartX));
            let dx2 = Math.max(padLeft, Math.min(canvas.width - padRight, dragCurrentX));
            let dy1 = Math.max(padTop, Math.min(canvas.height - padBottom, dragStartY));
            let dy2 = Math.max(padTop, Math.min(canvas.height - padBottom, dragCurrentY));

            // 鏍规嵁鎷栨斁妯″紡闄愬埗閫夊尯褰㈢姸
            if (dragMode === 'y') {
                // 浠匶杞达細閫夊尯妯悜閾烘弧鏁翠釜缁樺浘鍖?
                dx1 = padLeft;
                dx2 = canvas.width - padRight;
            }

            const w = dx2 - dx1;
            const h = dy2 - dy1;
            ctx.fillRect(dx1, dy1, w, h);
            ctx.strokeRect(dx1, dy1, w, h);
            
            // 鏄剧ず閫夊尯鏁板€?
            ctx.fillStyle = '#fff';
            ctx.textAlign = 'left';
            const dx = Math.abs(dx2 - dx1);
            const dy = Math.abs(dy2 - dy1);
            const valSpan = max - min;
            const timeSpan = (endIdx - startIdx) / 40;
            const dVal = (dy / plotH) * valSpan;
            const dTime = (dx / plotW) * timeSpan;
            
            ctx.fillText(`螖X: ${dTime.toFixed(1)}s, 螖Y: ${dVal.toFixed(2)}mV`, Math.max(padLeft + 5, Math.min(dx1, dx2)), Math.max(padTop + 15, Math.min(dy1, dy2) - 5));
        }
        
        // 鏄剧ず宸叉斁澶х姸鎬?
        if (zoomState) {
            ctx.fillStyle = '#facc15';
            ctx.textAlign = 'right';
            ctx.textBaseline = 'top';
            ctx.fillText('馃攳 宸叉斁澶?(鍙屽嚮杩樺師)', canvas.width - 10, 20);
        }
    }
}
