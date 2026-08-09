import re

with open(r'c:\Users\connor\.trae\memory\projects\-i-GIT-VS2022-Chromatography-workstation\project_memory.md', 'r', encoding='utf-8') as f:
    content = f.read()

new_rule = """
## 硬性约束
- **强制版本与提交管理**：任何代码修改必须增加版本号（执行 update_version.py），且必须提交包含 [IChanged], Root Cause, Solution, Test Plan 详细信息的 Git Commit。严禁在没有增加版本号和详细提交记录的情况下敷衍了事，该规则**永远要执行**。"""

content = content.replace('## 硬性约束', new_rule)

with open(r'c:\Users\connor\.trae\memory\projects\-i-GIT-VS2022-Chromatography-workstation\project_memory.md', 'w', encoding='utf-8') as f:
    f.write(content)
