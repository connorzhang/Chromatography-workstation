import os

js_content = """export function initAudit() {
    const viewPanel = document.getElementById('view-audit');
    if (!viewPanel) return;

    viewPanel.innerHTML = \
        <div class="card">
            <div class="card-header">
                <h3>审计快照配置</h3>
                <div>
                    <label>
                        <input type="checkbox" id="audit-enabled-checkbox"> 启用定时快照
                    </label>
                    <label style="margin-left: 15px;">
                        间隔 (分钟): <input type="number" id="audit-interval-input" value="5" min="1" max="1440" style="width: 80px;">
                    </label>
                    <button class="btn btn-primary" id="audit-save-btn" style="margin-left: 15px;">保存配置</button>
                    <button class="btn" id="audit-refresh-btn" style="margin-left: 15px;"> 刷新数据</button>
                </div>
            </div>
            <div class="card-body" style="overflow-x: auto; max-height: calc(100vh - 250px); overflow-y: auto;">
                <table class="data-table" id="audit-table">
                    <thead>
                        <tr>
                            <th>时间</th>
                            <th>柱温()</th>
                            <th>进样1()</th>
                            <th>进样2()</th>
                            <th>检测1()</th>
                            <th>检测2()</th>
                            <th>检测3()</th>
                            <th>载气压力(psi)</th>
                            <th>载气流量(sccm)</th>
                            <th>氢气压力(psi)</th>
                            <th>氢气流量(sccm)</th>
                            <th>空气压力(psi)</th>
                            <th>空气流量(sccm)</th>
                            <th>桥流(mA)</th>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    \;

    const enabledCheckbox = document.getElementById('audit-enabled-checkbox');
    const intervalInput = document.getElementById('audit-interval-input');
    const saveBtn = document.getElementById('audit-save-btn');
    const refreshBtn = document.getElementById('audit-refresh-btn');
    const tableBody = document.querySelector('#audit-table tbody');

    function loadAuditData() {
        fetch('/api/v1/audit')
            .then(res => res.json())
            .then(data => {
                if (data.config) {
                    enabledCheckbox.checked = data.config.enabled;
                    intervalInput.value = data.config.intervalMins;
                }
                if (data.history) {
                    renderTable(data.history);
                }
            })
            .catch(err => console.error('Failed to load audit data', err));
    }

    function renderTable(history) {
        tableBody.innerHTML = '';
        // Reverse to show newest first
        const reversed = [...history].reverse();
        reversed.forEach(snap => {
            const tr = document.createElement('tr');
            
            const d = new Date(snap.timestamp);
            const timeStr = d.getFullYear() + '-' + 
                String(d.getMonth() + 1).padStart(2, '0') + '-' + 
                String(d.getDate()).padStart(2, '0') + ' ' +
                String(d.getHours()).padStart(2, '0') + ':' + 
                String(d.getMinutes()).padStart(2, '0') + ':' + 
                String(d.getSeconds()).padStart(2, '0');

            const val = (v) => v !== null && v !== undefined ? parseFloat(v).toFixed(2) : '-';
            const intVal = (v) => v !== null && v !== undefined ? v : '-';

            tr.innerHTML = \
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
                <td>\</td>
            \;
            tableBody.appendChild(tr);
        });
    }

    saveBtn.addEventListener('click', () => {
        const payload = {
            enabled: enabledCheckbox.checked,
            intervalMins: parseInt(intervalInput.value, 10)
        };
        fetch('/api/v1/audit', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        })
        .then(res => res.json())
        .then(data => {
            if(window.showToast) window.showToast('配置已保存');
        })
        .catch(err => {
            if(window.showToast) window.showToast('保存失败', true);
        });
    });

    refreshBtn.addEventListener('click', loadAuditData);

    loadAuditData();
}
"""

with open(r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\static\js\views\audit.js', 'w', encoding='utf-8') as f:
    f.write(js_content.replace('\', '').replace('\$', '$'))

print('audit.js generated.')
