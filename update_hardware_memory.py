import os

memory_file = r'c:\Users\connor\.trae\memory\projects\-i-GIT-VS2022-Chromatography-workstation\project_memory.md'

hardware_constraints = """
## 硬件参数约束 (TCD & 保温箱)
- **TCD 传感器**：自研极小死体积，高集成度。安全工作桥流必须控制在 **20mA以内**（极限状态/40mL大流量时最高33mA），严禁超流以防烧丝。
- **保温箱控温**：常规工作温度范围为 **40 - 80**，最高不得超过 120。
- **排查基线准则**：分析基线问题时必须以此参数为红线，排除因温度越限或桥流过载引入的干扰。
"""

with open(memory_file, 'a', encoding='utf-8') as f:
    f.write(hardware_constraints)

print("Memory updated successfully.")