export function initReport() {
    const container = document.getElementById('view-report');
    container.innerHTML = `
        <div style="display: flex; flex-direction: column; height: 100%; gap: 1rem;">
            <div class="control-group" style="margin: 0; display: flex; gap: 1rem; align-items: center;">
                <div>
                    时间范围: 
                    <input type="datetime-local" id="report-from" class="input"> 
                    至 
                    <input type="datetime-local" id="report-to" class="input">
                </div>
                <button class="btn" id="btn-query-report">查询</button>
                <button class="btn" id="btn-export-report">导出 CSV</button>
            </div>
            
            <div class="control-group" style="flex: 1; margin: 0; overflow-y: auto;">
                <table id="report-table">
                    <thead>
                        <tr>
                            <th>时间</th><th>设备 ID</th><th>Trace ID</th><th>总烃</th><th>甲烷</th><th>非甲烷总烃</th>
                        </tr>
                    </thead>
                    <tbody id="tbody-report">
                        <tr><td colspan="6" style="text-align:center; color:#94a3b8">点击查询加载数据</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;

    setTimeout(() => {
        // Default to last 24 hours
        const now = new Date();
        const yesterday = new Date(now.getTime() - 24 * 60 * 60 * 1000);
        
        // Format to YYYY-MM-DDThh:mm
        const formatDt = (d) => d.toISOString().slice(0, 16);
        
        document.getElementById('report-from').value = formatDt(yesterday);
        document.getElementById('report-to').value = formatDt(now);

        document.getElementById('btn-query-report').addEventListener('click', async () => {
            const from = new Date(document.getElementById('report-from').value).toISOString();
            const to = new Date(document.getElementById('report-to').value).toISOString();
            
            try {
                const res = await fetch(\`/api/history/results?from=\${encodeURIComponent(from)}&to=\${encodeURIComponent(to)}&limit=100\`);
                const data = await res.json();
                
                const tbody = document.getElementById('tbody-report');
                if(!data || data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center">暂无数据</td></tr>';
                    return;
                }
                
                tbody.innerHTML = '';
                data.forEach(row => {
                    let thc = 0, ch4 = 0, nmhc = 0;
                    if(row.pollutants) {
                        row.pollutants.forEach(p => {
                            if(p.code === 'THC') thc = p.amount;
                            if(p.code === 'CH4') ch4 = p.amount;
                        });
                    }
                    if(row.groups) {
                        row.groups.forEach(g => {
                            if(g.code === 'NMHC') nmhc = g.amount;
                        });
                    }
                    
                    tbody.innerHTML += \`<tr>
                        <td>\${new Date(row.created_at).toLocaleString()}</td>
                        <td>\${row.device_id}</td>
                        <td>\${row.trace_id.substring(0, 8)}...</td>
                        <td>\${thc.toFixed(2)}</td>
                        <td>\${ch4.toFixed(2)}</td>
                        <td>\${nmhc.toFixed(2)}</td>
                    </tr>\`;
                });
            } catch(e) {
                console.error(e);
                document.getElementById('tbody-report').innerHTML = '<tr><td colspan="6" style="text-align:center; color: var(--danger)">查询失败</td></tr>';
            }
        });

        document.getElementById('btn-export-report').addEventListener('click', () => {
            const from = new Date(document.getElementById('report-from').value).toISOString();
            const to = new Date(document.getElementById('report-to').value).toISOString();
            window.location.href = \`/api/v1/results/nmhc/export.csv?from=\${encodeURIComponent(from)}&to=\${encodeURIComponent(to)}\`;
        });
    }, 0);
}
