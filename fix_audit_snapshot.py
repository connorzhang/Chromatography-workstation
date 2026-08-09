import io
import re

# 1. Update audit_snapshot.go
go_path = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\audit_snapshot.go'
with io.open(go_path, 'r', encoding='utf-8') as f:
    go_content = f.read()

# Remove TempCol
go_content = re.sub(r'\s*TempCol\s+\*float64\s+json:"tempCol"', '', go_content)
go_content = re.sub(r'\s*TempCol\s+\*float64\s+json:"tempCol,omitempty"', '', go_content)

# Change *float64 to float64 for fields that should always be present
go_content = go_content.replace('*float64  json:"tempInj1"', 'float64   json:"tempBox"')
go_content = go_content.replace('*float64  json:"carrierPsi"', 'float64   json:"carrierPsi"')
go_content = go_content.replace('*float64  json:"carrierSccm"', 'float64   json:"carrierSccm"')
go_content = go_content.replace('*float64  json:"baselineMax"', 'float64   json:"baselineMax"')
go_content = go_content.replace('*float64  json:"baselineMin"', 'float64   json:"baselineMin"')
go_content = go_content.replace('*float64  json:"baselineDrift"', 'float64   json:"baselineDrift"')
go_content = go_content.replace('*float64  json:"baselineNoise"', 'float64   json:"baselineNoise"')

# Remove omitempty versions if they exist
go_content = go_content.replace('*float64  json:"tempInj1,omitempty"', 'float64   json:"tempBox"')
go_content = go_content.replace('*float64  json:"carrierPsi,omitempty"', 'float64   json:"carrierPsi"')
go_content = go_content.replace('*float64  json:"carrierSccm,omitempty"', 'float64   json:"carrierSccm"')
go_content = go_content.replace('*float64  json:"baselineMax,omitempty"', 'float64   json:"baselineMax"')
go_content = go_content.replace('*float64  json:"baselineMin,omitempty"', 'float64   json:"baselineMin"')
go_content = go_content.replace('*float64  json:"baselineDrift,omitempty"', 'float64   json:"baselineDrift"')
go_content = go_content.replace('*float64  json:"baselineNoise,omitempty"', 'float64   json:"baselineNoise"')

# Replace round4 function
old_round4 = '''func round4(v *float64) *float64 {
if v == nil {
return nil
}
val := math.Round(*v*10000) / 10000
return &val
}'''
new_round4 = '''func round4(v float64) float64 {
return math.Round(v*10000) / 10000
}'''
go_content = go_content.replace(old_round4, new_round4)

# Replace local variables in takeAuditSnapshot
go_content = go_content.replace('var baselineMax, baselineMin, baselineDrift, baselineNoise *float64', 'var baselineMax, baselineMin, baselineDrift, baselineNoise float64')

# In takeAuditSnapshot: update assignments
go_content = go_content.replace('baselineMax = &maxVal', 'baselineMax = maxVal')
go_content = go_content.replace('baselineMin = &minVal', 'baselineMin = minVal')
go_content = go_content.replace('baselineDrift = &drift', 'baselineDrift = drift')
go_content = go_content.replace('baselineNoise = &noise', 'baselineNoise = noise')

# When creating snap, remove TempCol, update TempInj1 to TempBox, use round4
# Since te.TempInj1 is *float64, we check nil and assign 0.0 or round4(*te.TempInj1)
snap_init_regex = r'snap := AuditSnapshot\{.*?\n\t\}'
# We will just write a custom takeAuditSnapshot string replacement
