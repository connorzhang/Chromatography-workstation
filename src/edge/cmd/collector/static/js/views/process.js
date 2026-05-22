export function initProcess() {
    const container = document.getElementById('view-process');
    container.innerHTML = `
        <div style="display: flex; flex-direction: column; height: 100%; gap: 1rem;">
            <div class="control-group" style="margin: 0; display: flex; gap: 0.5rem; flex-wrap: wrap;">
                <button class="btn">打开谱图</button>
                <button class="btn btn-danger">关闭谱图</button>
                <button class="btn">保存 xml</button>
                <button class="btn">框选放大</button>
                <button class="btn">满屏</button>
                <button class="btn">重置</button>
            </div>
            
            <div style="flex: 2; background: var(--panel); border-radius: 8px; border: 1px solid #334155; position: relative;">
                <div style="position:absolute; top:50%; left:50%; transform:translate(-50%,-50%); color:#94a3b8;">
                    [历史色谱图处理区域 - 待接入 Canvas]
                </div>
            </div>
            
            <div class="control-group" style="flex: 1; margin: 0; overflow-y: auto;">
                <h3 style="margin-top:0">处理结果</h3>
                <table>
                    <thead>
                        <tr>
                            <th>序号</th><th>组份名称</th><th>保留时间</th><th>面积</th><th>高度</th><th>开始时间</th><th>结束时间</th><th>浓度</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr><td colspan="8" style="text-align:center; color:#94a3b8">请先加载历史谱图</td></tr>
                    </tbody>
                </table>
            </div>
        </div>
    `;
}
