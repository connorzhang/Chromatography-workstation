import sys

filepath = 'src/edge/cmd/collector/static/js/app.js'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

old_str = '''            // 如果是开始分析，提示输入审计备注
            let remarkParam = '';
            if (cmdName === 'startAll' || cmdName === 'start') {
                const remark = window.prompt(\"请输入审计备注（例如：标气类型、操作说明等），可留空：\", \"\");
                if (remark === null) {
                    // 用户点击取消，中止发送指令
                    return;
                }
                remarkParam = &remark=;
            }'''

new_str = '''            // 取消了原有的 prompt 弹窗，防止阻塞浏览器主线程导致自动刷新失效
            let remarkParam = '';'''

if old_str in content:
    content = content.replace(old_str, new_str)
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    print('Replace success.')
else:
    print('Old string not found.')
