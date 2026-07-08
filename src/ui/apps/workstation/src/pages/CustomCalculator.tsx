import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

export default function CustomCalculator() {
  const { t } = useTranslation();
  const [expression, setExpression] = useState('Area / ISTD_Area * 100 * Multiplier');
  const [columnName, setColumnName] = useState('Normalized Conc.');

  return (
    <div className="p-6 h-full flex flex-col bg-white">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('Custom Calculator & Reporting')}</h1>
        <button className="bg-blue-600 text-white px-4 py-2 rounded shadow hover:bg-blue-700">{t('Apply to Method')}</button>
      </div>
      
      <div className="grid grid-cols-3 gap-6 flex-1">
        <div className="col-span-2 flex flex-col gap-4">
          <div className="bg-slate-50 p-4 rounded border border-slate-200">
            <label className="block font-semibold text-slate-700 mb-2">{t('New Column Name')}</label>
            <input 
              type="text" 
              value={columnName}
              onChange={e => setColumnName(e.target.value)}
              className="w-full border border-slate-300 rounded p-2 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500" 
            />
          </div>
          
          <div className="bg-slate-50 p-4 rounded border border-slate-200 flex-1 flex flex-col">
            <label className="block font-semibold text-slate-700 mb-2">{t('Expression (AST Parser)')}</label>
            <textarea 
              value={expression}
              onChange={e => setExpression(e.target.value)}
              className="w-full flex-1 border border-slate-300 rounded p-2 text-sm font-mono focus:outline-none focus:ring-2 focus:ring-blue-500" 
            />
            <div className="mt-2 text-xs text-slate-500">
              Valid variables: Area, Height, Width, RT, ISTD_Area, Multiplier, Dilution, Sample_Amount
            </div>
          </div>
        </div>
        
        <div className="bg-slate-50 border border-slate-200 rounded p-4 overflow-y-auto">
          <h3 className="font-semibold text-slate-700 mb-4">{t('Preview Results')}</h3>
          <div className="space-y-3 text-sm">
            <div className="p-2 bg-white border border-slate-200 rounded shadow-sm">
              <div className="text-xs text-slate-500 mb-1">Peak 1 (Caffeine)</div>
              <div className="flex justify-between">
                <span>{t('Result:')}</span>
                <span className="font-mono font-bold text-blue-600">45.21</span>
              </div>
            </div>
            <div className="p-2 bg-white border border-slate-200 rounded shadow-sm">
              <div className="text-xs text-slate-500 mb-1">Peak 2 (Impurity A)</div>
              <div className="flex justify-between">
                <span>{t('Result:')}</span>
                <span className="font-mono font-bold text-blue-600">0.05</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}