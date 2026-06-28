import os
import re
import json

PAGES_DIR = r"D:\GIT\VS2022\Chromatography-workstation\src\ui\apps\workstation\src\pages"

# Keys matching the ones defined in i18n.ts
keys = [
    "Method & Run Control", "Run Status: OFFLINE", "Run Status: IDLE", "Run Status: RUNNING",
    "Start Run", "Stop Run", "Current Method:", "Instrument Status", "Pump (Quat)", "Flow:",
    "Pressure:", "Solvent A:", "Solvent B:", "Column Comp.", "Temp:", "Left:", "Right:",
    "Detector (DAD)", "Signal A:", "Signal B:", "Lamp:", "Live Chromatogram", "Time (min)",
    "Intensity (mAU)", "Detector (TCD)", "Bridge Current:", "Polarity:", "Detector (FID)",
    "Flame:", "Ignite", "Positive", "Sequence Table", "Run Sequence", "Pause", "Stop",
    "Line", "Location", "Sample Name", "Method Name", "Inj/Loc", "Inj Vol (μL)", "Data File",
    "Status", "Add Row", "Fill Down", "Data Analysis", "Load Data", "Integration Results",
    "Ret Time", "Type", "Width", "Area", "Height", "Area %", "Tools", "Auto Integrate",
    "Manual Integration", "Tangent Skim", "Drop Baseline", "Calibration & Quantitation",
    "Update Method", "Curve Fit:", "Linear", "Quadratic", "Origin:", "Include", "Force",
    "Ignore", "Level", "Amount", "Response", "ISTD", "Batch Review", "Approve Batch",
    "Reject", "Generate PDF", "Sample", "Vial", "Method", "Acq. Date", "Integr.",
    "Reviewed", "Pending", "Auto", "Manual", "Report Layout Editor", "Save Template",
    "Available Elements", "Header / Logo", "Chromatogram", "Results Table", "Method Parameters",
    "Calibration Curve", "Drag elements here to build your report layout.",
    "Diagnostics & Early Maintenance Feedback (EMF)", "Run System Test", "Pump Seal Wear",
    "Total Liters Pumped:", "Limit:", "Status: Warning", "Status: Good", "Replace Seals",
    "DAD Lamp Usage", "Hours Lit:", "Reset Counter", "Injector Needle", "Total Injections:",
    "Replace Needle", "Audit Trail (21 CFR Part 11)", "Export Log", "Filter by Category:",
    "All", "System", "Date/Time", "User", "Category", "Action", "Details",
    "User Management & E-Signatures", "Add User", "Role", "Last Login", "Active", "Admin",
    "Operator", "Manager", "Signature Workflow Policy", "Single Sign-off (Reviewer)",
    "Two-Step (Submitter -> Reviewer)", "Three-Step (Submitter -> Reviewer -> Approver)",
    "Save Policy", "3D Spectral Analysis (DAD)", "Extract Spectrum", "Library Search",
    "Extracted UV-Vis Spectrum", "Wavelength (nm)", "Absorbance (AU)", "Peak Purity Analysis",
    "Purity Factor:", "Threshold:", "Result:", "Passed",
    "Advanced Chromatography (RTL & Translator)", "Apply Settings", "Retention Time Locking (RTL)",
    "Target Compound:", "Target RT (min):", "Current RT (min):", "Calculated Pressure Shift:",
    "Lock RT", "Method Translator", "Original Carrier Gas:", "New Carrier Gas:",
    "Original Column:", "New Column:", "Translate Method", "Intelligent Sample Prep",
    "Save Program", "Build custom injector programs for online dilution, derivatization, and internal standard addition.",
    "Step", "Speed (μL/min)", "Wait/Mix Time (s)", "Volume (μL)",
    "2D-LC / GCxGC Valve Control", "Apply to Method", "Select Valve", "Position", "Add Event",
    "Valve Name", "SST Control Charts (Trending)", "Export Report",
    "Retention Time Control Chart (3-Sigma)", "Custom Calculator & Reporting", "New Column Name",
    "Expression (AST Parser)", "Preview Results", "ECM Integration (Enterprise Content Management)",
    "Sync to Vault", "Lock Dataset",
    "This system is configured for 21 CFR Part 11 compliance. Data saved to the ECM Vault cannot be deleted or overwritten.",
    "Version", "Last Modified", "Checksum (SHA-256)", "Actions", "View History", "Commit",
    "System Configuration", "Save Config", "Instrument Settings", "Network Settings",
    "Hardware Address:", "Port:", "Dashboard", "Connected", "Disconnected"
]

for filename in os.listdir(PAGES_DIR):
    if not filename.endswith('.tsx'):
        continue
    filepath = os.path.join(PAGES_DIR, filename)
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    modified = False
    
    # Check if translation hook is used
    if "useTranslation" not in content:
        content = content.replace("import React", "import React\nimport { useTranslation } from 'react-i18next';")
        # Ensure it only gets added once
        modified = True
        
    if "const { t } = useTranslation();" not in content:
        # Match "export default function ComponentName() {"
        content = re.sub(r'(export default function \w+\([^)]*\)\s*\{)', r'\1\n  const { t } = useTranslation();', content)
        # Match "const ComponentName = () => {"
        content = re.sub(r'(const \w+\s*=\s*\([^)]*\)\s*=>\s*\{)', r'\1\n  const { t } = useTranslation();', content)
        modified = True

    # Sort keys by length descending to prevent partial replacements
    keys.sort(key=len, reverse=True)

    for k in keys:
        if k in content:
            k_esc = k.replace("'", "\\'")
            # Match strict text nodes `>Key<`
            if f">{k}<" in content:
                content = content.replace(f">{k}<", f">{{t('{k_esc}')}}<")
                modified = True
            # Match text nodes with space `> Key <`
            if f"> {k} <" in content:
                content = content.replace(f"> {k} <", f"> {{t('{k_esc}')}} <")
                modified = True
            # Match placeholders
            if f'placeholder="{k}"' in content:
                content = content.replace(f'placeholder="{k}"', f'placeholder={{t(\'{k_esc}\')}}')
                modified = True
            # Match titles
            if f'title="{k}"' in content:
                content = content.replace(f'title="{k}"', f'title={{t(\'{k_esc}\')}}')
                modified = True
            # Match trailing spaces in JSX text
            if f"{k} " in content and f">{k} <" in content:
                content = content.replace(f">{k} <", f">{{t('{k_esc}')}} <")
                modified = True

    if modified:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Translated {filename}")

print("Translation injection completed.")