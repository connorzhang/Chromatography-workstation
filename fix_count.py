import io
with io.open('src/edge/cmd/collector/hal_gckc_legacy.go', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('if count > 99 {\n\t\tcount = 99\n\t}', 'if count > 9999 {\n\t\tcount = 9999\n\t}')

with io.open('src/edge/cmd/collector/hal_gckc_legacy.go', 'w', encoding='utf-8') as f:
    f.write(content)

