export function initProcess() {
    const container = document.getElementById('view-process');
    container.innerHTML = `
        <div style="display: flex; flex-direction: column; height: 100%; gap: 1rem;">
            <div class="control-group" style="margin: 0; display: flex; gap: 0.5rem; flex-wrap: wrap;">
                <button class="btn" id="btn-load-process">加载最近一针</button>
                <button class="btn btn-success" id="btn-calibrate">单点标定</button>
                <button class="btn btn-danger" onclick="document.getElementById('process-canvas').getContext('2d').clearRect(0,0,10000,10000)">关闭谱图</button>
                <button class="btn" id="btn-unzoom">满屏</button>
                <button class="btn" id="btn-reset" style="display: none;">旧版重置(隐藏)</button>
                <span style="border-left: 1px solid #334155; margin: 0 0.5rem;"></span>
                <button class="btn" id="btn-mode-zoom" style="background: var(--panel);">放大(拖拽)</button>
                <button class="btn" id="btn-mode-delete">删峰(点两下)</button>
                <button class="btn" id="btn-mode-add">加峰(点两下)</button>
                <button class="btn" id="btn-reset-peaks">重置所有峰</button>
            </div>
            
            <div style="flex: 2; background: var(--panel); border-radius: 8px; border: 1px solid #334155; position: relative; overflow: hidden; cursor: crosshair;">
                <canvas id="process-canvas" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;"></canvas>
            </div>
            
            <div class="control-group" style="flex: 1; margin: 0; overflow-y: auto;">
                <h3 style="margin-top:0">处理结果与标定</h3>
                <table>
                    <thead>
                        <tr>
                            <th>序号</th><th>组份名称</th><th>保留时间</th><th>面积</th><th>高度</th><th>浓度</th><th>标气值(标定用)</th>
                        </tr>
                    </thead>
                    <tbody id="tbody-process-results">
                        <tr><td colspan="7" style="text-align:center; color:#94a3b8">请先加载历史谱图</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;

    let currentRunData = null;
    let originalPollutants = null; // Store original for reset
    let zoomDomain = null; // { iMin, iMax, yMin, yMax }
    let interactionMode = 'zoom'; // 'zoom', 'delete', 'add'
    let pendingClickX = null; // Used for 2-click interactions
    let isDragging = false;
    let dragStart = null;
    let dragCurrent = null;
    let methodData = null;

    const renderTable = () => {
        const tbody = document.getElementById('tbody-process-results'); 
        tbody.innerHTML = '';
        let hasData = false;
        
        // 预定义的可选峰名称
        const availablePeakNames = ['THC', 'CH4'];

        let html = '';
          if (currentRunData && currentRunData.pollutants) {
              hasData = true;
              let displayIndex = 1;
              currentRunData.pollutants.forEach((p, i) => {
                  if (p.status === 'calculated') return;

                  let rtMin = p.rtS !== undefined ? p.rtS / 60.0 : p.retain_time;

                  let defaultAmount = 10.0;
                  if ((p.code || p.name) === 'THC') {
                      defaultAmount = 2.0;
                  } else if ((p.code || p.name) === 'CH4') {
                      defaultAmount = 1.0;
                  }

                  if (methodData && methodData.compounds) {
                      const comp = methodData.compounds.find(c => c.name === (p.code || p.name));
                      if (comp && comp.levels && comp.levels.length > 0) {
                          defaultAmount = comp.levels[0].amount;
                      }
                  }

                  // 构建下拉选择框
                  let selectHtml = `<select class="calib-name-select input" data-index="${i}" style="padding: 2px 4px;">`;
                  let isCustom = true;
                  availablePeakNames.forEach(name => {
                      const selected = (p.code || p.name) === name ? 'selected' : '';
                      if (selected) isCustom = false;
                      selectHtml += `<option value="${name}" ${selected}>${name}</option>`;
                  });
                  if (isCustom) {
                      selectHtml += `<option value="${p.code || p.name}" selected>${p.code || p.name}</option>`;
                  }
                  selectHtml += `</select>`;

                  html += `<tr>
                      <td>${displayIndex++}</td>
                      <td style="font-weight:bold">${selectHtml}</td>
                      <td>${rtMin !== undefined ? rtMin.toFixed(3) : '-'}</td>
                      <td>${p.area !== undefined && p.area !== null ? p.area.toFixed(2) : '-'}</td>
                      <td>${p.height !== undefined && p.height !== null ? p.height.toFixed(2) : '-'}</td>
                      <td style="color:var(--success)">${p.amount !== undefined && p.amount !== null ? p.amount.toFixed(3) : '-'}</td>
                      <td><input type="number" class="calib-amount input" data-index="${i}" value="${defaultAmount}" style="width:70px; padding: 2px 4px; text-align: center;"></td>
                  </tr>`;
              });
          }
        
        if (!hasData) {
            html = '<tr><td colspan="7" style="text-align:center; color:#94a3b8">该针无组分检出</td></tr>';
        }
        tbody.innerHTML = html;

        // 绑定下拉框事件，更新 currentRunData 中的名称
        const selects = tbody.querySelectorAll('.calib-name-select');
        selects.forEach(select => {
            select.addEventListener('change', (e) => {
                const idx = parseInt(e.target.getAttribute('data-index'));
                const newName = e.target.value;
                if (currentRunData && currentRunData.pollutants && currentRunData.pollutants[idx]) {
                    currentRunData.pollutants[idx].code = newName;
                    currentRunData.pollutants[idx].name = newName;
                }
            });
        });
    };

    setTimeout(() => {
        const canvas = document.getElementById('process-canvas');
        if(!canvas) return;
        const ctx = canvas.getContext('2d');
        
        const resizeObserver = new ResizeObserver(entries => {
            for (let entry of entries) {
                if (entry.contentRect.width > 0 && entry.contentRect.height > 0) {
                    canvas.width = entry.contentRect.width;
                    canvas.height = entry.contentRect.height;
                    if (currentRunData) {
                        drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, isDragging ? {start: dragStart, current: dragCurrent} : null);
                    }
                }
            }
        });
        resizeObserver.observe(canvas.parentElement);

        const setMode = (mode) => {
            interactionMode = mode;
            pendingClickX = null;
            document.getElementById('btn-mode-zoom').style.background = mode === 'zoom' ? 'var(--panel)' : '';
            document.getElementById('btn-mode-delete').style.background = mode === 'delete' ? 'var(--panel)' : '';
            document.getElementById('btn-mode-add').style.background = mode === 'add' ? 'var(--panel)' : '';
            canvas.parentElement.style.cursor = mode === 'zoom' ? 'crosshair' : 'pointer';
            if (currentRunData) {
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
            }
        };

        document.getElementById('btn-mode-zoom').addEventListener('click', () => setMode('zoom'));
        document.getElementById('btn-mode-delete').addEventListener('click', () => setMode('delete'));
        document.getElementById('btn-mode-add').addEventListener('click', () => setMode('add'));

        document.getElementById('btn-unzoom').addEventListener('click', () => {
            zoomDomain = null;
            if (currentRunData) {
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
            }
        });

        document.getElementById('btn-reset').addEventListener('click', () => {
            zoomDomain = null;
            if (currentRunData) {
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
            }
        });

        document.getElementById('btn-reset-peaks').addEventListener('click', async () => {
            if (!currentRunData || !currentRunData.samples) {
                window.showToast('没有数据可重置', true);
                return;
            }
            try {
                const res = await fetch('/api/process/detect_all', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ trace_id: currentRunData.trace_id || currentRunData.traceId || '' })
                });
                if (!res.ok) throw new Error('寻峰失败');
                const peaks = await res.json();
                
                currentRunData.pollutants = peaks || [];
                renderTable();
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                window.showToast('重置所有峰成功');
            } catch(e) {
                console.error(e);
                window.showToast('重置所有峰失败: ' + e.message, true);
            }
        });

        const xToTimeS = (x) => {
            const samples = currentRunData.samples;
            let currentI_Min = 0, currentI_Max = samples.length - 1;
            if (zoomDomain) {
                currentI_Min = zoomDomain.iMin;
                currentI_Max = zoomDomain.iMax;
            }
            const i = currentI_Min + (x / canvas.width) * (currentI_Max - currentI_Min);
            const dtS = currentRunData.dtS || 0.05;
            const t0S = currentRunData.t0S || 0.0;
            return t0S + i * dtS;
        };

        // Mouse events
        canvas.addEventListener('mousedown', async (e) => {
            if (!currentRunData || !currentRunData.samples) return;
            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            if (interactionMode === 'zoom') {
                isDragging = true;
                dragStart = { x, y };
                dragCurrent = { x, y };
            } else if (interactionMode === 'delete' || interactionMode === 'add') {
                if (pendingClickX === null) {
                    pendingClickX = x;
                    drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                } else {
                    const x1 = Math.min(pendingClickX, x);
                    const x2 = Math.max(pendingClickX, x);
                    const t1S = xToTimeS(x1);
                    const t2S = xToTimeS(x2);
                    pendingClickX = null;

                    if (!currentRunData.pollutants) currentRunData.pollutants = [];

                    if (interactionMode === 'delete') {
                        // Delete peaks between t1 and t2
                        currentRunData.pollutants = currentRunData.pollutants.filter(p => {
                            const rtS = p.retain_time !== undefined ? p.retain_time * 60.0 : p.rtS;
                            return rtS < t1S || rtS > t2S;
                        });
                        window.showToast('已删除选中区域内的峰');
                        renderTable();
                        drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                    } else if (interactionMode === 'add') {
                        try {
                            const res = await fetch('/api/process/detect_window', {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify({ 
                                    trace_id: currentRunData.trace_id || currentRunData.traceId || '',
                                    start_s: t1S,
                                    end_s: t2S,
                                    name: 'Custom_' + Math.floor((t1S+t2S)/2)
                                })
                            });
                            if (!res.ok) {
                                const err = await res.json();
                                throw new Error(err.error || '添加失败');
                            }
                            const newPeak = await res.json();
                            currentRunData.pollutants.push(newPeak);
                            window.showToast('已添加新峰');
                            
                            renderTable();
                            drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                        } catch(err) {
                            console.error(err);
                            window.showToast(err.message, true);
                            drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                        }
                    }
                }
            }
        });

        canvas.addEventListener('mousemove', (e) => {
            if (isDragging && interactionMode === 'zoom') {
                const rect = canvas.getBoundingClientRect();
                dragCurrent = { x: e.clientX - rect.left, y: e.clientY - rect.top };
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, {start: dragStart, current: dragCurrent}, pendingClickX);
            }
        });

        canvas.addEventListener('mouseup', (e) => {
            if (!isDragging || interactionMode !== 'zoom') return;
            isDragging = false;
            
            const rect = canvas.getBoundingClientRect();
            dragCurrent = { x: e.clientX - rect.left, y: e.clientY - rect.top };
            
            // Check if dragged distance is enough to be a zoom
            if (Math.abs(dragCurrent.x - dragStart.x) > 10 && Math.abs(dragCurrent.y - dragStart.y) > 10) {
                // Calculate new domain based on current view
                const samples = currentRunData.samples;
                
                // Get current view boundaries
                let currentI_Min = 0, currentI_Max = samples.length - 1;
                let currentY_Min, currentY_Max;

                if (zoomDomain) {
                    currentI_Min = zoomDomain.iMin;
                    currentI_Max = zoomDomain.iMax;
                    currentY_Min = zoomDomain.yMin;
                    currentY_Max = zoomDomain.yMax;
                } else {
                    let maxVal = -Infinity, minVal = Infinity;
                    for (let i = 0; i < samples.length; i++) {
                        if (samples[i] > maxVal) maxVal = samples[i];
                        if (samples[i] < minVal) minVal = samples[i];
                    }
                    if (minVal === Infinity) { minVal = 0; maxVal = 1; }
                    let span = (maxVal - minVal);
                    if (span < 0.5) span = 0.5;
                    const V = span / 0.55;
                    currentY_Min = minVal - 0.05 * V;
                    currentY_Max = maxVal + 0.40 * V;
                }

                // Map canvas coordinates to data domain
                const xToI = (x) => currentI_Min + (x / canvas.width) * (currentI_Max - currentI_Min);
                const yToVal = (y) => currentY_Max - (y / canvas.height) * (currentY_Max - currentY_Min);

                const x1 = Math.min(dragStart.x, dragCurrent.x);
                const x2 = Math.max(dragStart.x, dragCurrent.x);
                const y1 = Math.min(dragStart.y, dragCurrent.y); // canvas y increases downwards
                const y2 = Math.max(dragStart.y, dragCurrent.y);

                const newI_Min = Math.max(0, Math.floor(xToI(x1)));
                const newI_Max = Math.min(samples.length - 1, Math.ceil(xToI(x2)));
                
                const newY_Max = yToVal(y1); // Smaller canvas y means larger data y
                const newY_Min = yToVal(y2);

                if (newI_Max > newI_Min && newY_Max > newY_Min) {
                    zoomDomain = {
                        iMin: newI_Min,
                        iMax: newI_Max,
                        yMin: newY_Min,
                        yMax: newY_Max
                    };
                }
            }
            
            drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
        });

        canvas.addEventListener('mouseleave', () => {
            if (isDragging) {
                isDragging = false;
                drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
            }
        });

        document.getElementById('btn-load-process').addEventListener('click', async () => {
            window.showToast('正在获取最近数据..');
            try {
                // Try to get current device ID, if offline, fallback to recent history without filter
                const devRes = await fetch('/api/v1/devices');
                const devices = await devRes.json();
                let deviceIdQuery = '';
                if (devices && devices.length > 0) {
                    deviceIdQuery = `deviceId=${encodeURIComponent(devices[0].deviceId)}&`;
                }

                // Get the latest history result without time boundary (let backend find the absolute latest)
                const res = await fetch(`/api/history/results?${deviceIdQuery}limit=1`);
                const data = await res.json();
                if (!data || data.length === 0) {
                    window.showToast('暂无最近的历史数据', true);      
                    return;
                }

                const record = data[0];
                const traceId = record.trace_id;
                
                // Load full run
                window.showToast('加载完整谱图...');
                const runRes = await fetch(`/api/history/run/${traceId}`);
                const runData = await runRes.json();
                
                // Fetch method to get current calibration amounts
                const methodRes = await fetch('/api/method');
                let methodData = null;
                if (methodRes.ok) {
                    methodData = await methodRes.json();
                }

                if (runData && runData.samples) {
                    currentRunData = runData;
                    
                    // Normalize pollutants array into currentRunData for editing
                    if (!currentRunData.pollutants && currentRunData.result && currentRunData.result.pollutants) {
                        currentRunData.pollutants = currentRunData.result.pollutants;
                    }
                    if (!currentRunData.pollutants) {
                        currentRunData.pollutants = [];
                    }
                    
                    // Backup original for reset
                    originalPollutants = JSON.parse(JSON.stringify(currentRunData.pollutants));

                    zoomDomain = null; // reset zoom on new load
                    pendingClickX = null;
                    drawStaticWaveform(canvas, ctx, currentRunData, zoomDomain, null, pendingClickX);
                }
                
                renderTable();

            } catch(e) {
                console.error(e);
                window.showToast('加载失败: ' + e.message, true);
            }
        });

        document.getElementById('btn-calibrate').addEventListener('click', async () => {
            if (!currentRunData || !currentRunData.pollutants && !(currentRunData.result && currentRunData.result.pollutants)) {
                window.showToast('请先加载包含有效组分的谱图', true);
                return;
            }
            if (!confirm('是否将当前谱图的组分响应值(面积/峰高)作为标定数据更新到分析方法中？')) return;

            try {
                // 1. Fetch current method
                const methodRes = await fetch('/api/method');
                if (!methodRes.ok) throw new Error('无法获取分析方法');
                const method = await methodRes.json();
                
                if (!method.compounds) {
                    method.compounds = [];
                }

                // 2. Extract pollutants from current run
                let pollutants = currentRunData.pollutants || currentRunData.result.pollutants;

                // 3. Update method compounds
                let updatedCount = 0;
                let activeCodes = [];

                pollutants.forEach(p => {
                    if (p.status === 'calculated') return; // Do not calibrate calculated peaks like NMHC

                    // 如果是在表格里重新选了下拉框的名字，这里要以 amountMap 收集到的新名字为准
                    // 不过 amountMap 的键也是我们在表格渲染时生成的，这里需要获取到当前行的 select 真实选择的值
                });

                const amountInputs = document.querySelectorAll('.calib-amount');
                const nameSelects = document.querySelectorAll('.calib-name-select');
                
                for (let i = 0; i < amountInputs.length; i++) {
                    const idx = parseInt(amountInputs[i].getAttribute('data-index'));
                    const p = pollutants[idx];
                    if (!p || p.status === 'calculated') continue;

                    const code = nameSelects[i].value;
                    activeCodes.push(code);

                    let comp = method.compounds.find(c => c.name === code);     

                    let newAmount = parseFloat(amountInputs[i].value) || 10.0;

                    if (!comp) {
                        // Create new compound if not exists
                        comp = {
                            name: code,
                            retain_time: p.retain_time !== undefined ? p.retain_time : (p.rtS ? p.rtS / 60.0 : 0),
                            left_window: 0.1,
                            right_window: 0.1,
                            resp_style: 0, // default area
                            levels: [{ level_index: 1, amount: newAmount, response: 0 }]
                        };
                        method.compounds.push(comp);
                    }

                    if (!comp.levels || comp.levels.length === 0) {
                        comp.levels = [{ level_index: 1, amount: newAmount, response: 0 }];
                    }

                    // Update response (Area or Height) and amount
                    const val = comp.resp_style === 1 ? p.height : p.area;      
                    comp.levels[0].response = val;
                    comp.levels[0].amount = newAmount;

                    // Update retain time to the latest actual RT
                    comp.retain_time = p.retain_time !== undefined ? p.retain_time : (p.rtS ? p.rtS / 60.0 : comp.retain_time);
                    updatedCount++;
                }

                // 4. 清理旧的方法：只保留当前页面实际标定下来的这些峰（例如清掉之前的 Unk_X）
                method.compounds = method.compounds.filter(c => activeCodes.includes(c.name));

                // 5. Save method back
                const saveRes = await fetch('/api/method', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(method)
                });
                
                if (!saveRes.ok) throw new Error('保存标定方法失败');
                
                window.showToast(`标定成功！已更新 ${updatedCount} 个组分的响应系数。`);
                
            } catch(e) {
                console.error(e);
                window.showToast(e.message, true);
            }
        });
        
        // Trigger initial resize
        window.dispatchEvent(new Event('resize'));
    }, 0);
}

function drawStaticWaveform(canvas, ctx, runData, zoomDomain, dragInfo, pendingClickX) {
    const samples = runData.samples;
    const rect = canvas.parentElement.getBoundingClientRect();
    canvas.width = rect.width;
    canvas.height = rect.height;
    
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    
    // Draw grid will be done after we determine the view domain

    if (!samples || samples.length === 0) {
        ctx.fillStyle = '#94a3b8';
        ctx.font = '14px sans-serif';
        ctx.fillText('暂无谱图数据，请点击左上角加载历史记录', 20, 30);
        return;
    }
    
    // Determine view domain
    let iMin = 0, iMax = samples.length - 1;
    let yBeg, yEnd, ySpan;

    if (zoomDomain) {
        iMin = zoomDomain.iMin;
        iMax = zoomDomain.iMax;
        yBeg = zoomDomain.yMin;
        yEnd = zoomDomain.yMax;
        ySpan = yEnd - yBeg;
    } else {
        let maxVal = -Infinity;
        let minVal = Infinity;
        for (let i = 0; i < samples.length; i++) {
            if (samples[i] > maxVal) maxVal = samples[i];
            if (samples[i] < minVal) minVal = samples[i];
        }
        if (minVal === Infinity) { minVal = 0; maxVal = 1; }

        let span = (maxVal - minVal);
        if (span < 0.5) span = 0.5;

        // 下面预留 5%，上面预留 40%，数据占 55%
        const V = span / 0.55;
        yBeg = minVal - 0.05 * V;
        yEnd = maxVal + 0.40 * V;
        ySpan = yEnd - yBeg;
    }

    const scaleX = canvas.width / (iMax - iMin);
    const dtS = runData.dtS || 0.05;
    const t0S = runData.t0S || 0.0;

    // Draw Grid and Axis Labels
    ctx.strokeStyle = '#334155';
    ctx.lineWidth = 1;
    ctx.fillStyle = '#94a3b8';
    ctx.font = '11px system-ui';
    ctx.textBaseline = 'bottom';
    
    // X Axis Grid & Labels (Time in min)
    for (let x = 0; x < canvas.width; x += 80) {
        ctx.beginPath(); ctx.moveTo(x, 0); ctx.lineTo(x, canvas.height); ctx.stroke();
        if (x > 0) {
            const dataI = iMin + (x / canvas.width) * (iMax - iMin);
            const tS = t0S + dataI * dtS;
            ctx.fillText((tS / 60.0).toFixed(2) + 'm', x + 4, canvas.height - 4);
        }
    }
    // Y Axis Grid & Labels (Signal amplitude)
    ctx.textBaseline = 'top';
    for (let y = 0; y < canvas.height; y += 50) {
        ctx.beginPath(); ctx.moveTo(0, y); ctx.lineTo(canvas.width, y); ctx.stroke();
        if (y > 0 && y < canvas.height - 20) {
            const yVal = yBeg + (1 - y / canvas.height) * ySpan;
            ctx.fillText(yVal.toFixed(1), 4, y + 4);
        }
    }

    ctx.strokeStyle = '#3b82f6';
    ctx.lineWidth = 2;
    ctx.beginPath();

    let firstPoint = true;
    for (let i = iMin; i <= iMax; i++) {
        if (i < 0 || i >= samples.length) continue;
        const x = (i - iMin) * scaleX;
        const yn = (samples[i] - yBeg) / ySpan;
        const y = canvas.height * (1 - yn);
        if (firstPoint) {
            ctx.moveTo(x, y);
            firstPoint = false;
        } else {
            ctx.lineTo(x, y);
        }
    }
    ctx.stroke();

    // Draw peak labels if pollutants exist
    let pollutants = runData.pollutants;
    if (!pollutants && runData.result && runData.result.pollutants) {
        pollutants = runData.result.pollutants;
    }

    if (pollutants) {
        pollutants.forEach((p, idx) => {
            if (p.status === 'calculated') return; // Skip drawing calculated peaks like NMHC

            let rtS = p.rtS !== undefined ? p.rtS : (p.retain_time * 60.0);
            if (rtS === undefined || isNaN(rtS)) return;
            // find index in samples
            const i = Math.round((rtS - t0S) / dtS);
            if (i >= iMin && i <= iMax) {
                const x = (i - iMin) * scaleX;
                const yn = (samples[i] - yBeg) / ySpan;
                const y = canvas.height * (1 - yn);

                // Draw vertical dashed line down to peak
                ctx.strokeStyle = '#10b981';
                ctx.lineWidth = 1;
                ctx.setLineDash([4, 4]);
                ctx.beginPath();
                ctx.moveTo(x, 20 + (idx % 3) * 30); // staggered height
                ctx.lineTo(x, y);
                ctx.stroke();
                ctx.setLineDash([]);

                // If we have startS and endS, draw the baseline and shaded area
                if (p.startS !== undefined && p.endS !== undefined && p.startS < p.endS) {
                    const iStart = Math.round((p.startS - t0S) / dtS);
                    const iEnd = Math.round((p.endS - t0S) / dtS);
                    
                    if (iStart >= iMin && iEnd <= iMax && iStart >= 0 && iEnd < samples.length) {
                        const xStart = (iStart - iMin) * scaleX;
                        const yStart = canvas.height * (1 - (samples[iStart] - yBeg) / ySpan);
                        
                        const xEnd = (iEnd - iMin) * scaleX;
                        const yEnd = canvas.height * (1 - (samples[iEnd] - yBeg) / ySpan);
                        
                        // Draw red baseline
                        ctx.strokeStyle = '#ef4444'; // Red
                        ctx.lineWidth = 2;
                        ctx.beginPath();
                        ctx.moveTo(xStart, yStart);
                        ctx.lineTo(xEnd, yEnd);
                        ctx.stroke();
                        
                        // Fill shaded area
                        ctx.fillStyle = 'rgba(239, 68, 68, 0.2)'; // Red transparent
                        ctx.beginPath();
                        ctx.moveTo(xStart, yStart);
                        // trace along the curve
                        for (let j = iStart; j <= iEnd; j++) {
                            const xj = (j - iMin) * scaleX;
                            const yj = canvas.height * (1 - (samples[j] - yBeg) / ySpan);
                            ctx.lineTo(xj, yj);
                        }
                        ctx.lineTo(xEnd, yEnd);
                        ctx.closePath();
                        ctx.fill();
                    }
                }

                // Draw Label Box
                const text = `${p.code || p.name}: ${p.amount ? p.amount.toFixed(2) : '0.00'}`;
                ctx.font = '12px system-ui';
                const textW = ctx.measureText(text).width;
                
                const boxY = 10 + (idx % 3) * 30;
                ctx.fillStyle = 'rgba(15, 23, 42, 0.8)';
                ctx.fillRect(x - textW/2 - 4, boxY - 2, textW + 8, 20);
                
                ctx.strokeStyle = '#10b981';
                ctx.strokeRect(x - textW/2 - 4, boxY - 2, textW + 8, 20);

                ctx.fillStyle = '#10b981';
                ctx.textAlign = 'center';
                ctx.textBaseline = 'top';
                ctx.fillText(text, x, boxY + 2);
            }
        });
    }

    // Draw drag rectangle if zooming
    if (dragInfo && dragInfo.start && dragInfo.current) {
        const x1 = Math.min(dragInfo.start.x, dragInfo.current.x);
        const x2 = Math.max(dragInfo.start.x, dragInfo.current.x);
        const y1 = Math.min(dragInfo.start.y, dragInfo.current.y);
        const y2 = Math.max(dragInfo.start.y, dragInfo.current.y);
        
        ctx.fillStyle = 'rgba(59, 130, 246, 0.2)'; // semi-transparent blue
        ctx.fillRect(x1, y1, x2 - x1, y2 - y1);
        ctx.strokeStyle = '#3b82f6';
        ctx.lineWidth = 1;
        ctx.strokeRect(x1, y1, x2 - x1, y2 - y1);
    }

    // Draw pending click line for interactions
    if (pendingClickX !== null && pendingClickX !== undefined) {
        ctx.strokeStyle = '#ef4444'; // red
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(pendingClickX, 0);
        ctx.lineTo(pendingClickX, canvas.height);
        ctx.stroke();
        
        ctx.fillStyle = '#ef4444';
        ctx.font = '12px system-ui';
        ctx.textAlign = 'left';
        ctx.fillText('请点击右侧结束点', pendingClickX + 5, 20);
    }
}
