export function initDashboard() {
    const container = document.getElementById('view-dashboard');
    container.innerHTML = `
        <div class="card-grid">
            <div class="card">
                <div class="card-title">非甲烷总烃 (NMHC)</div>
                <div class="card-value" id="val-nmhc">0.00</div>
                <div class="card-unit">mg/m³</div>
            </div>
            <div class="card">
                <div class="card-title">总烃 (THC)</div>
                <div class="card-value" id="val-thc">0.00</div>
                <div class="card-unit">mg/m³</div>
            </div>
            <div class="card">
                <div class="card-title">甲烷 (CH4)</div>
                <div class="card-value" id="val-ch4">0.00</div>
                <div class="card-unit">mg/m³</div>
            </div>
        </div>
        
        <div class="control-group" style="margin-top: 2rem; display: flex; gap: 1rem; align-items: center;">
            <button class="btn" id="btn-start-analysis" onclick="window.sendCmd('startAll')">▶ 开始分析</button>
            <button class="btn btn-danger" id="btn-stop-analysis" onclick="window.sendCmd('stopAll')">■ 停止分析</button>
            <div style="margin-left: 2rem; color: #94a3b8;">
                循环状态: <span id="cycle-status" style="color: white;">等待中</span>
            </div>
        </div>
    `;

    // SSE Integration for Dashboard
    let dashboardRestored = false;
    const evtSource = new EventSource('/events');
    evtSource.onmessage = function(event) {
        try {
            const parsed = JSON.parse(event.data);

            if (parsed.deviceId && !dashboardRestored) {
                dashboardRestored = true;
                fetch('/api/v1/session/active?deviceId=' + encodeURIComponent(parsed.deviceId) + '&channel=0')
                    .then(r => r.json())
                    .then(sess => {
                        if (sess && sess.result) {
                            updateDashboardResult(sess.result);
                        }
                    }).catch(e => {
                        console.error('Dashboard restore failed:', e);
                        dashboardRestored = false;
                    });
            }

            if (parsed.type === 'result' && parsed.result) {
                updateDashboardResult(parsed.result);
                document.getElementById('cycle-status').textContent = '分析完成';
            } else if (parsed.type === 'samples') {
                document.getElementById('cycle-status').textContent = '分析中...';
            } else if (parsed.type === 'telemetry') {
                // Temperatures and EPC are no longer displayed on the dashboard
            }
        } catch (e) {
            console.error('SSE parse error:', e);
        }
    };

    function updateDashboardResult(resultObj) {
        let thc = 0, ch4 = 0;

        if (resultObj.pollutants) {
            resultObj.pollutants.forEach(p => {
                if (p.code === 'THC') thc = p.amount || 0;
                if (p.code === 'CH4') ch4 = p.amount || 0;
            });
        }

        // Override if groups defined
        if (resultObj.groups) {
            resultObj.groups.forEach(g => {
                if (g.code === 'NMHC') {
                    const el = document.getElementById('val-nmhc');
                    if (el) el.textContent = g.amount.toFixed(2);
                }
            });
        } else {
            const el = document.getElementById('val-nmhc');
            if (el) el.textContent = (thc - ch4 > 0 ? (thc - ch4) : 0).toFixed(2);
        }

        const thcEl = document.getElementById('val-thc');
        const ch4El = document.getElementById('val-ch4');
        if (thcEl) thcEl.textContent = thc.toFixed(2);
        if (ch4El) ch4El.textContent = ch4.toFixed(2);
    }
}
