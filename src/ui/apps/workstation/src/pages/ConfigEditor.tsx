import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

const apiBase = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';

export default function ConfigEditor() {
  const { t } = useTranslation();
  const [ports, setPorts] = useState<string[]>([]);
  const [tcdPort, setTcdPort] = useState<string>('');
  const [tempPort, setTempPort] = useState<string>('');
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    // Fetch available ports
    fetch(apiBase + '/api/v1/serial/ports')
      .then(res => res.json())
      .then(data => setPorts(data))
      .catch(err => console.error('Failed to fetch ports', err));

    // Fetch current config
    fetch(apiBase + '/api/v1/serial/config')
      .then(res => res.json())
      .then(data => {
        if (data.tcd_port) setTcdPort(data.tcd_port);
        if (data.temp_port) setTempPort(data.temp_port);
      })
      .catch(err => console.error('Failed to fetch serial config', err));
  }, []);

  const handleSave = async () => {
    setSaving(true);
    try {
      await fetch(apiBase + '/api/v1/serial/config', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ tcd_port: tcdPort || null, temp_port: tempPort || null })
      });
      alert(t('config.save_success', 'Configuration saved successfully!'));
    } catch (e) {
      console.error(e);
      alert('Failed to save config');
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="p-4 bg-gray-50 h-full flex flex-col">
      <h1 className="text-2xl font-bold text-gray-800 mb-2">{t('config.title', 'Hardware Configuration')}</h1>
      <p className="text-gray-600 mb-6">{t('config.desc', 'Configure instrument IPs and hardware communication ports.')}</p>
      
      <div className="bg-white border border-gray-300 shadow-sm p-6 rounded mb-6">
        <h3 className="text-lg font-bold text-gray-800 border-b pb-2 mb-4">{t('config.instrument_1', 'Network Instrument')}</h3>
        <div className="flex items-center gap-4 mb-6">
          <label className="font-medium text-gray-700 w-32">{t('config.ip_address', 'IP Address')}</label>
          <input 
            type="text" 
            defaultValue="192.168.1.100" 
            className="border border-gray-300 rounded p-2 flex-1 max-w-xs font-mono"
          />
        </div>
      </div>

      <div className="bg-white border border-gray-300 shadow-sm p-6 rounded mb-6">
        <h3 className="text-lg font-bold text-gray-800 border-b pb-2 mb-4">{t('config.serial_ports', 'Serial Port Modules (Win)')}</h3>
        
        <div className="flex flex-col gap-4 mb-6">
          <div className="flex items-center gap-4">
            <label className="font-medium text-gray-700 w-48">{t('config.tcd_port', 'TCD Amplifier Port')}</label>
            <select 
              value={tcdPort} 
              onChange={e => setTcdPort(e.target.value)}
              className="border border-gray-300 rounded p-2 flex-1 max-w-xs"
            >
              <option value="">-- {t('common.select', 'Select')} --</option>
              {ports.map(p => <option key={p} value={p}>{p}</option>)}
            </select>
          </div>

          <div className="flex items-center gap-4">
            <label className="font-medium text-gray-700 w-48">{t('config.temp_port', 'Temp Control Port')}</label>
            <select 
              value={tempPort} 
              onChange={e => setTempPort(e.target.value)}
              className="border border-gray-300 rounded p-2 flex-1 max-w-xs"
            >
              <option value="">-- {t('common.select', 'Select')} --</option>
              {ports.map(p => <option key={p} value={p}>{p}</option>)}
            </select>
          </div>
        </div>

        <div className="flex gap-4">
          <button 
            onClick={handleSave}
            disabled={saving}
            className="bg-blue-600 hover:bg-blue-700 disabled:bg-blue-300 text-white px-6 py-2 rounded shadow"
          >
            {saving ? 'Saving...' : t('config.save_config', 'Save Configuration')}
          </button>
          <button className="bg-gray-200 hover:bg-gray-300 text-gray-800 px-4 py-2 rounded shadow">
            {t('config.test_connection', 'Test Connection')}
          </button>
        </div>
      </div>
    </div>
  );
}