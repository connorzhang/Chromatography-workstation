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
                    <thead id="thead-report">
                        <tr>
                            <th>时间</th><th>设备 ID</th><th>Trace ID</th><th>组分数据</th>
                        </tr>
                    </thead>
                    <tbody id="tbody-report">
                        <tr><td colspan="4" style="text-align:center; color:#94a3b8">点击查询 加载数据</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;

    setTimeout(() => {
        // Default to last 24 hours
        const now = new Date();
        const yesterday = new Date(now.getTime() - 24 * 60 * 60 * 1000);
        
        // Format to local YYYY-MM-DDThh:mm
        const formatDt = (d) => {
            const pad = (n) => n.toString().padStart(2, '0');
            return `${d.getFullYear()}-${pad(d.getMonth()+1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
        };
        
        document.getElementById('report-from').value = formatDt(yesterday);
        document.getElementById('report-to').value = formatDt(now);

        document.getElementById('btn-query-report').addEventListener('click', async () => {
            const from = new Date(document.getElementById('report-from').value).toISOString();
            const to = new Date(document.getElementById('report-to').value).toISOString();
            
            try {
                // Try to get current device ID, if offline, fallback to recent history without filter
                const devRes = await fetch('/api/v1/devices');
                const devices = await devRes.json();
                let deviceIdQuery = '';
                if (devices && devices.length > 0) {
                    deviceIdQuery = `deviceId=${encodeURIComponent(devices[0].deviceId)}&`;
                }

                const res = await fetch(`/api/history/results?${deviceIdQuery}from=${encodeURIComponent(from)}&to=${encodeURIComponent(to)}&limit=100`);
                const data = await res.json();
                
                const tbody = document.getElementById('tbody-report');
                const thead = document.getElementById('thead-report');
                if(!data || data.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="10" style="text-align:center">暂无数据</td></tr>';
                    return;
                }

                // Collect all unique component names
                const compSet = new Set();
                data.forEach(row => {
                    const resObj = (row.result && row.result.result) ? row.result.result : (row.result || row);
                    if(resObj.pollutants) resObj.pollutants.forEach(p => compSet.add(p.code || p.name));
                    if(resObj.groups) resObj.groups.forEach(g => compSet.add(g.code || g.name));
                });
                
                const compArray = Array.from(compSet);
                if (compArray.length > 0) {
                    let headHtml = '<tr><th>时间</th><th>设备 ID</th><th>Trace ID</th>';
                    compArray.forEach(c => { headHtml += `<th>${c}</th>`; });
                    headHtml += '</tr>';
                    thead.innerHTML = headHtml;
                }

                tbody.innerHTML = '';
                data.forEach(row => {
                    const resObj = (row.result && row.result.result) ? row.result.result : (row.result || row);
                    const valMap = {};
                    
                    if(resObj.pollutants) {
                        resObj.pollutants.forEach(p => {
                            valMap[p.code || p.name] = p.amount;
                        });
                    }
                    if(resObj.groups) {
                        resObj.groups.forEach(g => {
                            valMap[g.code || g.name] = g.amount;
                        });
                    }

                    let trHtml = `<tr>
                        <td>${new Date(row.created_at).toLocaleString()}</td>
                        <td>${row.device_id}</td>
                        <td>${row.trace_id.substring(0, 8)}...</td>`;
                    
                    if (compArray.length > 0) {
                        compArray.forEach(c => {
                            const v = valMap[c] !== undefined ? valMap[c] : 0;
                            trHtml += `<td>${v.toFixed(2)}</td>`;
                        });
                    } else {
                        trHtml += `<td>(无分析组分)</td>`;
                    }
                    trHtml += `</tr>`;
                    tbody.innerHTML += trHtml;
                });
            } catch(e) {
                console.error(e);
                document.getElementById('tbody-report').innerHTML = '<tr><td colspan="6" style="text-align:center; color: var(--danger)">查询失败</td></tr>';
            }
        });

        document.getElementById('btn-export-report').addEventListener('click', async () => {
            try {
                const devRes = await fetch('/api/v1/devices');
                const devices = await devRes.json();
                let deviceIdQuery = '';
                if (devices && devices.length > 0) {
                    deviceIdQuery = `deviceId=${encodeURIComponent(devices[0].deviceId)}&`;
                }
                
                const from = new Date(document.getElementById('report-from').value).toISOString();
                const to = new Date(document.getElementById('report-to').value).toISOString();
                window.location.href = `/api/v1/results/nmhc/export.csv?${deviceIdQuery}from=${encodeURIComponent(from)}&to=${encodeURIComponent(to)}`;
            } catch(e) {
                window.showToast('导出失败: ' + e.message, true);
            }
        });
    }, 0);
}
