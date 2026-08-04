import os

file_path = 'I*/GIT/VS2022/Chromatography-workstation/src/edge/cmd/collector/audit_snapshot.go'
with open(file_path, 'r') as f:
    content = f.read()

# Fix select case

content = content.replace(
    'case <-auditRoutineDone:',
    'case <- auditRoutineDone:'
).replace(
    'case <-auditRoutineTicker.C:',
    'case <- auditRoutineTicker.C:'
)

take_fn_old = '''	te = st.LastTelemetry
		}
		st.mu.Unlock()
		return te == nil // if found one, stop ranging
	})

	if te == nil {
		return
	}

	snap := AuditSnapshot{
		Timestamp: time.Now(),
		TempCol:   te.CurTempCol,
		TempInj1:  te.CurTempInj1,
		TempInj2:  te.CurTempInj2,
		TempDet1:  te.CurTempDet1,
		TempDet2:  te.CurTempDet2,
		TempAux1:  te.CurTempAux1,
		TempAux2:  te.CurTempAux2,
		PressInj1: te.CurPressInj1,
		PressInj2: te.CurPressInj2,
		FlowInj1:  te.CurFlowInj1,
		FlowInj2:  te.CurFlowInj2,
		FlowDet1:  te.CurFlowDet1,
		FlowDet2:  te.CurFlowDet2,
	}'''

take_fn_new = '''	te = st.LastTelemetry
		}
		st.mu.Unlock()
		return te == nil
	})

	if te == nil {
		return
	}

	snap := AuditSnapshot{
		Timestamp: time.Now(),
		TempCol:   te.TempCol,
		TempInj1:  te.TempInj1,
		TempInj2:  te.TempInj2,
		TempDet1:  te.TempDet1,
		TempDet2:  te.TempDet2,
		
		PressInj1: te.CarrierPsi,
		FlowInj1:  te.CarrierSccm,
		FlowDet1:  te.H2Sccm,
		FlowDet2:  te.AirSccm,
	}
	if len(te.Epc) > 0 {
		snap.PressInj2 = &(te.Epc[0].Psi)
		snap.FlowInj2  = &(te.Epc[0].Sccm)
	}'/'

if take_fn_old in content:
    content = content.replace(take_fn_old, take_fn_new)

with open(file_path, 'w') as f:
    f.write(content)

print('Fixed audit_snapshot.go')
