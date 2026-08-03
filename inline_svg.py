import os
import re

md_file = 'docs/01-overview/valve_logic_demo.md'
svg10_file = 'docs/01-overview/valve_10port.svg'
svg6_file = 'docs/01-overview/valve_6port.svg'

with open(md_file, 'r', encoding='utf-8') as f:
    md = f.read()

with open(svg10_file, 'r', encoding='utf-8') as f:
    svg10 = f.read()

with open(svg6_file, 'r', encoding='utf-8') as f:
    svg6 = f.read()

def clean_and_wrap(svg_str):
    # Remove body rule
    svg_str = re.sub(r'body\s*\{.*?\}', '', svg_str, flags=re.DOTALL)
    # Replace svg { ... } with inline style
    svg_str = re.sub(r'svg\s*\{.*?\}', '', svg_str, flags=re.DOTALL)
    svg_str = svg_str.replace('<svg ', '<svg style="background-color: #252526; box-shadow: 0 10px 30px rgba(0,0,0,0.5); border-radius: 12px; max-width: 100%; height: auto;" ')
    
    # Escape for JSX template string
    escaped = svg_str.replace('\\', '\\\\').replace('`', '\\`').replace('$', '\\$')
    return f'<div style={{{{display: "flex", justifyContent: "center", margin: "20px 0"}}}} dangerouslySetInnerHTML={{{{ __html: `{escaped}` }}}} />'

# Replace the img tags. Note: regex pattern matching requires escaping parenthesis and curly braces
pattern10 = r'<div align="center">\s*<img src=\{require\(\'\./valve_10port\.svg\'\)\}.*?>\s*</div>'
md = re.sub(pattern10, clean_and_wrap(svg10), md, flags=re.DOTALL)

pattern6 = r'<div align="center">\s*<img src=\{require\(\'\./valve_6port\.svg\'\)\}.*?>\s*</div>'
md = re.sub(pattern6, clean_and_wrap(svg6), md, flags=re.DOTALL)

with open(md_file, 'w', encoding='utf-8') as f:
    f.write(md)

print('Inline SVG completed.')
