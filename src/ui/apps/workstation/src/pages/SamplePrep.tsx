import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

type PrepAction = 'Draw' | 'Dispense' | 'Mix' | 'Wash' | 'Wait';

interface PrepStep {
  id: string;
  action: PrepAction;
  volume?: number;
  location?: string;
  speed?: number;
  duration?: number;
}

export default function SamplePrep() {
  const { t } = useTranslation();
  const [steps, setSteps] = useState<PrepStep[]>([]);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchProgram = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/prep/injector`);
        if (res.ok) {
          setSteps(await res.json());
        }
      } catch (e) {
        console.error("Failed to load injector program", e);
      }
    };
    fetchProgram();
  }, []);

  const handleSave = async () => {
    setSaving(true);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/prep/injector`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(steps)
      });
      alert(t('common.saved', 'Saved successfully'));
    } catch (e) {
      console.error("Failed to save injector program", e);
    } finally {
      setSaving(false);
    }
  };

  const addStep = (action: PrepAction) => {
    setSteps([...steps, { id: Date.now().toString(), action }]);
  };

  return (
    <div className="p-6 h-full flex flex-col bg-white">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('Intelligent Sample Prep')}</h1>
        <button 
          className="bg-blue-600 text-white px-4 py-2 rounded shadow hover:bg-blue-700 disabled:opacity-50"
          onClick={handleSave}
          disabled={saving}
        >
          {saving ? t('common.saving', 'Saving...') : t('Save Program')}
        </button>
      </div>
      <div className="bg-slate-50 p-4 rounded border border-slate-200 mb-6">
        <p className="text-sm text-slate-600 mb-2">{t('Build custom injector programs for online dilution, derivatization, and internal standard addition.')}</p>
        <div className="flex gap-2">
          {(['Draw', 'Dispense', 'Mix', 'Wash', 'Wait'] as PrepAction[]).map(action => (
            <button key={action} className="px-3 py-1 bg-white border border-slate-300 rounded text-sm hover:bg-slate-100" onClick={() => addStep(action)}>+ {action}</button>
          ))}
        </div>
      </div>
      <div className="flex-1 overflow-auto border border-slate-200 rounded">
        <table className="w-full text-left text-sm border-collapse">
          <thead className="bg-slate-100 border-b border-slate-200 sticky top-0">
            <tr>
              <th className="p-3 font-semibold text-slate-700">{t('Step')}</th>
              <th className="p-3 font-semibold text-slate-700">{t('Action')}</th>
              <th className="p-3 font-semibold text-slate-700">{t('Location')}</th>
              <th className="p-3 font-semibold text-slate-700">{t('Volume (μL)')}</th>
              <th className="p-3 font-semibold text-slate-700">{t('Speed (μL/min)')}</th>
              <th className="p-3 font-semibold text-slate-700">{t('Wait/Mix Time (s)')}</th>
            </tr>
          </thead>
          <tbody>
            {steps.map((step, index) => (
              <tr key={step.id} className="border-b border-slate-100 hover:bg-blue-50">
                <td className="p-3 text-slate-600">{index + 1}</td>
                <td className="p-3 font-medium text-blue-700">{step.action}</td>
                <td className="p-3">{step.location || '-'}</td>
                <td className="p-3">{step.volume || '-'}</td>
                <td className="p-3">{step.speed || '-'}</td>
                <td className="p-3">{step.duration || '-'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}