import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

interface SequenceRow {
  line: number;
  location: string;
  sample_name: string;
  method_name: string;
  inj_vol: string;
  inj_per_loc: number;
  sample_type: string;
  multiplier: number;
  dilution: number;
  data_file: string;
}

interface SequenceStatus {
  status: string;
  current_line: number;
  current_inj: number;
  message: string;
}

export default function SequenceTable() {
  const { t } = useTranslation();
  
  const [rows, setRows] = useState<SequenceRow[]>([
    { line: 1, location: 'Vial 1', sample_name: 'System Suitability', method_name: 'DEF_GC.M', inj_vol: '1.0', inj_per_loc: 1, sample_type: 'Calibration', multiplier: 1.0, dilution: 1.0, data_file: '001F0101.D' },
    { line: 2, location: 'Vial 2', sample_name: 'Blank', method_name: 'DEF_GC.M', inj_vol: '1.0', inj_per_loc: 1, sample_type: 'Blank', multiplier: 1.0, dilution: 1.0, data_file: '002F0201.D' },
    { line: 3, location: 'Vial 3', sample_name: 'Sample A', method_name: 'DEF_GC.M', inj_vol: '1.0', inj_per_loc: 3, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '003F0301.D' },
    { line: 4, location: 'Vial 4', sample_name: 'Sample B', method_name: 'DEF_GC.M', inj_vol: '1.0', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 10.0, data_file: '004F0401.D' },
    { line: 5, location: '', sample_name: '', method_name: '', inj_vol: '', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '' },
    { line: 6, location: '', sample_name: '', method_name: '', inj_vol: '', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '' },
    { line: 7, location: '', sample_name: '', method_name: '', inj_vol: '', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '' },
    { line: 8, location: '', sample_name: '', method_name: '', inj_vol: '', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '' },
  ]);

  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [seqStatus, setSeqStatus] = useState<SequenceStatus>({ status: 'IDLE', current_line: 0, current_inj: 0, message: '就绪 - 序列未运行' });

  // Modal states
  const [showSeqParams, setShowSeqParams] = useState(false);
  const [showSeqOutput, setShowSeqOutput] = useState(false);
  const [showCustomCalc, setShowCustomCalc] = useState(false);
  const [showSmartSequence, setShowSmartSequence] = useState(false);

  useEffect(() => {
    const interval = setInterval(async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/status`);
        const data = await res.json();
        setSeqStatus(data);
      } catch (e) {
        // Ignore errors for polling
      }
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  const loadSequence = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/load`);
      const data = await res.json();
      if (data && data.rows && data.rows.length > 0) {
        // Pad with empty rows to reach at least 8 rows
        const loadedRows = [...data.rows];
        while (loadedRows.length < 8) {
          loadedRows.push({ line: loadedRows.length + 1, location: '', sample_name: '', method_name: '', inj_vol: '', inj_per_loc: 1, sample_type: 'Sample', multiplier: 1.0, dilution: 1.0, data_file: '' });
        }
        setRows(loadedRows);
        alert(t('Sequence loaded successfully'));
      } else {
        alert(t('No saved sequence found'));
      }
    } catch (e) {
      console.error(e);
      alert(t('Failed to load sequence'));
    }
  };

  const saveSequence = async () => {
    const validRows = rows.filter(r => r.location && r.sample_name);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/save`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ rows: validRows })
      });
      alert(t('Sequence saved successfully'));
    } catch (e) {
      console.error(e);
      alert(t('Failed to save sequence'));
    }
  };

  const startSequence = async () => {
    // Filter out empty rows
    const validRows = rows.filter(r => r.location && r.sample_name);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/start`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ rows: validRows })
      });
    } catch (e) {
      console.error(e);
    }
  };

  const stopSequence = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/stop`, { method: 'POST' });
    } catch (e) {
      console.error(e);
    }
  };

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'Sequence Parameters') setShowSeqParams(true);
    else if (action === 'Sequence Output') setShowSeqOutput(true);
    else if (action === 'Custom Calculator') setShowCustomCalc(true);
    else if (action === 'Smart Sequence') setShowSmartSequence(true);
    else alert(`${t('Feature in development')}: ${action}`);
  };

  const handleCellChange = (index: number, field: keyof SequenceRow, value: string | number) => {
    const newRows = [...rows];
    newRows[index] = { ...newRows[index], [field]: value };
    setRows(newRows);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'seq' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'seq')}>{t('Sequence(S)')}</div>
          {activeMenu === 'seq' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Sequence Parameters')}>{t('Sequence Parameters...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Sequence Table')}>{t('Sequence Table...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => { handleMenuClick('Load Sequence'); loadSequence(); }}><span>{t('Load Sequence...')}</span></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => { handleMenuClick('Save Sequence'); saveSequence(); }}><span>{t('Save Sequence')}</span></div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Sequence Output')}>{t('Sequence Output...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print Sequence')}>{t('Print Sequence...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'view' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'view')}>{t('View(V)')}</div>
          {activeMenu === 'view' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('System Status')}><span className="w-3">✓</span>{t('System Status')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('Online Signal')}><span className="w-3">✓</span>{t('Online Signal')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'tools' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'tools')}>{t('Tools(T)')}</div>
          {activeMenu === 'tools' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Options')}>{t('Options...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Custom Calculator')}>{t('Custom Calculator...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Smart Sequence')}>{t('Smart Sequence...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'help' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'help')}>{t('Help(H)')}</div>
          {activeMenu === 'help' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer">{t('Help Topics')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer">{t('About CDS Workstation')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 items-center border-b border-gray-300 shadow-sm">
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title={t('Load Sequence...')} onClick={loadSequence}>📂</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title={t('Save Sequence')} onClick={saveSequence}>💾</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-blue-700 font-bold px-2" title={t('Run Sequence')} onClick={startSequence}>▶️ {t('Run Sequence')}</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-red-700 px-2" title={t('Stop')} onClick={stopSequence}>⏹️ {t('Stop')}</button>
      </div>

      {/* Main Workspace */}
      <div className="flex flex-1 overflow-hidden p-2 bg-[#a0a0a0]">
        <div className="flex-1 bg-white border border-gray-500 flex flex-col shadow-md overflow-hidden">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Sequence Table')} - {t('Editing')}</div>
          
          <div className="overflow-auto flex-1">
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
              <thead className="bg-[#e0e0e0] sticky top-0 z-10 shadow-sm">
                <tr>
                  <th className="border border-gray-400 p-1 w-10 text-center bg-gray-200">{t('Line')}</th>
                  <th className="border border-gray-400 p-1 min-w-[60px]">{t('Location')}</th>
                  <th className="border border-gray-400 p-1 min-w-[120px]">{t('Sample Name')}</th>
                  <th className="border border-gray-400 p-1 min-w-[120px]">{t('Method Name')}</th>
                  <th className="border border-gray-400 p-1 w-20">{t('Inj/Loc')}</th>
                  <th className="border border-gray-400 p-1 w-24">{t('Inj Vol (μL)')}</th>
                  <th className="border border-gray-400 p-1 min-w-[100px]">{t('Sample Type')}</th>
                  <th className="border border-gray-400 p-1 w-20">{t('Multiplier')}</th>
                  <th className="border border-gray-400 p-1 w-20">{t('Dilution')}</th>
                  <th className="border border-gray-400 p-1 min-w-[100px]">{t('Data File')}</th>
                </tr>
              </thead>
              <tbody>
                {rows.map((row, index) => (
                  <tr key={index} className="hover:bg-blue-50 focus-within:bg-blue-100">
                    <td className="border border-gray-300 p-0 text-center bg-gray-100 text-gray-600 font-mono">{row.line}</td>
                    <td className="border border-gray-300 p-0">
                      <input type="text" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.location} onChange={(e) => handleCellChange(index, 'location', e.target.value)} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="text" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.sample_name} onChange={(e) => handleCellChange(index, 'sample_name', e.target.value)} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <div className="flex h-full">
                        <input type="text" className="flex-1 p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.method_name} onChange={(e) => handleCellChange(index, 'method_name', e.target.value)} />
                        <button className="px-1 bg-gray-200 border-l border-gray-300 hover:bg-gray-300 text-[10px]">...</button>
                      </div>
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="number" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.inj_per_loc} onChange={(e) => handleCellChange(index, 'inj_per_loc', parseInt(e.target.value))} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="text" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.inj_vol} onChange={(e) => handleCellChange(index, 'inj_vol', e.target.value)} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <select className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.sample_type} onChange={(e) => handleCellChange(index, 'sample_type', e.target.value)}>
                        <option value="Sample">{t('Sample')}</option>
                        <option value="Calibration">Calibration</option>
                        <option value="Control">Control</option>
                        <option value="Blank">Blank</option>
                      </select>
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="number" step="0.1" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.multiplier} onChange={(e) => handleCellChange(index, 'multiplier', parseFloat(e.target.value))} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="number" step="0.1" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.dilution} onChange={(e) => handleCellChange(index, 'dilution', parseFloat(e.target.value))} />
                    </td>
                    <td className="border border-gray-300 p-0">
                      <input type="text" className="w-full h-full p-1 bg-transparent outline-none focus:ring-1 focus:ring-blue-500" value={row.data_file} onChange={(e) => handleCellChange(index, 'data_file', e.target.value)} />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>

      {/* Status Bar */}
      <div className={`h-6 border-t border-gray-400 flex items-center px-2 text-xs gap-4 shadow-inner ${seqStatus.status === 'RUNNING' ? 'bg-green-200 text-green-900' : 'bg-[#f0f0f0] text-gray-800'}`}>
        <div className="flex items-center gap-1 w-48">
          <div className={`w-3 h-3 rounded-full shadow-sm ${seqStatus.status === 'RUNNING' ? 'bg-green-500 animate-pulse' : 'bg-[#0080ff]'}`}></div>
          <span className="font-bold">CDS Workstation</span>
        </div>
        <div className="border-l border-gray-400 h-4"></div>
        <div className="flex-1 font-bold">{seqStatus.message}</div>
      </div>

      {/* Sequence Parameters Modal */}
      {showSeqParams && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Sequence Parameters')}</span>
              <span className="cursor-pointer" onClick={() => setShowSeqParams(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-32">{t('Operator Name')}:</label><input type="text" defaultValue="Admin" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Data Path')}:</label><input type="text" defaultValue="C:\CDS_Data\Project1" className="border border-gray-400 p-1 flex-1" /></div>
              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Part 11 Audit Trail enabled')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSeqParams(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSeqParams(false)}>{t('OK')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Sequence Output Modal */}
      {showSeqOutput && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[450px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Sequence Output')}</span>
              <span className="cursor-pointer" onClick={() => setShowSeqOutput(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Print sequence summary report')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" /> {t('Save individual sample reports')}</label>
              <div className="border-t border-gray-300 my-2"></div>
              <label className="font-bold">{t('Destination')}:</label>
              <label className="flex items-center gap-2"><input type="radio" name="dest" defaultChecked /> {t('Printer')}</label>
              <label className="flex items-center gap-2"><input type="radio" name="dest" /> {t('PDF File')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSeqOutput(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSeqOutput(false)}>{t('OK')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Custom Calculator Modal */}
      {showCustomCalc && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Custom Calculator')}</span>
              <span className="cursor-pointer" onClick={() => setShowCustomCalc(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-32">{t('Formula Name')}:</label><input type="text" defaultValue="EP Signal-to-Noise" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex flex-col mt-2">
                <label className="mb-1">{t('Expression')}:</label>
                <textarea className="border border-gray-400 p-2 h-24 font-mono text-sm" defaultValue="(2 * Peak.Height) / Noise.HalfPeakToPeak" />
              </div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowCustomCalc(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowCustomCalc(false)}>{t('Save Formula')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Smart Sequence Modal */}
      {showSmartSequence && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Smart Sequence Actions')}</span>
              <span className="cursor-pointer" onClick={() => setShowSmartSequence(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold border-b border-gray-300 pb-1">{t('On Hardware Error')}</label>
              <select className="border border-gray-400 p-1 w-full">
                <option>{t('Abort sequence and turn off lamps/pump')}</option>
                <option>{t('Pause sequence and wait for user')}</option>
                <option>{t('Skip current line and continue')}</option>
              </select>
              <label className="font-bold border-b border-gray-300 pb-1 mt-2">{t('Post-Sequence Actions')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Turn off pump')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Turn off detector lamps')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" /> {t('Load standby method')}:</label>
              <select className="border border-gray-400 p-1 w-full" disabled>
                <option>STANDBY.M</option>
              </select>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSmartSequence(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSmartSequence(false)}>{t('Apply')}</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}