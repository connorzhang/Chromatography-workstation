import re

filepath = r'i:\GIT\VS2022\Chromatography-workstation\src\edge\cmd\collector\hal_modbus_epc.go'

with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()

# Add a batch write method to ModbusEPCController that locks once for all writes
old_write_float = '''func (m *ModbusEPCController) WriteFloat32(addr uint16, val float32) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	bits := math.Float32bits(val)
	data := make([]byte, 4)
	binary.BigEndian.PutUint32(data, bits)

	_, err := m.client.WriteMultipleRegisters(addr, 2, data)
	return err
}'''

new_write_float = '''func (m *ModbusEPCController) WriteFloat32(addr uint16, val float32) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	bits := math.Float32bits(val)
	data := make([]byte, 4)
	binary.BigEndian.PutUint32(data, bits)

	_, err := m.client.WriteMultipleRegisters(addr, 2, data)
	return err
}

// WriteAllConfig writes mode, pressure, flow, gasType, units in a single locked session
// to avoid being interleaved by the 500ms background poll, which was causing ~10s delays.
func (m *ModbusEPCController) WriteAllConfig(mode *uint16, pressure *float32, flow *float32, gasType *uint16, units *uint16) error {
	m.mu.Lock()
	defer m.mu.Unlock()
	if err := m.ensureClient(); err != nil { return err }

	m.lockPort()
	defer m.unlockPort()

	// All writes share the same port lock session so the 500ms poll cannot interleave
	if mode != nil {
		if _, err := m.client.WriteSingleRegister(0x0014, *mode); err != nil {
			return fmt.Errorf("set mode failed: %w", err)
		}
	}
	if pressure != nil {
		bits := math.Float32bits(*pressure)
		data := make([]byte, 4)
		binary.BigEndian.PutUint32(data, bits)
		if _, err := m.client.WriteMultipleRegisters(0x0015, 2, data); err != nil {
			return fmt.Errorf("set pressure failed: %w", err)
		}
	}
	if flow != nil {
		bits := math.Float32bits(*flow)
		data := make([]byte, 4)
		binary.BigEndian.PutUint32(data, bits)
		if _, err := m.client.WriteMultipleRegisters(0x0017, 2, data); err != nil {
			return fmt.Errorf("set flow failed: %w", err)
		}
	}
	if gasType != nil {
		if _, err := m.client.WriteSingleRegister(0x0019, *gasType); err != nil {
			return fmt.Errorf("set gas type failed: %w", err)
		}
	}
	if units != nil {
		if _, err := m.client.WriteSingleRegister(0x001A, *units); err != nil {
			return fmt.Errorf("set units failed: %w", err)
		}
	}
	return nil
}'''

content = content.replace(old_write_float, new_write_float)

# Now update handleEPCConfig to use WriteAllConfig instead of individual writes
old_handle = '''	if req.Mode != nil {
		if err := ctrl.WriteControlMode(*req.Mode); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "设置控制模式失败: " + err.Error()})
			return
		}
	}
	if req.Pressure != nil {
		if err := ctrl.WriteTargetPressure(*req.Pressure); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "设置目标压力失败: " + err.Error()})
			return
		}
	}
	if req.Flow != nil {
		if err := ctrl.WriteTargetFlow(*req.Flow); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "设置目标流量失败: " + err.Error()})
			return
		}
	}
	if req.GasType != nil {
		if err := ctrl.WriteGasType(*req.GasType); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "设置载气类型失败: " + err.Error()})
			return
		}
	}
	if req.Units != nil {
		if err := ctrl.WriteUnits(*req.Units); err != nil {
			writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "设置单位失败: " + err.Error()})
			return
		}
	}

	writeJSON(w, http.StatusOK, map[string]any{"success": true})'''

new_handle = '''	if err := ctrl.WriteAllConfig(req.Mode, req.Pressure, req.Flow, req.GasType, req.Units); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": err.Error()})
		return
	}

	writeJSON(w, http.StatusOK, map[string]any{"success": true})'''

content = content.replace(old_handle, new_handle)

with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)

print("hal_modbus_epc.go patched: batch write to fix EPC config delay")