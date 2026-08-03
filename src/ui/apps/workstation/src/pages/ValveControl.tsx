import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

interface ValveEvent {
  id: string;
  time: number;
  valve: string;
  position: 'ON' | 'OFF';
}

interface EpcRamp {
  id: string;
  rate: number;
  final_value: number;
  hold_time: number;
}

interface EpcProgram {
  mode: string;
  initial_value: number;
  initial_time: number;
  ramps: EpcRamp[];
}

export default function ValveControl() {
  const { t } = useTranslation();
  const [events, setEvents] = useState<ValveEvent[]>([]);
  const [epc, setEpc] = useState<EpcProgram>({
    mode: 'Constant Flow',
    initial_value: 1.0,
    initial_time: 0.0,
    ramps: []
  });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchProgram = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const resValve = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/valve/program`);
        if (resValve.ok) setEvents(await resValve.json());

        const resEpc = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/epc/program`);
        if (resEpc.ok) setEpc(await resEpc.json());
      } catch (e) {
        console.error("Failed to load hardware program", e);
      }
    };
    fetchProgram();
  }, []);

  const handleSave = async () => {
    setSaving(true);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/valve/program`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(events)
      });
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/epc/program`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(epc)
      });
      alert(t('common.saved', 'Saved successfully'));
    } catch (e) {
      console.error("Failed to save hardware program", e);
    } finally {
      setSaving(false);
    }
  };

  const addEvent = () => {
    const timeVal = (document.getElementById('valveTime') as HTMLInputElement).value;
    const valveVal = (document.getElementById('valveName') as HTMLSelectElement).value;       
    const posVal = (document.getElementById('valvePos') as HTMLSelectElement).value;
    if (!timeVal) return;
    setEvents([...events, {
      id: Date.now().toString(),
      time: parseFloat(timeVal),
      valve: valveVal,
      position: posVal as 'ON' | 'OFF'
    }].sort((a, b) => a.time - b.time));
  };

  const removeEvent = (id: string) => {
    setEvents(events.filter(e => e.id !== id));
  };

  const addRamp = () => {
    setEpc({
      ...epc,
      ramps: [...epc.ramps, {
        id: Date.now().toString(),
        rate: 0,
        final_value: 0,
        hold_time: 0
      }]
    });
  };

  const updateRamp = (id: string, field: keyof EpcRamp, value: number) => {
    setEpc({
      ...epc,
      ramps: epc.ramps.map(r => r.id === id ? { ...r, [field]: value } : r)
    });
  };

  const removeRamp = (id: string) => {
    setEpc({
      ...epc,
      ramps: epc.ramps.filter(r => r.id !== id)
    });
  };

  return (
    <div className="p-6 h-full flex flex-col bg-slate-100 overflow-y-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('Instrument Control (EPC & Valve)')}</h1>
        <button
          className="bg-blue-600 text-white px-6 py-2 rounded shadow hover:bg-blue-700 disabled:opacity-50"
          onClick={handleSave}
          disabled={saving}
        >
          {saving ? t('common.saving', 'Saving...') : t('Apply to Method')}
        </button>
      </div>

      <div className="grid grid-cols-1 xl:grid-cols-2 gap-6">
        {/* EPC Control Panel */}
        <div className="bg-white p-5 rounded shadow-sm border border-slate-200">
          <h2 className="text-lg font-semibold text-slate-700 mb-4">{t('EPC Control')}</h2>
          
          <div className="grid grid-cols-3 gap-4 mb-6">
            <div>
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Control Mode')}</label>
              <select 
                className="border border-slate-300 rounded p-1.5 text-sm w-full"
                value={epc.mode}
                onChange={e => setEpc({...epc, mode: e.target.value})}
              >
                <option value="Constant Flow">{t('Constant Flow')}</option>
                <option value="Constant Pressure">{t('Constant Pressure')}</option>
                <option value="Ramped Flow">{t('Ramped Flow')}</option>
                <option value="Ramped Pressure">{t('Ramped Pressure')}</option>
              </select>
            </div>
            <div>
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Initial Value')}</label>
              <input 
                type="number" 
                className="border border-slate-300 rounded p-1.5 text-sm w-full"
                value={epc.initial_value}
                onChange={e => setEpc({...epc, initial_value: parseFloat(e.target.value) || 0})}
              />
            </div>
            <div>
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Initial Time (min)')}</label>
              <input 
                type="number" 
                className="border border-slate-300 rounded p-1.5 text-sm w-full"
                value={epc.initial_time}
                onChange={e => setEpc({...epc, initial_time: parseFloat(e.target.value) || 0})}
              />
            </div>
          </div>

          <div className="mb-2 flex justify-between items-center">
            <h3 className="text-sm font-semibold text-slate-600">{t('Ramp Table')}</h3>
            <button className="text-xs bg-slate-100 border border-slate-300 px-2 py-1 rounded hover:bg-slate-200" onClick={addRamp}>
              + {t('Add Ramp')}
            </button>
          </div>
          <table className="w-full text-left text-sm border-collapse border border-slate-200">
            <thead className="bg-slate-50">
              <tr>
                <th className="p-2 border-b border-slate-200">{t('Rate')}</th>
                <th className="p-2 border-b border-slate-200">{t('Final Value')}</th>
                <th className="p-2 border-b border-slate-200">{t('Hold Time (min)')}</th>
                <th className="p-2 border-b border-slate-200 w-10"></th>
              </tr>
            </thead>
            <tbody>
              {epc.ramps.map((r, idx) => (
                <tr key={r.id} className="border-b border-slate-100">
                  <td className="p-1">
                    <input type="number" className="w-full border border-slate-200 rounded p-1 text-xs" value={r.rate} onChange={e => updateRamp(r.id, 'rate', parseFloat(e.target.value) || 0)} />
                  </td>
                  <td className="p-1">
                    <input type="number" className="w-full border border-slate-200 rounded p-1 text-xs" value={r.final_value} onChange={e => updateRamp(r.id, 'final_value', parseFloat(e.target.value) || 0)} />
                  </td>
                  <td className="p-1">
                    <input type="number" className="w-full border border-slate-200 rounded p-1 text-xs" value={r.hold_time} onChange={e => updateRamp(r.id, 'hold_time', parseFloat(e.target.value) || 0)} />
                  </td>
                  <td className="p-1 text-center">
                    <button className="text-red-500 hover:text-red-700 text-xs font-bold" onClick={() => removeRamp(r.id)}>×</button>
                  </td>
                </tr>
              ))}
              {epc.ramps.length === 0 && (
                <tr><td colSpan={4} className="p-4 text-center text-slate-400 text-xs italic">{t('No ramps configured. Operates isothermally/isobarically.')}</td></tr>
              )}
            </tbody>
          </table>
        </div>

        {/* Valve Control Panel */}
        <div className="bg-white p-5 rounded shadow-sm border border-slate-200">
          <h2 className="text-lg font-semibold text-slate-700 mb-4">{t('Valve Time Events')}</h2>
          <div className="bg-slate-50 p-3 rounded border border-slate-200 mb-4 flex items-end gap-3">
            <div className="flex-1">
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Select Valve')}</label>
              <select id="valveName" className="border border-slate-300 rounded p-1.5 text-sm w-full">     
                {[1,2,3,4].map(v => (
                  <option key={v} value={`Event ${v}`}>{t(`Event Switch ${v}`)}</option>
                ))}
              </select>
            </div>
            <div className="w-24">
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Time (min)')}</label>
              <input id="valveTime" type="number" step="0.01" className="border border-slate-300 rounded p-1.5 text-sm w-full" placeholder="0.00" />
            </div>
            <div className="w-24">
              <label className="block text-xs font-semibold text-slate-500 mb-1">{t('Position')}</label>
              <select id="valvePos" className="border border-slate-300 rounded p-1.5 text-sm w-full">      
                <option>ON</option>
                <option>OFF</option>
              </select>
            </div>
            <div>
              <button className="bg-slate-200 text-slate-700 px-3 py-1.5 rounded text-sm hover:bg-slate-300 font-semibold" onClick={addEvent}>{t('Add')}</button>
            </div>
          </div>
          
          <div className="overflow-auto border border-slate-200 rounded max-h-64">
            <table className="w-full text-left text-sm border-collapse">
              <thead className="bg-slate-100 border-b border-slate-200 sticky top-0">
                <tr>
                  <th className="p-2 font-semibold text-slate-700">{t('Time (min)')}</th>
                  <th className="p-2 font-semibold text-slate-700">{t('Event Switch')}</th>
                  <th className="p-2 font-semibold text-slate-700">{t('Position')}</th>
                  <th className="p-2 w-10"></th>
                </tr>
              </thead>
              <tbody>
                {events.map((event) => (
                  <tr key={event.id} className="border-b border-slate-100 hover:bg-blue-50">      
                    <td className="p-2 font-medium text-slate-700">{event.time.toFixed(2)}</td>   
                    <td className="p-2 text-slate-600">{event.valve}</td>
                    <td className="p-2">
                      <span className={`px-2 py-0.5 rounded text-xs font-bold ${event.position === 'ON' ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'}`}>
                        {event.position}
                      </span>
                    </td>
                    <td className="p-2 text-center">
                      <button className="text-red-500 hover:text-red-700 text-xs font-bold" onClick={() => removeEvent(event.id)}>×</button>
                    </td>
                  </tr>
                ))}
                {events.length === 0 && (
                  <tr><td colSpan={4} className="p-4 text-center text-slate-400 text-xs italic">{t('No valve events configured.')}</td></tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}