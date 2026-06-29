export function initSettings() {
    const container = document.getElementById('view-settings');
    container.innerHTML = `
        <div class="settings-container">
            <div class="settings-tabs">
                <button class="tab-btn active" data-target="tab-inst1">仪器参数1</button>
                <button class="tab-btn" data-target="tab-inst2">仪器参数2</button>
                <button class="tab-btn" data-target="tab-upload">上传参数</button>
                <button class="tab-btn" data-target="tab-log">log</button>
                <button class="tab-btn" data-target="tab-hw-verify" style="color: #38bdf8;">硬件核对</button>
                <button id="btn-show-license" style="background:transparent; color:#10b981; border:none; cursor:pointer; padding: 10px 20px; font-weight:bold;" title="授权状态">🔐授权</button>
                <button id="btn-secret-menu" style="margin-left:auto; background:transparent; color:#64748b; border:none; cursor:pointer; padding: 10px 20px;" title="高级设置">⚙️高级</button>
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
                                <td>载气1(psi / sccm)</td>
                                <td id="real-epc-carrier1">0.00 / 0.00</td>
                                <td><input type="number" id="set-epc-carrier1" class="input-cell" value="13.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Carrier1')">设定</button></td>
                                
                                <td>载气2(psi / sccm)</td>
                                <td id="real-epc-carrier2">0.00 / 0.00</td>
                                <td><input type="number" id="set-epc-carrier2" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Carrier2')">设定</button></td>
                            </tr>
                            <tr>
                                <td>氢气1(psi / sccm)</td>
                                <td id="real-epc-h2-1">0.00 / 0.00</td>
                                <td><input type="number" id="set-epc-h2-1" class="input-cell" value="60.00"></td>
                                <td><button class="btn" onclick="window.setEPC('H2_1')">设定</button></td>
                                
                                <td>氢气2(psi / sccm)</td>
                                <td id="real-epc-h2-2">0.00 / 0.00</td>
                                <td><input type="number" id="set-epc-h2-2" class="input-cell" value="0.00"></td>
                                <td><button class="btn" onclick="window.setEPC('H2_2')">设定</button></td>
                            </tr>
                            <tr>
                                <td>空气1(psi / sccm)</td>
                                <td id="real-epc-air-1">0.00 / 0.00</td>
                                <td><input type="number" id="set-epc-air-1" class="input-cell" value="200.00"></td>
                                <td><button class="btn" onclick="window.setEPC('Air1')">设定</button></td>
                                
                                <td>空气2(psi / sccm)</td>
                                <td id="real-epc-air-2">0.00 / 0.00</td>
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
                        <h3 style="margin-top:0;">温度控制 <span id="status-heating" style="font-size:12px; margin-left:10px; padding: 2px 6px; border-radius: 4px; background-color: #334155; color: #cbd5e1;">状态: 获取中...</span></h3>
        <table class="settings-table">
            <thead>
                <tr>
                    <th>名称</th><th>启用</th><th>实测(℃)</th><th>设定(℃)</th><th>保护(℃)</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>进样1</td>
                    <td><input type="checkbox" id="en-temp-inj1" checked></td>
                    <td id="real-temp-inj1">0.0</td>
                    <td><input type="number" id="set-temp-inj1" class="input-cell" value="100"></td>
                    <td><input type="number" id="prot-temp-inj1" class="input-cell" value="400"></td>
                </tr>
                <tr>
                    <td>柱箱</td>
                    <td><input type="checkbox" id="en-temp-col" checked></td>
                    <td id="real-temp-col">0.0</td>
                    <td><input type="number" id="set-temp-col" class="input-cell" value="100"></td>
                    <td><input type="number" id="prot-temp-col" class="input-cell" value="400"></td>
                </tr>
                <tr>
                    <td>检测1</td>
                    <td><input type="checkbox" id="en-temp-det1" checked></td>
                    <td id="real-temp-det1">0.0</td>
                    <td><input type="number" id="set-temp-det1" class="input-cell" value="220"></td>
                    <td><input type="number" id="prot-temp-det1" class="input-cell" value="400"></td>
                </tr>
                <tr>
                    <td>进样2</td>
                    <td><input type="checkbox" id="en-temp-inj2"></td>
                    <td id="real-temp-inj2">0.0</td>
                    <td><input type="number" id="set-temp-inj2" class="input-cell" value="100"></td>
                    <td><input type="number" id="prot-temp-inj2" class="input-cell" value="400"></td>
                </tr>
                <tr>
                    <td>检测2</td>
                    <td><input type="checkbox" id="en-temp-det2"></td>
                    <td id="real-temp-det2">0.0</td>
                    <td><input type="number" id="set-temp-det2" class="input-cell" value="0"></td>
                    <td><input type="number" id="prot-temp-det2" class="input-cell" value="400"></td>
                </tr>
                <tr>
                    <td>检测3</td>
                    <td><input type="checkbox" id="en-temp-det3"></td>
                    <td id="real-temp-det3">0.0</td>
                    <td><input type="number" id="set-temp-det3" class="input-cell" value="0"></td>
                    <td><input type="number" id="prot-temp-det3" class="input-cell" value="400"></td>
                </tr>
            </tbody>
        </table>
                    <div style="margin-top: 10px; display: flex; gap: 10px;">
                        <button class="btn" id="btn-toggle-temp" style="background-color: #2e7d32; color: white;">开始控温</button>
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
                        <div style="display: flex; gap: 10px; margin-bottom: 20px;">
                            <button class="btn" id="btn-query-ignite-config" style="flex: 1;">查询</button>
                            <button class="btn" id="btn-apply-ignite-config" style="flex: 1;">设定</button>
                        </div>

                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 90px;">循环次数(次):</span>
                            <input type="number" id="set-time-cycle-max" class="input" value="9999999">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 10px;">
                            <span style="width: 90px;">循环间隔(min):</span>
                            <input type="number" id="set-time-cycle" class="input" step="0.1" value="2">
                        </div>
                        <div style="display: flex; gap: 10px; margin-bottom: 20px;">
                            <button class="btn" id="btn-query-time" style="flex: 1;">查询</button>
                            <button class="btn" id="btn-apply-time" style="flex: 1;">设定</button>
                        </div>
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
                <div style="display: flex; flex-direction: column; gap: 10px; height: 100%;">
                    <div style="display: flex; justify-content: space-between; align-items: center;">
                        <div style="display: flex; align-items: center; gap: 15px;">
                            <h3 style="margin: 0;">系统日志</h3>
                            <label style="color: #94a3b8; font-size: 13px; display: flex; align-items: center; gap: 4px; cursor: pointer;">
                                <input type="checkbox" id="chk-log-debug" style="margin:0;"> 硬件通信 (DEBUG)
                            </label>
                            <label style="color: #38bdf8; font-size: 13px; display: flex; align-items: center; gap: 4px; cursor: pointer;">
                                <input type="checkbox" id="chk-log-info" checked style="margin:0;"> 业务信息 (INFO)
                            </label>
                            <label style="color: #facc15; font-size: 13px; display: flex; align-items: center; gap: 4px; cursor: pointer;">
                                <input type="checkbox" id="chk-log-warn" checked style="margin:0;"> 警告 (WARN)
                            </label>
                            <label style="color: #ff6b6b; font-size: 13px; display: flex; align-items: center; gap: 4px; cursor: pointer;">
                                <input type="checkbox" id="chk-log-error" checked style="margin:0;"> 错误 (ERROR)
                            </label>
                        </div>
                        <button class="btn" id="btn-clear-log">清空日志</button>
                    </div>
                    <div id="sys-log-viewer" class="hide-debug" style="flex: 1; height: 500px; background: var(--panel); border: 1px solid #334155; font-family: monospace; padding: 10px; font-size: 13px; overflow-y: auto;"></div>
                </div>
            </div>

            <div class="tab-content" id="tab-hw-verify">
                <div style="display: flex; gap: 20px;">
                    <div class="control-group" style="flex: 1;">
                        <h3 style="margin-top:0;">温度全量核对 (Cmd 143/128)</h3>
                        <table class="settings-table">
            <thead>
                <tr><th>通道</th><th>实时值 (℃)</th><th>设定值 (℃)</th><th>保护值 (℃)</th></tr>
            </thead>
            <tbody>
                <tr><td>Temp 1 (Inj1)</td><td id="vrf-rt-temp1">0.0</td><td id="vrf-st-temp1">0.0</td><td id="vrf-pt-temp1">0.0</td></tr>
                <tr><td>Temp 2 (Col)</td><td id="vrf-rt-temp2">0.0</td><td id="vrf-st-temp2">0.0</td><td id="vrf-pt-temp2">0.0</td></tr>
                <tr><td>Temp 3 (Det1)</td><td id="vrf-rt-temp3">0.0</td><td id="vrf-st-temp3">0.0</td><td id="vrf-pt-temp3">0.0</td></tr>
                <tr><td>Temp 4 (Inj2)</td><td id="vrf-rt-temp4">0.0</td><td id="vrf-st-temp4">0.0</td><td id="vrf-pt-temp4">0.0</td></tr>
                <tr><td>Temp 5 (Det2)</td><td id="vrf-rt-temp5">0.0</td><td id="vrf-st-temp5">0.0</td><td id="vrf-pt-temp5">0.0</td></tr>
                <tr><td>Temp 6 (Det3)</td><td id="vrf-rt-temp6">0.0</td><td id="vrf-st-temp6">0.0</td><td id="vrf-pt-temp6">0.0</td></tr>
            </tbody>
        </table>
                    </div>
                    <div class="control-group" style="flex: 1;">
                        <h3 style="margin-top:0;">气路全量核对 (Cmd 159)</h3>
                        <table class="settings-table">
                            <thead>
                                <tr><th>通道</th><th>实时压力(psi)</th><th>实时流量(sccm)</th><th>设定压力(psi)</th></tr>
                            </thead>
                            <tbody>
                                <tr><td>EPC 1</td><td id="vrf-rt-epc1-psi">0.00</td><td id="vrf-rt-epc1-sccm">0.00</td><td id="vrf-st-epc1-psi">0.00</td></tr>
                                <tr><td>EPC 2</td><td id="vrf-rt-epc2-psi">0.00</td><td id="vrf-rt-epc2-sccm">0.00</td><td id="vrf-st-epc2-psi">0.00</td></tr>
                                <tr><td>EPC 3</td><td id="vrf-rt-epc3-psi">0.00</td><td id="vrf-rt-epc3-sccm">0.00</td><td id="vrf-st-epc3-psi">0.00</td></tr>
                                <tr><td>EPC 4</td><td id="vrf-rt-epc4-psi">0.00</td><td id="vrf-rt-epc4-sccm">0.00</td><td id="vrf-st-epc4-psi">0.00</td></tr>
                                <tr><td>EPC 5</td><td id="vrf-rt-epc5-psi">0.00</td><td id="vrf-rt-epc5-sccm">0.00</td><td id="vrf-st-epc5-psi">0.00</td></tr>
                                <tr><td>EPC 6</td><td id="vrf-rt-epc6-psi">0.00</td><td id="vrf-rt-epc6-sccm">0.00</td><td id="vrf-st-epc6-psi">0.00</td></tr>
                                <tr><td>EPC 7</td><td id="vrf-rt-epc7-psi">0.00</td><td id="vrf-rt-epc7-sccm">0.00</td><td id="vrf-st-epc7-psi">0.00</td></tr>
                                <tr><td>EPC 8</td><td id="vrf-rt-epc8-psi">0.00</td><td id="vrf-rt-epc8-sccm">0.00</td><td id="vrf-st-epc8-psi">0.00</td></tr>
                                <tr><td>EPC 9</td><td id="vrf-rt-epc9-psi">0.00</td><td id="vrf-rt-epc9-sccm">0.00</td><td id="vrf-st-epc9-psi">0.00</td></tr>
                                <tr><td>EPC 10</td><td id="vrf-rt-epc10-psi">0.00</td><td id="vrf-rt-epc10-sccm">0.00</td><td id="vrf-st-epc10-psi">0.00</td></tr>
                                <tr><td>EPC 11</td><td id="vrf-rt-epc11-psi">0.00</td><td id="vrf-rt-epc11-sccm">0.00</td><td id="vrf-st-epc11-psi">0.00</td></tr>
                                <tr><td>EPC 12</td><td id="vrf-rt-epc12-psi">0.00</td><td id="vrf-rt-epc12-sccm">0.00</td><td id="vrf-st-epc12-psi">0.00</td></tr>
                                <tr><td>EPC 13</td><td id="vrf-rt-epc13-psi">0.00</td><td id="vrf-rt-epc13-sccm">0.00</td><td id="vrf-st-epc13-psi">0.00</td></tr>
                                <tr><td>EPC 14</td><td id="vrf-rt-epc14-psi">0.00</td><td id="vrf-rt-epc14-sccm">0.00</td><td id="vrf-st-epc14-psi">0.00</td></tr>
                                <tr><td>EPC 15</td><td id="vrf-rt-epc15-psi">0.00</td><td id="vrf-rt-epc15-sccm">0.00</td><td id="vrf-st-epc15-psi">0.00</td></tr>
                                <tr><td>EPC 16</td><td id="vrf-rt-epc16-psi">0.00</td><td id="vrf-rt-epc16-sccm">0.00</td><td id="vrf-st-epc16-psi">0.00</td></tr>
                                <tr><td>EPC 17</td><td id="vrf-rt-epc17-psi">0.00</td><td id="vrf-rt-epc17-sccm">0.00</td><td id="vrf-st-epc17-psi">0.00</td></tr>
                                <tr><td>EPC 18</td><td id="vrf-rt-epc18-psi">0.00</td><td id="vrf-rt-epc18-sccm">0.00</td><td id="vrf-st-epc18-psi">0.00</td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <!-- 隐藏的高级设置 Modal -->
        <div id="sysconfig-modal" class="modal-overlay" style="display:none; position:fixed; top:0; left:0; right:0; bottom:0; background:rgba(0,0,0,0.5); z-index:999; justify-content:center; align-items:center;">
            <div class="modal-content" style="background:#1e293b; padding:20px; border-radius:8px; width:850px; height:600px; display:flex; flex-direction:column; color:#fff; overflow:hidden;">
                <h3 style="margin-top:0; border-bottom:1px solid #334155; padding-bottom:10px; flex-shrink:0;">系统高级配置</h3>

                <div id="sysconfig-login" style="margin-top:20px; flex:1; overflow-y:auto;">
                    <div style="display:flex; flex-direction:column; gap:10px;">
                        <label>请输入加密密码：</label>
                        <input type="password" id="sys-auth-pass" class="input" placeholder="输入密码以解锁配置">
                        <button class="btn" id="btn-sys-login" style="margin-top:10px;">解锁</button>
                        <button class="btn btn-danger" id="btn-sys-close1" style="margin-top:5px; background:transparent; border:1px solid #475569;">取消</button>
                    </div>
                </div>

                <div id="sysconfig-form" style="display:none; margin-top:10px; flex:1; flex-direction:column; overflow:hidden;">
                    <!-- Tabs Header -->
                    <div style="display:flex; border-bottom:1px solid #334155; margin-bottom:15px; gap:15px; flex-shrink:0;" id="sysconfig-tabs">
                        <div class="sys-tab" data-target="sys-tab-basic" style="padding:8px 12px; cursor:pointer; border-bottom:2px solid #38bdf8; color:#38bdf8; font-weight:bold;">基础设置</div>
                        <div class="sys-tab" data-target="sys-tab-mqtt" style="padding:8px 12px; cursor:pointer; color:#94a3b8;">MQTT 遥测</div>
                        <div class="sys-tab" data-target="sys-tab-daq" style="padding:8px 12px; cursor:pointer; color:#94a3b8;">环保数采仪</div>
                        <div class="sys-tab" data-target="sys-tab-modbus" style="padding:8px 12px; cursor:pointer; color:#94a3b8;">Modbus TCP</div>
                    </div>

                    <div style="flex:1; overflow-y:auto; padding-right:10px;">
                        <div id="sys-tab-basic" class="sys-tab-content-pane" style="display:flex; flex-direction:column; gap:10px;">
                        <!-- 硬件 驱动模式 -->
                        <h4 style="margin: 0; color: #38bdf8; border-bottom: 1px dashed #334155; padding-bottom: 5px;">硬件架构模式</h4>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">驱动模式</label>
                            <select id="sys-driver-mode" class="input" style="flex: 1;">
                                <option value="legacy">加载中...</option>
                            </select>
                        </div>
                        
                        <!-- 仅在 Modular 模式下显示硬件连接配置 -->
                        <div id="sys-modular-config" style="display: none; padding: 10px; background: rgba(0,0,0,0.2); border: 1px solid #334155; border-radius: 6px; margin-top: 10px;">
                            <h5 style="margin: 0 0 10px 0; color: #94a3b8;">Modular (散件) 硬件连接参数配置</h5>
                            <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 5px;">
                                <label style="width: 130px; font-size: 13px;">TCD放大器 串口</label>
                                <input type="text" id="sys-modular-tcd-port" class="input" style="flex: 1;" placeholder="例如: COM11">
                            </div>
                            <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 5px;">
                                <label style="width: 130px; font-size: 13px;">温控板 串口</label>
                                <input type="text" id="sys-modular-temp-port" class="input" style="flex: 1;" placeholder="例如: COM7">
                            </div>
                            <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 5px;">
                                <label style="width: 130px; font-size: 13px;">温控板 从机ID</label>
                                <input type="number" id="sys-modular-temp-slave-id" class="input" style="flex: 1;" placeholder="例如: 20">
                            </div>
                            <div style="display: flex; align-items: center; gap: 10px; margin-bottom: 5px;">
                                <label style="width: 130px; font-size: 13px;">EPC 串口 (预留)</label>
                                <input type="text" id="sys-modular-epc-port" class="input" style="flex: 1;" placeholder="暂未实现">
                            </div>
                            <p style="margin: 5px 0 0 0; font-size: 12px; color: #64748b;">* 配置修改后需重启采集器生效，启动后会自动连接上述硬件。</p>
                        </div>

                        <!-- 密码修改部分 -->
                        <h4 style="margin: 15px 0 0 0; color: #38bdf8; border-bottom: 1px dashed #334155; padding-bottom: 5px;">安全设置</h4>
                        <label>修改管理员密码 (可选)</label>
                        <input type="password" id="sys-admin-pass-new" class="input" placeholder="留空则不修改">
                    </div>

                    <div id="sys-tab-mqtt" class="sys-tab-content-pane" style="display:none; flex-direction:row; gap:20px;">
                        <!-- 左侧：MQTT参数 -->
                        <div style="flex:1; display:flex; flex-direction:column; gap:10px;">
                            <div style="display: flex; align-items: center; justify-content: space-between; margin: 0 0 0 0; border-bottom: 1px dashed #334155; padding-bottom: 5px;">
                            <h4 style="margin: 0; color: #38bdf8;">MQTT 增量遥测参数</h4>
                            <div style="display: flex; align-items: center; gap: 10px;">
                                <span id="mqtt-status-indicator" style="font-size: 12px; color: #94a3b8;">状态: 未知</span>
                                <button class="btn" id="btn-mqtt-test" style="padding: 2px 8px; font-size: 12px;">测试连接</button>
                            </div>
                        </div>
                        <label style="color:#10b981; margin-top: 5px; display: block;"><input type="checkbox" id="sys-mqtt-enable"> 启用 MQTT 上传</label>
                        
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">Broker</label>
                            <input type="text" id="sys-mqtt-broker" class="input" style="flex: 1;" placeholder="tcp://127.0.0.1:1883">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">Topic</label>
                            <input type="text" id="sys-mqtt-topic" class="input" style="flex: 1;" placeholder="vocs/telemetry/results">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">Client ID</label>
                            <input type="text" id="sys-mqtt-clientid" class="input" style="flex: 1;" placeholder="自定义 Client ID">
                            <label style="display: flex; align-items: center; gap: 4px; color: #94a3b8; font-size: 13px; cursor: pointer; white-space: nowrap;">
                                <input type="checkbox" id="sys-mqtt-use-deviceid"> 使用设备唯一ID
                            </label>
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">Username</label>
                            <input type="text" id="sys-mqtt-user" class="input" style="flex: 1;">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">Password</label>
                            <input type="password" id="sys-mqtt-pass" class="input" style="flex: 1;">
                        </div>
                        </div> <!-- 关闭左侧列 -->

                        <!-- 右侧：上传内容控制 -->
                        <div style="flex:1; display:flex; flex-direction:column; gap:10px;">
                            <h4 style="margin: 0; color: #38bdf8; border-bottom: 1px dashed #334155; padding-bottom: 5px;">上传内容控制</h4>
                            <div style="display: flex; flex-direction: column; gap: 10px; margin-top: 5px;">
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer;">
                                <input type="checkbox" id="mqtt-upload-info" checked>
                                <div>
                                    <div style="color: #f8fafc;">上传设备基础信息 (info)</div>
                                    <div style="font-size: 11px; color: #94a3b8;">开机时和每小时整点触发一次，节省流量</div>
                                </div>
                            </label>
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer;">
                                <input type="checkbox" id="mqtt-upload-status" checked>
                                <div>
                                    <div style="color: #f8fafc;">上传设备实时状态 (status)</div>
                                    <div style="font-size: 11px; color: #94a3b8;">每分钟上报一次当前温度、压力等运行状态</div>
                                </div>
                            </label>
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer;">
                                <input type="checkbox" id="mqtt-upload-result" checked>
                                <div>
                                    <div style="color: #f8fafc;">上传分析结果 (result)</div>
                                    <div style="font-size: 11px; color: #94a3b8;">核心数据，每次色谱分析完成时立即触发</div>
                                </div>
                            </label>
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer;">
                                <input type="checkbox" id="mqtt-upload-log" checked>
                                <div>
                                    <div style="color: #f8fafc;">上传系统日志 (log)</div>
                                    <div style="font-size: 11px; color: #94a3b8;">包含系统错误、警告等关键事件记录</div>
                                </div>
                            </label>
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer; padding-left: 20px;">
                                <input type="checkbox" id="mqtt-upload-debug">
                                <div>
                                    <div style="color: #f8fafc;">包含底层通信 (DEBUG) 日志</div>
                                    <div style="font-size: 11px; color: #94a3b8;">默认关闭，开启后将上传心跳和报文等频繁日志</div>
                                </div>
                            </label>
                        </div>
                        </div>
                    </div>

                    <div id="sys-tab-daq" class="sys-tab-content-pane" style="display:none; flex-direction:column; gap:10px;">
                        <!-- 数采仪配置部分 -->
                        <h4 style="margin: 0; color: #38bdf8; border-bottom: 1px dashed #334155; padding-bottom: 5px;">数采仪 (HJ212)</h4>
                        <div style="margin-top: 5px; margin-bottom: 5px;">
                            <label style="color: #10b981;"><input type="checkbox" id="daq-enable" checked> 启用数采仪谱图上传</label>
                        </div>
                        
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">设备唯一标识</label>
                            <input type="text" id="daq-device-no" class="input" style="flex: 1;" value="1A1GBHKL9011202180011101">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">上传IP</label>
                            <input type="text" id="daq-upload-ip" class="input" style="flex: 1;" value="192.168.1.105">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">上传端口</label>
                            <input type="text" id="daq-upload-port" class="input" style="flex: 1;" value="5300">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">色谱IP</label>
                            <input type="text" id="daq-chrom-ip" class="input" style="flex: 1;" value="192.168.1.20">
                        </div>
                    </div>

                    <div id="sys-tab-modbus" class="sys-tab-content-pane" style="display:none; flex-direction:column; gap:10px;">
                        <!-- Modbus TCP Server 配置部分 -->
                        <h4 style="margin: 0; color: #38bdf8; border-bottom: 1px dashed #334155; padding-bottom: 5px;">Modbus TCP Server</h4>
                        
                        <div style="display: flex; align-items: center; gap: 10px; margin-top: 10px;">
                            <label style="width: 80px;">服务端口</label>
                            <input type="number" id="modbus-server-port" class="input" style="flex: 1;" value="1502">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px;">
                            <label style="width: 80px;">设备标识</label>
                            <input type="text" id="modbus-server-addr" class="input" style="flex: 1;" value="1">
                        </div>
                        <div style="display: flex; align-items: center; gap: 10px; margin-top: 10px;">
                            <label style="display: flex; align-items: center; gap: 10px; cursor: pointer;">
                                <input type="checkbox" id="modbus-upload-log" checked>
                                <div>
                                    <div style="color: #f8fafc;">上传日志 (Modbus 700)</div>
                                    <div style="font-size: 11px; color: #94a3b8;">控制是否将INFO及以上级别的系统日志推送到 Modbus 队列</div>
                                </div>
                            </label>
                        </div>
                    </div>
                    </div> <!-- End of scrollable area -->

                    <div style="display:flex; gap:10px; margin-top:20px; flex-shrink:0;">
                        <button class="btn" id="btn-sys-save" style="flex:1;">保存并应用</button>
                        <button class="btn btn-danger" id="btn-sys-close2" style="flex:1; background:transparent; border:1px solid #475569;">关闭</button>
                    </div>
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

    // Sysconfig Modal Tabs logic
    const sysTabs = container.querySelectorAll('.sys-tab');
    const sysContents = container.querySelectorAll('.sys-tab-content-pane');
    
    sysTabs.forEach(tab => {
        tab.addEventListener('click', () => {
            sysTabs.forEach(t => {
                t.style.fontWeight = 'normal';
                t.style.color = '#94a3b8';
                t.style.borderBottom = 'none';
            });
            sysContents.forEach(c => c.style.display = 'none');
            
            tab.style.fontWeight = 'bold';
            tab.style.color = '#38bdf8';
            tab.style.borderBottom = '2px solid #38bdf8';
            container.querySelector('#' + tab.dataset.target).style.display = 'flex';
        });
    });

    const useDeviceIdCheck = document.getElementById('sys-mqtt-use-deviceid');
    const clientIdInput = document.getElementById('sys-mqtt-clientid');
    if (useDeviceIdCheck) {
        useDeviceIdCheck.addEventListener('change', (e) => {
            if (e.target.checked) {
                clientIdInput.value = '';
                clientIdInput.disabled = true;
            } else {
                clientIdInput.disabled = false;
                clientIdInput.focus();
            }
        });
    }

    let uiSettings = {};
    let hwSettings = {};
    let uploadSettings = {};
    let deviceId = "GC-MODULAR";

    setTimeout(async () => {
        try {
            const devRes = await fetch('/api/v1/devices');
            const devices = await devRes.json();
            if(devices && devices.length > 0) {
                const connectedDev = devices.find(d => d.connected);
                deviceId = connectedDev ? connectedDev.deviceId : devices[0].deviceId;
                const gcDev = devices.find(d => String(d.deviceId).startsWith('GC-MODULAR'));
                if (gcDev) deviceId = gcDev.deviceId;
            }

            // Load UI Settings (Time settings)
              const uiRes = await fetch('/api/sila2/v1/SystemConfigurationService/UILayout?deviceId=' + encodeURIComponent(deviceId));
              if (uiRes.ok) {
                  uiSettings = await uiRes.json();
              }

              // Load Hardware Settings
              const hwRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
              if (hwRes.ok) {
                  hwSettings = await hwRes.json();
                  
                  if (hwSettings.cycleInterval !== undefined) document.getElementById('set-time-cycle').value = hwSettings.cycleInterval;
                  if (hwSettings.cycleCount !== undefined) document.getElementById('set-time-cycle-max').value = hwSettings.cycleCount;
                
                // Populate Events
                  if (hwSettings.events && hwSettings.events.length > 0) {
                      // 还原时间程序到8路UI (吸合1 / 释放1)
                      const channelOn = new Array(8).fill(null);
                      const channelOff = new Array(8).fill(null);
                      let prevMask = 0;
                      
                      for (const evt of hwSettings.events) {
                          const mask = evt.event_mask;
                          for (let b = 0; b < 8; b++) {
                              const wasOn = (prevMask & (1 << b)) !== 0;
                              const isOn = (mask & (1 << b)) !== 0;
                              
                              if (!wasOn && isOn && channelOn[b] === null) {
                                  channelOn[b] = evt.time;
                              }
                              if (wasOn && !isOn && channelOff[b] === null) {
                                  channelOff[b] = evt.time;
                              }
                          }
                          prevMask = mask;
                      }
                      
                      for (let i = 0; i < 8; i++) {
                          document.getElementById('ev-on-' + (i + 1)).value = channelOn[i] !== null ? channelOn[i] : 0;
                          document.getElementById('ev-off-' + (i + 1)).value = channelOff[i] !== null ? channelOff[i] : 0;
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
                    if (hwSettings.temperatures['Inj1'] !== undefined) document.getElementById('set-temp-inj1').value = hwSettings.temperatures['Inj1'];
                    if (hwSettings.temperatures['ProtInj1'] !== undefined) document.getElementById('prot-temp-inj1').value = hwSettings.temperatures['ProtInj1'];

                    if (hwSettings.temperatures['Col'] !== undefined) document.getElementById('set-temp-col').value = hwSettings.temperatures['Col'];
                    if (hwSettings.temperatures['ProtCol'] !== undefined) document.getElementById('prot-temp-col').value = hwSettings.temperatures['ProtCol'];
                    
                    if (hwSettings.temperatures['Det1'] !== undefined) document.getElementById('set-temp-det1').value = hwSettings.temperatures['Det1'];
                    if (hwSettings.temperatures['ProtDet1'] !== undefined) document.getElementById('prot-temp-det1').value = hwSettings.temperatures['ProtDet1'];
                    
                    if (hwSettings.temperatures['Inj2'] !== undefined) document.getElementById('set-temp-inj2').value = hwSettings.temperatures['Inj2'];
                    if (hwSettings.temperatures['ProtInj2'] !== undefined) document.getElementById('prot-temp-inj2').value = hwSettings.temperatures['ProtInj2'];
                    
                    if (hwSettings.temperatures['Det2'] !== undefined) document.getElementById('set-temp-det2').value = hwSettings.temperatures['Det2'];
                    if (hwSettings.temperatures['ProtDet2'] !== undefined) document.getElementById('prot-temp-det2').value = hwSettings.temperatures['ProtDet2'];

                    if (hwSettings.temperatures['Det3'] !== undefined) {
                        const setDet3 = document.getElementById('set-temp-det3');
                        if (setDet3) setDet3.value = hwSettings.temperatures['Det3'];
                    }
                    if (hwSettings.temperatures['ProtDet3'] !== undefined) {
                        const protDet3 = document.getElementById('prot-temp-det3');
                        if (protDet3) protDet3.value = hwSettings.temperatures['ProtDet3'];
                    }
                }

                if (hwSettings.temp_enables) {
                    if (hwSettings.temp_enables['Inj1'] !== undefined) document.getElementById('en-temp-inj1').checked = hwSettings.temp_enables['Inj1'];
                    if (hwSettings.temp_enables['Col'] !== undefined) document.getElementById('en-temp-col').checked = hwSettings.temp_enables['Col'];
                    if (hwSettings.temp_enables['Det1'] !== undefined) document.getElementById('en-temp-det1').checked = hwSettings.temp_enables['Det1'];
                    if (hwSettings.temp_enables['Inj2'] !== undefined) document.getElementById('en-temp-inj2').checked = hwSettings.temp_enables['Inj2'];
                    if (hwSettings.temp_enables['Det2'] !== undefined) document.getElementById('en-temp-det2').checked = hwSettings.temp_enables['Det2'];
                    if (hwSettings.temp_enables['Det3'] !== undefined) document.getElementById('en-temp-det3').checked = hwSettings.temp_enables['Det3'];
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
                        if (parsed.heating !== undefined) {
                            const heatingEl = document.getElementById('status-heating');
                            const toggleBtn = document.getElementById('btn-toggle-temp');
                            if (heatingEl) {
                                if (parsed.heating) {
                                    heatingEl.innerText = "状态: 升温中 (ON)";
                                    heatingEl.style.backgroundColor = "#166534";
                                    heatingEl.style.color = "#bbf7d0";
                                    if (toggleBtn) {
                                        toggleBtn.innerText = "关闭控温";
                                        toggleBtn.style.backgroundColor = "#d32f2f";
                                    }
                                } else {
                                    heatingEl.innerText = "状态: 已停止 (OFF)";
                                    heatingEl.style.backgroundColor = "#7f1d1d";
                                    heatingEl.style.color = "#fecaca";
                                    if (toggleBtn) {
                                        toggleBtn.innerText = "开始控温";
                                        toggleBtn.style.backgroundColor = "#2e7d32";
                                    }
                                }
                            }
                        }

                        if (parsed.tempInj1 !== undefined) {
                            const elInj1 = document.getElementById('real-temp-inj1');
                            const elCol = document.getElementById('real-temp-col');
                            const elDet1 = document.getElementById('real-temp-det1');
                            const elInj2 = document.getElementById('real-temp-inj2');
                            const elDet2 = document.getElementById('real-temp-det2');
                            
                            if (elCol) elCol.innerText = (parsed.tempCol || 0).toFixed(1);
                            if (elDet1) elDet1.innerText = (parsed.tempDet1 || 0).toFixed(1);
                            if (elInj1) elInj1.innerText = (parsed.tempInj1 || 0).toFixed(1);
                            if (elInj2) elInj2.innerText = (parsed.tempInj2 || 0).toFixed(1);
                            if (elDet2) elDet2.innerText = (parsed.tempDet2 || 0).toFixed(1);
                        }
                        
                        if (parsed.carrierPsi !== undefined || parsed.epc) {
                            if (parsed.carrierPsi !== undefined) {
                                document.getElementById('real-epc-carrier1').innerText = (parsed.carrierPsi || 0).toFixed(2) + " / " + (parsed.carrierSccm || 0).toFixed(1);
                                document.getElementById('real-epc-h2-1').innerText = (parsed.h2Psi || 0).toFixed(2) + " / " + (parsed.h2Sccm || 0).toFixed(1);
                                document.getElementById('real-epc-air-1').innerText = (parsed.airPsi || 0).toFixed(2) + " / " + (parsed.airSccm || 0).toFixed(1);
                            } else if (parsed.epc && parsed.epc.length >= 3) {
                                document.getElementById('real-epc-carrier1').innerText = (parsed.epc[0].psi || 0).toFixed(2) + " / " + (parsed.epc[0].sccm || 0).toFixed(1);
                                document.getElementById('real-epc-h2-1').innerText = (parsed.epc[1].psi || 0).toFixed(2) + " / " + (parsed.epc[1].sccm || 0).toFixed(1);
                                document.getElementById('real-epc-air-1').innerText = (parsed.epc[2].psi || 0).toFixed(2) + " / " + (parsed.epc[2].sccm || 0).toFixed(1);
                            }
                        }

                        // Hardware Verify Tab Update
                        if (parsed.tempInj1 !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp1');
                            if (rtEl) rtEl.innerText = parsed.tempInj1.toFixed(1);
                        }
                        if (parsed.tempCol !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp2');
                            if (rtEl) rtEl.innerText = parsed.tempCol.toFixed(1);
                        }
                        if (parsed.tempDet1 !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp3');
                            if (rtEl) rtEl.innerText = parsed.tempDet1.toFixed(1);
                        }
                        if (parsed.tempInj2 !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp4');
                            if (rtEl) rtEl.innerText = parsed.tempInj2.toFixed(1);
                        }
                        if (parsed.tempDet2 !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp5');
                            if (rtEl) rtEl.innerText = parsed.tempDet2.toFixed(1);
                        }
                        if (parsed.tempDet3 !== undefined) {
                            const rtEl = document.getElementById('vrf-rt-temp6');
                            if (rtEl) rtEl.innerText = parsed.tempDet3.toFixed(1);
                        }

                        if (parsed.setTempInj1 !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp1');
                            if (stEl) stEl.innerText = parsed.setTempInj1.toFixed(1);
                        }
                        if (parsed.setTempCol !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp2');
                            if (stEl) stEl.innerText = parsed.setTempCol.toFixed(1);
                        }
                        if (parsed.setTempDet1 !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp3');
                            if (stEl) stEl.innerText = parsed.setTempDet1.toFixed(1);
                        }
                        if (parsed.setTempInj2 !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp4');
                            if (stEl) stEl.innerText = parsed.setTempInj2.toFixed(1);
                        }
                        if (parsed.setTempDet2 !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp5');
                            if (stEl) stEl.innerText = parsed.setTempDet2.toFixed(1);
                        }
                        if (parsed.setTempDet3 !== undefined) {
                            const stEl = document.getElementById('vrf-st-temp6');
                            if (stEl) stEl.innerText = parsed.setTempDet3.toFixed(1);
                        }

                        if (parsed.protTempInj1 !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp1');
                            if (ptEl) ptEl.innerText = parsed.protTempInj1.toFixed(1);
                        }
                        if (parsed.protTempCol !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp2');
                            if (ptEl) ptEl.innerText = parsed.protTempCol.toFixed(1);
                        }
                        if (parsed.protTempDet1 !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp3');
                            if (ptEl) ptEl.innerText = parsed.protTempDet1.toFixed(1);
                        }
                        if (parsed.protTempInj2 !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp4');
                            if (ptEl) ptEl.innerText = parsed.protTempInj2.toFixed(1);
                        }
                        if (parsed.protTempDet2 !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp5');
                            if (ptEl) ptEl.innerText = parsed.protTempDet2.toFixed(1);
                        }
                        if (parsed.protTempDet3 !== undefined) {
                            const ptEl = document.getElementById('vrf-pt-temp6');
                            if (ptEl) ptEl.innerText = parsed.protTempDet3.toFixed(1);
                        }

                        if (parsed.epc && parsed.epc.length > 0) {
                            for (let i = 0; i < parsed.epc.length && i < 18; i++) {
                                const pEl = document.getElementById('vrf-rt-epc' + (i + 1) + '-psi');
                                const sEl = document.getElementById('vrf-rt-epc' + (i + 1) + '-sccm');
                                const iEl = document.getElementById('vrf-st-epc' + (i + 1) + '-psi');
                                if (pEl) pEl.innerText = (parsed.epc[i].psi || 0).toFixed(2);
                                if (sEl) sEl.innerText = (parsed.epc[i].sccm || 0).toFixed(1);
                                if (iEl) iEl.innerText = (parsed.epc[i].inputPsi || 0).toFixed(2);
                            }
                        }
                    } else if (parsed.type === 'logs') {
                        const logViewer = document.getElementById('sys-log-viewer');
                        if (logViewer && parsed.data && parsed.data.logs) {
                            parsed.data.logs.forEach(l => {
                                const t = new Date(l.time * 1000).toLocaleString();
                                const div = document.createElement('div');
                                div.className = 'log-entry log-level-' + l.level;
                                div.style.marginBottom = '4px';
                                div.style.color = l.level === 'ERROR' ? '#ff6b6b' : (l.level === 'WARN' ? '#facc15' : (l.level === 'DEBUG' ? '#94a3b8' : '#38bdf8'));
                                div.innerText = `[${t}] [${l.level}] ${l.msg}`;
                                logViewer.prepend(div);
                            });
                            // Keep max 1000 logs
                            while (logViewer.children.length > 1000) {
                                logViewer.removeChild(logViewer.lastChild);
                            }
                        }
                    }
                } catch(e) {}
            };

            // Fetch initial logs
            try {
                const logRes = await fetch('/api/sila2/v1/SystemLogService/Logs');
                if (logRes.ok) {
                    const logs = await logRes.json();
                    const logViewer = document.getElementById('sys-log-viewer');
                    if (logViewer && logs) {
                        // Render initial logs (array is oldest to newest, we want newest on top, so we reverse it)
                        [...logs].reverse().forEach(l => {
                            const t = new Date(l.time * 1000).toLocaleString();
                            const div = document.createElement('div');
                            div.className = 'log-entry log-level-' + l.level;
                            div.style.marginBottom = '4px';
                            div.style.color = l.level === 'ERROR' ? '#ff6b6b' : (l.level === 'WARN' ? '#facc15' : (l.level === 'DEBUG' ? '#94a3b8' : '#38bdf8'));
                            div.innerText = `[${t}] [${l.level}] ${l.msg}`;
                            logViewer.appendChild(div);
                        });
                    }
                }
            } catch (e) {}

            // Setup checkboxes logic
            const logViewer = document.getElementById('sys-log-viewer');
            if (logViewer) {
                const toggles = [
                    { id: 'chk-log-debug', class: 'hide-debug' },
                    { id: 'chk-log-info', class: 'hide-info' },
                    { id: 'chk-log-warn', class: 'hide-warn' },
                    { id: 'chk-log-error', class: 'hide-error' }
                ];
                toggles.forEach(t => {
                    const el = document.getElementById(t.id);
                    if (el) {
                        el.addEventListener('change', (e) => {
                            if (e.target.checked) {
                                logViewer.classList.remove(t.class);
                            } else {
                                logViewer.classList.add(t.class);
                            }
                        });
                    }
                });
            }

            // Setup clear log button
            const btnClearLog = document.getElementById('btn-clear-log');
            if (btnClearLog) {
                btnClearLog.addEventListener('click', () => {
                    const logViewer = document.getElementById('sys-log-viewer');
                    if (logViewer) logViewer.innerHTML = '';
                });
            }

        } catch (e) {
            console.error('Failed to init settings', e);
        }

        // Events Query
        document.getElementById('btn-query-events').addEventListener('click', async () => {
            try {
                const res = await fetch('/api/control/events?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({control: 'query'})
                });
                if (!res.ok) {
                    window.showToast('查询失败', true);
                    return;
                }
                window.showToast('查询指令已下发，稍后刷新...');
                
                // 等待 1 秒后重新拉取硬件配置并渲染
                setTimeout(async () => {
                    const hwRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
                    if (hwRes.ok) {
                        hwSettings = await hwRes.json();
                        if (hwSettings.events && hwSettings.events.length > 0) {
                            const channelOn = new Array(8).fill(null);
                            const channelOff = new Array(8).fill(null);
                            let prevMask = 0;
                            
                            for (const evt of hwSettings.events) {
                                const mask = evt.event_mask;
                                for (let b = 0; b < 8; b++) {
                                    const wasOn = (prevMask & (1 << b)) !== 0;
                                    const isOn = (mask & (1 << b)) !== 0;
                                    
                                    if (!wasOn && isOn && channelOn[b] === null) {
                                        channelOn[b] = evt.time;
                                    }
                                    if (wasOn && !isOn && channelOff[b] === null) {
                                        channelOff[b] = evt.time;
                                    }
                                }
                                prevMask = mask;
                            }
                            
                            for (let i = 0; i < 8; i++) {
                                document.getElementById('ev-on-' + (i + 1)).value = channelOn[i] !== null ? channelOn[i] : 0;
                                document.getElementById('ev-off-' + (i + 1)).value = channelOff[i] !== null ? channelOff[i] : 0;
                            }
                            window.showToast('事件程序已刷新');
                        } else {
                            window.showToast('设备返回了空事件');
                        }
                    }
                }, 1000);
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // Events Apply
        document.getElementById('btn-apply-events').addEventListener('click', async () => {
            const transitions = [];
            for (let i = 1; i <= 8; i++) {
                const onTime = parseFloat(document.getElementById('ev-on-' + i).value);
                const offTime = parseFloat(document.getElementById('ev-off-' + i).value);
                
                if (!isNaN(onTime) && onTime >= 0) {
                    transitions.push({ time: onTime, bit: i - 1, state: 1 });
                }
                if (!isNaN(offTime) && offTime >= 0) {
                    transitions.push({ time: offTime, bit: i - 1, state: 0 });
                }
            }

            // 获取所有唯一的时间点并排序
            const timePoints = [...new Set(transitions.map(t => t.time))].sort((a, b) => a - b);
            
            const events = [];
            let currentMask = 0; // 默认时间0时所有阀/继电器都是释放状态

            for (const t of timePoints) {
                const transAtT = transitions.filter(x => x.time === t);
                for (const trans of transAtT) {
                    if (trans.state === 1) {
                        currentMask |= (1 << trans.bit);
                    } else {
                        currentMask &= ~(1 << trans.bit);
                    }
                }
                events.push({ time: t, event_mask: currentMask });
            }

            hwSettings.events = events;

            try {
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });
                
                const res = await fetch('/api/control/events?deviceId=' + encodeURIComponent(deviceId), {
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
                const res = await fetch('/api/control/epc?deviceId=' + encodeURIComponent(deviceId), {
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

        // Ignite Config Query
        const btnQueryIgnite = document.getElementById('btn-query-ignite-config');
        if (btnQueryIgnite) {
            btnQueryIgnite.addEventListener('click', async () => {
                window.showToast('正在向设备下发点火参数查询指令...');
                try {
                    await fetch('/api/control/ignite_config?deviceId=' + encodeURIComponent(deviceId), {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({control: 'query'})
                    });
                    
                    await new Promise(r => setTimeout(r, 500));
                    
                    const hwRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
                    if (hwRes.ok) {
                        hwSettings = await hwRes.json();
                        if (hwSettings.igniteThreshold1 !== undefined) document.getElementById('set-ignite-th1').value = hwSettings.igniteThreshold1;
                        if (hwSettings.igniteThreshold2 !== undefined) document.getElementById('set-ignite-th2').value = hwSettings.igniteThreshold2;
                        if (hwSettings.igniteDuration !== undefined) document.getElementById('set-ignite-dur').value = hwSettings.igniteDuration;
                        window.showToast('点火参数已刷新');
                    }
                } catch(e) {
                    window.showToast('异常: ' + e.message, true);
                }
            });
        }

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
                    
                    const ctrlRes = await fetch('/api/control/ignite_config?deviceId=' + encodeURIComponent(deviceId), {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({control: 'set'})
                    });

                    if (ctrlRes.ok) window.showToast('点火参数已保存并下发!');
                    else window.showToast('保存下发失败', true);
                } catch(e) {
                    window.showToast('异常: ' + e.message, true);
                }
            });
        }

        // Temperature Apply
        document.getElementById('btn-apply-temp').addEventListener('click', async () => {
            if (!hwSettings.temperatures) hwSettings.temperatures = {};
            if (!hwSettings.temp_enables) hwSettings.temp_enables = {};
            
            hwSettings.temperatures['Col'] = parseFloat(document.getElementById('set-temp-col').value) || 0;
            hwSettings.temperatures['ProtCol'] = parseFloat(document.getElementById('prot-temp-col').value) || 0;
            hwSettings.temp_enables['Col'] = document.getElementById('en-temp-col').checked;
            
            hwSettings.temperatures['Inj1'] = parseFloat(document.getElementById('set-temp-inj1').value) || 0;
            hwSettings.temperatures['ProtInj1'] = parseFloat(document.getElementById('prot-temp-inj1').value) || 0;
            hwSettings.temp_enables['Inj1'] = document.getElementById('en-temp-inj1').checked;
            
            hwSettings.temperatures['Det1'] = parseFloat(document.getElementById('set-temp-det1').value) || 0;
            hwSettings.temperatures['ProtDet1'] = parseFloat(document.getElementById('prot-temp-det1').value) || 0;
            hwSettings.temp_enables['Det1'] = document.getElementById('en-temp-det1').checked;
            
            hwSettings.temperatures['Inj2'] = parseFloat(document.getElementById('set-temp-inj2').value) || 0;
            hwSettings.temperatures['ProtInj2'] = parseFloat(document.getElementById('prot-temp-inj2').value) || 0;
            hwSettings.temp_enables['Inj2'] = document.getElementById('en-temp-inj2').checked;
            
            hwSettings.temperatures['Det2'] = parseFloat(document.getElementById('set-temp-det2').value) || 0;
            hwSettings.temperatures['ProtDet2'] = parseFloat(document.getElementById('prot-temp-det2').value) || 0;
            hwSettings.temp_enables['Det2'] = document.getElementById('en-temp-det2').checked;

            const setDet3 = document.getElementById('set-temp-det3');
            const protDet3 = document.getElementById('prot-temp-det3');
            const enDet3 = document.getElementById('en-temp-det3');
            if (setDet3) hwSettings.temperatures['Det3'] = parseFloat(setDet3.value) || 0;
            if (protDet3) hwSettings.temperatures['ProtDet3'] = parseFloat(protDet3.value) || 0;
            if (enDet3) hwSettings.temp_enables['Det3'] = enDet3.checked;

            try {
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });
                
                const res = await fetch('/api/control/temp?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({ 
                        targets: hwSettings.temperatures,
                        enables: hwSettings.temp_enables
                    })
                });
                if (res.ok) window.showToast('温度控制指令已下发!');
                else {
                    const errTxt = await res.text();
                    window.showToast('发送失败: ' + errTxt, true);
                }
            } catch(e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        document.getElementById('btn-query-temp').addEventListener('click', async () => {
            window.showToast('正在向设备下发查询指令...');
            try {
                // 1. 下发 Cmd 0
                await fetch('/api/control/temp?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({control: 'query'})
                });
                
                // 2. 等待 500ms 让设备回传 Cmd 128
                await new Promise(r => setTimeout(r, 500));
                
                // 3. 拉取最新的缓存
                const hwRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
                if (hwRes.ok) {
                    hwSettings = await hwRes.json();
                    if (hwSettings.temperatures) {
                        if (hwSettings.temperatures['Inj1'] !== undefined) document.getElementById('set-temp-inj1').value = hwSettings.temperatures['Inj1'];
                        if (hwSettings.temperatures['ProtInj1'] !== undefined) document.getElementById('prot-temp-inj1').value = hwSettings.temperatures['ProtInj1'];
                        
                        if (hwSettings.temperatures['Col'] !== undefined) document.getElementById('set-temp-col').value = hwSettings.temperatures['Col'];
                        if (hwSettings.temperatures['ProtCol'] !== undefined) document.getElementById('prot-temp-col').value = hwSettings.temperatures['ProtCol'];
                        
                        if (hwSettings.temperatures['Det1'] !== undefined) document.getElementById('set-temp-det1').value = hwSettings.temperatures['Det1'];
                        if (hwSettings.temperatures['ProtDet1'] !== undefined) document.getElementById('prot-temp-det1').value = hwSettings.temperatures['ProtDet1'];
                        
                        if (hwSettings.temperatures['Inj2'] !== undefined) document.getElementById('set-temp-inj2').value = hwSettings.temperatures['Inj2'];
                        if (hwSettings.temperatures['ProtInj2'] !== undefined) document.getElementById('prot-temp-inj2').value = hwSettings.temperatures['ProtInj2'];
                        
                        if (hwSettings.temperatures['Det2'] !== undefined) document.getElementById('set-temp-det2').value = hwSettings.temperatures['Det2'];
                        if (hwSettings.temperatures['ProtDet2'] !== undefined) document.getElementById('prot-temp-det2').value = hwSettings.temperatures['ProtDet2'];

                        if (hwSettings.temperatures['Det3'] !== undefined) {
                            const setDet3 = document.getElementById('set-temp-det3');
                            if (setDet3) setDet3.value = hwSettings.temperatures['Det3'];
                        }
                        if (hwSettings.temperatures['ProtDet3'] !== undefined) {
                            const protDet3 = document.getElementById('prot-temp-det3');
                            if (protDet3) protDet3.value = hwSettings.temperatures['ProtDet3'];
                        }
                    }
                    if (hwSettings.temp_enables) {
                        if (hwSettings.temp_enables['Inj1'] !== undefined) document.getElementById('en-temp-inj1').checked = hwSettings.temp_enables['Inj1'];
                        if (hwSettings.temp_enables['Col'] !== undefined) document.getElementById('en-temp-col').checked = hwSettings.temp_enables['Col'];
                        if (hwSettings.temp_enables['Det1'] !== undefined) document.getElementById('en-temp-det1').checked = hwSettings.temp_enables['Det1'];
                        if (hwSettings.temp_enables['Inj2'] !== undefined) document.getElementById('en-temp-inj2').checked = hwSettings.temp_enables['Inj2'];
                        if (hwSettings.temp_enables['Det2'] !== undefined) document.getElementById('en-temp-det2').checked = hwSettings.temp_enables['Det2'];
                        if (hwSettings.temp_enables['Det3'] !== undefined) document.getElementById('en-temp-det3').checked = hwSettings.temp_enables['Det3'];
                    }
                    window.showToast('温度参数已刷新');
                }
            } catch (e) {
                window.showToast('查询异常: ' + e.message, true);
            }
        });

        document.getElementById('btn-toggle-temp').addEventListener('click', async (e) => {
            const isStarting = e.target.innerText === '开始控温';
            const action = isStarting ? 'start' : 'stop';
            try {
                const res = await fetch('/api/control/temp?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({control: action})
                });
                if (res.ok) window.showToast(`已下发${isStarting ? '开始' : '关闭'}控温指令`);
                else {
                    const errTxt = await res.text();
                    window.showToast('下发失败: ' + errTxt, true);
                }
            } catch (e) {
                window.showToast('异常: ' + e.message, true);
            }
        });

        // Time / UI Settings Apply
        // Time Config Query
        const btnQueryTime = document.getElementById('btn-query-time');
        if (btnQueryTime) {
            btnQueryTime.addEventListener('click', async () => {
                window.showToast('正在获取工作站循环参数...');
                try {
                    await fetch('/api/control/cycle?deviceId=' + encodeURIComponent(deviceId), {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({control: 'query'})
                    });
                    
                    await new Promise(r => setTimeout(r, 500));

                    const uiRes = await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId));
                    if (uiRes.ok) {
                        hwSettings = await uiRes.json();
                        if (hwSettings.cycleInterval !== undefined) document.getElementById('set-time-cycle').value = hwSettings.cycleInterval;
                        if (hwSettings.cycleCount !== undefined) document.getElementById('set-time-cycle-max').value = hwSettings.cycleCount;
                        window.showToast('循环参数已刷新');
                    }
                } catch(e) {
                    window.showToast('异常: ' + e.message, true);
                }
            });
        }

        document.getElementById('btn-apply-time').addEventListener('click', async () => {
            // uiSettings.acqMin = parseFloat(document.getElementById('set-time-acq').value) || 0;
            hwSettings.cycleInterval = parseFloat(document.getElementById('set-time-cycle').value) || 0;
            hwSettings.cycleCount = parseInt(document.getElementById('set-time-cycle-max').value) || 9999999;
            
            try {
                await fetch('/api/v1/hardware?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(hwSettings)
                });

                const res = await fetch('/api/control/cycle?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({control: 'set'})
                });
                if (res.ok) window.showToast('时间和循环参数已保存并下发!');
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

        // ================= 系统高级配置 Modal 逻辑 =================
        const modal = document.getElementById('sysconfig-modal');
        const loginDiv = document.getElementById('sysconfig-login');
        const formDiv = document.getElementById('sysconfig-form');
        let currentAuthPass = "";

        window.showLicenseModal = async () => {
            try {
                const res = await fetch('/api/license/status');
                if (res.ok) {
                    const data = await res.json();
                    window.showToast(`授权状态: ${data.valid ? '有效' : '无效'} | 版本: ${data.tier} | 到期: ${data.exp}`);
                } else {
                    window.showToast('无法获取授权信息', true);
                }
            } catch (e) {
                window.showToast('无法获取授权信息(网络异常)', true);
            }
        };

        document.getElementById('btn-show-license').addEventListener('click', () => {
            if (window.showLicenseModal) {
                window.showLicenseModal();
            } else {
                window.showToast('无法获取授权信息', true);
            }
        });

        document.getElementById('btn-secret-menu').addEventListener('click', () => {
            modal.style.display = 'flex';
            loginDiv.style.display = 'block';
            formDiv.style.display = 'none';
            document.getElementById('sys-auth-pass').value = '';
        });

        document.getElementById('btn-sys-close1').addEventListener('click', () => modal.style.display = 'none');
        document.getElementById('btn-sys-close2').addEventListener('click', () => modal.style.display = 'none');

        document.getElementById('btn-sys-login').addEventListener('click', async () => {
            const pass = document.getElementById('sys-auth-pass').value;
            if (!pass) return window.showToast('请输入密码', true);
            
            // 动态拉取驱动列表
            try {
                const drvRes = await fetch('/api/v1/sys/drivers');
                if (drvRes.ok) {
                    const drvData = await drvRes.json();
                    const sel = document.getElementById('sys-driver-mode');
                    sel.innerHTML = '';
                    if (drvData.drivers && drvData.drivers.length > 0) {
                        drvData.drivers.forEach(d => {
                            const opt = document.createElement('option');
                            if (d.includes('Modular')) {
                                opt.value = 'modular';
                            } else {
                                opt.value = 'legacy';
                            }
                            opt.textContent = d;
                            sel.appendChild(opt);
                        });
                    }
                }
            } catch (e) {
                console.error('Failed to load drivers:', e);
            }

            try {
                const res = await fetch('/api/sysconfig?auth=' + encodeURIComponent(pass));
                if (res.ok) {
                    const cfg = await res.json();
                    currentAuthPass = pass;
                    loginDiv.style.display = 'none';
                    formDiv.style.display = 'block';
                    
                    document.getElementById('sys-mqtt-enable').checked = cfg.mqtt_enabled;
                    document.getElementById('sys-mqtt-broker').value = cfg.mqtt_broker || '';
                    document.getElementById('sys-mqtt-topic').value = cfg.mqtt_topic || '';
                        
                        const clientIdInput = document.getElementById('sys-mqtt-clientid');
                        const useDeviceIdCheck = document.getElementById('sys-mqtt-use-deviceid');
                        if (!cfg.mqtt_client_id || cfg.mqtt_client_id === '') {
                            useDeviceIdCheck.checked = true;
                            clientIdInput.value = '';
                            clientIdInput.disabled = true;
                        } else {
                            useDeviceIdCheck.checked = false;
                            clientIdInput.value = cfg.mqtt_client_id;
                            clientIdInput.disabled = false;
                        }
                        
                        document.getElementById('sys-mqtt-user').value = cfg.mqtt_user || '';
                    document.getElementById('sys-mqtt-pass').value = cfg.mqtt_pass || '';
                    
                    document.getElementById('mqtt-upload-info').checked = cfg.mqtt_upload_info !== false; // default true
                    document.getElementById('mqtt-upload-status').checked = cfg.mqtt_upload_status !== false;
                    document.getElementById('mqtt-upload-result').checked = cfg.mqtt_upload_result !== false;
   document.getElementById('mqtt-upload-log').checked = cfg.mqtt_upload_log !== false;
   document.getElementById('mqtt-upload-debug').checked = cfg.mqtt_upload_debug === true;

   document.getElementById('sys-driver-mode').value = cfg.driver_mode || 'legacy';
                    document.getElementById('sys-admin-pass-new').value = '';
                    
                    document.getElementById('sys-modular-tcd-port').value = cfg.modular_tcd_port || '';
                    document.getElementById('sys-modular-temp-port').value = cfg.modular_temp_port || '';
                    document.getElementById('sys-modular-temp-slave-id').value = cfg.modular_temp_slave_id || 20;
                    document.getElementById('sys-modular-epc-port').value = cfg.modular_epc_port || '';

                    const toggleModularConfig = () => {
                        const isModular = document.getElementById('sys-driver-mode').value === 'modular';
                        document.getElementById('sys-modular-config').style.display = isModular ? 'block' : 'none';
                    };
                    document.getElementById('sys-driver-mode').addEventListener('change', toggleModularConfig);
                    toggleModularConfig();

                    document.getElementById('modbus-server-port').value = cfg.modbus_server_port || 1502;
                    document.getElementById('modbus-server-addr').value = cfg.modbus_server_address || '1';
                    document.getElementById('modbus-upload-log').checked = cfg.modbus_upload_log !== false;

                    // Check MQTT status
                    fetch('/api/sysconfig/mqtt_test').then(r => r.json()).then(st => {
                        const ind = document.getElementById('mqtt-status-indicator');
                        if (st.connected) {
                            ind.textContent = '状态: 已连接';
                            ind.style.color = '#10b981';
                        } else {
                            ind.textContent = '状态: ' + (st.status || '未连接');
                            ind.style.color = '#ef4444';
                        }
                    }).catch(e => {});

                } else {
                    window.showToast('密码错误', true);
                }
            } catch (e) {
                window.showToast('网络异常', true);
            }
        });

        // MQTT Test
        document.getElementById('btn-mqtt-test').addEventListener('click', async () => {
            const btn = document.getElementById('btn-mqtt-test');
            const ind = document.getElementById('mqtt-status-indicator');
            btn.disabled = true;
            btn.textContent = '测试中...';
            try {
                const deviceNo = document.getElementById('daq-device-no').value || document.getElementById('sys-mqtt-clientid').value || 'test_device';
                const res = await fetch('/api/sysconfig/mqtt_test', { 
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ deviceId: deviceNo })
                });
                const data = await res.json();
                if (res.ok && data.ok) {
                    window.showToast('测试连接成功!');
                    ind.textContent = '状态: 已连接';
                    ind.style.color = '#10b981';
                } else {
                    window.showToast('测试失败: ' + (data.error || '未知错误'), true);
                    ind.textContent = '状态: 连接失败';
                    ind.style.color = '#ef4444';
                }
            } catch (e) {
                window.showToast('网络异常', true);
            }
            btn.disabled = false;
            btn.textContent = '测试连接';
        });

        document.getElementById('btn-sys-save').addEventListener('click', async () => {
            // 1. 保存数采仪参数
            uploadSettings.deviceNo = document.getElementById('daq-device-no').value;
            uploadSettings.uploadIP = document.getElementById('daq-upload-ip').value;
            uploadSettings.uploadPort = parseInt(document.getElementById('daq-upload-port').value)||0;
            uploadSettings.chromatographIP = document.getElementById('daq-chrom-ip').value;
            uploadSettings.enableUpload = document.getElementById('daq-enable').checked;

            try {
                await fetch('/api/v1/uploadconfig?deviceId=' + encodeURIComponent(deviceId), {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(uploadSettings)
                });
            } catch(e) {
                console.error("Failed to save DAQ", e);
            }

            // 2. 保存系统参数
            const payload = {
                auth_pass: currentAuthPass,
                mqtt_enabled: document.getElementById('sys-mqtt-enable').checked,
                mqtt_broker: document.getElementById('sys-mqtt-broker').value,
                mqtt_topic: document.getElementById('sys-mqtt-topic').value,
                mqtt_client_id: document.getElementById('sys-mqtt-clientid').value,
                mqtt_user: document.getElementById('sys-mqtt-user').value,
                mqtt_pass: document.getElementById('sys-mqtt-pass').value,
                mqtt_upload_info: document.getElementById('mqtt-upload-info').checked,
                mqtt_upload_status: document.getElementById('mqtt-upload-status').checked,
                mqtt_upload_result: document.getElementById('mqtt-upload-result').checked,
                mqtt_upload_log: document.getElementById('mqtt-upload-log').checked,
                mqtt_upload_debug: document.getElementById('mqtt-upload-debug').checked,
                driver_mode: document.getElementById('sys-driver-mode').value,
                admin_pass: document.getElementById('sys-admin-pass-new').value,
                modbus_server_port: parseInt(document.getElementById('modbus-server-port').value) || 1502,
                modbus_server_address: document.getElementById('modbus-server-addr').value,
                modbus_upload_log: document.getElementById('modbus-upload-log').checked,
                modular_tcd_port: document.getElementById('sys-modular-tcd-port').value,
                modular_temp_port: document.getElementById('sys-modular-temp-port').value,
                modular_temp_slave_id: parseInt(document.getElementById('sys-modular-temp-slave-id').value) || 20,
                modular_epc_port: document.getElementById('sys-modular-epc-port').value
            };
            try {
                const res = await fetch('/api/sysconfig', {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(payload)
                });
                if (res.ok) {
                    window.showToast('系统高级配置已保存');
                    modal.style.display = 'none';
                } else {
                    window.showToast('保存失败', true);
                }
            } catch (e) {
                window.showToast('网络异常', true);
            }
        });

    }, 0);
}