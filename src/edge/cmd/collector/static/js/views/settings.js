export function initSettings() {
    const container = document.getElementById('view-settings');
    container.innerHTML = `
        <div class="control-group">
            <h3>温度设定 (Cmd 8)</h3>
            进样口: <input type="number" id="set-temp-inj" class="input" value="120" style="width: 80px;"> ℃
            柱温: <input type="number" id="set-temp-col" class="input" value="80" style="width: 80px;"> ℃
            检测器: <input type="number" id="set-temp-det" class="input" value="150" style="width: 80px;"> ℃
            <button class="btn" id="btn-apply-temp">下发控温</button>
        </div>
        <div class="control-group">
            <h3>点火控制 (Cmd 20/21)</h3>
            <button class="btn" id="btn-ignite-fid1">🔥 FID1 点火</button>
        </div>
    `;

    setTimeout(() => {
        document.getElementById('btn-apply-temp').addEventListener('click', async () => {
            const inj = parseFloat(document.getElementById('set-temp-inj').value);
            try {
                await fetch('/api/control/temp', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ zone: 'Inj1', target: inj })
                });
                alert('进样口控温指令已下发!');
            } catch(e) {
                alert('发送失败');
            }
        });
        
        document.getElementById('btn-ignite-fid1').addEventListener('click', async () => {
            try {
                await fetch('/api/control/ignite', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ action: 'start', detector: 'FID1' })
                });
                alert('FID1 点火指令已下发!');
            } catch(e) {
                alert('发送失败');
            }
        });
    }, 0);
}
