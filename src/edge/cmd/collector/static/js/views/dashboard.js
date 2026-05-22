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
}
