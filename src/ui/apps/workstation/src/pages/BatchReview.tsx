import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

interface PeakResult {
  num: number;
  rt_min: number;
  area: number;
  height: number;
  width: number;
  baseline_type: string;
  area_percent: number;
}

interface IntegrationReport {
  peaks: PeakResult[];
  total_area: number;
  total_height: number;
}

interface BatchItem {
  file: string;
  status: string;
  report?: IntegrationReport;
}

export default function BatchReview() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [batchItems, setBatchItems] = useState<BatchItem[]>([]);
  const [selectedIndex, setSelectedIndex] = useState<number>(0);
  const [showControlCharts, setShowControlCharts] = useState(false);
  const [showCustomFields, setShowCustomFields] = useState(false);
  const [showReprocess, setShowReprocess] = useState(false);
  const [showSeqSummary, setShowSeqSummary] = useState(false);

  const fetchBatch = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/data/files`);
      const files: string[] = await res.json();
      
      const items = files.map(f => ({ file: f, status: 'Pending' }));
      setBatchItems(items);
      
      if (items.length > 0) {
        fetchAnalysis(0, items[0].file);
      }
    } catch (err) {
      console.error(err);
    }
  };

  const fetchAnalysis = async (index: number, fileName: string) => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/analyze`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          file_name: fileName,
          events: {
            initial_area_reject: 100.0,
            initial_peak_width: 0.04,
            tangent_skim_mode: false
          }
        })
      });
      const data = await res.json();
      if (data.status === 'success') {
        setBatchItems(prev => {
          const newItems = [...prev];
          newItems[index].report = data.report;
          return newItems;
        });
      }
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchBatch();
  }, []);

  const handleSelect = (idx: number) => {
    setSelectedIndex(idx);
    if (!batchItems[idx].report) {
      fetchAnalysis(idx, batchItems[idx].file);
    }
  };

  const approveCurrent = () => {
    setBatchItems(prev => {
      const newItems = [...prev];
      newItems[selectedIndex].status = 'Reviewed';
      return newItems;
    });
    if (selectedIndex < batchItems.length - 1) {
      handleSelect(selectedIndex + 1);
    }
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'Control Charts') setShowControlCharts(true);
    else if (action === 'Custom Fields') setShowCustomFields(true);
    else if (action === 'Reprocess Sequence') setShowReprocess(true);
    else if (action === 'Sequence Summary') setShowSeqSummary(true);
    else alert(`${t('Feature in development')}: ${action}`);
  };

  const selectedItem = batchItems[selectedIndex];

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'batch' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => { e.stopPropagation(); setActiveMenu(activeMenu === 'batch' ? null : 'batch'); }}>{t('Batch(B)')}</div>
          {activeMenu === 'batch' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Load Batch')}>{t('Load Batch...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Save Batch')}>{t('Save Batch')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Start Review')}>{t('Start Review')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Reprocess Sequence')}>{t('Reprocess Sequence...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Generate Report')}>{t('Generate Report')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Sequence Summary')}>{t('Sequence Summary Report...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Control Charts')}>{t('Control Charts...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Custom Fields')}>{t('Custom Fields...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'view' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => { e.stopPropagation(); setActiveMenu(activeMenu === 'view' ? null : 'view'); }}>{t('View(V)')}</div>
          {activeMenu === 'view' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('Toggle Queue')}><span className="w-3">✓</span>{t('Batch Queue')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('Toggle Details')}><span className="w-3">✓</span>{t('Result Details')}</div>
            </div>
          )}
        </div>
      </div>

      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" title="Approve" onClick={approveCurrent}>✔️ {t('Approve Current')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" title={t('Reject')}>❌ {t('Reject Current')}</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs">{t('Generate Report (PDF)')}</button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        <div className="w-1/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex justify-between">
            <span>{t('Batch Queue')}</span>
            <span className="cursor-pointer hover:text-gray-300" onClick={fetchBatch}>🔄</span>
          </div>
          <div className="overflow-auto flex-1">
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
              <thead className="bg-[#e0e0e0] sticky top-0">
                <tr>
                  <th className="border border-gray-400 p-1 w-12 text-center">{t('Status')}</th>
                  <th className="border border-gray-400 p-1">{t('Data File')}</th>
                </tr>
              </thead>
              <tbody>
                {batchItems.map((row, idx) => (
                  <tr key={idx} onClick={() => handleSelect(idx)} className={`hover:bg-blue-50 cursor-pointer ${idx === selectedIndex ? 'bg-blue-200' : ''}`}>
                    <td className="border border-gray-300 p-1 text-center">
                      {row.status === 'Reviewed' ? '✔️' : '⏳'}
                    </td>
                    <td className="border border-gray-300 p-1 font-mono">{row.file}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        <div className="w-2/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Result Details')} - {selectedItem?.file || 'N/A'}</div>
          <div className="p-4 flex flex-col gap-4 overflow-auto">
            {selectedItem?.report ? (
              <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
                <thead className="bg-[#e0e0e0]">
                  <tr>
                    <th className="border border-gray-400 p-1 text-center">{t('Peak')}</th>
                    <th className="border border-gray-400 p-1 text-right">{t('RetTime')}</th>
                    <th className="border border-gray-400 p-1 text-right">{t('Area')}</th>
                    <th className="border border-gray-400 p-1 text-right">{t('Height')}</th>
                    <th className="border border-gray-400 p-1 text-right">{t('Area %')}</th>
                  </tr>
                </thead>
                <tbody>
                  {selectedItem.report.peaks.map(p => (
                    <tr key={p.num} className="hover:bg-blue-50">
                      <td className="border border-gray-300 p-1 text-center">{p.num}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{p.rt_min.toFixed(3)}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{p.area.toFixed(1)}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{p.height.toFixed(1)}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{p.area_percent.toFixed(2)}</td>
                    </tr>
                  ))}
                  <tr className="bg-gray-100 font-bold">
                    <td colSpan={2} className="border border-gray-300 p-1 text-right">Totals:</td>
                    <td className="border border-gray-300 p-1 text-right font-mono">{selectedItem.report.total_area.toFixed(1)}</td>
                    <td className="border border-gray-300 p-1 text-right font-mono">{selectedItem.report.total_height.toFixed(1)}</td>
                    <td className="border border-gray-300 p-1 text-right font-mono">100.00</td>
                  </tr>
                </tbody>
              </table>
            ) : (
              <div className="text-gray-500 italic">No integration results available.</div>
            )}
          </div>
        </div>
      </div>

      {/* Control Charts Modal */}
      {showControlCharts && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[700px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Control Charts (QC Trending)')}</span>
              <span className="cursor-pointer" onClick={() => setShowControlCharts(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex gap-4">
                <label className="flex items-center gap-2 font-bold">{t('Parameter to Trend')}:
                  <select className="border border-gray-400 p-1 w-48 font-normal">
                    <option>{t('Retention Time')}</option>
                    <option>{t('Peak Area')}</option>
                    <option>{t('Theoretical Plates')}</option>
                    <option>{t('Resolution')}</option>
                  </select>
                </label>
                <label className="flex items-center gap-2 font-bold">{t('Target Compound')}:
                  <select className="border border-gray-400 p-1 w-48 font-normal">
                    <option>Main API</option>
                    <option>Internal Standard</option>
                  </select>
                </label>
              </div>
              
              {/* Mock Levey-Jennings Chart */}
              <div className="border border-gray-400 h-64 bg-white relative mt-2 flex items-center justify-center overflow-hidden">
                <div className="absolute w-full h-px bg-red-500 top-1/4"></div>
                <div className="absolute w-full h-px bg-green-500 top-1/2"></div>
                <div className="absolute w-full h-px bg-red-500 top-3/4"></div>
                <div className="absolute left-2 top-[20%] text-red-500 text-xs">UCL (+3σ)</div>
                <div className="absolute left-2 top-[45%] text-green-600 text-xs">Mean</div>
                <div className="absolute left-2 top-[70%] text-red-500 text-xs">LCL (-3σ)</div>
                
                {/* Mock Data Points */}
                <svg className="absolute inset-0 w-full h-full">
                  <polyline points="50,160 150,150 250,120 350,130 450,170 550,125" fill="none" stroke="blue" strokeWidth="2" />
                  <circle cx="50" cy="160" r="4" fill="blue" />
                  <circle cx="150" cy="150" r="4" fill="blue" />
                  <circle cx="250" cy="120" r="4" fill="blue" />
                  <circle cx="350" cy="130" r="4" fill="blue" />
                  <circle cx="450" cy="170" r="4" fill="red" />
                  <circle cx="550" cy="125" r="4" fill="blue" />
                </svg>
                <div className="absolute right-4 bottom-4 bg-red-100 border border-red-400 text-red-700 px-2 py-1 text-xs">
                  {t('Warning: Sample #5 violates Westgard Rules (1_3s)')}
                </div>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowControlCharts(false)}>{t('Close')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowControlCharts(false)}>{t('Export Chart')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Custom Fields Modal */}
      {showCustomFields && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Custom Fields Configuration')}</span>
              <span className="cursor-pointer" onClick={() => setShowCustomFields(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Define custom parameters to be associated with sequences, samples, or compounds for LIMS integration.')}</p>
              
              <table className="w-full text-xs text-left border-collapse bg-white">
                <thead className="bg-[#e0e0e0]">
                  <tr>
                    <th className="border border-gray-400 p-1">{t('Field Name')}</th>
                    <th className="border border-gray-400 p-1">{t('Type')}</th>
                    <th className="border border-gray-400 p-1">{t('Mandatory')}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="border border-gray-300 p-1"><input type="text" defaultValue="LIMS_Sample_ID" className="w-full border-none outline-none" /></td>
                    <td className="border border-gray-300 p-1"><select className="w-full"><option>String</option><option>Number</option></select></td>
                    <td className="border border-gray-300 p-1 text-center"><input type="checkbox" defaultChecked /></td>
                  </tr>
                  <tr>
                    <td className="border border-gray-300 p-1"><input type="text" defaultValue="Batch_Number" className="w-full border-none outline-none" /></td>
                    <td className="border border-gray-300 p-1"><select className="w-full"><option>String</option><option>Number</option></select></td>
                    <td className="border border-gray-300 p-1 text-center"><input type="checkbox" defaultChecked /></td>
                  </tr>
                  <tr>
                    <td className="border border-gray-300 p-1"><input type="text" defaultValue="Expiration_Date" className="w-full border-none outline-none" /></td>
                    <td className="border border-gray-300 p-1"><select className="w-full"><option>Date</option></select></td>
                    <td className="border border-gray-300 p-1 text-center"><input type="checkbox" /></td>
                  </tr>
                </tbody>
              </table>

              <button className="self-start px-2 py-1 bg-gray-200 border border-gray-400 text-xs hover:bg-gray-300">+ {t('Add Field')}</button>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowCustomFields(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowCustomFields(false)}>{t('Save Fields')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Reprocess Sequence Modal */}
      {showReprocess && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Reprocess Sequence')}</span>
              <button onClick={() => setShowReprocess(false)} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Select processing parameters to apply to the entire batch.')}</p>
              
              <div className="border border-gray-400 bg-white p-3 flex flex-col gap-2">
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Re-integrate all signals using current events')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Recalibrate (update curves using standards in batch)')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Re-quantify unknown samples')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" /> {t('Generate individual reports for each run')}</label>
              </div>

              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Method to apply')}:</label>
                <select className="border border-gray-400 p-1 w-48">
                  <option>Current Open Method</option>
                  <option>Original Sequence Method</option>
                </select>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowReprocess(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => {setShowReprocess(false); fetchBatch();}}>{t('Start Reprocessing')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Sequence Summary Modal */}
      {showSeqSummary && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Sequence Summary Report')}</span>
              <button onClick={() => setShowSeqSummary(false)} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Generate statistical summary across multiple injections.')}</p>
              
              <div className="flex gap-4">
                <div className="w-1/2 border border-gray-400 bg-white p-2">
                  <div className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Select Runs to Include')}</div>
                  <label className="flex items-center gap-2"><input type="radio" name="runs_inc" defaultChecked /> {t('All runs in batch')}</label>
                  <label className="flex items-center gap-2"><input type="radio" name="runs_inc" /> {t('Calibration standards only')}</label>
                  <label className="flex items-center gap-2"><input type="radio" name="runs_inc" /> {t('Unknown samples only')}</label>
                </div>
                <div className="w-1/2 border border-gray-400 bg-white p-2">
                  <div className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Statistical Calculations')}</div>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Mean (Average)')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Standard Deviation (SD)')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Relative SD (%RSD)')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" /> {t('95% Confidence Interval')}</label>
                </div>
              </div>

              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Output Format')}:</label>
                <select className="border border-gray-400 p-1 w-48">
                  <option>PDF Document</option>
                  <option>Excel (XLSX)</option>
                  <option>CSV Data Export</option>
                </select>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSeqSummary(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSeqSummary(false)}>{t('Generate Summary')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

    </div>
  );
}