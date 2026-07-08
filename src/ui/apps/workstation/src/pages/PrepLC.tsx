import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

export default function PrepLC() {
  const { t } = useTranslation();
  const [settings, setSettings] = useState({
    trigger_mode: 'Slope',
    slope_up: 5.0,
    slope_down: -2.0,
    max_volume: 15.0
  });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchSettings = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/prep/settings`);
        if (res.ok) {
          const data = await res.json();
          setSettings(data);
        }
      } catch (e) {
        console.error("Failed to load prep settings", e);
      }
    };
    fetchSettings();
  }, []);

  const handleSave = async () => {
    setSaving(true);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/prep/settings`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(settings)
      });
      alert(t('common.saved', 'Saved successfully'));
    } catch (e) {
      console.error("Failed to save prep settings", e);
    } finally {
      setSaving(false);
    }
  };

  return (
    <div className="p-6 h-full flex flex-col bg-white overflow-y-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('prep.title', 'Prep-LC & Intelligent Sequence')}</h1>
        <button 
          className="bg-blue-600 text-white px-4 py-2 rounded shadow hover:bg-blue-700 disabled:opacity-50"
          onClick={handleSave}
          disabled={saving}
        >
          {saving ? t('common.saving', 'Saving...') : t('prep.save', 'Save Intelligent Method')}
        </button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Fraction Collection */}
        <div className="bg-slate-50 border border-slate-200 rounded p-5">
          <h3 className="text-lg font-bold text-slate-700 border-b pb-2 mb-4">{t('prep.fraction', 'Fraction Collection Settings')}</h3>
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-slate-700 mb-1">{t('prep.trigger_mode', 'Collection Trigger Mode')}</label>
              <select 
                className="w-full border border-slate-300 rounded p-2 text-sm"
                value={settings.trigger_mode}
                onChange={e => setSettings({...settings, trigger_mode: e.target.value})}
              >
                <option value="Time">{t('prep.trig_time', 'Time-Based Slices')}</option>
                <option value="Threshold">{t('prep.trig_thresh', 'Peak Height Threshold')}</option>
                <option value="Slope">{t('prep.trig_slope', 'Peak Slope (Up/Down)')}</option>
              </select>
            </div>
            
            {settings.trigger_mode === 'Slope' && (
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-xs text-slate-500 mb-1">{t('prep.slope_up', 'Start Slope (mAU/s)')}</label>
                  <input 
                    type="number" 
                    value={settings.slope_up} 
                    onChange={e => setSettings({...settings, slope_up: parseFloat(e.target.value)})}
                    className="w-full border border-slate-300 rounded p-2 text-sm" 
                  />
                </div>
                <div>
                  <label className="block text-xs text-slate-500 mb-1">{t('prep.slope_down', 'Stop Slope (mAU/s)')}</label>
                  <input 
                    type="number" 
                    value={settings.slope_down} 
                    onChange={e => setSettings({...settings, slope_down: parseFloat(e.target.value)})}
                    className="w-full border border-slate-300 rounded p-2 text-sm" 
                  />
                </div>
              </div>
            )}
            
            <div>
              <label className="block text-sm font-medium text-slate-700 mb-1">{t('prep.max_vol', 'Max Volume per Tube (mL)')}</label>
              <input 
                type="number" 
                value={settings.max_volume} 
                onChange={e => setSettings({...settings, max_volume: parseFloat(e.target.value)})}
                className="w-full border border-slate-300 rounded p-2 text-sm" 
              />
            </div>
          </div>
        </div>

        {/* Intelligent Sequence & Sleep/Wake */}
        <div className="space-y-6">
          <div className="bg-orange-50 border border-orange-200 rounded p-5">
            <h3 className="text-lg font-bold text-orange-800 border-b border-orange-200 pb-2 mb-4">
              {t('prep.intelligent', 'Intelligent Sequence Decisions')}
            </h3>
            <div className="space-y-3 text-sm">
              <label className="flex items-start gap-2">
                <input type="checkbox" defaultChecked className="mt-1" />
                <span className="text-orange-900">{t('prep.action1', 'If Target Peak Area < Limit, Automatically Re-inject from same vial.')}</span>
              </label>
              <label className="flex items-start gap-2">
                <input type="checkbox" defaultChecked className="mt-1" />
                <span className="text-orange-900">{t('prep.action2', 'If Unknown Peak > 10%, Run Needle Wash Method immediately.')}</span>
              </label>
              <label className="flex items-start gap-2">
                <input type="checkbox" className="mt-1" />
                <span className="text-orange-900">{t('prep.action3', 'Abort Sequence if System Pressure exceeds 350 bar for 5 mins.')}</span>
              </label>
            </div>
          </div>

          <div className="bg-slate-50 border border-slate-200 rounded p-5">
            <h3 className="text-lg font-bold text-slate-700 border-b pb-2 mb-4">{t('prep.sleep_wake', 'Sleep / Wake Automation')}</h3>
            <div className="grid grid-cols-2 gap-4 text-sm">
              <div>
                <label className="block font-medium text-slate-700 mb-1">{t('prep.after_seq', 'After Sequence Completes:')}</label>
                <select className="w-full border border-slate-300 rounded p-2">
                  <option>{t('prep.sleep_method', 'Run Sleep Method (0.1 mL/min, Lamps Off)')}</option>
                  <option>{t('prep.turn_off', 'Turn Off All Modules')}</option>
                  <option>{t('prep.keep_on', 'Keep Current Conditions')}</option>
                </select>
              </div>
              <div>
                <label className="block font-medium text-slate-700 mb-1">{t('prep.wake_at', 'Wake & Pre-heat at:')}</label>
                <input type="datetime-local" className="w-full border border-slate-300 rounded p-2" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}