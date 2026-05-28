import { initDashboard } from './views/dashboard.js?v=0.3.18';
import { initLiveChromatogram } from './views/live.js?v=0.3.18';
import { initMethod } from './views/method.js?v=0.3.18';
import { initSettings } from './views/settings.js?v=0.3.18';
import { initProcess } from './views/process.js?v=0.3.18';
import { initReport } from './views/report.js?v=0.3.18';
import { initDebug } from './views/debug.js?v=0.3.18';

document.addEventListener('DOMContentLoaded', () => {
    const navItems = document.querySelectorAll('.nav-item');
    const viewPanels = document.querySelectorAll('.view-panel');
    const headerTitle = document.getElementById('header-title');
    
    // 初始化标题为谱图
    headerTitle.textContent = '谱图';

    // 初始化各个视图
    initDashboard();
    initLiveChromatogram();
    initMethod();
    initSettings();
    initProcess();
    initReport();
    initDebug();

    navItems.forEach(item => {
        item.addEventListener('click', () => {
            navItems.forEach(n => n.classList.remove('active'));
            item.classList.add('active');
            headerTitle.textContent = item.querySelector('div:last-child').textContent;
            const targetId = item.getAttribute('data-target');
            viewPanels.forEach(p => p.classList.remove('active'));
            document.getElementById(targetId).classList.add('active');
            
            // 触发特定视图的重新加载逻辑
            if(targetId === 'view-method') {
                window.dispatchEvent(new CustomEvent('load-method'));
            }
        });
    });

    // 全局提示方法 (替代原生的 alert 以避免在某些内置浏览器中崩溃)
    window.showToast = function(msg, isError = false) {
        const toast = document.createElement('div');
        toast.style.position = 'fixed';
        toast.style.top = '20px';
        toast.style.left = '50%';
        toast.style.transform = 'translateX(-50%)';
        toast.style.background = isError ? 'var(--danger)' : 'var(--accent)';
        toast.style.color = 'white';
        toast.style.padding = '10px 20px';
        toast.style.borderRadius = '4px';
        toast.style.zIndex = '9999';
        toast.style.boxShadow = '0 4px 6px rgba(0,0,0,0.1)';
        toast.innerText = msg;
        document.body.appendChild(toast);
        setTimeout(() => {
            toast.style.opacity = '0';
            toast.style.transition = 'opacity 0.5s';
            setTimeout(() => toast.remove(), 500);
        }, 2000);
    };

    // 覆盖默认 alert
    window.alert = function(msg) {
        window.showToast(msg);
    };

    // 全局发送命令方法
    window.sendCmd = async function(cmdName) {
        try {
            // 1. 获取当前设备ID
            const res = await fetch('/api/v1/devices');
            const devices = await res.json();
            if (!devices || devices.length === 0) {
                window.showToast('未找到在线设备', true);
                return;
            }
            // 默认选第一个 GC 设备
            let deviceId = devices[0].deviceId;
            for (let d of devices) {
                if (String(d.deviceId).startsWith('GC')) {
                    deviceId = d.deviceId;
                    break;
                }
            }

            // 2. 发送指令
            const cmdRes = await fetch(`/api/v1/devices/${deviceId}/cmd?name=${cmdName}`, {
                method: 'POST'
            });
            const cmdData = await cmdRes.json();
            if (cmdRes.ok) {
                window.showToast(`指令 ${cmdName} 下发成功`);
            } else {
                window.showToast(`下发失败: ${cmdData.error}`, true);
            }
        } catch(e) {
            console.error(e);
            window.showToast(`发送指令异常: ${e.message}`, true);
        }
    }
});
