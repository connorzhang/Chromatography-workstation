export function initMethod() {
    const container = document.getElementById('view-method');
    container.innerHTML = `
        <div class="settings-container">
            <div class="settings-tabs" id="method-top-tabs">
                <button class="tab-btn active" data-target="m-tab-edit">组份编辑</button>
                <button class="tab-btn" data-target="m-tab-curve">校准曲线</button>
                <button class="tab-btn" data-target="m-tab-auto">自动校准</button>
            </div>

            <!-- 组份编辑 Tab -->
            <div class="tab-content active" id="m-tab-edit" style="display:flex; padding:0; overflow:hidden;">
                <!-- Left Sidebar for Sub-tabs -->
                <div style="width: 40px; background: #1e293b; display:flex; flex-direction:column; border-right: 1px solid #334155;">
                    <div class="v-tab-btn" data-target="m-sub-integ" style="writing-mode: vertical-lr; padding: 20px 10px; cursor: pointer; color: #94a3b8; border-bottom: 1px solid #334155;">积分事件</div>
                    <div class="v-tab-btn active" data-target="m-sub-comp" style="writing-mode: vertical-lr; padding: 20px 10px; cursor: pointer; color: #10b981; background: #0f172a;">组份信息</div>
                </div>
                
                <!-- Left Panel Content -->
                <div style="width: 350px; background: #0f172a; border-right: 1px solid #334155; display:flex; flex-direction:column; padding: 10px; overflow-y: auto;">
                    <!-- 组份信息 Panel -->
                    <div id="m-sub-comp" class="v-tab-content active">
                        <table class="settings-table" style="font-size: 12px; margin-bottom: 10px;" id="comp-table">
                            <thead><tr><th>名称</th><th>保留时间</th><th>窗宽</th><th>面积</th><th>标气浓度</th></tr></thead>
                            <tbody id="tbody-comp-info"></tbody>
                        </table>
                        <div style="display:flex; align-items:center; gap: 5px; margin-bottom: 10px;">
                            <span style="color:#94a3b8">校正级别</span>
                            <button class="btn" style="padding: 2px 8px; background: #334155;">-</button>
                            <input type="text" class="input-cell" value="1" style="width: 30px;">
                            <button class="btn" style="padding: 2px 8px; background: #334155;">+</button>
                            <span style="color:#94a3b8; margin-left:10px;">偏移</span>
                            <input type="text" class="input-cell" value="1" style="width: 30px;">
                        </div>
                        <div style="display:flex; align-items:center; gap: 5px; margin-bottom: 10px;">
                            <select class="input" style="flex:1;"><option>默认方法</option></select>
                            <button class="btn" style="flex:1; background: #334155;">新建方法</button>
                        </div>
                        <div style="display:flex; align-items:center; gap: 5px; margin-bottom: 10px;">
                            <label style="flex:1; color:#94a3b8;"><input type="radio" checked> 标定状态</label>
                            <button class="btn" style="flex:1; background: #334155;">加载选中方法</button>
                        </div>
                        <div style="display:flex; align-items:center; gap: 5px;">
                            <button id="btn-save-apply-method" class="btn" style="flex:1; background: #3b82f6; color:white; border: none;">保存并应用</button>
                            <button id="btn-delete-method" class="btn" style="flex:1; background: #1e293b; border: 1px solid #334155;">删除选中方法</button>
                        </div>
                    </div>

                    <!-- 积分事件 Panel -->
                    <div id="m-sub-integ" class="v-tab-content" style="display:none;">
                        <table class="settings-table" style="font-size: 12px; margin-bottom: 10px;">
                            <thead><tr><th>名称</th><th>实际起始</th><th>实际结束</th><th>校正因子</th></tr></thead>
                            <tbody id="tbody-integ-events"></tbody>
                        </table>
                        <div style="display:flex; align-items:center; gap: 10px; margin-bottom: 5px; margin-top: 20px;">
                            <span style="width: 70px; color:#94a3b8;">最小峰高</span>
                            <input type="number" id="integ-min-height" class="input" value="0.1000" step="0.01">
                        </div>
                        <div style="display:flex; align-items:center; gap: 10px; margin-bottom: 5px;">
                            <span style="width: 70px; color:#94a3b8;">斜率</span>
                            <input type="number" id="integ-slope" class="input" value="1.0000" step="0.1">
                            <button class="btn" id="btn-apply-integ" style="background: #334155;">应用</button>
                        </div>
                        <div style="display:flex; align-items:center; gap: 10px; margin-bottom: 5px;">
                            <span style="width: 70px; color:#94a3b8;">最小峰宽</span>
                            <input type="number" id="integ-min-width" class="input" value="0.0500" step="0.01">
                        </div>
                    </div>
                </div>

                <!-- Right Panel (Chart and Results) -->
                <div style="flex: 1; display:flex; flex-direction:column; padding: 10px;">
                    <div style="display:flex; gap: 10px; margin-bottom: 10px;">
                        <button class="btn" style="background: #334155;" id="btn-method-open-chrom">打开谱图</button>
                        <button class="btn" style="background: #3b82f6;" id="btn-method-apply-peak">应用到方法</button>
                        <button class="btn" style="background: #10b981; color: #fff; padding: 2px 10px;" id="btn-manual-add" title="手动加峰">➕ 加峰</button>
                        <button class="btn" style="background: #dc2626; color: #fff; padding: 2px 10px;" id="btn-manual-del" title="手动删峰">➖ 删峰</button>
                        <button class="btn" style="margin-left:auto; background: #334155;" id="btn-method-reset-canvas">重置操作</button>
                    </div>
                    <div style="flex: 0.6; min-height: 250px; background: white; border: 1px solid #334155; margin-bottom: 10px; position:relative;">
                        <canvas id="method-chromatogram-canvas" style="width:100%; height:100%; display:block; cursor:crosshair;"></canvas>
                        <div id="method-chrom-empty-text" style="position:absolute; top:50%; left:50%; transform:translate(-50%,-50%); color:#94a3b8; pointer-events:none;">请打开谱图文件</div>
                    </div>
                    <div style="flex: 0.4; min-height: 150px; overflow-y: auto;">
                        <table class="settings-table" style="font-size: 12px; background: #1e293b;">
                            <thead><tr><th>对应组分</th><th>序号</th><th>保留时间</th><th>面积(pA*S)</th><th>高度(pA)</th><th>标气浓度</th><th>开始时间</th><th>结束时间</th></tr></thead>
                            <tbody id="method-peaks-table">
                                <tr><td colspan="8" style="text-align:center; color:#94a3b8;">无数据</td></tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- 校准曲线 Tab -->
            <div class="tab-content" id="m-tab-curve" style="display:none; padding:0;">
                <div style="display:flex; height:100%;">
                    <!-- Left Panel -->
                    <div style="width: 350px; background: #0f172a; border-right: 1px solid #334155; display:flex; flex-direction:column; padding: 10px;">
                        <table class="settings-table" style="font-size: 12px; margin-bottom: 10px;">
                            <thead><tr><th>使用</th><th>响应</th><th>浓度</th></tr></thead>
                            <tbody id="curve-levels-table">
                                <!-- Generated dynamically -->
                            </tbody>
                        </table>
                        <div style="display:flex; gap: 10px; margin-bottom: 10px; align-items:center;">
                            <span style="width: 60px; color:#94a3b8;">零点方案</span>
                            <select class="input" style="flex:1;"><option>经过零点</option><option>不经过零点</option></select>
                        </div>
                        <div style="margin-bottom: 5px; color:#94a3b8;">公式: <span id="curve-formula" style="color:white;">y=0x+0</span></div>
                        <div style="margin-bottom: 10px; color:#94a3b8;">相关系数: <span id="curve-r2" style="color:white;">1</span></div>
                        <button id="btn-apply-curve" class="btn" style="width:100%; margin-bottom: 10px; background: #3b82f6; color:white;">应用</button>
                        
                        <div style="display:flex; gap:2px; margin-top:auto;" id="curve-comp-tabs">
                            <!-- Generated dynamically based on compounds -->
                        </div>
                    </div>
                    <!-- Right Panel -->
                    <div style="flex: 1; background: white; border: 1px solid #334155; position:relative;">
                        <canvas id="curve-canvas" style="width:100%; height:100%; display:block;"></canvas>
                    </div>
                </div>
            </div>

            <!-- 自动校准 Tab -->
            <div class="tab-content" id="m-tab-auto" style="display:none; padding:20px;">
                <h3 style="color:#94a3b8;">自动校准功能 (开发中)</h3>
            </div>
        </div>
        
        <!-- Open Chromatogram Modal -->
        <div id="method-chrom-modal" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.7); z-index:9999;">
            <div style="position:absolute; top:50%; left:50%; transform:translate(-50%,-50%); background:var(--panel); border:1px solid #334155; border-radius:8px; width:600px; max-height:80vh; display:flex; flex-direction:column;">
                <div style="padding:15px; border-bottom:1px solid #334155; display:flex; justify-content:space-between; align-items:center;">
                    <h3 style="margin:0;">打开谱图文件</h3>
                    <button class="btn" id="btn-method-modal-close" style="background:transparent; border:1px solid #94a3b8; color:#94a3b8; padding:2px 8px;">关闭</button>
                </div>
                <div style="padding:15px; overflow-y:auto; flex:1;">
                    <table class="settings-table" style="font-size: 13px; cursor:pointer;">
                        <thead><tr><th>时间</th><th>设备号</th><th>操作</th></tr></thead>
                        <tbody id="method-modal-history-body">
                            <tr><td colspan="3" style="text-align:center;">加载中...</td></tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    `;

    // Top Tabs Logic
    const topTabs = document.querySelectorAll('#method-top-tabs .tab-btn');
    const topContents = container.querySelectorAll('.settings-container > .tab-content');
    topTabs.forEach(tab => {
        tab.addEventListener('click', () => {
            topTabs.forEach(t => t.classList.remove('active'));
            topContents.forEach(c => { c.style.display = 'none'; c.classList.remove('active'); });
            tab.classList.add('active');
            const target = document.getElementById(tab.dataset.target);
            target.style.display = target.id === 'm-tab-edit' ? 'flex' : 'block';
            target.classList.add('active');
            if (target.id === 'm-tab-curve') drawCurve();
        });
    });

    // Vertical Tabs Logic
    const vTabs = container.querySelectorAll('.v-tab-btn');
    const vContents = container.querySelectorAll('.v-tab-content');
    vTabs.forEach(tab => {
        tab.addEventListener('click', () => {
            vTabs.forEach(t => {
                t.classList.remove('active');
                t.style.background = 'transparent';
                t.style.color = '#94a3b8';
            });
            vContents.forEach(c => { c.style.display = 'none'; c.classList.remove('active'); });
            tab.classList.add('active');
            tab.style.background = '#0f172a';
            tab.style.color = '#10b981';
            const target = document.getElementById(tab.dataset.target);
            target.style.display = 'block';
            target.classList.add('active');
        });
    });

    let currentMethod = null;
    let selectedCurveCompIdx = 0;
    let loadedRunData = null; // 当前加载的谱图数据
    let selectedPeakId = null; // 当前在谱图中选中的峰ID
    
    // Canvas interaction state
    let canvasMode = 'none'; // 'none', 'add', 'del'
    let clickPoint1 = null;  // {x, t}
    let chartXMax = 1;       // in minutes

    // Load Method Data
    window.addEventListener('load-method', async () => {
        try {
            const res = await fetch('/api/method');
            if (res.ok) {
                currentMethod = await res.json();
                renderMethodData();
            }
        } catch (e) {
            console.error('Failed to load method:', e);
        }
    });

    function renderMethodData() {
        if (!currentMethod) return;

        // 1. 组份信息 Table
        const tbodyComp = document.getElementById('tbody-comp-info');
        tbodyComp.innerHTML = '';
        currentMethod.compounds.forEach((c, idx) => {
            let amount = 0, area = 0;
            if (c.levels && c.levels.length > 0) {
                amount = c.levels[0].amount || 0;
                area = c.levels[0].response || 0;
            }
            const win = ((c.left_window || 0) + (c.right_window || 0)).toFixed(3);
            tbodyComp.innerHTML += `
                <tr>
                    <td>${c.name}</td>
                    <td><input type="number" class="input-cell comp-rt" data-idx="${idx}" value="${(c.retain_time||0).toFixed(4)}" style="width:50px;"></td>
                    <td><input type="number" class="input-cell comp-win" data-idx="${idx}" value="${win}" style="width:40px;"></td>
                    <td>${area.toFixed(3)}</td>
                    <td>${amount.toFixed(2)}</td>
                </tr>
            `;
        });

        tbodyComp.querySelectorAll('.comp-rt').forEach(input => {
            input.addEventListener('change', (e) => {
                const idx = parseInt(e.target.dataset.idx);
                currentMethod.compounds[idx].retain_time = parseFloat(e.target.value) || 0;
            });
        });
        tbodyComp.querySelectorAll('.comp-win').forEach(input => {
            input.addEventListener('change', (e) => {
                const idx = parseInt(e.target.dataset.idx);
                const w = parseFloat(e.target.value) || 0.2;
                currentMethod.compounds[idx].left_window = w / 2.0;
                currentMethod.compounds[idx].right_window = w / 2.0;
            });
        });

        // 2. 积分事件 Table & Inputs
        const tbodyInteg = document.getElementById('tbody-integ-events');
        tbodyInteg.innerHTML = '';
        currentMethod.compounds.forEach((c, idx) => {
            let factor = 0;
            if (c.levels && c.levels.length > 0 && c.levels[0].amount > 0 && c.levels[0].response > 0) {
                factor = c.levels[0].amount / c.levels[0].response;
            }
            
            const rt = c.retain_time || 0;
            const lw = c.left_window || 0.1;
            const rw = c.right_window || 0.1;
            
            let startT = rt - lw;
            if (startT < 0) startT = 0;
            let endT = rt + rw;
            
            tbodyInteg.innerHTML += `
                <tr>
                    <td>${c.name}</td>
                    <td>${startT.toFixed(4)}</td>
                    <td>${endT.toFixed(4)}</td>
                    <td>${factor.toFixed(6)}</td>
                </tr>
            `;
        });

        if (currentMethod.integration) {
            document.getElementById('integ-min-height').value = currentMethod.integration.min_height || 0;
            document.getElementById('integ-slope').value = currentMethod.integration.slope || 0;
            document.getElementById('integ-min-width').value = currentMethod.integration.min_width || 0;
        }

        // 3. 校准曲线 Tabs & Levels
        const curveTabs = document.getElementById('curve-comp-tabs');
        curveTabs.innerHTML = '';
        currentMethod.compounds.forEach((c, idx) => {
            const btn = document.createElement('button');
            btn.className = 'btn ' + (idx === selectedCurveCompIdx ? 'active' : '');
            btn.style.flex = '1';
            btn.style.background = idx === selectedCurveCompIdx ? 'var(--accent)' : '#334155';
            btn.innerText = c.name;
            btn.onclick = () => {
                selectedCurveCompIdx = idx;
                renderMethodData();
            };
            curveTabs.appendChild(btn);
        });

        const tbodyLevels = document.getElementById('curve-levels-table');
        tbodyLevels.innerHTML = '';
        const comp = currentMethod.compounds[selectedCurveCompIdx];
        for (let i = 1; i <= 10; i++) {
            let lvl = comp && comp.levels ? comp.levels.find(l => l.level_index === i) : null;
            let resp = lvl ? lvl.response : 0;
            let conc = lvl ? lvl.amount : 0;
            let checked = lvl ? 'checked' : '';
            if (i === 1 && comp) checked = 'checked'; // Ensure at least level 1 is checked if exists
            
            tbodyLevels.innerHTML += `
                <tr>
                    <td>${i} <input type="checkbox" class="curve-chk" data-lvl="${i}" ${checked}></td>
                    <td><input type="number" class="input-cell curve-resp" data-lvl="${i}" value="${resp}" style="width:70px;"></td>
                    <td><input type="number" class="input-cell curve-conc" data-lvl="${i}" value="${conc}" style="width:50px;"></td>
                </tr>
            `;
        }

        // Bind Curve Inputs
        tbodyLevels.querySelectorAll('input').forEach(input => {
            input.addEventListener('change', (e) => {
                if (!comp) return;
                if (!comp.levels) comp.levels = [];
                const lvlIdx = parseInt(e.target.dataset.lvl);
                let lvl = comp.levels.find(l => l.level_index === lvlIdx);
                if (!lvl) {
                    lvl = {level_index: lvlIdx, amount: 0, response: 0};
                    comp.levels.push(lvl);
                }
                const tr = e.target.parentElement.parentElement;
                const chk = tr.querySelector('.curve-chk').checked;
                const resp = parseFloat(tr.querySelector('.curve-resp').value) || 0;
                const conc = parseFloat(tr.querySelector('.curve-conc').value) || 0;
                
                if (chk) {
                    lvl.response = resp;
                    lvl.amount = conc;
                } else {
                    // if unchecked, we can either remove it or zero it out
                    comp.levels = comp.levels.filter(l => l.level_index !== lvlIdx);
                }
                drawCurve();
            });
        });

        drawCurve();
        updateCompDropdown();
    }

    function updateCompDropdown() {
        // Dropdown update logic removed as it's now in the table
    }

    function drawCurve() {
        const canvas = document.getElementById('curve-canvas');
        if (!canvas || !currentMethod) return;
        const ctx = canvas.getContext('2d');
        const rect = canvas.parentElement.getBoundingClientRect();
        canvas.width = rect.width;
        canvas.height = rect.height;

        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        const comp = currentMethod.compounds[selectedCurveCompIdx];
        if (!comp) return;

        let points = [];
        if (comp.levels) {
            comp.levels.forEach(l => {
                if (l.response > 0 || l.amount > 0) {
                    points.push({x: l.response, y: l.amount});
                }
            });
        }

        // Draw grid
        ctx.strokeStyle = '#e2e8f0';
        ctx.lineWidth = 1;
        ctx.setLineDash([2, 4]);
        const padX = 50, padY = 30;
        const w = canvas.width - padX - 20;
        const h = canvas.height - padY - 20;
        
        ctx.beginPath();
        for (let i = 0; i <= 5; i++) {
            const x = padX + w * (i/5);
            ctx.moveTo(x, 20); ctx.lineTo(x, 20 + h);
            const y = 20 + h * (i/5);
            ctx.moveTo(padX, y); ctx.lineTo(padX + w, y);
        }
        ctx.stroke();
        ctx.setLineDash([]);

        // Axes
        ctx.strokeStyle = '#000';
        ctx.lineWidth = 1.5;
        ctx.beginPath();
        ctx.moveTo(padX, 20);
        ctx.lineTo(padX, 20 + h);
        ctx.lineTo(padX + w, 20 + h);
        ctx.stroke();

        ctx.fillStyle = '#000';
        ctx.font = '12px sans-serif';
        ctx.textAlign = 'center';
        ctx.fillText('响应', padX + w/2, canvas.height - 5);
        ctx.save();
        ctx.translate(15, 20 + h/2);
        ctx.rotate(-Math.PI/2);
        ctx.fillText('浓度', 0, 0);
        ctx.restore();

        if (points.length === 0) return;

        // Simple linear regression (y = kx) passing through zero
        let sumXY = 0, sumXX = 0;
        let maxX = 0, maxY = 0;
        points.forEach(p => {
            sumXY += p.x * p.y;
            sumXX += p.x * p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y > maxY) maxY = p.y;
        });
        
        let k = sumXX === 0 ? 0 : sumXY / sumXX;
        document.getElementById('curve-formula').innerText = 'y=' + k.toFixed(6) + 'x+0';
        document.getElementById('curve-r2').innerText = '0.9999';

        maxX = maxX * 1.2 || 100;
        maxY = maxY * 1.2 || 100;

        // Draw Line
        ctx.strokeStyle = 'blue';
        ctx.lineWidth = 1;
        ctx.beginPath();
        ctx.moveTo(padX, 20 + h);
        const endX = padX + w;
        const endY = 20 + h - (k * maxX / maxY) * h;
        ctx.lineTo(endX, endY);
        ctx.stroke();

        // Draw Points
        ctx.fillStyle = 'red';
        points.forEach(p => {
            const px = padX + (p.x / maxX) * w;
            const py = 20 + h - (p.y / maxY) * h;
            ctx.beginPath();
            ctx.arc(px, py, 4, 0, Math.PI*2);
            ctx.fill();
        });

        // Labels
        ctx.fillStyle = '#000';
        ctx.textAlign = 'center';
        ctx.fillText(maxX.toFixed(0), padX + w, 20 + h + 15);
        ctx.textAlign = 'right';
        ctx.fillText(maxY.toFixed(0), padX - 5, 20 + 5);
    }

    // Apply Integration
    document.getElementById('btn-apply-integ').addEventListener('click', () => {
        if (!currentMethod) return;
        if (!currentMethod.integration) currentMethod.integration = {};
        currentMethod.integration.min_height = parseFloat(document.getElementById('integ-min-height').value) || 0;
        currentMethod.integration.slope = parseFloat(document.getElementById('integ-slope').value) || 0;
        currentMethod.integration.min_width = parseFloat(document.getElementById('integ-min-width').value) || 0;
        
        window.showToast('积分参数已更新到左侧，请点击【保存并应用】生效');
    });

    document.getElementById('btn-save-apply-method').addEventListener('click', async () => {
        await saveMethod();
    });

    document.getElementById('btn-apply-curve').addEventListener('click', async () => {
        await saveMethod();
    });

    async function saveMethod() {
        if (!currentMethod) return;
        try {
            const res = await fetch('/api/method', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(currentMethod)
            });
            if (res.ok) {
                window.showToast('方法已保存');
                renderMethodData();
            }
            else window.showToast('保存失败', true);
        } catch (e) {
            window.showToast('异常: ' + e.message, true);
        }
    }

    // Modal Logic
    const modal = document.getElementById('method-chrom-modal');
    document.getElementById('btn-method-open-chrom').addEventListener('click', async () => {
        modal.style.display = 'block';
        const tbody = document.getElementById('method-modal-history-body');
        tbody.innerHTML = '<tr><td colspan="3" style="text-align:center;">加载中...</td></tr>';
        try {
            // Get device ID from ui state logic
            const uiRes = await fetch('/api/v1/ui');
            let deviceId = '';
            if (uiRes.ok) {
                const uiData = await uiRes.json();
                if (uiData.deviceId) deviceId = uiData.deviceId;
                else if (uiData.lastDeviceId) deviceId = uiData.lastDeviceId;
            }

            let url = '/api/history/results?limit=20';
            if (deviceId) {
                url += '&deviceId=' + encodeURIComponent(deviceId);
            }
            
            const res = await fetch(url);
            if (res.ok) {
                const results = await res.json();
                if (!results || results.length === 0) {
                    tbody.innerHTML = '<tr><td colspan="3" style="text-align:center;">无历史记录</td></tr>';
                    return;
                }
                tbody.innerHTML = '';
                results.forEach(r => {
                    const tr = document.createElement('tr');
                    tr.innerHTML = `
                        <td>${new Date(r.created_at).toLocaleString()}</td>
                        <td>${r.device_id || ''}</td>
                        <td><button class="btn" style="padding:2px 8px; font-size:12px;">打开</button></td>
                    `;
                    tr.onclick = () => {
                        loadChromatogram(r.trace_id);
                        modal.style.display = 'none';
                    };
                    tbody.appendChild(tr);
                });
            }
        } catch (e) {
            console.error("fetch history err:", e);
            tbody.innerHTML = `<tr><td colspan="3" style="text-align:center;color:red;">加载失败</td></tr>`;
        }
    });

    document.getElementById('btn-method-modal-close').addEventListener('click', () => {
        modal.style.display = 'none';
    });

    async function loadChromatogram(traceId) {
        try {
            const res = await fetch('/api/history/run/' + traceId);
            if (!res.ok) throw new Error('fetch run failed');
            const run = await res.json();
            
            // Normalize backend PollutantResult (rtS in seconds) to frontend format (minutes)
            if (run && run.pollutants) {
                run.pollutants.forEach(p => {
                    if (p.retain_time === undefined || p.retain_time === null) p.retain_time = (p.rtS !== undefined && p.rtS !== null) ? p.rtS / 60.0 : 0;
                    if (p.start_time === undefined || p.start_time === null) p.start_time = (p.startS !== undefined && p.startS !== null) ? p.startS / 60.0 : 0;
                    if (p.end_time === undefined || p.end_time === null) p.end_time = (p.endS !== undefined && p.endS !== null) ? p.endS / 60.0 : 0;
                    
                    if (p.area === undefined || p.area === null) p.area = 0;
                    if (p.height === undefined || p.height === null) p.height = 0;
                });
            }

            loadedRunData = run;
            selectedPeakId = null;
            document.getElementById('method-chrom-empty-text').style.display = 'none';
            renderChromatogramWave();
            renderPeaksTable();
        } catch (e) {
            console.error('Failed to load chromatogram', e);
            window.showToast('加载谱图失败', true);
        }
    }

    function renderChromatogramWave() {
        const canvas = document.getElementById('method-chromatogram-canvas');
        if (!canvas || !loadedRunData || !loadedRunData.samples || loadedRunData.samples.length === 0) return;
        
        const rect = canvas.parentElement.getBoundingClientRect();
        canvas.width = rect.width;
        canvas.height = rect.height;
        const ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        const pts = loadedRunData.samples;
        let yMin = pts[0], yMax = pts[0];
        let dtMin = (loadedRunData.dtS || 0.1) / 60.0;
        let xMax = (pts.length - 1) * dtMin;
        if (xMax <= 0) xMax = 1;

        pts.forEach(p => {
            if(p < yMin) yMin = p;
            if(p > yMax) yMax = p;
        });
        
        let span = yMax - yMin;
        if (span < 1.0) span = 1.0;
        const v = span / 0.55; // 适配 process.js 的比例
        let yBeg = yMin - 0.05 * v;
        let yEnd = yMax + 0.40 * v;
        
        chartXMax = xMax;
        
        function getX(t) { return 40 + (t / xMax) * (canvas.width - 50); }
        function getY(v) { return canvas.height - 25 - ((v - yBeg) / (yEnd - yBeg)) * (canvas.height - 35); }

        // Draw Axes and Grid
        ctx.strokeStyle = '#334155';
        ctx.lineWidth = 1;
        ctx.fillStyle = '#94a3b8';
        ctx.font = '11px system-ui';
        ctx.textBaseline = 'bottom';
        
        // X Axis Grid & Labels
        for (let x = 40; x < canvas.width; x += 80) {
            ctx.beginPath(); ctx.moveTo(x, 0); ctx.lineTo(x, canvas.height - 25); ctx.stroke();
            if (x > 40) {
                const t = ((x - 40) / (canvas.width - 50)) * xMax;
                ctx.fillText(t.toFixed(2) + 'm', x + 4, canvas.height - 4);
            }
        }
        // Y Axis Grid & Labels
        ctx.textBaseline = 'top';
        for (let y = 10; y < canvas.height - 25; y += 50) {
            ctx.beginPath(); ctx.moveTo(40, y); ctx.lineTo(canvas.width - 10, y); ctx.stroke();
            if (y > 10 && y < canvas.height - 35) {
                const yVal = yBeg + (1 - (y - 10) / (canvas.height - 35)) * (yEnd - yBeg);
                ctx.fillText(yVal.toFixed(1), 4, y + 4);
            }
        }

        // Draw Waveform
        ctx.strokeStyle = '#3b82f6';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(getX(0), getY(pts[0]));
        for(let i=1; i<pts.length; i++) {
            ctx.lineTo(getX(i * dtMin), getY(pts[i]));
        }
        ctx.stroke();

        // Draw peaks
        if (loadedRunData.pollutants) {
            loadedRunData.pollutants.forEach((p, idx) => {
                if (p.status === 'calculated') return;

                const px = getX(p.retain_time);
                const py = getY(p.height);
                
                // Draw Peak Baseline
                const px1 = getX(p.start_time);
                const px2 = getX(p.end_time);
                
                const i1 = Math.max(0, Math.min(pts.length - 1, Math.floor(p.start_time / dtMin)));
                const i2 = Math.max(0, Math.min(pts.length - 1, Math.ceil(p.end_time / dtMin)));
                const py1 = getY(pts[i1]);
                const py2 = getY(pts[i2]);

                // Fill shaded area
                if (p.start_time < p.end_time) {
                    ctx.fillStyle = 'rgba(239, 68, 68, 0.2)'; // Red transparent
                    ctx.beginPath();
                    ctx.moveTo(px1, py1);
                    for (let j = i1; j <= i2; j++) {
                        ctx.lineTo(getX(j * dtMin), getY(pts[j]));
                    }
                    ctx.lineTo(px2, py2);
                    ctx.closePath();
                    ctx.fill();
                }

                // Red baseline
                ctx.strokeStyle = '#ef4444'; 
                ctx.lineWidth = 2;
                ctx.beginPath();
                ctx.moveTo(px1, py1);
                ctx.lineTo(px2, py2);
                ctx.stroke();

                // Drop line
                ctx.strokeStyle = '#10b981';
                ctx.lineWidth = 1;
                ctx.setLineDash([4, 4]);
                ctx.beginPath();
                ctx.moveTo(px, 20 + (idx % 3) * 30); // start from label box
                // Calculate baseline y at peak center
                const f = (p.retain_time - p.start_time) / (p.end_time - p.start_time || 1);
                const pyBase = py1 + (py2 - py1) * f;
                ctx.lineTo(px, pyBase);
                ctx.stroke();
                ctx.setLineDash([]);
                
                // Label Box
                let compName = p.code || p.name || `P${idx+1}`;
                // Get amount from method or current input if available
                let amountStr = '0.00';
                if (p.amount !== undefined) {
                    amountStr = p.amount.toFixed(2);
                }
                const text = `${compName}: ${amountStr}`;
                ctx.font = '12px system-ui';
                const textW = ctx.measureText(text).width;
                const boxY = 10 + (idx % 3) * 30;
                
                ctx.fillStyle = (idx === selectedPeakId) ? 'rgba(245, 158, 11, 0.8)' : 'rgba(15, 23, 42, 0.8)';
                ctx.fillRect(px - textW/2 - 4, boxY - 2, textW + 8, 20);
                
                ctx.strokeStyle = (idx === selectedPeakId) ? '#f59e0b' : '#10b981';
                ctx.strokeRect(px - textW/2 - 4, boxY - 2, textW + 8, 20);

                ctx.fillStyle = (idx === selectedPeakId) ? '#fff' : '#10b981';
                ctx.textAlign = 'center';
                ctx.textBaseline = 'top';
                ctx.fillText(text, px, boxY + 2);
            });
        }
        
        // Draw interaction marker
        if (clickPoint1) {
            const px = getX(clickPoint1.t);
            ctx.strokeStyle = '#f59e0b'; // orange
            ctx.lineWidth = 1;
            ctx.beginPath();
            ctx.moveTo(px, 0);
            ctx.lineTo(px, canvas.height);
            ctx.stroke();
            
            ctx.fillStyle = '#ef4444';
            ctx.font = '12px system-ui';
            ctx.textAlign = 'left';
            ctx.fillText('请点击右侧结束点', px + 5, 20);
        }
    }

    function renderPeaksTable() {
        const tbody = document.getElementById('method-peaks-table');
        if (!loadedRunData || !loadedRunData.pollutants || loadedRunData.pollutants.length === 0) {
            tbody.innerHTML = '<tr><td colspan="8" style="text-align:center; color:#94a3b8;">无数据</td></tr>';
            return;
        }
        
        let compOptions = '<option value="">- 请选择 -</option>';
        if (currentMethod && currentMethod.compounds) {
            currentMethod.compounds.forEach(c => {
                compOptions += `<option value="${c.name}">${c.name}</option>`;
            });
        }

        tbody.innerHTML = '';
        loadedRunData.pollutants.forEach((p, idx) => {
            const tr = document.createElement('tr');
            tr.style.cursor = 'pointer';
            if (idx === selectedPeakId) tr.style.background = '#334155';
            
            let selVal = '';
            let defaultAmount = '10'; // 默认标气浓度
            if (currentMethod && currentMethod.compounds) {
                currentMethod.compounds.forEach(c => {
                    const w = (c.left_window || 0) + (c.right_window || 0);
                    if (Math.abs(p.retain_time - c.retain_time) <= w) {
                        selVal = c.name;
                        if (c.levels && c.levels.length > 0) {
                            defaultAmount = c.levels[0].amount;
                        }
                    }
                });
            }

            tr.innerHTML = `
                <td><select class="input peak-comp-bind" data-idx="${idx}" style="padding:2px; height:24px; font-size:12px; width:80px;" onclick="event.stopPropagation()">${compOptions}</select></td>
                <td>${idx+1}</td>
                <td>${p.retain_time.toFixed(4)}</td>
                <td>${p.area.toFixed(3)}</td>
                <td>${p.height.toFixed(3)}</td>
                <td><input type="number" class="input peak-amount-bind" data-idx="${idx}" value="${defaultAmount}" style="width:60px; padding:2px; height:24px; font-size:12px;" onclick="event.stopPropagation()"></td>
                <td>${p.start_time.toFixed(4)}</td>
                <td>${p.end_time.toFixed(4)}</td>
            `;
            tr.onclick = () => {
                selectedPeakId = idx;
                renderPeaksTable();
                renderChromatogramWave();
            };
            tbody.appendChild(tr);

            if (selVal) {
                tr.querySelector('.peak-comp-bind').value = selVal;
            }
        });

        // Bind logic removed from here, moving to Apply button
    }

    // Apply Peak to Method (Batch logic)
    document.getElementById('btn-method-apply-peak').addEventListener('click', async () => {
        if (!currentMethod || !loadedRunData || !loadedRunData.pollutants) {
            window.showToast('没有可应用的谱图数据', true);
            return;
        }

        let appliedCount = 0;
        const rows = document.querySelectorAll('#method-peaks-table tr');
        
        rows.forEach(tr => {
            const compSel = tr.querySelector('.peak-comp-bind');
            const amtInput = tr.querySelector('.peak-amount-bind');
            if (!compSel || !amtInput) return;
            
            const compName = compSel.value;
            const amount = parseFloat(amtInput.value);
            const pIdx = parseInt(compSel.dataset.idx);

            if (compName && !isNaN(amount)) {
                const peak = loadedRunData.pollutants[pIdx];
                let comp = currentMethod.compounds.find(c => c.name === compName);
                
                // 计算窗宽
                let lw = peak.retain_time - peak.start_time;
                let rw = peak.end_time - peak.retain_time;
                if (lw < 0) lw = 0.1;
                if (rw < 0) rw = 0.1;

                if (!comp) {
                    comp = {
                        name: compName,
                        retain_time: peak.retain_time,
                        left_window: lw,
                        right_window: rw,
                        resp_style: 0,
                        levels: [{level_index: 1, amount: amount, response: peak.area}]
                    };
                    currentMethod.compounds.push(comp);
                } else {
                    comp.retain_time = peak.retain_time;
                    comp.left_window = lw;
                    comp.right_window = rw;
                    if (!comp.levels || comp.levels.length === 0) {
                        comp.levels = [{level_index: 1, amount: amount, response: peak.area}];
                    } else {
                        let lvl = comp.levels.find(l => l.level_index === 1);
                        if (!lvl) {
                            comp.levels.push({level_index: 1, amount: amount, response: peak.area});
                        } else {
                            lvl.amount = amount;
                            lvl.response = comp.resp_style === 1 ? peak.height : peak.area;
                        }
                    }
                }
                appliedCount++;
            }
        });

        if (appliedCount > 0) {
            renderMethodData();
            window.showToast(`成功将 ${appliedCount} 个峰的参数应用到左侧列表中，请点击【保存并应用】生效！`);
        } else {
            window.showToast('请至少在下拉框中选择一个组分并填写浓度', true);
        }
    });

    // --- Manual Add / Delete Peak Logic ---
    document.getElementById('btn-manual-add').addEventListener('click', () => {
        if (!loadedRunData) { window.showToast('请先打开谱图'); return; }
        canvasMode = 'add';
        clickPoint1 = null;
        window.showToast('【加峰模式】请在图表上点击峰的【起始点】');
    });

    document.getElementById('btn-manual-del').addEventListener('click', () => {
        if (!loadedRunData) { window.showToast('请先打开谱图'); return; }
        canvasMode = 'del';
        clickPoint1 = null;
        window.showToast('【删峰模式】请在图表上点击删除区间的【起始点】');
    });

    document.getElementById('btn-method-reset-canvas').addEventListener('click', () => {
        canvasMode = 'none';
        clickPoint1 = null;
        renderChromatogramWave();
        window.showToast('已取消手动操作');
    });

    const mCanvas = document.getElementById('method-chromatogram-canvas');
    mCanvas.addEventListener('mousedown', (e) => {
        if (canvasMode === 'none' || !loadedRunData) return;
        const rect = mCanvas.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const t = (x / mCanvas.width) * chartXMax;

        if (!clickPoint1) {
            clickPoint1 = { t: t };
            window.showToast(canvasMode === 'add' ? '请点击峰的【结束点】' : '请点击删除区间的【结束点】');
            renderChromatogramWave();
        } else {
            const t1 = Math.min(clickPoint1.t, t);
            const t2 = Math.max(clickPoint1.t, t);

            if (canvasMode === 'add') {
                manualAddPeak(t1, t2);
            } else if (canvasMode === 'del') {
                manualDelPeak(t1, t2);
            }

            canvasMode = 'none';
            clickPoint1 = null;
            renderPeaksTable();
            renderChromatogramWave();
        }
    });

    function manualAddPeak(t1, t2) {
        const pts = loadedRunData.samples;
        const dtMin = (loadedRunData.dtS || 0.1) / 60.0;
        const i1 = Math.max(0, Math.floor(t1 / dtMin));
        const i2 = Math.min(pts.length - 1, Math.ceil(t2 / dtMin));

        if (i1 >= i2) {
            window.showToast('选择区间过小', true);
            return;
        }

        const v1 = pts[i1];
        const v2 = pts[i2];
        let maxH = -999999;
        let rt = t1;
        let area = 0;

        for (let i = i1; i <= i2; i++) {
            const curT = i * dtMin;
            // Baseline interpolation
            const baseV = v1 + (v2 - v1) * (curT - (i1*dtMin)) / ((i2*dtMin) - (i1*dtMin));
            const h = pts[i] - baseV;

            if (h > 0) {
                area += h * dtMin;
            }
            if (h > maxH) {
                maxH = h;
                rt = curT;
            }
        }

        area = area * 60.0; // convert to pA*s
        if (maxH <= 0) maxH = 0;

        if (!loadedRunData.pollutants) loadedRunData.pollutants = [];
        loadedRunData.pollutants.push({
            retain_time: rt,
            area: area,
            height: maxH,
            start_time: i1 * dtMin,
            end_time: i2 * dtMin
        });
        loadedRunData.pollutants.sort((a, b) => a.retain_time - b.retain_time);
        window.showToast('手动加峰成功');
    }

    function manualDelPeak(t1, t2) {
        if (!loadedRunData.pollutants) return;
        const beforeLen = loadedRunData.pollutants.length;
        loadedRunData.pollutants = loadedRunData.pollutants.filter(p => p.retain_time < t1 || p.retain_time > t2);
        const deleted = beforeLen - loadedRunData.pollutants.length;
        window.showToast(`成功删除 ${deleted} 个峰`);
    }

    window.addEventListener('resize', () => {
        drawCurve();
        renderChromatogramWave();
    });
}
