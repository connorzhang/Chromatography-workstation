import { initDashboard } from './views/dashboard.js';
import { initLiveChromatogram } from './views/live.js';
import { initMethod } from './views/method.js';
import { initSettings } from './views/settings.js';
import { initProcess } from './views/process.js';
import { initReport } from './views/report.js';

document.addEventListener('DOMContentLoaded', () => {
    const navItems = document.querySelectorAll('.nav-item');
    const viewPanels = document.querySelectorAll('.view-panel');
    const headerTitle = document.getElementById('header-title');

    // 初始化各个视图
    initDashboard();
    initLiveChromatogram();
    initMethod();
    initSettings();
    initProcess();
    initReport();

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

    // 全局发送命令方法
    window.sendCmd = async function(cmdName) {
        alert('指令 ' + cmdName + ' 发送逻辑待接入');
    }
});
