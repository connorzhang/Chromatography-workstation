import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, ResponsiveContainer, ReferenceArea } from 'recharts';

interface PeakResult {
  num: number;
  rt_min: number;
  area: number;
  height: number;
  width: number;
  baseline_type: string;
  area_percent: number;
  theoretical_plates: number;
  resolution: number;
  tailing_factor: number;
}

interface TimeEvent {
  id: string;
  time: number;
  event_type: string;
  value: number;
}

interface IntegrationReport {
  peaks: PeakResult[];
  total_area: number;
  total_height: number;
}

interface TraceData {
  times: number[];
  values: number[];
}

export default function DataAnalysis() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [chartData, setChartData] = useState<{time: number, value: number}[]>([]);
  const [report, setReport] = useState<IntegrationReport | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [files, setFiles] = useState<string[]>([]);
  const [selectedFile, setSelectedFile] = useState<string | null>(null);

  // Zooming states
  const [refAreaLeft, setRefAreaLeft] = useState<number | null>(null);
  const [refAreaRight, setRefAreaRight] = useState<number | null>(null);
  const [xDomain, setXDomain] = useState<[number | string, number | string]>(['dataMin', 'dataMax']);

  // Manual integration states
  const [tangentSkimMode, setTangentSkimMode] = useState(false);
  const [dropBaselineMode, setDropBaselineMode] = useState(false);
  const [areaReject, setAreaReject] = useState<number>(10.0);
  const [timeEvents, setTimeEvents] = useState<TimeEvent[]>([]);

  // Tabs for result grids
  const [activeTab, setActiveTab] = useState<'integration' | 'sst'>('integration');

  // Modal states
  const [showLoadSignal, setShowLoadSignal] = useState(false);
  const [showSignalOptions, setShowSignalOptions] = useState(false);
  const [showIntegrationEvents, setShowIntegrationEvents] = useState(false);
  const [showIntegrationResults, setShowIntegrationResults] = useState(false);
  const [showCalibrationTable, setShowCalibrationTable] = useState(false);
  const [showReportSpecify, setShowReportSpecify] = useState(false);
  const [showSSTConfig, setShowSSTConfig] = useState(false);
  const [showGPCAnalysis, setShowGPCAnalysis] = useState(false);
  const [showReportDesigner, setShowReportDesigner] = useState(false);
  const [showMethodScouting, setShowMethodScouting] = useState(false);
  const [showImpurityProfile, setShowImpurityProfile] = useState(false);
  const [showSignalOverlay, setShowSignalOverlay] = useState(false);
  const [showPeakPurity, setShowPeakPurity] = useState(false);
  const [showLibrarySearch, setShowLibrarySearch] = useState(false);
  const [show3DPlot, setShow3DPlot] = useState(false);

  const fetchFiles = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/data/files`);
      const data = await res.json();
      setFiles(data);
      if (data.length > 0 && !selectedFile) {
        setSelectedFile(data[0]);
      }
    } catch (err) {
      console.error('Failed to fetch files:', err);
    }
  };

  const fetchAnalysis = async (fileName?: string) => {
    setIsLoading(true);
    try {
        const targetFile = fileName || selectedFile;
        const payload: any = {
          events: {
            initial_area_reject: areaReject,
            initial_peak_width: 0.04,
            tangent_skim_mode: tangentSkimMode,
            drop_baseline: dropBaselineMode,
            time_events: timeEvents
          }
        };
      if (targetFile) {
        payload.file_name = targetFile;
      }

      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/analyze`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload)
      });
      const data = await res.json();
      if (data.status === 'success') {
        setReport(data.report);
        const pts = data.trace.times.map((t: number, i: number) => ({
          time: t,
          value: data.trace.values[i]
        }));
        setChartData(pts);
      }
    } catch (err) {
      console.error('Failed to fetch analysis:', err);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchFiles();
  }, []);

  useEffect(() => {
    if (selectedFile) {
      fetchAnalysis(selectedFile);
    } else {
      fetchAnalysis();
    }
  }, [selectedFile, tangentSkimMode, dropBaselineMode]);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'integrate') fetchAnalysis();
    else if (action === 'Load Signal') setShowLoadSignal(true);
    else if (action === 'Signal Options') setShowSignalOptions(true);
    else if (action === 'Integration Events') setShowIntegrationEvents(true);
    else if (action === 'Integration Results') setShowIntegrationResults(true);
    else if (action === 'Calibration Table') setShowCalibrationTable(true);
    else if (action === 'Specify Report') setShowReportSpecify(true);
    else if (action === 'System Suitability') setShowSSTConfig(true);
    else if (action === 'GPC/SEC Analysis') setShowGPCAnalysis(true);
    else if (action === 'Intelligent Report Designer') setShowReportDesigner(true);
    else if (action === 'Method Scouting') setShowMethodScouting(true);
    else if (action === 'Impurity Profiling') setShowImpurityProfile(true);
    else if (action === 'Signal Overlay') setShowSignalOverlay(true);
    else if (action === 'Peak Purity') setShowPeakPurity(true);
    else if (action === 'Library Search') setShowLibrarySearch(true);
    else if (action === '3D Plot') setShow3DPlot(true);
    else if (action === 'Isoabsorbance Plot') setShow3DPlot(true); // combine 3D and Isoabsorbance
    else alert(`${t('Feature in development')}: ${action}`);
  };

  const zoom = () => {
    if (refAreaLeft === refAreaRight || refAreaLeft === null || refAreaRight === null) {
      setRefAreaLeft(null);
      setRefAreaRight(null);
      return;
    }

    let [left, right] = [refAreaLeft, refAreaRight];
    if (left > right) [left, right] = [right, left];

    setXDomain([left, right]);
    setRefAreaLeft(null);
    setRefAreaRight(null);
  };

  const zoomOut = () => {
    setXDomain(['dataMin', 'dataMax']);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'file' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'file')}>{t('File(F)')}</div>
          {activeMenu === 'file' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => handleMenuClick('Load Signal')}><span>{t('Load Signal...')}</span><span className="text-gray-500 hover:text-white">Ctrl+O</span></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => handleMenuClick('Save Method')}><span>{t('Save Method')}</span></div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print')}>{t('Print...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'graphics' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'graphics')}>{t('Graphics(G)')}</div>
          {activeMenu === 'graphics' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Signal Options')}>{t('Signal Options...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Change Scale')}>{t('Change Scale...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Signal Overlay')}>{t('Signal Overlay...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'spectra' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'spectra')}>{t('Spectra(S)')}</div>
          {activeMenu === 'spectra' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Peak Purity')}>{t('Peak Purity...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Library Search')}>{t('Library Search...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('3D Plot')}>{t('3D Plot...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Isoabsorbance Plot')}>{t('Isoabsorbance Plot...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'integration' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'integration')}>{t('Integration(I)')}</div>
          {activeMenu === 'integration' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('integrate')}>{t('Auto Integrate')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Integration Events')}>{t('Integration Events...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Integration Results')}>{t('Integration Results...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'calibration' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'calibration')}>{t('Calibration(C)')}</div>
          {activeMenu === 'calibration' && (
            <div className="absolute top-full left-0 mt-0 w-56 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Calibration Table')}>{t('Calibration Table...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('New Calibration Table')}>{t('New Calibration Table...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'report' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'report')}>{t('Report(R)')}</div>
          {activeMenu === 'report' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Specify Report')}>{t('Specify Report...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print Report')}>{t('Print Report...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Intelligent Report Designer')}>{t('Intelligent Report Designer...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('System Suitability')}>{t('System Suitability...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('GPC/SEC Analysis')}>{t('GPC/SEC Analysis...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Impurity Profiling')}>{t('Impurity Profiling...')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm overflow-x-auto">
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Open Data">📂</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Save Method">💾</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        {/* Integration Tools */}
        <div className="flex items-center gap-1 mx-1 text-xs">
          <span>{t('Area Reject')}:</span>
          <input 
            type="number" 
            className="w-16 px-1 py-0.5 border border-gray-400 rounded" 
            value={areaReject} 
            onChange={(e) => setAreaReject(Number(e.target.value))}
            step="10"
          />
        </div>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs flex items-center gap-1" title={t('Auto Integrate')} onClick={() => fetchAnalysis()}>
          ⚡ {isLoading ? t('Integrating...') : t('Re-integrate')}
        </button>
        <button className={`px-2 py-1 border rounded text-xs ${tangentSkimMode ? 'bg-blue-300 border-blue-500 shadow-inner' : 'hover:bg-gray-200 border-transparent hover:border-gray-400'}`} title={t('Tangent Skim')} onClick={() => setTangentSkimMode(!tangentSkimMode)}>↘️ {t('Tangent Skim')}</button>
        <button className={`px-2 py-1 border rounded text-xs ${dropBaselineMode ? 'bg-blue-300 border-blue-500 shadow-inner' : 'hover:bg-gray-200 border-transparent hover:border-gray-400'}`} title={t('Drop Baseline')} onClick={() => setDropBaselineMode(!dropBaselineMode)}>_ {t('Drop Baseline')}</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Zoom Out / Reset View" onClick={zoomOut}>🔄</button>
      </div>

      {/* Main Workspace */}
      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        
        {/* Navigation / File Tree */}
        <div className="w-64 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex justify-between">
            <span>{t('Navigation')}</span>
            <span className="cursor-pointer hover:text-gray-300" onClick={fetchFiles} title={t('Refresh Files')}>🔄</span>
          </div>
          <div className="p-2 text-xs border-b border-gray-300 bg-[#f0f0f0]">
            C:\Chem32\1\DATA\Sequence_001
          </div>
          <div className="flex-1 overflow-y-auto p-1 text-xs">
            {files.length === 0 ? (
              <div className="p-2 text-gray-500 italic">{t('No data files found. Run a sequence first.')}</div>
            ) : (
              files.map(f => (
                <div 
                  key={f}
                  onClick={() => setSelectedFile(f)}
                  className={`flex items-center gap-1 cursor-pointer p-1 ${selectedFile === f ? 'bg-blue-200 hover:bg-blue-300' : 'hover:bg-blue-100'}`}
                >
                  <span>📄</span> {f}
                </div>
              ))
            )}
          </div>
        </div>

        {/* Chart & Results Area */}
        <div className="flex-1 flex flex-col gap-2 min-w-0">
          {/* Chromatogram View */}
          <div className="flex-[2] bg-white border border-gray-500 shadow-md flex flex-col relative">
            <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex justify-between">
              <span>{t('Chromatogram')} - {selectedFile || 'Mock Data'}</span>
              <span>FID1 A, Front Signal</span>
            </div>
            <div className="flex-1 p-4 pb-6 relative">
              <div className="absolute left-2 top-10 text-xs font-bold rotate-90 origin-left translate-y-8">{t('Response')} (pA)</div>
              <div className="absolute bottom-1 w-full text-center text-xs">{t('Time')} (min)</div>
              <ResponsiveContainer width="100%" height="100%">
                <LineChart
                  data={chartData}
                  onMouseDown={(e) => e?.activeLabel && setRefAreaLeft(Number(e.activeLabel))}
                  onMouseMove={(e) => refAreaLeft && e?.activeLabel && setRefAreaRight(Number(e.activeLabel))}
                  onMouseUp={zoom}
                  onDoubleClick={zoomOut}
                >
                  <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
                  <XAxis dataKey="time" type="number" domain={xDomain} allowDataOverflow tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <Line type="monotone" dataKey="value" stroke="#0000ff" strokeWidth={1.5} dot={false} isAnimationActive={false} />
                  
                  {refAreaLeft && refAreaRight ? (
                    <ReferenceArea x1={refAreaLeft} x2={refAreaRight} strokeOpacity={0.3} fill="#8884d8" fillOpacity={0.3} />
                  ) : null}
                </LineChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* Integration Results / SST Table */}
          <div className="flex-1 bg-white border border-gray-500 shadow-md flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex gap-4">
              <span 
                className={`cursor-pointer ${activeTab === 'integration' ? 'underline' : 'text-gray-300 hover:text-white'}`}
                onClick={() => setActiveTab('integration')}
              >
                {t('Integration Results')}
              </span>
              <span 
                className={`cursor-pointer ${activeTab === 'sst' ? 'underline' : 'text-gray-300 hover:text-white'}`}
                onClick={() => setActiveTab('sst')}
              >
                {t('System Suitability')}
              </span>
            </div>
            <div className="overflow-auto flex-1">
              {activeTab === 'integration' ? (
                <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
                  <thead className="bg-[#e0e0e0] sticky top-0">
                    <tr>
                      <th className="border border-gray-400 p-1 text-center w-12">{t('Peak')} #</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('RetTime')}</th>
                      <th className="border border-gray-400 p-1 text-center w-16">{t('Type')}</th>
                      <th className="border border-gray-400 p-1 text-right w-20">{t('Width')}</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('Area')}</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('Height')}</th>
                      <th className="border border-gray-400 p-1 text-right w-20">{t('Area %')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {report?.peaks.map((peak, idx) => (
                      <tr key={idx} className="hover:bg-blue-50 cursor-pointer">
                        <td className="border border-gray-300 p-1 text-center">{peak.num}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.rt_min.toFixed(3)}</td>
                        <td className="border border-gray-300 p-1 text-center">{peak.baseline_type}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.width.toFixed(3)}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.area.toFixed(1)}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.height.toFixed(1)}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.area_percent.toFixed(2)}</td>
                      </tr>
                    ))}
                    <tr className="bg-[#f5f5f5] font-bold">
                      <td colSpan={4} className="border border-gray-300 p-1 text-right">Totals:</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{report?.total_area.toFixed(1)}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">{report?.total_height.toFixed(1)}</td>
                      <td className="border border-gray-300 p-1 text-right font-mono">100.00</td>
                    </tr>
                  </tbody>
                </table>
              ) : (
                <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
                  <thead className="bg-[#e0e0e0] sticky top-0">
                    <tr>
                      <th className="border border-gray-400 p-1 text-center w-12">{t('Peak')} #</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('RetTime')}</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('Plates (N)')}</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('Resolution (Rs)')}</th>
                      <th className="border border-gray-400 p-1 text-right w-24">{t('Tailing (Tf)')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    {report?.peaks.map((peak, idx) => (
                      <tr key={idx} className="hover:bg-blue-50 cursor-pointer">
                        <td className="border border-gray-300 p-1 text-center">{peak.num}</td>
                        <td className="border border-gray-300 p-1 text-right font-mono">{peak.rt_min.toFixed(3)}</td>
                        <td className={`border border-gray-300 p-1 text-right font-mono ${peak.theoretical_plates < 2000 ? 'text-red-600 font-bold' : ''}`}>
                          {peak.theoretical_plates.toLocaleString()}
                        </td>
                        <td className={`border border-gray-300 p-1 text-right font-mono ${peak.resolution > 0 && peak.resolution < 1.5 ? 'text-red-600 font-bold' : ''}`}>
                          {peak.resolution > 0 ? peak.resolution.toFixed(2) : '-'}
                        </td>
                        <td className={`border border-gray-300 p-1 text-right font-mono ${peak.tailing_factor > 1.5 ? 'text-orange-600 font-bold' : ''}`}>
                          {peak.tailing_factor.toFixed(2)}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              )}
            </div>
          </div>
        </div>

      </div>

      {/* Status Bar */}
      <div className="h-6 bg-[#f0f0f0] border-t border-gray-400 flex items-center px-2 text-xs text-gray-800 gap-4 shadow-inner">
        <div className="flex items-center gap-1 w-48">
          <div className="w-3 h-3 rounded-full bg-[#0080ff] shadow-sm"></div>
          <span className="font-bold">CDS Workstation</span>
        </div>
        <div className="border-l border-gray-400 h-4"></div>
        <div className="flex-1">{t('Data File')}: {selectedFile || 'Mock Data'} {t('Loaded')}</div>
      </div>

      {/* Load Signal Modal */}
      {showLoadSignal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Load Signal')}</span>
              <span className="cursor-pointer" onClick={() => setShowLoadSignal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold">{t('Select Data File')} (*.D):</label>
              <select className="border border-gray-400 p-2" size={5}>
                {files.map(f => <option key={f} value={f}>{f}</option>)}
                {files.length === 0 && <option disabled>{t('No data files found')}</option>}
              </select>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowLoadSignal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowLoadSignal(false)}>{t('Load')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Signal Options Modal */}
      {showSignalOptions && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Signal Options')}</span>
              <span className="cursor-pointer" onClick={() => setShowSignalOptions(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Show Baseline')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Show Peak Area/Height')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Show Retention Time')}</label>
              <div className="flex items-center justify-between"><label>{t('Line Color')}:</label><input type="color" defaultValue="#0000ff" /></div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSignalOptions(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSignalOptions(false)}>{t('Apply')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* SST Config Modal */}
      {showSSTConfig && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('System Suitability Configuration')}</span>
              <span className="cursor-pointer" onClick={() => setShowSSTConfig(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold border-b border-gray-300 pb-1">{t('Pharmacopeia Settings')}</label>
              <div className="flex gap-4">
                <label className="flex items-center gap-2"><input type="radio" name="sst_standard" defaultChecked /> USP (United States Pharmacopeia)</label>
                <label className="flex items-center gap-2"><input type="radio" name="sst_standard" /> EP (European Pharmacopoeia)</label>
                <label className="flex items-center gap-2"><input type="radio" name="sst_standard" /> JP (Japanese Pharmacopoeia)</label>
              </div>
              <div className="grid grid-cols-2 gap-4 mt-2">
                <div className="flex flex-col gap-2">
                  <label className="flex items-center justify-between"><span>{t('Calculate Resolution')}:</span><input type="checkbox" defaultChecked /></label>
                  <label className="flex items-center justify-between"><span>{t('Resolution Threshold')}:</span><input type="number" defaultValue={1.5} className="border border-gray-400 p-1 w-20 text-right" /></label>
                </div>
                <div className="flex flex-col gap-2">
                  <label className="flex items-center justify-between"><span>{t('Calculate Tailing Factor')}:</span><input type="checkbox" defaultChecked /></label>
                  <label className="flex items-center justify-between"><span>{t('Calculate Theoretical Plates')}:</span><input type="checkbox" defaultChecked /></label>
                </div>
              </div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSSTConfig(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => {setShowSSTConfig(false); fetchAnalysis();}}>{t('Apply & Recalculate')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* GPC Analysis Modal */}
      {showGPCAnalysis && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('GPC/SEC Data Analysis')}</span>
              <span className="cursor-pointer" onClick={() => setShowGPCAnalysis(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold border-b border-gray-300 pb-1">{t('Molecular Weight Distribution')}</label>
              <div className="flex items-center justify-between"><label>{t('Calibration File')}:</label><select className="border border-gray-400 p-1 w-48"><option>PS_Cal_2026.cal</option></select></div>
              <div className="flex items-center justify-between"><label>{t('Algorithm')}:</label><select className="border border-gray-400 p-1 w-48"><option>Conventional</option><option>Universal</option></select></div>
              <div className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Calculate Mn, Mw, Mz, PD')}</div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowGPCAnalysis(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowGPCAnalysis(false)}>{t('Process GPC Data')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Intelligent Report Designer Modal */}
      {showReportDesigner && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[800px] h-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Intelligent Report Designer')} (WYSIWYG)</span>
              <span className="cursor-pointer" onClick={() => setShowReportDesigner(false)}>✕</span>
            </div>
            <div className="flex flex-1 overflow-hidden">
              <div className="w-48 bg-white border-r border-gray-400 p-2 flex flex-col gap-2 overflow-y-auto">
                <div className="font-bold border-b border-gray-300 pb-1 text-sm">{t('Report Elements')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">📄 {t('Header / Logo')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">📊 {t('Chromatogram Plot')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">📋 {t('Integration Results Table')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">📈 {t('Calibration Curve')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">⚠️ {t('SST & Exceptions')}</div>
                <div className="border border-gray-300 bg-gray-100 p-2 text-xs cursor-move hover:bg-gray-200">✍️ {t('E-Signature Block')}</div>
              </div>
              <div className="flex-1 bg-gray-200 p-4 overflow-y-auto flex justify-center">
                <div className="w-[500px] min-h-[700px] bg-white shadow-md border border-gray-300 flex flex-col p-8 relative">
                  {/* Mock Canvas */}
                  <div className="border-2 border-dashed border-blue-300 h-24 mb-4 flex items-center justify-center text-blue-400 text-sm font-bold">
                    {t('Drop Header Here')}
                  </div>
                  <div className="border-2 border-dashed border-blue-300 h-64 mb-4 flex items-center justify-center text-blue-400 text-sm font-bold">
                    {t('Drop Chromatogram Here')}
                  </div>
                  <div className="border-2 border-dashed border-blue-300 h-48 mb-4 flex items-center justify-center text-blue-400 text-sm font-bold">
                    {t('Drop Results Table Here')}
                  </div>
                </div>
              </div>
            </div>
            <div className="p-2 bg-[#e0e0e0] flex justify-between border-t border-gray-400">
              <button className="px-4 py-1 bg-white border border-gray-400 hover:bg-gray-100">{t('Preview')}</button>
              <div className="flex gap-2">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowReportDesigner(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowReportDesigner(false)}>{t('Save Template')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Method Scouting Modal */}
      {showMethodScouting && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Automated Method Scouting')}</span>
              <span className="cursor-pointer" onClick={() => setShowMethodScouting(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Automatically screen combinations of columns, solvents, and gradients to find the optimal separation.')}</p>
              
              <div className="grid grid-cols-2 gap-4 mt-2">
                <div className="border border-gray-400 p-2 bg-white">
                  <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Columns (Valve Positions)')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> Pos 1: ZORBAX Eclipse Plus C18</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> Pos 2: Poroshell 120 EC-C8</label>
                  <label className="flex items-center gap-2"><input type="checkbox" /> Pos 3: Bonus-RP</label>
                </div>
                <div className="border border-gray-400 p-2 bg-white">
                  <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Solvents')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> A1: Water (0.1% FA)</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> B1: Acetonitrile</label>
                  <label className="flex items-center gap-2"><input type="checkbox" /> B2: Methanol</label>
                </div>
              </div>

              <div className="flex items-center justify-between mt-2">
                <label className="font-bold">{t('Optimization Goal')}:</label>
                <select className="border border-gray-400 p-1 w-64">
                  <option>{t('Maximize Resolution (Rs > 1.5)')}</option>
                  <option>{t('Minimize Run Time')}</option>
                  <option>{t('Equal Peak Spacing')}</option>
                </select>
              </div>

              <div className="bg-yellow-100 border border-yellow-400 p-2 text-xs mt-2">
                {t('Total permutations')}: 4. {t('Estimated time')}: 2.5 hours.
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowMethodScouting(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowMethodScouting(false)}>{t('Generate Scouting Sequence')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Impurity Profiling Modal */}
      {showImpurityProfile && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Impurity Profiling')}</span>
              <span className="cursor-pointer" onClick={() => setShowImpurityProfile(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center justify-between"><span>{t('Main API Peak')}:</span>
                <select className="border border-gray-400 p-1 w-48">
                  <option>Peak 1 (RT: 12.450)</option>
                  <option>Peak 2 (RT: 15.200)</option>
                </select>
              </label>
              
              <div className="border border-gray-400 p-2 bg-white mt-2">
                <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Reporting Thresholds (ICH Q3A/B)')}</label>
                <div className="flex items-center justify-between mb-1"><span>{t('Reporting Threshold (%)')}:</span><input type="number" defaultValue="0.05" className="border border-gray-400 p-1 w-20 text-right" /></div>
                <div className="flex items-center justify-between mb-1"><span>{t('Identification Threshold (%)')}:</span><input type="number" defaultValue="0.10" className="border border-gray-400 p-1 w-20 text-right" /></div>
                <div className="flex items-center justify-between"><span>{t('Qualification Threshold (%)')}:</span><input type="number" defaultValue="0.15" className="border border-gray-400 p-1 w-20 text-right" /></div>
              </div>

              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Apply Relative Response Factors (RRF)')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Perform DAD Spectral Deconvolution on co-eluting impurities')}</label>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowImpurityProfile(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowImpurityProfile(false)}>{t('Calculate Impurities')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Integration Events Modal */}
      {showIntegrationEvents && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Integration Events')}</span>
              <span className="cursor-pointer" onClick={() => setShowIntegrationEvents(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3 max-h-[500px] overflow-y-auto">
              <table className="w-full text-xs text-left border-collapse border border-gray-400 bg-white">
                <thead className="bg-[#e0e0e0]">
                  <tr>
                    <th className="border border-gray-400 p-1">{t('Event')}</th>
                    <th className="border border-gray-400 p-1">{t('Value')}</th>
                    <th className="border border-gray-400 p-1">{t('Time')}</th>
                    <th className="border border-gray-400 p-1 w-12 text-center">{t('Action')}</th>
                  </tr>
                </thead>
                <tbody>
                  {/* Global/Initial Events */}
                  <tr className="bg-gray-50">
                    <td className="border border-gray-300 p-1">{t('Initial Area Reject')}</td>
                    <td className="border border-gray-300 p-1">
                      <input type="number" className="w-full border p-1" value={areaReject} onChange={(e) => setAreaReject(Number(e.target.value))} />
                    </td>
                    <td className="border border-gray-300 p-1">Initial</td>
                    <td className="border border-gray-300 p-1 text-center">-</td>
                  </tr>
                  <tr className="bg-gray-50">
                    <td className="border border-gray-300 p-1">{t('Tangent Skim Mode')}</td>
                    <td className="border border-gray-300 p-1">
                      <select className="w-full border p-1" value={tangentSkimMode ? 'On' : 'Off'} onChange={(e) => setTangentSkimMode(e.target.value === 'On')}>
                        <option>On</option><option>Off</option>
                      </select>
                    </td>
                    <td className="border border-gray-300 p-1">0.000</td>
                    <td className="border border-gray-300 p-1 text-center">-</td>
                  </tr>
                  <tr className="bg-gray-50">
                    <td className="border border-gray-300 p-1">{t('Drop Baseline')}</td>
                    <td className="border border-gray-300 p-1">
                      <select className="w-full border p-1" value={dropBaselineMode ? 'On' : 'Off'} onChange={(e) => setDropBaselineMode(e.target.value === 'On')}>
                        <option>On</option><option>Off</option>
                      </select>
                    </td>
                    <td className="border border-gray-300 p-1">0.000</td>
                    <td className="border border-gray-300 p-1 text-center">-</td>
                  </tr>

                  {/* Time Events */}
                  {timeEvents.map((evt, idx) => (
                    <tr key={evt.id}>
                      <td className="border border-gray-300 p-1">
                        <select className="w-full border p-1" value={evt.event_type} onChange={(e) => {
                          const newEvents = [...timeEvents];
                          newEvents[idx].event_type = e.target.value;
                          setTimeEvents(newEvents);
                        }}>
                          <option value="Integration">Integration</option>
                          <option value="Tangent Skim">Tangent Skim</option>
                          <option value="Drop Baseline">Drop Baseline</option>
                        </select>
                      </td>
                      <td className="border border-gray-300 p-1">
                        <input type="number" className="w-full border p-1" value={evt.value} onChange={(e) => {
                          const newEvents = [...timeEvents];
                          newEvents[idx].value = Number(e.target.value);
                          setTimeEvents(newEvents);
                        }} />
                      </td>
                      <td className="border border-gray-300 p-1">
                        <input type="number" step="0.1" className="w-full border p-1" value={evt.time} onChange={(e) => {
                          const newEvents = [...timeEvents];
                          newEvents[idx].time = Number(e.target.value);
                          setTimeEvents(newEvents);
                        }} />
                      </td>
                      <td className="border border-gray-300 p-1 text-center">
                        <button className="text-red-600 font-bold hover:text-red-800" onClick={() => {
                          setTimeEvents(timeEvents.filter(e => e.id !== evt.id));
                        }}>✕</button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
              <div className="flex justify-start">
                <button className="px-3 py-1 bg-green-600 text-white border border-green-800 hover:bg-green-700 text-xs" onClick={() => {
                  setTimeEvents([...timeEvents, { id: Math.random().toString(36).substr(2, 9), event_type: 'Integration', value: 0, time: 0.0 }]);
                }}>+ {t('Add Time Event')}</button>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => {
                  setShowIntegrationEvents(false);
                  fetchAnalysis();
                }}>{t('Apply & Re-integrate')}</button>
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowIntegrationEvents(false)}>{t('Close')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Signal Overlay Modal */}
      {showSignalOverlay && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Signal Overlay & Alignment')}</span>
              <span className="cursor-pointer" onClick={() => setShowSignalOverlay(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Overlay multiple detector signals (e.g. UV, MS, ELSD) with independent scaling and time alignment.')}</p>
              
              <table className="w-full text-xs text-left border-collapse bg-white">
                <thead className="bg-[#e0e0e0]">
                  <tr>
                    <th className="border border-gray-400 p-1 w-8 text-center">{t('Show')}</th>
                    <th className="border border-gray-400 p-1">{t('Signal')}</th>
                    <th className="border border-gray-400 p-1">{t('Color')}</th>
                    <th className="border border-gray-400 p-1">{t('Time Offset (min)')}</th>
                    <th className="border border-gray-400 p-1">{t('Scale Multiplier')}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="border border-gray-300 p-1 text-center"><input type="checkbox" defaultChecked /></td>
                    <td className="border border-gray-300 p-1">DAD1 A, Sig=254,4 Ref=360,100</td>
                    <td className="border border-gray-300 p-1"><div className="w-6 h-4 bg-blue-600 border border-gray-400 mx-auto"></div></td>
                    <td className="border border-gray-300 p-1"><input type="number" defaultValue="0.000" className="w-full text-right border-none outline-none" /></td>
                    <td className="border border-gray-300 p-1"><input type="number" defaultValue="1.0" className="w-full text-right border-none outline-none" /></td>
                  </tr>
                  <tr>
                    <td className="border border-gray-300 p-1 text-center"><input type="checkbox" defaultChecked /></td>
                    <td className="border border-gray-300 p-1">MSD1 TIC, Scan (ES+)</td>
                    <td className="border border-gray-300 p-1"><div className="w-6 h-4 bg-red-600 border border-gray-400 mx-auto"></div></td>
                    <td className="border border-gray-300 p-1"><input type="number" defaultValue="0.045" className="w-full text-right border-none outline-none bg-yellow-100" /></td>
                    <td className="border border-gray-300 p-1"><input type="number" defaultValue="0.05" className="w-full text-right border-none outline-none bg-yellow-100" /></td>
                  </tr>
                </tbody>
              </table>

              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Align X-Axis based on Time Offset')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" /> {t('Normalize all signals to 100% full scale')}</label>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSignalOverlay(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSignalOverlay(false)}>{t('Apply Overlay')}</button>
              </div>
            </div>
          </div>
        </div>
      )}
      {/* Peak Purity Modal */}
      {showPeakPurity && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Peak Purity Analysis')} (DAD)</span>
              <span className="cursor-pointer" onClick={() => setShowPeakPurity(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Assess peak homogeneity by comparing spectra across the peak using DAD data.')}</p>
              
              <div className="grid grid-cols-2 gap-4 mt-2">
                <div className="border border-gray-400 p-2 bg-white">
                  <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Purity Parameters')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Calculate Purity Factor')}</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Threshold Limit')}: <input type="number" defaultValue={990} className="w-16 border p-1" /></label>
                  <label className="flex items-center gap-2 mt-2">{t('Reference Spectrum')}:</label>
                  <select className="border border-gray-400 p-1 w-full text-xs">
                    <option>{t('Peak Apex')}</option>
                    <option>{t('Peak Start')}</option>
                    <option>{t('User Defined')}</option>
                  </select>
                </div>
                <div className="border border-gray-400 p-2 bg-white flex flex-col gap-2">
                  <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Wavelength Range')}</label>
                  <label className="flex items-center justify-between"><span>{t('Start')} (nm):</span> <input type="number" defaultValue={210} className="w-16 border p-1" /></label>
                  <label className="flex items-center justify-between"><span>{t('End')} (nm):</span> <input type="number" defaultValue={400} className="w-16 border p-1" /></label>
                  <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Noise Threshold Correction')}</label>
                </div>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowPeakPurity(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowPeakPurity(false)}>{t('Calculate Purity')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Library Search Modal */}
      {showLibrarySearch && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Spectral Library Search')}</span>
              <span className="cursor-pointer" onClick={() => setShowLibrarySearch(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Identify unknown peaks by searching DAD or MS spectra against local/cloud libraries.')}</p>
              
              <div className="border border-gray-400 p-2 bg-white mt-2">
                <label className="font-bold border-b border-gray-300 pb-1 block mb-2">{t('Target Libraries')}</label>
                <div className="flex flex-col gap-1 max-h-24 overflow-auto text-xs">
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> Default_UV_Library.lib</label>
                  <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> NIST_2020_MS.lib</label>
                  <label className="flex items-center gap-2"><input type="checkbox" /> Toxicological_Screening.lib</label>
                  <label className="flex items-center gap-2"><input type="checkbox" /> Custom_Pharma_Lib.lib</label>
                </div>
                <button className="mt-2 px-2 py-1 bg-gray-200 border border-gray-400 text-xs">{t('Add Library...')}</button>
              </div>

              <div className="flex items-center gap-4 mt-2 text-xs">
                <label className="flex items-center gap-1">{t('Search Threshold')}: <input type="number" defaultValue={850} className="w-16 border p-1" /></label>
                <label className="flex items-center gap-1">{t('Max Hits')}: <input type="number" defaultValue={5} className="w-12 border p-1" /></label>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowLibrarySearch(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowLibrarySearch(false)}>{t('Search Selected Peak')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* 3D Plot Modal */}
      {show3DPlot && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[700px] h-[550px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('3D Spectral Plot & Isoabsorbance')}</span>
              <span className="cursor-pointer" onClick={() => setShow3DPlot(false)}>✕</span>
            </div>
            <div className="flex-1 flex flex-col p-2 gap-2">
              <div className="flex gap-2 bg-gray-200 p-2 border border-gray-400 text-xs items-center">
                <label className="flex items-center gap-1">{t('View')}: 
                  <select className="border p-1">
                    <option>{t('3D Surface Plot')}</option>
                    <option>{t('Isoabsorbance (Contour)')}</option>
                  </select>
                </label>
                <div className="w-px h-4 bg-gray-400 mx-1"></div>
                <label className="flex items-center gap-1">{t('Time')}: <input type="text" defaultValue="0.0 - 20.0" className="w-20 border p-1" /> min</label>
                <label className="flex items-center gap-1">{t('WL')}: <input type="text" defaultValue="200 - 400" className="w-20 border p-1" /> nm</label>
                <button className="px-2 py-1 bg-white border border-gray-400 hover:bg-gray-100 ml-auto">{t('Extract Spectrum')}</button>
                <button className="px-2 py-1 bg-white border border-gray-400 hover:bg-gray-100">{t('Extract Chromatogram')}</button>
              </div>
              
              <div className="flex-1 border border-gray-500 bg-white relative flex items-center justify-center overflow-hidden">
                {/* Simulated 3D / Contour Plot using CSS gradients */}
                <div className="absolute top-2 left-2 text-xs font-bold text-gray-600">{t('Wavelength')} (nm)</div>
                <div className="absolute bottom-2 right-2 text-xs font-bold text-gray-600">{t('Time')} (min)</div>
                <div 
                  className="w-4/5 h-4/5"
                  style={{
                    background: 'radial-gradient(circle at 40% 60%, rgba(255,0,0,0.8) 0%, rgba(255,255,0,0.6) 20%, rgba(0,255,0,0.4) 40%, rgba(0,0,255,0.2) 60%, transparent 80%)',
                    transform: 'rotateX(60deg) rotateZ(-45deg)',
                    boxShadow: '0 20px 50px rgba(0,0,0,0.3)',
                    border: '1px solid #ccc'
                  }}
                ></div>
                {/* Mock axes */}
                <div className="absolute left-10 bottom-10 w-4/5 h-0.5 bg-black origin-left" style={{transform: 'rotate(-25deg)'}}></div>
                <div className="absolute left-10 bottom-10 w-0.5 h-4/5 bg-black"></div>
              </div>
            </div>
          </div>
        </div>
      )}

    </div>
  );
}