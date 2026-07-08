import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

export default function AdvancedChromatography() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [targetRt, setTargetRt] = useState<number>(4.211);
  const [currentPressure, setCurrentPressure] = useState<number>(15.0);
  const [calculatedPressure, setCalculatedPressure] = useState<number | null>(null);

  const calculateRTL = () => {
    // Mock RTL algorithm: Pressure adjustment is inversely proportional to RT shift
    // Real algorithm involves complex viscosity and column dimensions math
    const shift = (targetRt - 4.150) / 4.150; // Assume current observed RT is 4.150
    const newP = currentPressure * (1 - shift * 0.8);
    setCalculatedPressure(Math.round(newP * 100) / 100);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs">
        <div className="px-2 py-1 hover:bg-blue-100 cursor-pointer">高级(A)</div>
        <div className="px-2 py-1 hover:bg-blue-100 cursor-pointer">RTL(R)</div>
        <div className="px-2 py-1 hover:bg-blue-100 cursor-pointer">翻译器(T)</div>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        
        {/* Retention Time Locking (RTL) */}
        <div className="w-1/2 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">保留时间锁定 (Retention Time Locking - RTL)</div>
          <div className="p-4 flex flex-col gap-4">
            <div className="text-xs text-gray-700 mb-2">
              RTL allows you to exactly match retention times between different GC systems by making precise adjustments to the column head pressure.
            </div>
            
            <div className="border border-gray-300 p-4 bg-gray-50 flex flex-col gap-3">
              <h3 className="font-bold border-b border-gray-300 pb-1">Step 1: Calibration</h3>
              <div className="flex justify-between items-center">
                <span>{t('Target Compound:')}</span>
                <select className="border p-1 w-48"><option>Caffeine</option><option>Internal Standard A</option></select>
              </div>
              <div className="flex justify-between items-center">
                <span>Target Retention Time (min):</span>
                <input type="number" value={targetRt} onChange={e => setTargetRt(Number(e.target.value))} className="border p-1 w-24 text-right" />
              </div>
              <div className="flex justify-between items-center">
                <span>Current Method Pressure (psi):</span>
                <input type="number" value={currentPressure} onChange={e => setCurrentPressure(Number(e.target.value))} className="border p-1 w-24 text-right" />
              </div>
              <button onClick={calculateRTL} className="mt-2 bg-gray-200 border border-gray-400 py-1 hover:bg-gray-300 active:bg-gray-400">
                计算锁定压力 (Calculate Lock Pressure)
              </button>
            </div>

            {calculatedPressure !== null && (
              <div className="border border-green-500 bg-green-50 p-4 flex flex-col gap-2">
                <h3 className="font-bold text-green-800">RTL Result</h3>
                <div className="flex justify-between">
                  <span>Calculated Lock Pressure:</span>
                  <span className="font-mono font-bold text-lg">{calculatedPressure} psi</span>
                </div>
                <button className="mt-2 bg-blue-600 text-white py-1 hover:bg-blue-700">
                  Update Method (更新至当前方法)
                </button>
              </div>
            )}
          </div>
        </div>

        {/* Method Translator */}
        <div className="w-1/2 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">方法翻译器 (Method Translator)</div>
          <div className="p-4 flex flex-col gap-4">
             <div className="text-xs text-gray-700 mb-2">
              Automatically translate methods when changing carrier gas type, column dimensions, or detector outlet pressure.
            </div>

            <div className="grid grid-cols-2 gap-4">
              {/* Original Method */}
              <div className="border border-gray-300 p-2 bg-gray-50">
                <h3 className="font-bold border-b border-gray-300 pb-1 mb-2">Original Method</h3>
                <div className="flex flex-col gap-2 text-xs">
                  <div className="flex justify-between"><span>Carrier Gas:</span><span className="font-bold">Helium (He)</span></div>
                  <div className="flex justify-between"><span>Column L:</span><span>30.0 m</span></div>
                  <div className="flex justify-between"><span>Column ID:</span><span>0.25 mm</span></div>
                  <div className="flex justify-between"><span>{t('Flow:')}</span><span>1.0 mL/min</span></div>
                </div>
              </div>

              {/* Translated Method */}
              <div className="border border-blue-300 p-2 bg-blue-50">
                <h3 className="font-bold border-b border-blue-300 pb-1 mb-2 text-blue-800">Translated Method</h3>
                <div className="flex flex-col gap-2 text-xs">
                  <div className="flex justify-between items-center">
                    <span>Carrier Gas:</span>
                    <select className="border border-blue-300 p-0.5"><option>Hydrogen (H2)</option><option>Nitrogen (N2)</option></select>
                  </div>
                  <div className="flex justify-between items-center">
                    <span>Column L:</span>
                    <input type="text" defaultValue="20.0 m" className="border border-blue-300 p-0.5 w-16 text-right" />
                  </div>
                  <div className="flex justify-between items-center">
                    <span>Column ID:</span>
                    <input type="text" defaultValue="0.18 mm" className="border border-blue-300 p-0.5 w-16 text-right" />
                  </div>
                  <div className="flex justify-between items-center text-green-700 font-bold">
                    <span>New Flow:</span><span>0.65 mL/min</span>
                  </div>
                </div>
              </div>
            </div>

            <button className="bg-gray-200 border border-gray-400 py-2 hover:bg-gray-300 font-bold mt-auto">
              Translate (执行翻译)
            </button>
          </div>
        </div>

      </div>
    </div>
  );
}