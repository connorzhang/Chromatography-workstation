import React, { useState, useEffect, useRef } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, ResponsiveContainer } from 'recharts';

export default function MethodRun() {
  const { t } = useTranslation();
  const [isRunning, setIsRunning] = useState(false);
  const [status, setStatus] = useState("Offline");
  const [runTime, setRunTime] = useState("00:00:00.000");
  const [hardwareData, setHardwareData] = useState({
    pressure: 0,
    temperature: 0,
    tcd_bridge_current: 'OFF',
    tcd_temp: 0,
    tcd_polarity: 'Unknown',
    fid_flame: 'OFF',
    fid_temp: 0,
    ms_vacuum: 'Offline',
    prep_valve: 'WASTE',
  });
  const [traceData, setTraceData] = useState<any[]>([]);
  const wsRef = useRef<WebSocket | null>(null);
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [activeModal, setActiveModal] = useState<string | null>(null);

  const [showRTL, setShowRTL] = useState(false);
  const [showMethodTranslator, setShowMethodTranslator] = useState(false);
  const [showFractionCollector, setShowFractionCollector] = useState(false);
  const [showMSControl, setShowMSControl] = useState(false);
  const [showInstrumentConfig, setShowInstrumentConfig] = useState(false);
  const [show2DLC, setShow2DLC] = useState(false);

  useEffect(() => {
    const handleClickOutside = () => setActiveMenu(null);
    document.addEventListener('click', handleClickOutside);
    return () => document.removeEventListener('click', handleClickOutside);
  }, []);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const openModal = (modalName: string) => {
    setActiveModal(modalName);
    setActiveMenu(null);
  };

  const closeModal = () => setActiveModal(null);

  const toggleRun = async () => {
    const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
    
    if (isRunning) {
      if (wsRef.current) wsRef.current.close();
      setIsRunning(false);
      setStatus("Offline");
      
      try {
        await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/stop`, { method: 'POST' });
      } catch (e) {
        console.error(e);
      }
    } else {
      setTraceData([]);
      setRunTime("00:00:00.000");
      setIsRunning(true);
      setStatus("Connecting...");
      
      try {
        await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/sequence/start`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            rows: [{
              line: 1,
              location: "1",
              sample_name: t("Single Run"),
              method_name: t("Default"),
              inj_vol: "1.0",
              inj_per_loc: 1,
              sample_type: "Sample",
              multiplier: 1.0,
              dilution: 1.0,
              data_file: "001-0101.D"
            }]
          })
        });
      } catch (e) {
        console.error(e);
      }
      
      const wsProtocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:';
      const wsHost = window.location.port === '5173' ? `${window.location.hostname}:8082` : window.location.host;
      const wsUrl = `${wsProtocol}//${wsHost}/ws/v1/realtime`;
      const ws = new WebSocket(wsUrl);
      wsRef.current = ws;

      ws.onmessage = (event) => {
        try {
          const data = JSON.parse(event.data);
          
          if (data.msg_type === "STATE") {
            setStatus(data.state === "RUNNING" ? "Connected" : data.state);
          } else if (data.msg_type === "ERROR") {
            setStatus("Error");
            setIsRunning(false);
            ws.close();
          } else if (data.msg_type === "DATA") {
            if (data.state === "IDLE") {
              setStatus("READY");
              setIsRunning(false);
              ws.close();
              return;
            }

            setStatus((prev) => {
              if (prev === "Connecting..." || prev === "Offline" || prev === "READY") {
                return "Running";
              }
              return prev;
            });
            
            setHardwareData({
              pressure: data.pressure,
              temperature: data.temperature,
              tcd_bridge_current: data.tcd_bridge_current || 'OFF',
              tcd_temp: data.tcd_temp || 0,
              tcd_polarity: data.tcd_polarity || 'Unknown',
              fid_flame: data.fid_flame || 'OFF',
              fid_temp: data.fid_temp || 0,
              ms_vacuum: data.ms_vacuum || 'Offline',
              prep_valve: data.prep_valve || 'WASTE',
            });
            
            // Calculate runtime
            const totalSeconds = data.time;
            const mm = Math.floor(totalSeconds / 60).toString().padStart(2, '0');
            const ss = Math.floor(totalSeconds % 60).toString().padStart(2, '0');
            const ms = Math.floor((totalSeconds % 1) * 1000).toString().padStart(3, '0');
            setRunTime(`00:${mm}:${ss}.${ms}`);
            
            setTraceData(prev => {
              // Mocking a second signal for FID based on TCD signal
              const newTrace = [...prev, { time: Number(data.time.toFixed(1)), value: data.signal, fid_value: data.signal * 0.6 + 20 }];
              if (newTrace.length > 200) newTrace.shift();
              return newTrace;
            });
          }
        } catch (e) {
          console.error("Parse error", e);
        }
      };

      ws.onclose = () => {
        setIsRunning(false);
        setStatus(prev => prev.includes("Error") ? prev : "Offline");
      };
    }
  };

  useEffect(() => {
    return () => {
      if (wsRef.current) wsRef.current.close();
    };
  }, []);

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'file' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'file')}>{t('File(F)')}</div>
          {activeMenu === 'file' && (
            <div className="absolute top-full left-0 mt-0 w-56 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => openModal('loadMethod')}><span>{t('Load Method...')}</span><span className="text-gray-500 hover:text-white">Ctrl+O</span></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex justify-between" onClick={() => openModal('saveMethod')}><span>{t('Save Method')}</span><span className="text-gray-500 hover:text-white">Ctrl+S</span></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('saveMethod')}>{t('Save Method As...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('loadSequence')}>{t('Load Sequence...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('saveSequence')}>{t('Save Sequence')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('print')}>{t('Print...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'view' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'view')}>{t('View(V)')}</div>
          {activeMenu === 'view' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => openModal('sysStatus')}><span className="w-3">✓</span>{t('System Status')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => openModal('onlineSignal')}><span className="w-3">✓</span>{t('Online Signal')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => openModal('methodInfo')}><span className="w-3">✓</span>{t('Method Information')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => openModal('systemDiagram')}><span className="w-3">✓</span>{t('System Diagram')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'method' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'method')}>{t('Method(M)')}</div>
          {activeMenu === 'method' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('methodParams')}>{t('Edit Entire Method...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('rtl')}>{t('Retention Time Lock...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('methodTranslator')}>{t('Method Translator...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'instrument' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'instrument')}>{t('Instrument(I)')}</div>
          {activeMenu === 'instrument' && (
            <div className="absolute top-full left-0 mt-0 w-64 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('instrumentConfig')}>{t('Instrument Configuration...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('instrumentMethod')}>{t('Setup Instrument Method...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('moreSettings')}>{t('More Injector Settings...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('2dlcControl')}>{t('2D-LC Heart-Cutting...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('fractionCollector')}>{t('Fraction Collector Control...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('msControl')}>{t('MS/MSD Control...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'plot' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'plot')}>{t('Plot(P)')}</div>
          {activeMenu === 'plot' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('autoScale')}>{t('Auto Scale')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('rescale')}>{t('Rescale')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('plotOptions')}>{t('Signal Options...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'tools' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'tools')}>{t('Tools(T)')}</div>
          {activeMenu === 'tools' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('options')}>{t('Options...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('customCalculator')}>{t('Custom Calculator...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'window' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'window')}>{t('Window(W)')}</div>
          {activeMenu === 'window' && (
            <div className="absolute top-full left-0 mt-0 w-40 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('tile')}>{t('Tile')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('cascade')}>{t('Cascade')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('arrangeIcons')}>{t('Arrange Icons')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'help' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'help')}>{t('Help(H)')}</div>
          {activeMenu === 'help' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('helpTopics')}>{t('Help Topics')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('about')}>{t('About CDS Workstation')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 items-center border-b border-gray-300 shadow-sm">
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Open" onClick={() => openModal('loadMethod')}>📂</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Save" onClick={() => openModal('saveMethod')}>💾</button>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded" title="Print" onClick={() => openModal('print')}>🖨️</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="p-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-green-700" onClick={toggleRun} title="Start/Stop">
          {isRunning ? '⏹️' : '▶️'}
        </button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <select className="border border-gray-400 rounded px-1 bg-white text-xs py-0.5">
          <option>{t('Disable')}</option>
          <option>{t('Enable')}</option>
        </select>
      </div>

      {/* Main Workspace */}
      <div className="flex flex-1 overflow-hidden">
        {/* Left Panel: Status/Property Grid */}
        <div className="w-[280px] flex flex-col bg-white border-r border-gray-300">
          {/* Tabs */}
          <div className="flex bg-[#f0f0f0] border-b border-gray-300 text-xs">
            <div className="px-3 py-1 bg-white border-r border-gray-300 border-t-2 border-t-blue-500 font-bold">{t('Status')}</div>
            <div className="px-3 py-1 border-r border-gray-300 hover:bg-gray-200 cursor-pointer">{t('Target')}</div>
            <div className="px-3 py-1 border-r border-gray-300 hover:bg-gray-200 cursor-pointer">{t('Data File')}</div>
          </div>

          {/* Grid Header */}
          <div className="flex bg-[#e0e0e0] border-b border-gray-400 font-bold text-xs">
            <div className="w-1/2 p-1 border-r border-gray-400 pl-2">{t('Property')}</div>
            <div className="w-1/2 p-1 pl-2">{t('Name')}</div>
          </div>

          {/* Grid Content (Tree structure) */}
          <div className="flex-1 overflow-y-auto text-xs">
            {/* Application Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('Application')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('State')}</div>
              <div className={`w-1/2 p-1 font-bold ${isRunning ? 'bg-[#00ff00] text-black' : 'bg-[#e0e0e0] text-black'}`}>
                {t(status)}
              </div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Computer Name')}</div>
              <div className="w-1/2 p-1">WIN-WORKSTATION</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('IP Address')}</div>
              <div className="w-1/2 p-1">127.0.0.1</div>
            </div>

            {/* Run Info */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('Run Info')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Run time')}</div>
              <div className="w-1/2 p-1 bg-black text-[#00ff00] font-mono font-bold tracking-wider">{runTime}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Host Computer')}</div>
              <div className="w-1/2 p-1">localhost</div>
            </div>

            {/* TCD Detector Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('TCD Signal 1')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Bridge Current:')}</div>
              <div className={`w-1/2 p-1 font-bold ${hardwareData.tcd_bridge_current.includes('ON') ? 'bg-[#00ff00] text-black' : ''}`}>{t(hardwareData.tcd_bridge_current)}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.tcd_temp.toFixed(1)} °C</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Polarity:')}</div>
              <div className="w-1/2 p-1 font-bold">{t(hardwareData.tcd_polarity)}</div>
            </div>

            {/* FID Detector Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('Detector (FID)')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6 flex items-center justify-between">
                <span>{t('Flame:')}</span>
                <button className="text-xs bg-[#e0e0e0] border border-gray-500 px-2 py-0.5 rounded shadow-sm hover:bg-gray-200 active:bg-gray-300">{t('Ignite')}</button>
              </div>
              <div className={`w-1/2 p-1 font-bold flex items-center ${hardwareData.fid_flame === 'ON' ? 'bg-[#00ff00] text-black' : 'text-gray-500'}`}>
                {hardwareData.fid_flame === 'ON' && <div className="w-2 h-2 rounded-full bg-orange-500 mr-1 animate-pulse"></div>}
                {t(hardwareData.fid_flame)}
              </div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.fid_temp.toFixed(1)} °C</div>
            </div>

            {/* MS & Prep-LC Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('ms_prep')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('ms_vacuum')}</div>
              <div className={`w-1/2 p-1 font-bold ${hardwareData.ms_vacuum !== 'Offline' ? 'bg-[#00ffff]' : ''}`}>{t(hardwareData.ms_vacuum)}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('prep_valve')}</div>
              <div className={`w-1/2 p-1 font-bold ${hardwareData.prep_valve === 'COLLECT' ? 'bg-[#00ff00] text-black' : 'text-gray-600'}`}>{t(hardwareData.prep_valve)}</div>
            </div>
          </div>
        </div>

        {/* Right Panel: Signal Charts */}
        <div className="flex-1 flex flex-col p-1 gap-1 bg-[#a0a0a0] overflow-y-auto">
          {/* TCD Chart */}
          <div className="flex-1 bg-white border border-gray-600 flex flex-col min-h-[200px]">
            <div className="flex-1 p-2 pb-6 relative">
              <div className="absolute left-2 top-4 text-xs font-bold rotate-90 origin-left translate-y-8">{t('Intensity (mAU)')}</div>
              <div className="absolute bottom-1 w-full text-center text-xs">{t('Time (min)')}</div>
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={traceData}>
                  <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
                  <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <Line type="monotone" dataKey="value" stroke="#000000" strokeWidth={1.5} dot={false} isAnimationActive={false} />
                </LineChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* FID Chart */}
          <div className="flex-1 bg-white border border-gray-600 flex flex-col min-h-[200px]">
            <div className="flex-1 p-2 pb-6 relative">
              <div className="absolute left-2 top-4 text-xs font-bold rotate-90 origin-left translate-y-8">{t('Intensity (mAU)')}</div>
              <div className="absolute bottom-1 w-full text-center text-xs">{t('Time (min)')}</div>
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={traceData}>
                  <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
                  <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                  <Line type="monotone" dataKey="fid_value" stroke="#000000" strokeWidth={1.5} dot={false} isAnimationActive={false} />
                </LineChart>
              </ResponsiveContainer>
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
        <div className="flex-1">{t('Press F1 for Help')}</div>
        <div className="border-l border-gray-400 h-4"></div>
        <div className="flex gap-2 w-32 justify-end pr-2">
          <span className={isRunning ? 'text-black font-bold' : 'text-gray-400'}>{t('CAP')}</span>
          <span className="text-black font-bold">{t('NUM')}</span>
          <span className="text-gray-400">{t('SCRL')}</span>
        </div>
      </div>

      {/* Modals */}
      {activeModal === 'loadMethod' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-96 flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Load Method')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-2">
              <label>{t('Select Method File:')}</label>
              <select className="border border-gray-400 p-1 bg-white" size={5}>
                <option>DEF_LC.M</option>
                <option>CLEANUP.M</option>
                <option>SHUTDOWN.M</option>
                <option>TEST01.M</option>
              </select>
              <div className="flex justify-end gap-2 mt-4">
                <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('OK')}</button>
                <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'saveMethod' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-96 flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Save Method')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-2">
              <label>{t('Method Name:')}</label>
              <input type="text" className="border border-gray-400 p-1 bg-white" defaultValue="DEF_LC.M" />
              <label className="mt-2">{t('Method Description:')}</label>
              <textarea className="border border-gray-400 p-1 bg-white h-20" defaultValue="Default Method for Liquid Chromatography"></textarea>
              <div className="flex justify-end gap-2 mt-4">
                <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('OK')}</button>
                <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'print' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-96 flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Print Setup')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-2">
              <label>{t('Printer:')}</label>
              <select className="border border-gray-400 p-1 bg-white">
                <option>Microsoft Print to PDF</option>
                <option>Send To OneNote 2016</option>
                <option>Fax</option>
              </select>
              <label className="mt-2">{t('Paper Size:')}</label>
              <select className="border border-gray-400 p-1 bg-white">
                <option>A4</option>
                <option>Letter</option>
              </select>
              <div className="flex justify-end gap-2 mt-4">
                <button onClick={() => { window.print(); closeModal(); }} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('Print...')}</button>
                <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#e0e0e0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'about' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[450px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('About CDS Workstation')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-6 flex gap-4">
              <div className="w-16 h-16 bg-blue-600 rounded-full flex items-center justify-center text-white text-2xl font-bold">CDS</div>
              <div className="flex flex-col gap-1">
                <h2 className="text-lg font-bold">Chromatography Data System</h2>
                <p>{t('Version 1.0 (Web Edition)')}</p>
                <p className="text-gray-600 mt-2">© 2026 Open Source CDS</p>
                <div className="mt-4 border-t border-gray-400 pt-2">
                  <p className="font-bold">{t('System Information')}</p>
                  <p>{t('Memory Usage:')} 128 MB</p>
                  <p>{t('CPU Usage:')} 2.4%</p>
                </div>
              </div>
            </div>
            <div className="p-4 bg-[#e0e0e0] flex justify-end border-t border-gray-400">
              <button onClick={closeModal} className="px-6 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('OK')}</button>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'setupMethod' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Setup Instrument Method')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="flex flex-1 p-2 gap-2 h-80">
              <div className="w-1/3 border border-gray-400 bg-white">
                <ul className="text-sm">
                  <li className="px-2 py-1 bg-blue-200 cursor-pointer">{t('Pump')}</li>
                  <li className="px-2 py-1 hover:bg-gray-100 cursor-pointer">{t('Injector')}</li>
                  <li className="px-2 py-1 hover:bg-gray-100 cursor-pointer">{t('Column Comp.')}</li>
                  <li className="px-2 py-1 hover:bg-gray-100 cursor-pointer">{t('Detector (DAD)')}</li>
                  <li className="px-2 py-1 hover:bg-gray-100 cursor-pointer">{t('Detector (TCD)')}</li>
                </ul>
              </div>
              <div className="w-2/3 border border-gray-400 bg-white p-4">
                <h3 className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Pump Settings')}</h3>
                <div className="grid grid-cols-2 gap-2 text-sm">
                  <label>{t('Flow:')}</label>
                  <input type="text" className="border border-gray-400 px-1" defaultValue="1.000" />
                  <label>{t('Stop Time (min):')}</label>
                  <input type="text" className="border border-gray-400 px-1" defaultValue="35.0" />
                  <label>{t('Solvent A (%):')}</label>
                  <input type="text" className="border border-gray-400 px-1" defaultValue="100.0" />
                  <label>{t('Solvent B (%):')}</label>
                  <input type="text" className="border border-gray-400 px-1" defaultValue="0.0" />
                </div>
              </div>
            </div>
            <div className="p-2 bg-[#e0e0e0] flex justify-end gap-2 border-t border-gray-400">
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('OK')}</button>
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('Apply')}</button>
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
            </div>
          </div>
        </div>
      )}

      {/* RTL Modal */}
      {activeModal === 'rtl' && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Retention Time Lock')}</span>
              <span className="cursor-pointer" onClick={closeModal}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold border-b border-gray-300 pb-1">{t('Lock Parameters')}</label>
              <div className="flex items-center justify-between"><label>{t('Target Compound')}:</label><input type="text" defaultValue="Toluene" className="border border-gray-400 p-1 w-48" /></div>
              <div className="flex items-center justify-between"><label>{t('Target RT (min)')}:</label><input type="number" defaultValue="5.200" className="border border-gray-400 p-1 w-48 text-right" /></div>
              <div className="flex items-center justify-between"><label>{t('Current Pressure (bar)')}:</label><input type="number" defaultValue="120.0" className="border border-gray-400 p-1 w-48 text-right bg-gray-200" disabled /></div>
              <button className="bg-gray-200 border border-gray-400 hover:bg-gray-300 px-2 py-1 mt-2 self-start">{t('Calculate New Pressure')}</button>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={closeModal}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={closeModal}>{t('Lock Method')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Method Translator Modal */}
      {activeModal === 'methodTranslator' && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Method Translator')}</span>
              <span className="cursor-pointer" onClick={closeModal}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex gap-4">
                <div className="flex-1 flex flex-col gap-2">
                  <label className="font-bold border-b border-gray-300 pb-1">{t('Original Method')}</label>
                  <label className="flex items-center justify-between"><span>{t('Column Length (mm)')}:</span><input type="number" defaultValue="250" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Inner Diameter (mm)')}:</span><input type="number" defaultValue="4.6" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Particle Size (µm)')}:</span><input type="number" defaultValue="5.0" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Flow Rate (mL/min)')}:</span><input type="number" defaultValue="1.0" className="border border-gray-400 p-1 w-20" /></label>
                </div>
                <div className="flex-1 flex flex-col gap-2">
                  <label className="font-bold border-b border-gray-300 pb-1">{t('Translated Method')}</label>
                  <label className="flex items-center justify-between"><span>{t('Column Length (mm)')}:</span><input type="number" defaultValue="50" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Inner Diameter (mm)')}:</span><input type="number" defaultValue="2.1" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Particle Size (µm)')}:</span><input type="number" defaultValue="1.8" className="border border-gray-400 p-1 w-20" /></label>
                  <label className="flex items-center justify-between"><span>{t('Flow Rate (mL/min)')}:</span><input type="number" defaultValue="0.4" className="border border-gray-400 p-1 w-20 bg-yellow-100" /></label>
                </div>
              </div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={closeModal}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={closeModal}>{t('Export to Method')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Fraction Collector Modal */}
      {activeModal === 'fractionCollector' && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Fraction Collector Control')}</span>
              <span className="cursor-pointer" onClick={closeModal}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold border-b border-gray-300 pb-1">{t('Collection Trigger')}</label>
              <div className="flex gap-4">
                <label className="flex items-center gap-2"><input type="radio" name="frac_trigger" defaultChecked /> {t('Time-based')}</label>
                <label className="flex items-center gap-2"><input type="radio" name="frac_trigger" /> {t('Peak-based (Threshold)')}</label>
              </div>
              <div className="flex items-center justify-between mt-2"><label>{t('Max Volume per Tube (mL)')}:</label><input type="number" defaultValue="5.0" className="border border-gray-400 p-1 w-24 text-right" /></div>
              <div className="flex items-center justify-between"><label>{t('Current Tube')}:</label><span className="font-bold text-lg">A1</span></div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={closeModal}>{t('Close')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={closeModal}>{t('Apply')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* MS/MSD Control Modal */}
      {activeModal === 'msControl' && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('MS/MSD Control')}</span>
              <span className="cursor-pointer" onClick={closeModal}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center justify-between"><label>{t('Scan Mode')}:</label>
                <select className="border border-gray-400 p-1 w-48">
                  <option>Scan (TIC)</option>
                  <option>SIM (Selected Ion)</option>
                  <option>MRM (Multiple Reaction)</option>
                </select>
              </div>
              <div className="flex items-center justify-between"><label>{t('Mass Range (m/z)')}:</label>
                <div className="flex gap-2 items-center">
                  <input type="number" defaultValue="50" className="border border-gray-400 p-1 w-16" /> - 
                  <input type="number" defaultValue="1000" className="border border-gray-400 p-1 w-16" />
                </div>
              </div>
              <div className="flex items-center justify-between"><label>{t('Capillary Voltage (V)')}:</label><input type="number" defaultValue="3500" className="border border-gray-400 p-1 w-24 text-right" /></div>
              <div className="flex items-center justify-between"><label>{t('Gas Temp (°C)')}:</label><input type="number" defaultValue="300" className="border border-gray-400 p-1 w-24 text-right" /></div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={closeModal}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={closeModal}>{t('Save MS Method')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'plotOptions' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Signal Options')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-3 text-sm">
              <div className="flex items-center gap-2">
                <input type="checkbox" id="autoScale" defaultChecked />
                <label htmlFor="autoScale">{t('Auto Scale Y-Axis')}</label>
              </div>
              <div className="grid grid-cols-2 gap-2 mt-2">
                <label>{t('Y-Axis Min:')}</label>
                <input type="text" className="border border-gray-400 px-1" defaultValue="-10.0" disabled />
                <label>{t('Y-Axis Max:')}</label>
                <input type="text" className="border border-gray-400 px-1" defaultValue="100.0" disabled />
                <label>{t('X-Axis Min (min):')}</label>
                <input type="text" className="border border-gray-400 px-1" defaultValue="0.0" />
                <label>{t('X-Axis Max (min):')}</label>
                <input type="text" className="border border-gray-400 px-1" defaultValue="35.0" />
              </div>
            </div>
            <div className="p-2 bg-[#e0e0e0] flex justify-end gap-2 border-t border-gray-400">
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('OK')}</button>
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'sysStatus' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50 pointer-events-none">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-80 flex flex-col pointer-events-auto absolute right-10 top-20">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('System Status')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-2 text-sm">
              <div className="flex justify-between border-b border-gray-300 py-1"><span>{t('State')}</span><span className="font-bold">{t(status)}</span></div>
              <div className="flex justify-between border-b border-gray-300 py-1"><span>{t('Pressure:')}</span><span className="font-bold text-blue-600">{hardwareData.pressure.toFixed(1)} bar</span></div>
              <div className="flex justify-between border-b border-gray-300 py-1"><span>{t('Flow:')}</span><span className="font-bold">1.000 mL/min</span></div>
              <div className="flex justify-between border-b border-gray-300 py-1"><span>{t('Column Comp.')} {t('Temp:')}</span><span className="font-bold">{hardwareData.temperature.toFixed(1)} °C</span></div>
            </div>
          </div>
        </div>
      )}

      {activeModal === 'onlineSignal' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50 pointer-events-none">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[500px] flex flex-col pointer-events-auto absolute right-10 bottom-20">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Online Signal')} Monitor</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-2 bg-black text-[#00ff00] font-mono flex flex-col items-center justify-center h-32">
               <div className="text-4xl font-bold">{traceData.length > 0 ? traceData[traceData.length - 1].value.toFixed(3) : "0.000"} mAU</div>
               <div className="text-sm mt-2">TCD Signal 1</div>
            </div>
          </div>
        </div>
      )}

      {/* Instrument Configuration Modal */}
      {activeModal === 'instrumentConfig' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('Instrument Configuration')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex gap-4">
              <div className="w-1/2 border border-gray-400 bg-white p-2 h-64 overflow-auto">
                <div className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Available Modules')}</div>
                <ul className="text-xs flex flex-col gap-1">
                  <li className="p-1 hover:bg-gray-200 cursor-pointer">G7120A High Speed Pump</li>
                  <li className="p-1 hover:bg-gray-200 cursor-pointer">G7129A Vial Sampler</li>
                  <li className="p-1 hover:bg-gray-200 cursor-pointer">G7116B MCT (Column Comp.)</li>
                  <li className="p-1 hover:bg-gray-200 cursor-pointer">G7117B DAD Detector</li>
                  <li className="p-1 hover:bg-gray-200 cursor-pointer">G6125B Single Quad MS</li>
                </ul>
              </div>
              <div className="w-1/2 border border-gray-400 bg-white p-2 h-64 flex flex-col">
                <div className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Configured LC Stack')}</div>
                <div className="flex-1 flex flex-col items-center justify-start gap-1 p-2 bg-gray-100 border border-gray-300">
                  <div className="w-full bg-blue-100 border border-blue-400 p-1 text-center text-xs font-bold">G7129A Autosampler</div>
                  <div className="w-1 h-2 bg-gray-400"></div>
                  <div className="w-full bg-blue-100 border border-blue-400 p-1 text-center text-xs font-bold">G7120A Quaternary Pump</div>
                  <div className="w-1 h-2 bg-gray-400"></div>
                  <div className="w-full bg-blue-100 border border-blue-400 p-1 text-center text-xs font-bold">G7116B Column Comp.</div>
                  <div className="w-1 h-2 bg-gray-400"></div>
                  <div className="w-full bg-blue-100 border border-blue-400 p-1 text-center text-xs font-bold">G7117B DAD</div>
                </div>
              </div>
            </div>
            <div className="p-2 bg-[#e0e0e0] flex justify-end gap-2 border-t border-gray-400">
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
              <button onClick={closeModal} className="px-4 py-1 border border-blue-800 bg-blue-600 text-white hover:bg-blue-700 shadow-sm">{t('Save Configuration')}</button>
            </div>
          </div>
        </div>
      )}

      {/* 2D-LC Heart-Cutting Modal */}
      {activeModal === '2dlcControl' && (
        <div className="fixed inset-0 bg-black/20 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-gray-400 shadow-xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white px-2 py-1 flex justify-between items-center font-bold">
              <span>{t('2D-LC Heart-Cutting Control')}</span>
              <button onClick={closeModal} className="hover:bg-red-600 px-2 font-bold">✕</button>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Configure valve switching events to transfer peaks from 1D to 2D column.')}</p>
              
              <div className="border border-gray-400 bg-white p-2">
                <table className="w-full text-xs text-left border-collapse">
                  <thead className="bg-[#e0e0e0]">
                    <tr>
                      <th className="border border-gray-400 p-1">{t('Time (min)')}</th>
                      <th className="border border-gray-400 p-1">{t('Valve Position')}</th>
                      <th className="border border-gray-400 p-1">{t('Loop Loop Vol (µL)')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td className="border border-gray-300 p-1"><input type="number" defaultValue="5.20" className="w-full text-right border-none outline-none" /></td>
                      <td className="border border-gray-300 p-1">
                        <select className="w-full border-none outline-none">
                          <option>Position 1 (1D -&gt; Waste)</option>
                          <option selected>Position 2 (1D -&gt; Loop)</option>
                        </select>
                      </td>
                      <td className="border border-gray-300 p-1"><input type="number" defaultValue="40" className="w-full text-right border-none outline-none" /></td>
                    </tr>
                    <tr>
                      <td className="border border-gray-300 p-1"><input type="number" defaultValue="5.40" className="w-full text-right border-none outline-none" /></td>
                      <td className="border border-gray-300 p-1">
                        <select className="w-full border-none outline-none">
                          <option selected>Position 1 (Loop -&gt; 2D)</option>
                          <option>Position 2 (1D -&gt; Loop)</option>
                        </select>
                      </td>
                      <td className="border border-gray-300 p-1"><input type="number" defaultValue="40" className="w-full text-right border-none outline-none" /></td>
                    </tr>
                  </tbody>
                </table>
                <button className="mt-2 text-xs px-2 py-1 bg-gray-200 border border-gray-400">+ {t('Add Event')}</button>
              </div>

              <div className="flex gap-4 text-xs mt-2">
                <label className="flex items-center gap-1"><input type="checkbox" defaultChecked /> {t('Sync with 2D Pump Gradient')}</label>
                <label className="flex items-center gap-1"><input type="checkbox" /> {t('High-Res Sampling')}</label>
              </div>
            </div>
            <div className="p-2 bg-[#e0e0e0] flex justify-end gap-2 border-t border-gray-400">
              <button onClick={closeModal} className="px-4 py-1 border border-gray-400 bg-[#f0f0f0] hover:bg-[#d0d0d0] shadow-sm">{t('Cancel')}</button>
              <button onClick={closeModal} className="px-4 py-1 border border-blue-800 bg-blue-600 text-white hover:bg-blue-700 shadow-sm">{t('Apply Method')}</button>
            </div>
          </div>
        </div>
      )}

    </div>
  );
}
