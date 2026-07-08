import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ReferenceLine, ResponsiveContainer } from 'recharts';

export default function ControlCharts() {
  const { t } = useTranslation();
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sst/trends`);
        if (res.ok) {
          setData(await res.json());
        }
      } catch (e) {
        console.error("Failed to load SST trends", e);
      }
    };
    fetchData();
  }, []);

  return (
    <div className="p-6 h-full flex flex-col bg-white">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('SST Control Charts (Trending)')}</h1>
        <button className="bg-slate-200 text-slate-700 px-4 py-2 rounded shadow hover:bg-slate-300">{t('Export Report')}</button>
      </div>
      
      <div className="flex gap-4 mb-4">
        <select className="border border-slate-300 rounded p-2 text-sm">
          <option>Parameter: Retention Time (RT)</option>
          <option>Parameter: Theoretical Plates (N)</option>
          <option>Parameter: Resolution (Rs)</option>
        </select>
        <select className="border border-slate-300 rounded p-2 text-sm">
          <option>Compound: Caffeine</option>
          <option>Compound: Paracetamol</option>
        </select>
      </div>

      <div className="flex-1 bg-slate-50 border border-slate-200 rounded p-4 flex flex-col">
        <h3 className="font-semibold text-slate-700 mb-4 text-center">{t('Retention Time Control Chart (3-Sigma)')}</h3>
        <div className="flex-1 min-h-[300px]">
          <ResponsiveContainer width="100%" height="100%">
            <LineChart data={data} margin={{ top: 20, right: 30, left: 20, bottom: 5 }}>
              <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
              <XAxis dataKey="run" label={{ value: 'Injection Number', position: 'insideBottom', offset: -5 }} />
              <YAxis domain={['dataMin - 0.05', 'dataMax + 0.05']} />
              <Tooltip />
              <ReferenceLine y={4.20} stroke="red" strokeDasharray="3 3" label="Upper Control Limit (+3σ)" />
              <ReferenceLine y={4.10} stroke="green" label="Mean (X̄)" />
              <ReferenceLine y={4.00} stroke="red" strokeDasharray="3 3" label="Lower Control Limit (-3σ)" />
              <Line type="monotone" dataKey="rt" stroke="#2563eb" strokeWidth={2} dot={{ r: 4 }} activeDot={{ r: 6 }} />
            </LineChart>
          </ResponsiveContainer>
        </div>
      </div>
      
      <div className="mt-4 p-4 bg-red-50 border border-red-200 rounded text-red-700 text-sm">
        <strong>Warning:</strong> Run #6 exceeded the Upper Control Limit (4.20). System Suitability Failure.
      </div>
    </div>
  );
}