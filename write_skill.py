import os

content = """---
name: "mobius-execution-guard"
description: "防挂起与安全执行护城河技能。在处理文件替换、全盘搜索、测试机部署或修复疑难bug时强制调用，避免管道死锁和重度IO阻塞。"
---

# Mobius Execution Guard (安全执行护城河)

本技能用于总结并沉淀历史操作中导致 AI 阻塞、乱码、挂起或报错的经验，确保系统在未来的操作中“不二过”。

## 核心守则与经验沉淀

### 1. 编码与正则替换铁律 (Anti-Corruption)
- **风险场景**：在 Windows PowerShell 下使用管道传递文本或使用正则表达式替换含有中文字符或复杂引号的代码时，极易导致 UTF-8 编码被破坏、内容被截断，进而引发 unexpected newline 等致命语法错误。
- **强制规范**：
  1. 严禁使用 Set-Content、Out-File、Remove-Item 等极易触发系统拦截或权限锁死的 PowerShell 原生命令。
  2. 严禁通过 Shell 管道拼接复杂的字符串替换。
  3. **唯一推荐方式**：优先依赖 IDE 原生的 SearchReplace / Write 工具；若受限必须使用终端，强制编写并运行 Python 脚本，且必须以二进制 (rb/wb) 模式或指定 encoding='utf-8' 读取，确保实现零弹窗、零打扰的安全修改。

### 2. 长任务与 IO 阻断熔断机制 (Anti-Blocking)
- **风险场景**：在项目根目录执行全盘扫描时，如果遇到类似数百个 Base64 动画缓存碎片的重度 IO 目录，会导致终端长期挂起、失去响应。
- **强制规范**：
  1. 执行任何遍历扫描前，必须主动嗅探并强制排除已知的重度 IO 碎片目录。
  2. 超过 5 秒未反馈结果的非关键背景扫描任务，必须主动中止，坚决杜绝后台挂起阻塞整个工作流。

### 3. Go Module 解析机制避坑 (Go Environment)
- **风险场景**：远程或本地执行 go build 出现大量 package is not in std 或无法解析依赖的报错。
- **强制规范**：
  1. 必须首先检查代码根目录的 go.mod，确保其中的 module 声明名称与源码中的绝对导入路径完全一致。
  2. 若发生脱节，需第一时间修正 go.mod 或调整工作目录上下文。

### 4. 远程测试机安全部署流 (Safe Deployment)
- **风险场景**：通过 sshx 在目标机器部署时，直接运行目标进程会导致 SSH 会话阻塞；且未正确关闭旧进程会导致文件被占用无法覆盖。
- **强制规范**：
  1. 必须使用 taskkill /F /IM 彻底杀死旧进程。
  2. 上传解压后，严禁直接运行可执行文件，必须使用计划任务或守护进程拉起：schtasks /run /tn
  3. 最后校验进程的 StartTime，以明确部署和重启是否真正成功。
"""

path = r"I:\GIT\VS2022\Chromatography-workstation\.trae\skills\mobius-execution-guard\SKILL.md"
with open(path, "w", encoding="utf-8") as f:
    f.write(content)
print("done")
