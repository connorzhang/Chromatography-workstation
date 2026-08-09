import io, re
path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\main.go'
with io.open(path, 'r', encoding='utf-8') as f:
    content = f.read()

content = re.sub(r'LogInfof\("鍒嗘瀽缁撴潫, 鏁版嵁宸插瓨鍏ユ暟鎹.簱"\)', 'LogInfof("分析结束, 数据已存入数据库")', content)
content = re.sub(r'LogErrorf\("鍒嗘瀽寮傚父: %v", err\)', 'LogErrorf("分析异常: %v", err)', content)

with io.open(path, 'w', encoding='utf-8', newline='') as f:
    f.write(content)
print('Done')
