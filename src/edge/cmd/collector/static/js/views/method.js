export function initMethod() {
    const container = document.getElementById('view-method');
    container.innerHTML = `
        <div class="control-group">
            <h3>校准组分表</h3>
            <table>
                <thead>
                    <tr><th>组分名称</th><th>保留时间</th><th>左窗口</th><th>右窗口</th><th>计算方式</th><th>标气浓度(L1)</th><th>响应值(L1)</th></tr>
                </thead>
                <tbody id="tbody-compounds">
                    <tr><td colspan="7" style="text-align:center">加载中...</td></tr>
                </tbody>
            </table>
        </div>
    `;

    window.addEventListener('load-method', async () => {
        try {
            const res = await fetch('/api/method');
            const data = await res.json();
            const tbody = document.getElementById('tbody-compounds');
            if(!data.compounds || data.compounds.length === 0) {
                tbody.innerHTML = '<tr><td colspan="7" style="text-align:center">暂无组分，请添加</td></tr>';
                return;
            }
            tbody.innerHTML = '';
            data.compounds.forEach(c => {
                let amount = '-';
                let resp = '-';
                if (c.levels && c.levels.length > 0) {
                    amount = c.levels[0].amount.toFixed(2);
                    resp = c.levels[0].response.toFixed(2);
                }
                
                tbody.innerHTML += "<tr>" +
                    "<td><strong>" + c.name + "</strong></td>" +
                    "<td>" + (c.retain_time !== undefined ? c.retain_time.toFixed(3) : '-') + " min</td>" +
                    "<td>" + (c.left_window !== undefined ? c.left_window.toFixed(3) : '-') + "</td>" +
                    "<td>" + (c.right_window !== undefined ? c.right_window.toFixed(3) : '-') + "</td>" +
                    "<td>" + (c.resp_style === 0 ? "面积" : "峰高") + "</td>" +
                    "<td style='color:var(--success)'>" + amount + "</td>" +
                    "<td>" + resp + "</td>" +
                "</tr>";
            });
        } catch(e) { 
            console.error(e); 
            document.getElementById('tbody-compounds').innerHTML = '<tr><td colspan="7" style="text-align:center; color: var(--danger)">加载失败</td></tr>';
        }
    });
}
