import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { ScatterChart, Scatter, XAxis, YAxis, CartesianGrid, ResponsiveContainer, Tooltip, Line, ComposedChart } from 'recharts';

interface CalibrationPoint {
  level: number;
  amount: number;
  response: number;
  used: boolean;
}

interface CalibrationCurve {
  slope: number;
  intercept: number;
  r_squared: number;
  points: CalibrationPoint[];
}

export default function Calibration() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  const [points, setPoints] = useState<CalibrationPoint[]>([
    { level: 1, amount: 10, response: 1250, used: true },
    { level: 2, amount: 20, response: 2480, used: true },
    { level: 3, amount: 50, response: 6100, used: true },
    { level: 4, amount: 100, response: 12350, used: true },
    { level: 5, amount: 200, response: 24800, used: true },
  ]);

  const [fitType, setFitType] = useState('Linear');
  const [originTreatment, setOriginTreatment] = useState('Ignore Origin');
  const [curve, setCurve] = useState<CalibrationCurve | null>(null);
  const [isCalculating, setIsCalculating] = useState(false);
  const [showUpdateMode, setShowUpdateMode] = useState(false);
  const [showAdvancedSettings, setShowAdvancedSettings] = useState(false);

  const calculateCurve = async () => {
    setIsCalculating(true);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/calibration/calculate`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          points,
          fit_type: fitType,
          origin_treatment: originTreatment
        })
      });
      const data = await res.json();
      setCurve(data);
    } catch (err) {
      console.error('Failed to calculate calibration curve:', err);
    } finally {
      setIsCalculating(false);
    }
  };

  useEffect(() => {
    calculateCurve();
  }, [points, originTreatment, fitType]);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handlePointToggle = (level: number) => {
    setPoints(points.map(p => p.level === level ? { ...p, used: !p.used } : p));
  };

  const handleAmountChange = (level: number, newAmount: number) => {
    setPoints(points.map(p => p.level === level ? { ...p, amount: newAmount } : p));
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'clear') {
      setPoints([]);
      setCurve(null);
    } else if (action === 'recalibrate') {
      calculateCurve();
    } else if (action === 'Update Mode') {
      setShowUpdateMode(true);
    } else if (action === 'Advanced Settings') {
      setShowAdvancedSettings(true);
    } else {
      alert(`${t('Feature in development')}: ${action}`);
    }
  };

  // Generate line data for Recharts
  let lineData: any[] = [];
  if (curve) {
    const minAmount = 0;
    const maxAmount = Math.max(...points.map(p => p.amount)) * 1.1;
    lineData = [
      { amount: minAmount, lineY: curve.slope * minAmount + curve.intercept },
      { amount: maxAmount, lineY: curve.slope * maxAmount + curve.intercept },
    ];
  }

  const chartData = points.filter(p => p.used).map(p => ({
    amount: p.amount,
    response: p.response,
  }));

  // Combine point data and line data into one array for ComposedChart
  const combinedData = [...chartData, ...lineData].sort((a, b) => a.amount - b.amount);

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'calibration' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'calibration')}>{t('Calibration(C)')}</div>
          {activeMenu === 'calibration' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('New Calibration Table')}>{t('New Calibration Table...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Open Calibration Table')}>{t('Open Calibration Table...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => handleMenuClick('recalibrate')}>
                <span>{t('Recalibrate')}</span>
                <span className="text-gray-500 hover:text-white">F5</span>
              </div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('clear')}>{t('Clear All Points')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Update Mode')}>{t('Update Mode (Replace/Average)...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Advanced Settings')}>{t('Advanced Fit Settings...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'view' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'view')}>{t('View(V)')}</div>
          {activeMenu === 'view' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('Toggle Curve')}><span className="w-3">✓</span>{t('Calibration Curve')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('Toggle Table')}><span className="w-3">✓</span>{t('Calibration Table')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded">�️</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded">�</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <select className="border border-gray-400 rounded px-1 bg-white text-xs py-0.5" value={fitType} onChange={(e) => setFitType(e.target.value)}>
          <option>{t('Linear')}</option>
        </select>
        <select className="border border-gray-400 rounded px-1 bg-white text-xs py-0.5 ml-1" value={originTreatment} onChange={(e) => setOriginTreatment(e.target.value)}>
          <option>Include Origin</option>
          <option>Force Origin</option>
          <option>Ignore Origin</option>
        </select>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs ml-2 flex items-center" title="Recalibrate" onClick={calculateCurve}>
          <span className="mr-1">🔄</span> {t('Recalibrate Curve')}
          {isCalculating && <span className="ml-1 animate-spin">⏳</span>}
        </button>
      </div>

      {/* Main Workspace */}
      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        {/* Left: Calibration Table */}
        <div className="w-1/2 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Calibration Table')} - {t('Compound')} Toluene</div>
          <div className="overflow-auto flex-1">
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
              <thead className="bg-[#e0e0e0] sticky top-0">
                <tr>
                  <th className="border border-gray-400 p-1 text-center w-12">{t('Level')}</th>
                  <th className="border border-gray-400 p-1 text-right">{t('Amount (μg/mL)')}</th>
                  <th className="border border-gray-400 p-1 text-right">{t('Response (Area)')}</th>
                  <th className="border border-gray-400 p-1 text-center">{t('Ret. Time')}</th>
                  <th className="border border-gray-400 p-1 text-center">{t('Used')}</th>
                </tr>
              </thead>
              <tbody>
                {points.map((row) => (
                  <tr key={row.level} className="hover:bg-blue-50">
                    <td className="border border-gray-300 p-1 text-center font-bold">{row.level}</td>
                    <td className="border border-gray-300 p-1 text-right font-mono">
                      <input
                        type="number"
                        value={row.amount}
                        onChange={(e) => handleAmountChange(row.level, Number(e.target.value))}
                        className="w-full text-right bg-transparent outline-none border-b border-transparent hover:border-blue-400 focus:border-blue-500"
                      />
                    </td>
                    <td className="border border-gray-300 p-1 text-right font-mono">{row.response.toFixed(1)}</td>
                    <td className="border border-gray-300 p-1 text-center font-mono">4.211</td>
                    <td className="border border-gray-300 p-1 text-center">
                      <input
                        type="checkbox"
                        checked={row.used}
                        onChange={() => handlePointToggle(row.level)}
                        className="cursor-pointer"
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        {/* Right: Calibration Curve */}
        <div className="w-1/2 bg-white border border-gray-500 shadow-md flex flex-col relative">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Calibration Curve')}</div>
          <div className="flex-1 p-4 pb-8 relative">
            <div className="absolute left-2 top-1/2 text-xs font-bold -rotate-90 origin-left -translate-y-1/2 text-gray-700">{t('Response')} (Area)</div>
            <div className="absolute bottom-2 w-full text-center text-xs font-bold text-gray-700">{t('Amount')}</div>
            
            {curve && (
              <div className="absolute top-8 left-16 text-xs text-blue-800 font-mono border border-blue-200 bg-blue-50 p-2 shadow-sm rounded z-10">
                <div className="font-bold border-b border-blue-200 pb-1 mb-1">{t('Linear Regression Equation')}</div>
                <div>Y = {curve.slope.toFixed(4)} * X {curve.intercept >= 0 ? '+' : '-'} {Math.abs(curve.intercept).toFixed(4)}</div>
                <div>R² = {curve.r_squared.toFixed(6)}</div>
              </div>
            )}
            
            <ResponsiveContainer width="100%" height="100%">
              <ComposedChart margin={{ top: 20, right: 20, bottom: 20, left: 30 }} data={combinedData}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e0e0e0" />
                <XAxis type="number" dataKey="amount" name="Amount" tick={{fontSize: 10}} domain={['dataMin', 'dataMax']} />
                <YAxis type="number" name="Response" tick={{fontSize: 10}} domain={['auto', 'auto']} />
                <Tooltip cursor={{ strokeDasharray: '3 3' }} />
                
                <Scatter name="Data Points" data={chartData} fill="#ff0000" shape="cross" dataKey="response" />
                
                {curve && lineData.length > 0 && (
                  <Line 
                    type="linear" 
                    dataKey="lineY" 
                    stroke="#0000ff" 
                    strokeWidth={1.5} 
                    dot={false} 
                    activeDot={false} 
                    isAnimationActive={false} 
                  />
                )}
              </ComposedChart>
            </ResponsiveContainer>
          </div>
        </div>
      </div>

      {/* Update Mode Modal */}
      {showUpdateMode && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Calibration Update Mode')}</span>
              <span className="cursor-pointer" onClick={() => setShowUpdateMode(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Select how new calibration runs update the existing curve.')}</p>
              
              <div className="border border-gray-400 p-3 bg-white flex flex-col gap-2">
                <label className="flex items-center gap-2"><input type="radio" name="upd_mode" /> {t('Replace')} <span className="text-xs text-gray-500">- {t('Discard old points for this level')}</span></label>
                <label className="flex items-center gap-2"><input type="radio" name="upd_mode" defaultChecked /> {t('Average')} <span className="text-xs text-gray-500">- {t('Average new points with existing')}</span></label>
                <label className="flex items-center gap-2"><input type="radio" name="upd_mode" /> {t('Bracket')} <span className="text-xs text-gray-500">- {t('Interpolate between pre/post sequence runs')}</span></label>
                <label className="flex items-center gap-2"><input type="radio" name="upd_mode" /> {t('None')} <span className="text-xs text-gray-500">- {t('Do not update calibration')}</span></label>
              </div>

              <label className="flex items-center gap-2 mt-2 font-bold"><input type="checkbox" defaultChecked /> {t('Clear all calibration points before next sequence')}</label>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowUpdateMode(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowUpdateMode(false)}>{t('Apply')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Advanced Settings Modal */}
      {showAdvancedSettings && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Advanced Fit Settings')}</span>
              <span className="cursor-pointer" onClick={() => setShowAdvancedSettings(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              
              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Curve Weighting')}:</label>
                <select className="border border-gray-400 p-1 w-48">
                  <option>None (Equal)</option>
                  <option>1 / x</option>
                  <option>1 / x²</option>
                  <option>1 / y</option>
                  <option>1 / y²</option>
                </select>
              </div>

              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Internal Standard Correction')}:</label>
                <select className="border border-gray-400 p-1 w-48">
                  <option>Disabled</option>
                  <option>Amount Ratio</option>
                  <option>Response Ratio</option>
                </select>
              </div>

              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Multiplier / Dilution Factor')}:</label>
                <input type="number" defaultValue="1.000" className="border border-gray-400 p-1 w-48 text-right" />
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowAdvancedSettings(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => {setShowAdvancedSettings(false); calculateCurve();}}>{t('Recalculate')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Status Bar */}
      <div className="h-6 bg-[#f0f0f0] border-t border-gray-400 flex items-center px-2 text-xs text-gray-800 gap-4 shadow-inner">
        <div className="flex items-center gap-1 w-48">
          <div className="w-3 h-3 rounded-full bg-[#0080ff] shadow-sm"></div>
          <span className="font-bold">CDS Workstation</span>
        </div>
        <div className="border-l border-gray-400 h-4"></div>
        <div className="flex-1">{t('Ready')}</div>
        {isCalculating && <div className="text-blue-600">{t('Calculating...')}</div>}
      </div>
    </div>
  );
}
