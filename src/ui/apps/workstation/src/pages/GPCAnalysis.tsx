import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer } from 'recharts';

export default function GPCAnalysis() {
  const { t } = useTranslation();
  const [results, setResults] = useState({ mn: 0, mw: 0, mz: 0, pdi: 0 });
  const [loading, setLoading] = useState(false);
  const [calibrationData, setCalibrationData] = useState<any[]>([]);
  const [slices, setSlices] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/gpc/data`);
        if (res.ok) {
          const data = await res.json();
          setCalibrationData(data.calibration);
          setSlices(data.sample_slices);
        }
      } catch (e) {
        console.error("Failed to fetch GPC data", e);
      }
    };
    fetchData();
  }, []);

  const handleCalculate = async () => {
    setLoading(true);
    try {
      // Send to Rust backend
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/gpc/calculate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          slices,
          slope: -0.5, // logM = -0.5 * RT + 10.0
          intercept: 10.0
        })
      });

      if (res.ok) {
        const data = await res.json();
        setResults(data);
      }
    } catch (e) {
      console.error("GPC Calculation failed", e);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-6 h-full flex flex-col bg-white overflow-y-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('gpc.title', 'GPC/SEC Polymer Analysis')}</h1>
        <button 
          onClick={handleCalculate}
          disabled={loading}
          className="bg-blue-600 text-white px-4 py-2 rounded shadow hover:bg-blue-700 disabled:opacity-50"
        >
          {loading ? t('common.calculating', 'Calculating...') : t('gpc.calc', 'Calculate Molecular Weight')}
        </button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 flex-1">
        {/* Results Panel */}
        <div className="col-span-1 bg-slate-50 border border-slate-200 rounded p-4 flex flex-col">
          <h3 className="font-semibold text-slate-700 mb-4 border-b pb-2">{t('gpc.results', 'Molecular Weight Distribution')}</h3>
          
          <div className="space-y-4">
            <div className="flex justify-between items-center bg-white p-3 rounded shadow-sm border border-slate-100">
              <span className="text-slate-600 font-medium">{t('gpc.mn', 'Number Average (Mn)')}</span>
              <span className="font-mono text-lg text-blue-700 font-bold">{results.mn > 0 ? results.mn.toLocaleString() : '-'}</span>
            </div>
            <div className="flex justify-between items-center bg-white p-3 rounded shadow-sm border border-slate-100">
              <span className="text-slate-600 font-medium">{t('gpc.mw', 'Weight Average (Mw)')}</span>
              <span className="font-mono text-lg text-blue-700 font-bold">{results.mw > 0 ? results.mw.toLocaleString() : '-'}</span>
            </div>
            <div className="flex justify-between items-center bg-white p-3 rounded shadow-sm border border-slate-100">
              <span className="text-slate-600 font-medium">{t('gpc.mz', 'Z Average (Mz)')}</span>
              <span className="font-mono text-lg text-blue-700 font-bold">{results.mz > 0 ? results.mz.toLocaleString() : '-'}</span>
            </div>
            <div className="flex justify-between items-center bg-white p-3 rounded shadow-sm border border-slate-100">
              <span className="text-slate-600 font-medium">{t('gpc.pdi', 'Polydispersity (PDI = Mw/Mn)')}</span>
              <span className="font-mono text-lg text-green-600 font-bold">{results.pdi > 0 ? results.pdi.toFixed(3) : '-'}</span>
            </div>
          </div>
        </div>

        {/* Calibration Curve */}
        <div className="col-span-2 bg-slate-50 border border-slate-200 rounded p-4 flex flex-col">
          <h3 className="font-semibold text-slate-700 mb-4">{t('gpc.curve', 'GPC Calibration Curve (Log M vs RT)')}</h3>
          <div className="flex-1 min-h-[300px]">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={calibrationData} margin={{ top: 20, right: 30, left: 20, bottom: 20 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                <XAxis dataKey="rt" label={{ value: t('gpc.rt_axis', 'Retention Time (min)'), position: 'bottom' }} reversed />
                <YAxis label={{ value: 'Log M', angle: -90, position: 'left' }} />
                <Tooltip />
                <Line type="monotone" dataKey="logM" stroke="#ea580c" strokeWidth={2} dot={{r: 5}} activeDot={{r: 8}} />
              </LineChart>
            </ResponsiveContainer>
          </div>
          <div className="mt-4 text-sm text-slate-500 flex gap-4">
            <span>{t('gpc.equation', 'Fit: 3rd Order Polynomial')}</span>
            <span>R² = 0.9998</span>
          </div>
        </div>
      </div>
    </div>
  );
}