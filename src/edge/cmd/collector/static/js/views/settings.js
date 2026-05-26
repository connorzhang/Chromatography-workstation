export function initSettings() {
    const container = document.getElementById('view-settings');
    container.innerHTML = `
        <div class="settings-container">
            <div class="settings-tabs">
                <button class="tab-btn active" data-target="tab-inst1">仪器参数1</button>
                <button class="tab-btn" data-target="tab-inst2">仪器参数2</button>
                <button class="tab-btn" data-target="tab-upload">上传参数</button>
                <button class="tab-btn" data-target="tab-log">log</button>
                <button class="tab-btn" data-target="tab-daq">数采仪</button>
            </div>
            
            <div class="tab-content active" id="tab-inst1">
                <div class="control-group">
                    <h3 style="margin-top:0;">外部事件</h3>
                    <table class="settings-table">
                        <thead>
                            <tr>
                                <th></th>
                                <th>事件1</th><th>事件2</th><th>事件3</th><th>事件4</th>
                                <th>事件5</th><th>事件6</th><th>事件7</th><th>事件8</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>吸合1</td>
                                <td><input type="number" id="ev-on-1" class="input-cell" value="0.01"></td>
                                <td><input type="number" id="ev-on-2" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-3" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-4" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-5" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-6" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-7" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-on-8" class="input-cell" value="0"></td>
                            </tr>
                            <tr>
                                <td>释放1</td>
                                <td><input type="number" id="ev-off-1" class="input-cell" value="0.8"></td>
                                <td><input type="number" id="ev-off-2" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-3" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-4" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-5" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-6" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-7" class="input-cell" value="0"></td>
                                <td><input type="number" id="ev-off-8" class="input-cell" value="0"></td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="text-right; margin-top: 10px; display: flex; justify-content: flex-end; gap: 10px;">
                        <button class="btn" id="btn-query-events">查询</button>
                        <button class="btn" id="btn-apply-events">设定</button>
                    </div>
                </div>

                <div class="control-group">
                    <h3 style="margin-top:0;">气路控制</h3>
                    <table class="settings-table">
                        <thead>
                            <tr>
                                <th>名称</th><th>实测值</th><th>设定值</th><th>操作</th>
                                <th>名称</th><th>实测值</th><th>设定值</th><th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>载气1(psi)</td>
                                <td id="real-epc-carrier1">0.00</td>
                                <td><input type="number" id="set-epc-carrier1" class="input-cell" value="13.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Carrier1')">设定</button></td>
                                
                                <td>载气2(psi)</td>
                                <td id="real-epc-carrier2">0.00</td>
                                <td><input type="number" id="set-epc-carrier2" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Carrier2')">设定</button></td>
                            </tr>
                            <tr>
                                <td>氢气1(ml/min)</td>
                                <td id="real-epc-h2-1">0.00</td>
                                <td><input type="number" id="set-epc-h2-1" class="input-cell" value="60.00"></td>
                                <td><button class="btn" onclick="window.setEPC('H2_1')">设定</button></td>
                                
                                <td>氢气2(ml/min)</td>
                                <td id="real-epc-h2-2">0.00</td>
                                <td><input type="number" id="set-epc-h2-2" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('H2_2')">设定</button></td>
                            </tr>
                            <tr>
                                <td>空气1(ml/min)</td>
                                <td id="real-epc-air-1">0.00</td>
                                <td><input type="number" id="set-epc-air-1" class="input-cell" value="200.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Air1')">设定</button></td>
                                
                                <td>空气2(ml/min)</td>
                                <td id="real-epc-air-2">0.00</td>
                                <td><input type="number" id="set-epc-air-2" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Air2')">设定</button></td>
                            </tr>
                            <tr>
                                <td>辅助气(psi)</td>
                                <td id="real-epc-aux">0.00</td>
                                <td><input type="number" id="set-epc-aux" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Aux')">设定</button></td>
                                <td colspan="4"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="tab-content" id="tab-inst2">
                <div style="display: flex; gap: 20px;">
                    <div class="control-group" style="flex: 1;">
                        <h3 style="margin-top:0;">温度控制</h3>
                        <table class="settings-table">
                            <thead>
                                <tr>
                                    <th>名称</th><th>实测(℃)</th><th>设定(℃)</th><th>保护(℃)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>柱箱</td><td id="real-temp-col">0.0</td>
                                    <td><input type="number" id="set-temp-col" class="input-cell" value="100"></td>
                                    <td><input type="number" id="prot-temp-col" class="input-cell" value=""></td>
                                </tr>
                                <tr>
                                    <td>阀温</td><td id="real-temp-valve">0.0</td>
                                    <td><input type="number" id="set-temp-valve" class="input-cell" value="100"></td>
                                    <td><input type="number" id="prot-temp-valve" class="input-cell" value=""></td>
                                </tr>
                                <tr>
                                    <td>检测1</td><td id="real-temp-det1">0.0</td>
                                    <td><input type="number" id="set-temp-det1" class="input-cell" value="220"></td>
                                    <td><input type="number" id="prot-temp-det1" class="input-cell" value=""></td>
                                </tr>
                                <tr>
                                    <td>进样2</td><td id="real-temp-inj2">0.0</td>
                                    <td><input type="number" id="set-temp-inj2" class="input-cell" value="100"></td>
                                    <td><input type="number" id="prot-temp-inj2" class="input-cell" value=""></td>
                                </tr>
                                <tr>
                                    <td>检测2</td><td id="real-temp-det2">0.0</td>
                                    <td><input type="number" id="set-temp-det2" class="input-cell" value="0"></td>
                                    <td><input type="number" id="prot-temp-det2" class="input-cell" value=""></td>
                                </tr>
                            </tbody>
                        </table>
                        <div style="margin-top: 10px; display: flex; gap: 10px;">
                            <button class="btn btn-danger">关闭控温</button>
                            <button class="btn" id="btn-query-temp">查询</button>
                            <button class="btn" id="btn-apply-temp">设定</button>
                        </div>
                    </div>

                    <div class="control-group" style="flex: 1;">
                        <h3 style="margin-top:0;">点火与时间设定</h3>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 80px;">点火门限1</span>
                            <input type="number" id="set-ignite-th1" class="input" value="1">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 80px;">点火门限2</span>
                            <input type="number" id="set-ignite-th2" class="input" value="1">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 80px;">点火时长</span>
                            <input type="number" id="set-ignite-dur" class="input" value="10">
                        </div>
                        <button class="btn" id="btn-apply-ignite-config" style="width: 100%; margin-bottom: 20px;">设定</button>

                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 90px;">采集次数(次):</span>
                            <input type="number" id="set-time-cycle-max" class="input" value="9999999">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 90px;">通道1(min):</span>
                            <input type="number" id="set-time-acq" class="input" step="0.1" value="2">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 90px;">通道2(min):</span>
                            <input type="number" id="set-time-cycle" class="input" step="0.1" value="0">
                        </div>
                        <button class="btn" id="btn-apply-time" style="width: 100%;">保存并应用</button>
                    </div>
                </div>
            </div>

            <div class="tab-content" id="tab-upload">
                <div style="display: flex; gap: 20px;">
                    <div style="flex: 2;">
                        <table class="settings-table" style="margin-bottom: 20px;">
                            <thead>
                                <tr>
                                    <th></th><th>量程下限</th><th>量程1上限</th><th>量程2上限</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>总烃</td>
                                    <td><input type="number" id="range-thc-0" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-thc-1" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-thc-2" class="input-cell" value="0"></td>
                                </tr>
                                <tr>
                                    <td>甲烷</td>
                                    <td><input type="number" id="range-ch4-0" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-ch4-1" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-ch4-2" class="input-cell" value="0"></td>
                                </tr>
                                <tr>
                                    <td>非甲烷总烃</td>
                                    <td><input type="number" id="range-nmhc-0" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-nmhc-1" class="input-cell" value="0"></td>
                                    <td><input type="number" id="range-nmhc-2" class="input-cell" value="0"></td>
                                </tr>
                            </tbody>
                        </table>
                        
                        <div style="margin-bottom: 20px;">
                            <label><input type="checkbox" id="use-420ma"> 使用4-20mA</label>
                            <button class="btn" style="margin-left: 20px;">保存设置</button>
                        </div>
                        
                        <div class="control-group">
                            <h3 style="margin-top:0;">温度设置</h3>
                            <div style="display: flex; gap: 10px; margin-bottom: 5px;">
                                <span style="width: 100px;">富集温度(℃):</span>
                                <input type="number" id="set-enrich-temp" class="input" style="width: 80px;" value="0">
                            </div>
                            <div style="display: flex; gap: 10px; margin-bottom: 5px;">
                                <span style="width: 100px;">解析温度(℃):</span>
                                <input type="number" id="set-desorb-temp" class="input" style="width: 80px;" value="0">
                            </div>
                            <div style="display: flex; gap: 10px; margin-top: 15px; align-items: center;">
                                <span style="width: 100px;">样品流量(ml):</span>
                                <input type="number" id="set-sample-flow" class="input" style="width: 80px;" value="0.00">
                                <button class="btn">设定</button>
                            </div>
                        </div>
                    </div>
                    
                    <div style="flex: 1;" class="control-group">
                        <h3 style="margin-top:0;">流程控制</h3>
                        <div style="margin-bottom: 20px;">
                            <div>富集时长(s):</div>
                            <textarea id="set-enrich-time" class="input" style="width: 100%; height: 80px;">0</textarea>
                        </div>
                        <div style="margin-bottom: 20px;">
                            <div>解析时长(s):</div>
                            <textarea id="set-desorb-time" class="input" style="width: 100%; height: 80px;">0</textarea>
                        </div>
                        <button class="btn" id="btn-apply-upload" style="width: 100%;">保存并应用</button>
                    </div>
                </div>
            </div>

            <div class="tab-content" id="tab-log">
                <div style="display: flex; gap: 10px; height: 100%;">
                    <textarea style="flex: 1; height: 300px; background: var(--panel); color: #fff; border: 1px solid #334155;" readonly></textarea>
                    <textarea style="flex: 1; height: 300px; background: var(--panel); color: #fff; border: 1px solid #334155;" readonly></textarea>
                    <textarea style="flex: 1; height: 300px; background: var(--panel); color: #fff; border: 1px solid #334155;" readonly></textarea>
                </div>
            </div>

            <div class="tab-content" id="tab-daq">
                <div style="max-width: 400px; margin: 40px auto; padding: 20px; border: 1px solid #334155; border-radius: 8px;">
                    <div style="display: flex; margin-bottom: 15px; align-items: center;">
                        <span style="width: 100px;">设备号</span>
                        <input type="text" id="daq-device-no" class="input" style="flex: 1;" value="1A1GBHKL9011202180011101">
                    </div>
                    <div style="display: flex; margin-bottom: 15px; align-items: center;">
                        <span style="width: 100px;">上传IP</span>
                        <input type="text" id="daq-upload-ip" class="input" style="flex: 1;" value="192.168.1.105">
                    </div>
                    <div style="display: flex; margin-bottom: 15px; align-items: center;">
                        <span style="width: 100px;">上传端口号</span>
                        <input type="text" id="daq-upload-port" class="input" style="flex: 1;" value="5300">
                    </div>
                    <div style="display: flex; margin-bottom: 15px; align-items: center;">
                        <span style="width: 100px;">色谱IP</span>
                        <input type="text" id="daq-chrom-ip" class="input" style="flex: 1;" value="192.168.1.20">
                    </div>
                    <div style="margin-bottom: 20px; padding-left: 100px;">
                        <label style="color: #10b981;"><input type="checkbox" id="daq-enable" checked> 上传</label>
                    </div>
                    <button class="btn" id="btn-apply-daq" style="width: 100%;">保存并应用</button>
                </div>
            </div>
        </div>
    `;

    // Tab switching logic
    const tabs = container.querySelectorAll('.tab-btn');
    const contents = container.querySelectorAll('.tab-content');
    
    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            tabs.forEach(t => t.classList.remove('active'));
            contents.forEach(c => c.classList.remove('active'));
            tab.classList.add('active');
            container.querySelector('#' + tab.dataset.target).classList.add('active');
        });
    });

    let uiSettings = {};
    let hwSettings = {};
    let uploadSettings = {};
    let deviceId = "DEV001";

    setTimeout(async () => {
        try {
            const devRes = await fetch('/api/v1/devices');
            const devices = await devRes.json();
            if(devices && devices.length > 0) {
                deviceId = devices[0].deviceId;
            }

            // Load UI Settings (Time settings)
            const uiRes = await fetch('/api/v1/ui?deviceId=' + encodeURIComponent(deviceId));
            if (uiRes.ok) {
                uiSettings = await uiRes.json();
                if (uiSettings.acqMin !== undefined) document.getElementById('set-time-acq').value = uiSettings.acqMin;
                if (uiSettings.cycleMin !== undefined) document.getElementById('set-time-cycle').value = uiSettings.cycleMin;
                if (uiSettings.cycleMax !== undefined) document.getElementById('set-time-cycle-max').value = uiSettings.cycleMax;
            }

            // Load Hardware Settings
            const hwRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
            if (hwRes.ok) {
                hwSettings = await hwRes.json();
                
                // Populate Events
                if (hwSettings.events && hwSettings.events.length > 0) {
                    for (let i = 0; i < 8 && i < hwSettings.events.length; i++) {
                        const evt = hwSettings.events[i];
                        if (evt.event_mask === 1) { // Assume 1 is on, 0 is off for now, logic may vary
                            document.getElementById('ev-on-' + (i+1)).value = evt.time;
                        } else {
                            document.getElementById('ev-off-' + (i+1)).value = evt.time;
                        }
                    }
                }

                // Populate EPCs
                if (hwSettings.epcs) {
                    if (hwSettings.epcs['Carrier1'] !== undefined) document.getElementById('set-epc-carrier1').value = hwSettings.epcs['Carrier1'];
                    if (hwSettings.epcs['H2_1'] !== undefined) document.getElementById('set-epc-h2-1').value = hwSettings.epcs['H2_1'];
                    if (hwSettings.epcs['Air1'] !== undefined) document.getElementById('set-epc-air-1').value = hwSettings.epcs['Air1'];
                    if (hwSettings.epcs['Aux'] !== undefined) document.getElementById('set-epc-aux').value = hwSettings.epcs['Aux'];
                    if (hwSettings.epcs['Carrier2'] !== undefined) document.getElementById('set-epc-carrier2').value = hwSettings.epcs['Carrier2'];
                    if (hwSettings.epcs['H2_2'] !== undefined) document.getElementById('set-epc-h2-2').value = hwSettings.epcs['H2_2'];
                    if (hwSettings.epcs['Air2'] !== undefined) document.getElementById('set-epc-air-2').value = hwSettings.epcs['Air2'];
                }

                // Populate Temps
                if (hwSettings.temperatures) {
                    if (hwSettings.temperatures['Col'] !== undefined) document.getElementById('set-temp-col').value = hwSettings.temperatures['Col'];
                    if (hwSettings.temperatures['Valve'] !== undefined) document.getElementById('set-temp-valve').value = hwSettings.temperatures['Valve'];
                    if (hwSettings.temperatures['Det1'] !== undefined) document.getElementById('set-temp-det1').value = hwSettings.temperatures['Det1'];
                    if (hwSettings.temperatures['Inj2'] !== undefined) document.getElementById('set-temp-inj2').value = hwSettings.temperatures['Inj2'];
                    if (hwSettings.temperatures['Det2'] !== undefined) document.getElementById('set-temp-det2').value = hwSettings.temperatures['Det2'];
                }

                if (hwSettings.igniteThreshold1 !== undefined) document.getElementById('set-ignite-th1').value = hwSettings.igniteThreshold1;
                if (hwSettings.igniteThreshold2 !== undefined) document.getElementById('set-ignite-th2').value = hwSettings.igniteThreshold2;
                if (hwSettings.igniteDuration !== undefined) document.getElementById('set-ignite-dur').value = hwSettings.igniteDuration;
            }

            // Load Upload Config
            const upRes = await fetch('/api/v1/uploadconfig?deviceId=' + encodeURIComponent(deviceId));
            if (upRes.ok) {
                uploadSettings = await upRes.json();
                
                if (uploadSettings.ranges) {
                    ['thc', 'ch4', 'nmhc'].forEach(key => {
                        const r = uploadSettings.ranges[key.toUpperCase()] || [0, 0, 0];
                        document.getElementById('range-' + key + '-0').value = r[0];
                        document.getElementById('range-' + key + '-1').value = r[1];
                        document.getElementById('range-' + key + '-2').value = r[2];
                    });
                }
                
                if (uploadSettings.use420mA !== undefined) document.getElementById('use-420ma').checked = uploadSettings.use420mA;
                if (uploadSettings.enrichTemp !== undefined) document.getElementById('set-enrich-temp').value = uploadSettings.enrichTemp;
                if (uploadSettings.desorbTemp !== undefined) document.getElementById('set-desorb-temp').value = uploadSettings.desorbTemp;
                if (uploadSettings.sampleFlow !== undefined) document.getElementById('set-sample-flow').value = uploadSettings.sampleFlow;
                if (uploadSettings.enrichTime !== undefined) document.getElementById('set-enrich-time').value = uploadSettings.enrichTime;
                if (uploadSettings.desorbTime !== undefined) document.getElementById('set-desorb-time').value = uploadSettings.desorbTime;

                if (uploadSettings.deviceNo !== undefined) document.getElementById('daq-device-no').value = uploadSettings.deviceNo;
                if (uploadSettings.uploadIP !== undefined) document.getElementById('daq-upload-ip').value = uploadSettings.uploadIP;
                if (uploadSettings.uploadPort !== undefined) document.getElementById('daq-upload-port').value = uploadSettings.uploadPort;
                if (uploadSettings.chromatographIP !== undefined) document.getElementById('daq-chrom-ip').value = uploadSettings.chromatographIP;
                if (uploadSettings.enableUpload !== undefined) document.getElementById('daq-enable').checked = uploadSettings.enableUpload;
            }

            // Setup SSE for real-time telemetry updates
            const evtSource = new EventSource('/events');
            evtSource.onmessage = function(event) {
                try {
                    const parsed = JSON.parse(event.data);
                    if (parsed.type === 'telemetry') {
                        if (parsed.tempInj1 !== undefined) {
                            const elCol = document.getElementById('real-temp-col');
                            const elDet1 = document.getElementById('real-temp-det1');
                            const elValve = document.getElementById('real-temp-valve');
                            
                            if (elCol) elCol.innerText = (parsed.tempCol || 0).toFixed(1);
                            if (elDet1) elDet1.innerText = (parsed.tempDet1 || 0).toFixed(1);
                            if (elValve) elValve.innerText = (parsed.tempInj2 || 0).toFixed(1);
                        }
                        
                        if (parsed.carrierPsi !== undefined || parsed.epc) {
                            if (parsed.carrierPsi !== undefined) {
                                document.getElementById('real-epc-carrier1').innerText = (parsed.carrierPsi || 0).toFixed(2);
                                document.getElementById('real-epc-h2-1').innerText = (parsed.h2Psi || 0).toFixed(2);
                                document.getElementById('real-epc-air-1').innerText = (parsed.airPsi || 0).toFixed(2);
                            } else if (parsed.epc && parsed.epc.length >= 3) {
                                document.getElementById('real-epc-carrier1').innerText = (parsed.epc[0].psi || 0).toFixed(2);
                                document.getElementById('real-epc-h2-1').innerText = (parsed.epc[1].psi || 0).toFixed(2);
                                document.getElementById('real-epc-air-1').innerText = (parsed.epc[2].psi || 0).toFixed(2);
                            }
                        }
                    }
                } catch(e) {}
            };

        } catch (e) {
            console.error('Failed to init settings', e);
        }

        // Events Apply
        document.getElementById('btn-apply-events').addEventListener('click', async () => {
            const events = [];
            for (let i = 1; i <= 8; i++) {
                const onTime = parseFloat(document.getElementById('ev-on-' + i).value);
                const offTime = parseFloat(document.getElementById('ev-off-' + i).value);
                
                // Very simplified event logic mapping for UI
                if (!isNaN(onTime) && onTime > 0) {
                    events.push({ time: onTime, event_mask: 1 }); // 1 for ON
                }
                if (!isNaN(offTime) && offTime > 0) {
                    events.push({ time: offTime, event_mask: 0 }); // 0 for OFF
                }
            }
            
            // Sort events by time
            events.sort((a, b) => a.time - b.time);
            hwSettings.events = events;

            try {
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });
                
                const res = await fetch('/api/control/events', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(events)
                });
                if (res.ok) window.showToast('事件程序已下发!');
                else window.showToast('发送失败', true);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // Global function for EPC setting
        window.setEPC = async function(zone) {
            let val = 0;
            if (zone === 'Carrier1') val = parseFloat(document.getElementById('set-epc-carrier1').value) || 0;
            else if (zone === 'Carrier2') val = parseFloat(document.getElementById('set-epc-carrier2').value) || 0;
            else if (zone === 'H2_1') val = parseFloat(document.getElementById('set-epc-h2-1').value) || 0;
            else if (zone === 'H2_2') val = parseFloat(document.getElementById('set-epc-h2-2').value) || 0;
            else if (zone === 'Air1') val = parseFloat(document.getElementById('set-epc-air-1').value) || 0;
            else if (zone === 'Air2') val = parseFloat(document.getElementById('set-epc-air-2').value) || 0;
            else if (zone === 'Aux') val = parseFloat(document.getElementById('set-epc-aux').value) || 0;

            if (!hwSettings.epcs) hwSettings.epcs = {};
            hwSettings.epcs[zone] = val;

            try {
                // Save to hardware config endpoint
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });
                
                // Issue control command (assuming backend maps these appropriately)
                const res = await fetch('/api/control/epc', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ targets: { [zone]: val } })
                });
                
                if (res.ok) window.showToast('气路 [' + zone + '] 指令已下发!');
                else window.showToast('气路下发失败', true);
            } catch (e) {
                window.showToast('异常: ' + e.message, true);
            }
        };

        // Ignite Config Apply
        const btnIgniteConfig = document.getElementById('btn-apply-ignite-config');
        if (btnIgniteConfig) {
            btnIgniteConfig.addEventListener('click', async () => {
                hwSettings.igniteThreshold1 = parseFloat(document.getElementById('set-ignite-th1').value) || 0;
                hwSettings.igniteThreshold2 = parseFloat(document.getElementById('set-ignite-th2').value) || 0;
                hwSettings.igniteDuration = parseFloat(document.getElementById('set-ignite-dur').value) || 0;
                try {
                    const res = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify(hwSettings)
                    });
                    if (res.ok) window.showToast('点火参数已保存!');
                    else window.showToast('保存失败', true);
                } catch(e) {
                    window.showToast('异常: ' + e.message, true);
                }
            });
        }

        // Temperature Apply
        document.getElementById('btn-apply-temp').addEventListener('click', async () => {
            if (!hwSettings.temperatures) hwSettings.temperatures = {};
            hwSettings.temperatures['Col'] = parseFloat(document.getElementById('set-temp-col').value) || 0;
            hwSettings.temperatures['Valve'] = parseFloat(document.getElementById('set-temp-valve').value) || 0;
            hwSettings.temperatures['Det1'] = parseFloat(document.getElementById('set-temp-det1').value) || 0;
            hwSettings.temperatures['Inj2'] = parseFloat(document.getElementById('set-temp-inj2').value) || 0;
            hwSettings.temperatures['Det2'] = parseFloat(document.getElementById('set-temp-det2').value) || 0;

            try {
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });
                
                const res = await fetch('/api/control/temp', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ targets: hwSettings.temperatures })
                });
                if (res.ok) window.showToast('温度控制指令已下发!');
                else window.showToast('发送失败', true);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // Time / UI Settings Apply
        document.getElementById('btn-apply-time').addEventListener('click', async () => {
            uiSettings.acqMin = parseFloat(document.getElementById('set-time-acq').value) || 0;
            uiSettings.cycleMin = parseFloat(document.getElementById('set-time-cycle').value) || 0;
            uiSettings.cycleMax = parseInt(document.getElementById('set-time-cycle-max').value) || 9999999;
            uiSettings.deviceId = deviceId;
            
            try {
                const res = await fetch('/api/v1/ui', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(uiSettings)
                });
                if (res.ok) window.showToast('时间和循环参数已保存!');
                else window.showToast('保存失败', true);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // Upload/Process Config Apply
        document.getElementById('btn-apply-upload').addEventListener('click', async () => {
            uploadSettings.ranges = {
                'THC': [
                    parseFloat(document.getElementById('range-thc-0').value)||0,
                    parseFloat(document.getElementById('range-thc-1').value)||0,
                    parseFloat(document.getElementById('range-thc-2').value)||0
                ],
                'CH4': [
                    parseFloat(document.getElementById('range-ch4-0').value)||0,
                    parseFloat(document.getElementById('range-ch4-1').value)||0,
                    parseFloat(document.getElementById('range-ch4-2').value)||0
                ],
                'NMHC': [
                    parseFloat(document.getElementById('range-nmhc-0').value)||0,
                    parseFloat(document.getElementById('range-nmhc-1').value)||0,
                    parseFloat(document.getElementById('range-nmhc-2').value)||0
                ]
            };
            uploadSettings.use420mA = document.getElementById('use-420ma').checked;
            uploadSettings.enrichTemp = parseFloat(document.getElementById('set-enrich-temp').value)||0;
            uploadSettings.desorbTemp = parseFloat(document.getElementById('set-desorb-temp').value)||0;
            uploadSettings.sampleFlow = parseFloat(document.getElementById('set-sample-flow').value)||0;
            uploadSettings.enrichTime = parseFloat(document.getElementById('set-enrich-time').value)||0;
            uploadSettings.desorbTime = parseFloat(document.getElementById('set-desorb-time').value)||0;

            try {
                const res = await fetch('/api/v1/uploadconfig?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(uploadSettings)
                });
                if (res.ok) window.showToast('上传与流程参数已保存!');
                else window.showToast('保存失败', true);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // DAQ Config Apply
        document.getElementById('btn-apply-daq').addEventListener('click', async () => {
            uploadSettings.deviceNo = document.getElementById('daq-device-no').value;
            uploadSettings.uploadIP = document.getElementById('daq-upload-ip').value;
            uploadSettings.uploadPort = parseInt(document.getElementById('daq-upload-port').value)||0;
            uploadSettings.chromatographIP = document.getElementById('daq-chrom-ip').value;
            uploadSettings.enableUpload = document.getElementById('daq-enable').checked;

            try {
                const res = await fetch('/api/v1/uploadconfig?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(uploadSettings)
                });
                if (res.ok) window.showToast('数采仪配置已保存!');
                else window.showToast('保存失败', true);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

    }, 0);
}