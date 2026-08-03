# -*- coding: utf-8 -*-
html_content = r'''<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <title>10通阀气路：无机气中心切割与预柱反吹</title>
    <style>
        body { background-color: #1E1E1E; color: #FFF; font-family: "Segoe UI", "Microsoft YaHei", sans-serif; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        svg { background-color: #252526; box-shadow: 0 10px 30px rgba(0,0,0,0.5); border-radius: 12px; }
        @keyframes flow { to { stroke-dashoffset: -20; } }
        @keyframes st1_anim { 0%, 19.9% { opacity: 1; } 20%, 49.9% { opacity: 0; } 50%, 100% { opacity: 1; } }
        @keyframes st2_anim { 0%, 19.9% { opacity: 0; } 20%, 49.9% { opacity: 1; } 50%, 100% { opacity: 0; } }
        .gas-line { fill: none; stroke-width: 4; stroke-dasharray: 10, 10; animation: flow 1s linear infinite; }
        .bg-line { fill: none; stroke: #4d4d4d; stroke-width: 8; stroke-linecap: round; stroke-linejoin: round; }
        .st1 { animation: st1_anim 20s infinite; }
        .st2 { animation: st2_anim 20s infinite; }
    </style>
</head>
<body>
<svg width="800" height="620" viewBox="0 0 800 620" xmlns="http://www.w3.org/2000/svg">
    <!-- 背景管路 -->
    <g class="bg-line">
        <path d="M 400 80 L 400 200" /> <!-- Carrier 1 In -->
        <path d="M 680 269.1 L 495.1 269.1" /> <!-- Sample In -->
        <path d="M 495.1 330.9 L 680 330.9" /> <!-- Vent 2 -->
        <path d="M 304.9 540 L 304.9 330.9" /> <!-- Carrier 2 In -->
        <path d="M 100 380.9 L 341.2 380.9" /> <!-- Vent 1 -->
        
        <!-- 定量环 -->
        <path d="M 458.8 219.1 C 550 219.1, 550 380.9, 458.8 380.9" />
        
        <!-- 预柱 JN.PN (盘管) -->
        <path d="M 400 400 L 400 460 A 20 20 0 0 1 360 460 L 360 420 A 20 20 0 0 0 320 420 L 320 460 A 20 20 0 0 1 280 460 L 280 269.1 L 304.9 269.1" />
        
        <!-- 分析柱 JN.13x (盘管) -->
        <path d="M 341.2 219.1 L 280 219.1 A 20 20 0 0 0 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1" />
    </g>

    <circle cx="400" cy="300" r="110" fill="#34495E" />
    <circle cx="400" cy="300" r="90" fill="#2C3E50" />
    <!-- 阀口连线 -->
    <g stroke="#ECF0F1" stroke-width="6" stroke-linecap="round" class="st1">
        <line x1="400" y1="200" x2="341.2" y2="219.1"/>
        <line x1="458.8" y1="219.1" x2="495.1" y2="269.1"/>
        <line x1="495.1" y1="330.9" x2="458.8" y2="380.9"/>
        <line x1="400" y1="400" x2="341.2" y2="380.9"/>
        <line x1="304.9" y1="330.9" x2="304.9" y2="269.1"/>
    </g>
    <g stroke="#ECF0F1" stroke-width="6" stroke-linecap="round" class="st2">
        <line x1="400" y1="200" x2="458.8" y2="219.1"/>
        <line x1="495.1" y1="269.1" x2="495.1" y2="330.9"/>
        <line x1="458.8" y1="380.9" x2="400" y2="400"/>
        <line x1="341.2" y1="380.9" x2="304.9" y2="330.9"/>
        <line x1="304.9" y1="269.1" x2="341.2" y2="219.1"/>
    </g>

    <!-- 阀口与编号 -->
    <g fill="#ECF0F1" stroke="#BDC3C7" stroke-width="2">
        <circle cx="400" cy="200" r="8"/> <circle cx="458.8" cy="219.1" r="8"/> <circle cx="495.1" cy="269.1" r="8"/>
        <circle cx="495.1" cy="330.9" r="8"/> <circle cx="458.8" cy="380.9" r="8"/> <circle cx="400" cy="400" r="8"/>
        <circle cx="341.2" cy="380.9" r="8"/> <circle cx="304.9" cy="330.9" r="8"/> <circle cx="304.9" cy="269.1" r="8"/> <circle cx="341.2" cy="219.1" r="8"/>
    </g>
    <g fill="#FFF" font-size="12" font-weight="bold" text-anchor="middle">
        <text x="400" y="185">1</text> <text x="475" y="210">2</text> <text x="515" y="275">3</text> <text x="515" y="335">4</text>
        <text x="475" y="395">5</text> <text x="400" y="420">6</text> <text x="325" y="395">7</text> <text x="285" y="335">8</text>
        <text x="285" y="275">9</text> <text x="325" y="210">10</text>
    </g>

    <!-- 连续流气路 -->
    <g class="st1">
        <path class="gas-line" stroke="#00BFFF" d="M 400 80 L 400 200 L 341.2 219.1 L 280 219.1 A 20 20 0 0 0 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1" />
        <path class="gas-line" stroke="#FFA500" d="M 680 269.1 L 495.1 269.1 L 458.8 219.1 C 550 219.1, 550 380.9, 458.8 380.9 L 495.1 330.9 L 680 330.9" />
        <path class="gas-line" stroke="#00BFFF" d="M 304.9 540 L 304.9 330.9 L 304.9 269.1 L 280 269.1 L 280 460 A 20 20 0 0 0 320 460 L 320 420 A 20 20 0 0 1 360 420 L 360 460 A 20 20 0 0 0 400 460 L 400 400 L 341.2 380.9 L 100 380.9" />
    </g>
    <g class="st2">
        <path class="gas-line" stroke="#00BFFF" d="M 400 80 L 400 200 L 458.8 219.1 C 550 219.1, 550 380.9, 458.8 380.9 L 400 400 L 400 460 A 20 20 0 0 1 360 460 L 360 420 A 20 20 0 0 0 320 420 L 320 460 A 20 20 0 0 1 280 460 L 280 269.1 L 304.9 269.1 L 341.2 219.1 L 280 219.1 A 20 20 0 0 0 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1" />
        <path class="gas-line" stroke="#00BFFF" d="M 304.9 540 L 304.9 330.9 L 341.2 380.9 L 100 380.9" />
        <path class="gas-line" stroke="#FFA500" d="M 680 269.1 L 495.1 269.1 L 495.1 330.9 L 680 330.9" />
    </g>

    <!-- 动态分离物理逻辑 -->
    <!-- 1. 混合样品 (Orange Plug) - 充满定量环，切阀后推入预柱，发生粗分离 -->
    <path d="M 458.8 219.1 C 550 219.1, 550 380.9, 458.8 380.9 L 400 400 L 400 460 A 20 20 0 0 1 360 460 L 360 420 A 20 20 0 0 0 320 420 L 320 460 A 20 20 0 0 1 280 460 L 280 269.1"
          fill="none" stroke="#FFA500" stroke-width="8" stroke-linecap="round" pathLength="100" stroke-dasharray="15 100">
        <animate attributeName="stroke-dashoffset" values="-15;-15;-70;-70" keyTimes="0;0.2;0.35;1" dur="20s" repeatCount="indefinite" />
        <animate attributeName="opacity" values="0;1;1;0;0" keyTimes="0;0.05;0.3;0.35;1" dur="20s" repeatCount="indefinite" />
    </path>
'''
html_content += r'''
    <!-- 2. 轻组分 (Green Plug) - 在预柱中继续前进，进入分析柱 -->
    <path d="M 400 400 L 400 460 A 20 20 0 0 1 360 460 L 360 420 A 20 20 0 0 0 320 420 L 320 460 A 20 20 0 0 1 280 460 L 280 269.1 L 304.9 269.1 L 341.2 219.1 L 280 219.1 A 20 20 0 0 0 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1"
          fill="none" stroke="#32CD32" stroke-width="8" stroke-linecap="round" pathLength="100" stroke-dasharray="10 100">
        <animate attributeName="stroke-dashoffset" values="-15;-15;-35;-70;-70" keyTimes="0;0.3;0.5;0.7;1" dur="20s" repeatCount="indefinite" />
        <animate attributeName="opacity" values="0;0;1;1;0;0" keyTimes="0;0.29;0.3;0.69;0.7;1" dur="20s" repeatCount="indefinite" />
    </path>

    <!-- 3. 重组分杂质 (Red Plug) - 滞留预柱，随后反向反吹放空 -->
    <path d="M 100 380.9 L 341.2 380.9 L 400 400 L 400 460 A 20 20 0 0 1 360 460 L 360 420 A 20 20 0 0 0 320 420 L 320 460 A 20 20 0 0 1 280 460 L 280 269.1"
          fill="none" stroke="#FF4500" stroke-width="8" stroke-linecap="round" pathLength="100" stroke-dasharray="10 100">
        <animate attributeName="stroke-dashoffset" values="-50;-50;-70;0;0" keyTimes="0;0.3;0.5;0.8;1" dur="20s" repeatCount="indefinite" />
        <animate attributeName="opacity" values="0;0;1;1;0;0" keyTimes="0;0.29;0.3;0.79;0.8;1" dur="20s" repeatCount="indefinite" />
    </path>

    <!-- 4. 分析柱中彻底分离：轻组分1 H2/O2 (Green) -->
    <circle r="6" fill="#32CD32">
        <animateMotion dur="20s" repeatCount="indefinite" 
            path="M 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1"
            keyTimes="0; 0.7; 0.85; 1" 
            keyPoints="0; 0; 1; 1" 
            calcMode="linear" />
        <animate attributeName="opacity" values="0; 0; 1; 1; 0; 0" keyTimes="0; 0.69; 0.7; 0.84; 0.85; 1" dur="20s" repeatCount="indefinite" />
    </circle>

    <!-- 5. 分析柱中彻底分离：轻组分2 N2/CH4 (Cyan) -->
    <circle r="6" fill="#00FFFF">
        <animateMotion dur="20s" repeatCount="indefinite" 
            path="M 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1"
            keyTimes="0; 0.7; 0.92; 1" 
            keyPoints="0; 0; 1; 1" 
            calcMode="linear" />
        <animate attributeName="opacity" values="0; 0; 1; 1; 0; 0" keyTimes="0; 0.69; 0.7; 0.91; 0.92; 1" dur="20s" repeatCount="indefinite" />
    </circle>

    <!-- 6. 分析柱中彻底分离：轻组分3 CO (Yellow) -->
    <circle r="6" fill="#FFFF00">
        <animateMotion dur="20s" repeatCount="indefinite" 
            path="M 280 169.1 L 320 169.1 A 20 20 0 0 1 320 119.1 L 280 119.1 A 20 20 0 0 0 280 69.1 L 320 69.1 A 20 20 0 0 1 320 19.1 L 150 19.1"
            keyTimes="0; 0.7; 0.99; 1" 
            keyPoints="0; 0; 1; 1" 
            calcMode="linear" />
        <animate attributeName="opacity" values="0; 0; 1; 1; 0; 0" keyTimes="0; 0.69; 0.7; 0.98; 0.99; 1" dur="20s" repeatCount="indefinite" />
    </circle>


    <!-- 文本和标签 (布局优化，不遮挡柱子) -->
    <text x="400" y="70" text-anchor="middle" fill="#00BFFF" font-weight="bold">载气 1 (主气路)</text>
    <text x="690" y="265" text-anchor="start" fill="#FFA500" font-weight="bold">样品气 IN</text>
    <text x="690" y="335" text-anchor="start" fill="#BDC3C7">放空 2</text>
    <text x="305" y="555" text-anchor="middle" fill="#00BFFF" font-weight="bold">载气 2 (反吹气路)</text>
    <text x="100" y="400" text-anchor="middle" fill="#BDC3C7">放空 1 (反吹排废口)</text>
    <text x="560" y="300" text-anchor="start" fill="#FFF">定量环</text>

    <!-- 柱子标签 -->
    <rect x="250" y="490" width="100" height="25" rx="5" fill="#34495E" />
    <text x="300" y="507" text-anchor="middle" fill="#FFF" font-size="12">预柱: JN.PN</text>
    
    <rect x="240" y="25" width="130" height="25" rx="5" fill="#34495E" />
    <text x="305" y="42" text-anchor="middle" fill="#FFF" font-size="12">分析柱: JN.13x</text>

    <!-- TCD 标签 -->
    <rect x="100" y="0" width="60" height="40" rx="5" fill="#D35400" stroke="#E67E22" stroke-width="2"/>
    <text x="130" y="25" fill="#FFF" font-size="14" text-anchor="middle" font-weight="bold">TCD</text>

    <!-- 状态面板 -->
    <rect x="560" y="20" width="220" height="90" rx="10" fill="#1A252F" stroke="#7F8C8D" stroke-width="2"/>
    <text x="575" y="45" fill="#FFF" font-size="14" font-weight="bold">10通阀气路逻辑演示</text>
    <text x="575" y="70" fill="#ECF0F1" font-size="12">无机气中心切割与预柱反吹</text>
    <g class="st1">
        <text x="575" y="95" fill="#F1C40F" font-size="14" font-weight="bold">阶段 1: 取样与反吹</text>
    </g>
    <g class="st2">
        <text x="575" y="95" fill="#2ECC71" font-size="14" font-weight="bold">阶段 2: 进样与粗分离</text>
    </g>

    <!-- 图例 -->
    <rect x="480" y="480" width="300" height="120" rx="10" fill="#1A252F" stroke="#7F8C8D" stroke-width="1"/>
    <circle cx="500" cy="505" r="6" fill="#00BFFF"/>
    <text x="515" y="510" fill="#FFF" font-size="13">载气流 (Carrier Gas)</text>
    <circle cx="500" cy="535" r="6" fill="#FFA500"/>
    <text x="515" y="540" fill="#FFF" font-size="13">混合样品 (Sample Plug)</text>
    <circle cx="500" cy="565" r="7" fill="#FF4500"/>
    <text x="515" y="570" fill="#FFF" font-size="13">重组分 (反吹杂质)</text>
    
    <circle cx="650" cy="505" r="6" fill="#32CD32"/>
    <text x="665" y="510" fill="#FFF" font-size="13">H2 / O2</text>
    <circle cx="650" cy="535" r="6" fill="#00FFFF"/>
    <text x="665" y="540" fill="#FFF" font-size="13">N2 / CH4</text>
    <circle cx="650" cy="565" r="6" fill="#FFFF00"/>
    <text x="665" y="570" fill="#FFF" font-size="13">CO</text>
</svg>
</body>
</html>
'''
with open('valve_10port.html', 'w', encoding='utf-8') as f:
    f.write(html_content)