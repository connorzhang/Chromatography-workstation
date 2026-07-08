import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

export default function Diagnostics() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  // Modal states
  const [showRunTests, setShowRunTests] = useState(false);
  const [showModuleTests, setShowModuleTests] = useState(false);
  const [showResetCounters, setShowResetCounters] = useState(false);
  const [showEmfLimits, setShowEmfLimits] = useState(false);

  const emfData = [
    { component: 'Pump Seals', current: 15000, limit: 20000, unit: 'Liters', status: 'Warning' },
    { component: 'UV Lamp (Deuterium)', current: 1850, limit: 2000, unit: 'Hours', status: 'Critical' },
    { component: 'Injector Needle', current: 8500, limit: 10000, unit: 'Injections', status: 'Good' },
    { component: 'Column 1 (C18)', current: 450, limit: 1000, unit: 'Injections', status: 'Good' },
  ];

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'Run Tests') setShowRunTests(true);
    else if (action === 'Module Tests') setShowModuleTests(true);
    else if (action === 'Reset Counters') setShowResetCounters(true);
    else if (action === 'EMF Limits') setShowEmfLimits(true);
    else alert(`${t('Feature in development')}: ${action}`);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'file' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'file')}>{t('File(F)')}</div>
          {activeMenu === 'file' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Save Log')}>{t('Save Log...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print Log')}>{t('Print Log...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'instrument' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'instrument')}>{t('Instrument(I)')}</div>
          {activeMenu === 'instrument' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Run Tests')}>{t('Run Tests')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Module Tests')}>{t('Module Tests...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'maintenance' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'maintenance')}>{t('Maintenance(M)')}</div>
          {activeMenu === 'maintenance' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Reset Counters')}>{t('Reset Counters')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('EMF Limits')}>{t('EMF Limits...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Maintenance Log')}>{t('Maintenance Log...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Firmware Update')}>{t('Firmware Update...')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Run Tests Modal */}
      {showRunTests && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Run Diagnostic Tests')}</span>
              <span className="cursor-pointer" onClick={() => setShowRunTests(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Pump Leak Test')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Detector Lamp Intensity Test')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Autosampler Alignment Test')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Column Oven Thermal Test')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowRunTests(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowRunTests(false)}>{t('Start')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Module Tests Modal */}
      {showModuleTests && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Module Specific Tests')}</span>
              <span className="cursor-pointer" onClick={() => setShowModuleTests(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold">{t('Select Module')}:</label>
              <select className="border border-gray-400 p-2">
                <option>Agilent 1260 Infinity II Quaternary Pump</option>
                <option>Agilent 1260 Infinity II Vialsampler</option>
                <option>Agilent 1260 Infinity II DAD</option>
              </select>
              <div className="border border-gray-400 h-32 flex items-center justify-center bg-white text-gray-400 mt-2">
                {t('Select a module to view available tests')}
              </div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowModuleTests(false)}>{t('Close')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Reset Counters Modal */}
      {showResetCounters && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Reset EMF Counters')}</span>
              <span className="cursor-pointer" onClick={() => setShowResetCounters(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold">{t('Select Counter to Reset')}:</label>
              <select className="border border-gray-400 p-2" size={4}>
                {emfData.map(item => <option key={item.component}>{item.component} ({item.current} {item.unit})</option>)}
              </select>
              <p className="text-red-600 text-xs mt-2">{t('Warning: This action will reset the maintenance counter to zero and will be logged in the Audit Trail.')}</p>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowResetCounters(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowResetCounters(false)}>{t('Reset Counter')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* EMF Limits Modal */}
      {showEmfLimits && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('EMF Limits Configuration')}</span>
              <span className="cursor-pointer" onClick={() => setShowEmfLimits(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-4">
              <p className="text-xs text-gray-600">{t('Configure Early Maintenance Feedback thresholds. System will generate warnings when limits are reached.')}</p>
              
              <div className="border border-gray-400">
                <table className="w-full text-left border-collapse text-xs">
                  <thead>
                    <tr className="bg-gray-200 border-b border-gray-400">
                      <th className="p-2 border-r border-gray-400">{t('Component')}</th>
                      <th className="p-2 border-r border-gray-400">{t('Current Value')}</th>
                      <th className="p-2 border-r border-gray-400">{t('Warning Limit')}</th>
                      <th className="p-2">{t('Unit')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {emfData.map((item, idx) => (
                      <tr key={idx} className="border-b border-gray-300 bg-white">
                        <td className="p-2 border-r border-gray-300 font-medium">{item.component}</td>
                        <td className="p-2 border-r border-gray-300 text-right">{item.current.toLocaleString()}</td>
                        <td className="p-1 border-r border-gray-300">
                          <input type="number" defaultValue={item.limit} className="w-full border border-gray-300 p-1 text-right" />
                        </td>
                        <td className="p-2">{item.unit}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>

              <div className="flex justify-end gap-2 mt-2 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowEmfLimits(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowEmfLimits(false)}>{t('Apply Limits')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs font-bold text-blue-700">{t('Refresh Status')}</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs">{t('Run Diagnostics')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs">{t('Reset Counters')}</button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        
        {/* EMF Dashboard */}
        <div className="w-2/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Early Maintenance Feedback - EMF')}</div>
          <div className="p-6 flex flex-col gap-6 overflow-auto">
            <h2 className="text-lg font-bold border-b pb-2">{t('Consumables Status')}</h2>
            
            <div className="grid grid-cols-2 gap-8">
              {emfData.map((item, idx) => {
                const percentage = (item.current / item.limit) * 100;
                let colorClass = "bg-green-500";
                if (percentage > 80) colorClass = "bg-yellow-500";
                if (percentage > 90) colorClass = "bg-red-500";

                return (
                  <div key={idx} className="border border-gray-300 p-4 shadow-sm flex flex-col gap-2">
                    <div className="flex justify-between font-bold">
                      <span>{item.component}</span>
                      <span className={item.status === 'Critical' ? 'text-red-600' : item.status === 'Warning' ? 'text-yellow-600' : 'text-green-600'}>
                        {item.status}
                      </span>
                    </div>
                    
                    <div className="w-full bg-gray-200 h-4 border border-gray-400 rounded overflow-hidden">
                      <div className={`h-full ${colorClass}`} style={{ width: `${Math.min(percentage, 100)}%` }}></div>
                    </div>
                    
                    <div className="flex justify-between text-xs text-gray-600 font-mono">
                      <span>{item.current.toLocaleString()} {item.unit}</span>
                      <span>Limit: {item.limit.toLocaleString()} {item.unit}</span>
                    </div>
                  </div>
                );
              })}
            </div>
          </div>
        </div>

        {/* System Logs / Diagnostics */}
        <div className="w-1/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Diagnostics Logs')}</div>
          <div className="p-2 overflow-auto flex-1 font-mono text-xs bg-black text-green-400">
            <div>[2026-06-20 10:00:15] SYSTEM: Checking module connections... OK</div>
            <div>[2026-06-20 10:00:16] PUMP: Pressure ripple test passed (0.2%).</div>
            <div>[2026-06-20 10:00:18] ALS: Needle alignment verified.</div>
            <div>[2026-06-20 10:00:20] TCD: Filament resistance 35 ohms (Normal).</div>
            <div className="text-yellow-400">[2026-06-20 10:00:22] DAD: Lamp energy dropping at 230nm. Replace soon.</div>
            <div className="text-red-400">[2026-06-20 10:00:25] EMF: Deuterium lamp has exceeded 90% of expected life.</div>
            <div className="mt-4 text-white animate-pulse">_</div>
          </div>
        </div>

      </div>
    </div>
  );
}