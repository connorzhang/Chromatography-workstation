export function initSettings() {
    const container = document.getElementById('view-settings');
    container.innerHTML = `
        <div class="control-group">
            <h3>时间设定 (Cmd 23)</h3>
            采集时间(AcqMin): <input type="number" id="set-time-acq" class="input" value="2" style="width: 80px;"> min
            循环周期(CycleMin): <input type="number" id="set-time-cycle" class="input" value="2" style="width: 80px;"> min
            <button class="btn" id="btn-apply-time">保存设定</button>
        </div>
        <div class="control-group">
            <h3>温度设定 (Cmd 8)</h3>
            进样口(Inj1): <input type="number" id="set-temp-inj" class="input" value="120" style="width: 80px;"> ℃
            柱温箱(Col): <input type="number" id="set-temp-col" class="input" value="80" style="width: 80px;"> ℃
            检测器(Det1): <input type="number" id="set-temp-det" class="input" value="150" style="width: 80px;"> ℃
            <button class="btn" id="btn-apply-temp">下发控温</button>
        </div>
        <div class="control-group">
            <h3>气路压力设定 (Cmd 34)</h3>
            载气(Carrier1): <input type="number" id="set-epc-carrier" class="input" value="20" style="width: 80px;"> psi
            氢气(H2): <input type="number" id="set-epc-h2" class="input" value="30" style="width: 80px;"> psi
            空气(Air): <input type="number" id="set-epc-air" class="input" value="300" style="width: 80px;"> psi
            <button class="btn" id="btn-apply-epc">下发气路</button>
        </div>
        <div class="control-group">
            <h3>点火控制 (Cmd 20/21)</h3>
            <button class="btn" id="btn-ignite-fid1">🔥 FID1 点火</button>
        </div>
    `;

    setTimeout(() => {
        // 加载初始设置
        fetch('/api/v1/devices')
            .then(res => res.json())
            .then(data => {
                if(data && data.length > 0) {
                    const devId = data[0].deviceId;
                    fetch('/api/v1/ui?deviceId=' + encodeURIComponent(devId))
                        .then(r => r.json())
                        .then(ui => {
                            if (ui && ui.acqMin) document.getElementById('set-time-acq').value = ui.acqMin;
                            if (ui && ui.cycleMin) document.getElementById('set-time-cycle').value = ui.cycleMin;
                        }).catch(e => console.error(e));
                }
            }).catch(e => console.error(e));

        document.getElementById('btn-apply-time').addEventListener('click', async () => {
            const acq = parseFloat(document.getElementById('set-time-acq').value) || 2;
            const cycle = parseFloat(document.getElementById('set-time-cycle').value) || 2;
            
            try {
                // Get current deviceId
                const devRes = await fetch('/api/v1/devices');
                const devices = await devRes.json();
                const deviceId = (devices && devices.length > 0) ? devices[0].deviceId : "DEV001";

                const res = await fetch('/api/v1/ui', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ 
                        deviceId: deviceId,
                        acqMin: acq,
                        cycleMin: cycle,
                        loop: true
                    })
                });
                const data = await res.json();
                if(res.ok) {
                    window.showToast('时间设定已保存!');
                } else {
                    window.showToast('保存失败: ' + data.error, true);
                }
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        document.getElementById('btn-apply-temp').addEventListener('click', async () => {
            const inj = parseFloat(document.getElementById('set-temp-inj').value) || 0;
            const col = parseFloat(document.getElementById('set-temp-col').value) || 0;
            const det = parseFloat(document.getElementById('set-temp-det').value) || 0;
            
            try {
                const res = await fetch('/api/control/temp', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ 
                        targets: {
                            'Inj1': inj,
                            'Col': col,
                            'Det1': det
                        }
                    })
                });
                const data = await res.json();
                if(res.ok) {
                    window.showToast('温度控制指令已下发!');
                } else {
                    window.showToast('发送失败: ' + data.error, true);
                }
            } catch(e) {
                window.showToast('发送异常: ' + e.message, true);
            }
        });

        document.getElementById('btn-apply-epc').addEventListener('click', async () => {
            const carrier = parseFloat(document.getElementById('set-epc-carrier').value) || 0;
            const h2 = parseFloat(document.getElementById('set-epc-h2').value) || 0;
            const air = parseFloat(document.getElementById('set-epc-air').value) || 0;
            
            try {
                const res = await fetch('/api/control/epc', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ 
                        targets: {
                            'Carrier1': carrier,
                            'H2': h2,
                            'Air': air
                        }
                    })
                });
                const data = await res.json();
                if(res.ok) {
                    window.showToast('气路控制指令已下发!');
                } else {
                    window.showToast('发送失败: ' + data.error, true);
                }
            } catch(e) {
                window.showToast('发送异常: ' + e.message, true);
            }
        });
        
        document.getElementById('btn-ignite-fid1').addEventListener('click', async () => {
            try {
                const res = await fetch('/api/control/ignite', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ action: 'start', detector: 'FID1' })
                });
                const data = await res.json();
                if(res.ok) {
                    window.showToast('FID1 点火指令已下发!');
                } else {
                    window.showToast('发送失败: ' + data.error, true);
                }
            } catch(e) {
                window.showToast('发送异常: ' + e.message, true);
            }
        });
    }, 0);
}
