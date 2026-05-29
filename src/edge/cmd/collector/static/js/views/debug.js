export function initDebug() {
    const container = document.getElementById('view-debug');
    container.innerHTML = `
        <div class="card" style="margin-bottom: 20px; text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">温控模块 Modbus 连接设置</h3>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">串口号:</label>
                    <input type="text" id="modbus-port" value="/dev/ttyUSB3" class="input" style="width: 150px; margin-right: 0;">
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">从机地址:</label>
                    <input type="number" id="modbus-slave" value="20" class="input" style="width: 80px; margin-right: 0;">
                </div>
                <button class="btn" id="btn-modbus-connect">连接</button>
                <button class="btn btn-danger" id="btn-modbus-disconnect">断开</button>
                <div style="margin-left: auto; display: flex; align-items: center; gap: 8px;">
                    <span style="color: #94a3b8;">状态:</span>
                    <span id="modbus-status" style="font-weight: bold; color: #94a3b8;">未连接</span>
                </div>
            </div>
        </div>

        <div class="card" style="margin-bottom: 20px; text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">实时温度监控</h3>
            <div class="card-grid" style="margin-top: 15px;" id="modbus-channels-container">
                <!-- 通道由 JS 动态生成 -->
            </div>
        </div>

        <div class="card" style="text-align: left; margin-bottom: 20px;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">单通道设定</h3>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">通道:</label>
                    <select id="modbus-set-channel" class="input" style="margin-right: 0; width: 100px;">
                        <option value="1">CH 1</option>
                        <option value="2">CH 2</option>
                        <option value="3">CH 3</option>
                        <option value="4">CH 4</option>
                        <option value="5">CH 5</option>
                        <option value="6">CH 6</option>
                        <option value="7">CH 7</option>
                        <option value="8">CH 8</option>
                    </select>
                </div>
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">目标温度 (℃):</label>
                    <input type="number" id="modbus-set-temp" value="100" class="input" style="width: 100px; margin-right: 0;">
                </div>
                <button class="btn" id="btn-modbus-set">下发设定</button>
            </div>
        </div>

        <div class="card" style="text-align: left;">
            <h3 style="margin-top: 0; border-bottom: 1px solid #334155; padding-bottom: 10px; color: var(--text);">开关量输出测试 (IO 模式)</h3>
            <p style="font-size: 12px; color: #94a3b8; margin-top: 10px;">注意: 该功能要求在寄存器78将对应的模式设为1。测试主要针对 CH5 - CH8 (线圈地址 36-39)。</p>
            <div style="display: flex; gap: 15px; align-items: center; margin-top: 15px; flex-wrap: wrap;">
                <div style="display: flex; align-items: center; gap: 8px;">
                    <label style="color: #94a3b8;">CH 5:</label>
                    <button class="btn btn-io-toggle" data-ch="5" data-state="1" style="background-color: #2e7d32;">开</button>
                    <button class="btn btn-io-toggle" data-ch="5" data-state="0" style="background-color: #d32f2f;">关</button>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <label style="color: #94a3b8;">CH 6:</label>
                    <button class="btn btn-io-toggle" data-ch="6" data-state="1" style="background-color: #2e7d32;">开</button>
                    <button class="btn btn-io-toggle" data-ch="6" data-state="0" style="background-color: #d32f2f;">关</button>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <label style="color: #94a3b8;">CH 7:</label>
                    <button class="btn btn-io-toggle" data-ch="7" data-state="1" style="background-color: #2e7d32;">开</button>
                    <button class="btn btn-io-toggle" data-ch="7" data-state="0" style="background-color: #d32f2f;">关</button>
                </div>
                <div style="display: flex; align-items: center; gap: 8px; margin-left: 20px;">
                    <label style="color: #94a3b8;">CH 8:</label>
                    <button class="btn btn-io-toggle" data-ch="8" data-state="1" style="background-color: #2e7d32;">开</button>
                    <button class="btn btn-io-toggle" data-ch="8" data-state="0" style="background-color: #d32f2f;">关</button>
                </div>
            </div>
        </div>
    `;

    const channelsContainer = document.getElementById('modbus-channels-container');
    for (let i = 1; i <= 8; i++) {
        channelsContainer.innerHTML += `
            <div style="border: 1px solid #334155; padding: 15px; border-radius: 6px; background: #0f172a; position: relative;">
                <div style="font-weight: bold; margin-bottom: 12px; color: var(--accent); border-bottom: 1px dashed #334155; padding-bottom: 8px; display: flex; justify-content: space-between; align-items: center;">
                    <span>CH ${i}</span>
                    <span id="modbus-ch${i}-status" style="font-size: 12px; font-weight: normal; color: #94a3b8;">未知</span>
                </div>
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 8px;">
                    <span style="color: #94a3b8; font-size: 14px;">当前模式:</span>
                    <div style="display: flex; align-items: center; gap: 6px;">
                        <span id="modbus-ch${i}-mode-text" style="font-size: 13px; font-weight: bold; color: var(--text);">--</span>
                        <button class="btn btn-mode-toggle" data-ch="${i}" style="padding: 2px 8px; font-size: 11px; background: #334155;">切换</button>
                    </div>
                </div>
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 8px;">
                    <span style="color: #94a3b8; font-size: 14px;">设定温度:</span>
                    <span><span id="modbus-ch${i}-set" style="font-weight: bold;">--</span> <span style="color: #94a3b8; font-size: 12px;">℃</span></span>
                </div>
                <div style="display: flex; justify-content: space-between; align-items: center;">
                    <span style="color: #94a3b8; font-size: 14px;">实时温度:</span>
                    <span style="color: var(--text);"><span id="modbus-ch${i}-rt" style="font-weight: bold; font-size: 1.4em;">--</span> <span style="color: #94a3b8; font-size: 12px;">℃</span></span>
                </div>
            </div>
        `;
    }

    let pollInterval = null;

    document.getElementById('btn-modbus-connect').addEventListener('click', async () => {
        const port = document.getElementById('modbus-port').value;
        const slave = parseInt(document.getElementById('modbus-slave').value);
        try {
            const res = await fetch('/api/v1/modbus_temp/connect', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ port, slave_id: slave })
            });
            if (res.ok) {
                window.showToast('温控模块连接成功');
                document.getElementById('modbus-status').innerText = '已连接';
                document.getElementById('modbus-status').style.color = 'var(--success)';
                if (!pollInterval) {
                    pollInterval = setInterval(pollModbusState, 1000);
                }
            } else {
                const data = await res.json();
                window.showToast('连接失败: ' + data.error, true);
            }
        } catch (e) {
            window.showToast('连接请求异常', true);
        }
    });

    document.getElementById('btn-modbus-disconnect').addEventListener('click', async () => {
        try {
            await fetch('/api/v1/modbus_temp/disconnect', { method: 'POST' });
            window.showToast('已断开连接');
            document.getElementById('modbus-status').innerText = '未连接';
            document.getElementById('modbus-status').style.color = 'var(--text-muted)';
            if (pollInterval) {
                clearInterval(pollInterval);
                pollInterval = null;
            }
            resetChannels();
        } catch (e) {
            window.showToast('断开请求异常', true);
        }
    });

    document.getElementById('btn-modbus-set').addEventListener('click', async () => {
        const ch = parseInt(document.getElementById('modbus-set-channel').value);
        const temp = parseInt(document.getElementById('modbus-set-temp').value);
        try {
            const res = await fetch('/api/v1/modbus_temp/set', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ channel: ch, target_temp: temp })
            });
            if (res.ok) {
                window.showToast(`CH${ch} 设定温度下发成功`);
                pollModbusState();
            } else {
                const data = await res.json();
                window.showToast('下发失败: ' + data.error, true);
            }
        } catch (e) {
            window.showToast('下发请求异常', true);
        }
    });

    document.querySelectorAll('.btn-io-toggle').forEach(btn => {
        btn.addEventListener('click', async (e) => {
            const ch = parseInt(e.target.getAttribute('data-ch'));
            const state = e.target.getAttribute('data-state') === '1';
            try {
                const res = await fetch('/api/v1/modbus_temp/set_io', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ channel: ch, state: state })
                });
                if (res.ok) {
                    window.showToast(`CH${ch} IO ${state ? '开启' : '关闭'}指令下发成功`);
                } else {
                    const data = await res.json();
                    window.showToast('IO 下发失败: ' + data.error, true);
                }
            } catch (err) {
                window.showToast('IO 下发请求异常', true);
            }
        });
    });

    document.querySelectorAll('.btn-mode-toggle').forEach(btn => {
        btn.addEventListener('click', async (e) => {
            const ch = parseInt(e.target.getAttribute('data-ch'));
            const currentModeText = document.getElementById(`modbus-ch${ch}-mode-text`).innerText;
            const targetMode = currentModeText === 'IO模式' ? 0 : 1; // Toggle 0 and 1
            
            try {
                const res = await fetch('/api/v1/modbus_temp/set_mode', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ channel: ch, mode: targetMode })
                });
                if (res.ok) {
                    window.showToast(`CH${ch} 模式切换指令下发成功`);
                    pollModbusState();
                } else {
                    const data = await res.json();
                    window.showToast('模式切换失败: ' + data.error, true);
                }
            } catch (err) {
                window.showToast('模式切换请求异常', true);
            }
        });
    });

    async function pollModbusState() {
        try {
            const res = await fetch('/api/v1/modbus_temp/state');
            if (res.ok) {
                const data = await res.json();
                if (!data.connected) {
                    document.getElementById('modbus-status').innerText = '连接已断开';
                    document.getElementById('modbus-status').style.color = 'var(--danger)';
                    resetChannels();
                    return;
                }
                document.getElementById('modbus-status').innerText = '已连接 (通信中)';
                document.getElementById('modbus-status').style.color = 'var(--success)';
                
                for (let i = 0; i < 8; i++) {
                    document.getElementById(`modbus-ch${i+1}-set`).innerText = data.set_temps[i];
                    
                    const modeElem = document.getElementById(`modbus-ch${i+1}-mode-text`);
                    if (data.modes && data.modes[i] === 1) {
                        modeElem.innerText = 'IO模式';
                        modeElem.style.color = '#eab308'; // yellow-500
                    } else {
                        modeElem.innerText = '温控模式';
                        modeElem.style.color = '#38bdf8'; // emerald-400
                    }

                    const rtElem = document.getElementById(`modbus-ch${i+1}-rt`);
                    const statusElem = document.getElementById(`modbus-ch${i+1}-status`);
                    
                    if (data.disconnected[i]) {
                        rtElem.innerText = '---';
                        rtElem.style.color = '#94a3b8';
                        statusElem.innerText = '断偶/未连接';
                        statusElem.style.color = 'var(--danger)';
                        statusElem.style.background = 'rgba(239, 68, 68, 0.1)';
                    } else {
                        rtElem.innerText = data.realtime_temps[i].toFixed(2);
                        rtElem.style.color = 'var(--text)';
                        statusElem.innerText = '正常';
                        statusElem.style.color = 'var(--success)';
                        statusElem.style.background = 'rgba(16, 185, 129, 0.1)';
                    }
                }
            }
        } catch (e) {
            console.error('Poll modbus error', e);
        }
    }

    function resetChannels() {
        for (let i = 1; i <= 8; i++) {
            document.getElementById(`modbus-ch${i}-set`).innerText = '--';
            document.getElementById(`modbus-ch${i}-rt`).innerText = '--';
            document.getElementById(`modbus-ch${i}-mode-text`).innerText = '--';
            document.getElementById(`modbus-ch${i}-mode-text`).style.color = 'var(--text)';
            document.getElementById(`modbus-ch${i}-rt`).style.color = 'var(--text)';
            const statusElem = document.getElementById(`modbus-ch${i}-status`);
            statusElem.innerText = '未知';
            statusElem.style.color = '#94a3b8';
            statusElem.style.background = 'transparent';
        }
    }
}
