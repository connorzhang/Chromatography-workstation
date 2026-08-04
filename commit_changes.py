import os
import subprocess

commit_msg = "[default][clusterhmi][IChanged]-[]\n\n- 修复 audit.js 中的语法错误和导入错误（移除了不存在的 utils.js 导入）\n- 修复 audit.js 导致整个前端 ES Module 崩溃，进而导致主页面空白的问题\n- 修改 index.html 添加审计功能的入口，并将版本号升级至 v0.3.142 以强制刷新浏览器缓存\n- 同步修改 Makefile 和 bump.py 中的版本号"
with open('commit_msg.txt', 'w', encoding='utf-8') as f:
    f.write(commit_msg)

subprocess.run(['git', 'add', '.'])
subprocess.run(['git', 'commit', '-F', 'commit_msg.txt'])
print("Git commit done.")
