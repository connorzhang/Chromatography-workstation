import React, { useState, useEffect, useRef, memo } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, ResponsiveContainer } from 'recharts';

const TraceChart1 = memo(({ traceData }: { traceData: any[] }) => {
  const { t } = useTranslation();
  return (
  <ResponsiveContainer width="100%" height="100%">
    <LineChart data={traceData}>
      <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
      <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
      <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
      <Line type="monotone" dataKey="value" stroke="#000000" strokeWidth={1.5} dot={false} isAnimationActive={false} />
    </LineChart>
  </ResponsiveContainer>
  );
});

const TraceChart2 = memo(({ traceData }: { traceData: any[] }) => {
  const { t } = useTranslation();
  return (
  <ResponsiveContainer width="100%" height="100%">
    <LineChart data={traceData}>
      <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
      <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
      <YAxis yAxisId="temp" orientation="left" domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'red'}} tickLine={{stroke: 'red'}} />
      <YAxis yAxisId="press" orientation="right" domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'blue'}} tickLine={{stroke: 'blue'}} />
      <Line yAxisId="temp" type="stepAfter" dataKey="oven_temp" stroke="red" strokeWidth={1.5} dot={false} isAnimationActive={false} name={t('Oven Temp')} />
      <Line yAxisId="press" type="stepAfter" dataKey="pressure" stroke="blue" strokeWidth={1.5} dot={false} isAnimationActive={false} name={t('Pressure')} />
    </LineChart>
  </ResponsiveContainer>
  );
});

export default function MethodRun() {
  const { t } = useTranslation();
  const [isRunning, setIsRunning] = useState(false);
  const [status, setStatus] = useState("Offline");
  const [runTime, setRunTime] = useState("00:00:00.000");
  const [hardwareData, setHardwareData] = useState({
    pressure: 0,
    oven_temp: 0,
    inlet_temp: 0,
    tcd_block_temp: 0,
    aux_temp: 0,
    tcd_bridge_current: 'OFF',
    tcd_voltage: 0,
    tcd_resistance: 0,
    tcd_filament_temp: 0,
    tcd_polarity: 'Unknown',
    ms_vacuum: 'Offline',
    prep_valve: 'WASTE',
  });
  const [traceData, setTraceData] = useState<any[]>([]);
  const wsRef = useRef<WebSocket | null>(null);
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [activeModal, setActiveModal] = useState<string | null>(null);
  const [methodTab, setMethodTab] = useState<string>('Pump');

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

  const handleZeroing = async () => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/tcd/zeroing', { method: 'POST' });
    } catch (e) {
      console.error('Zeroing failed', e);
    }
  };

  const handleSetBridge = async (val: number) => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/tcd/set_bridge', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ val })
      });
    } catch (e) {
      console.error('Set bridge failed', e);
    }
  };

  const handleSetTemp = async (channel: number, target_temp: number) => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/modbus_temp/set', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ channel, target_temp })
      });
    } catch (e) {
      console.error('Set temp failed', e);
    }
  };

  const handleSetHeater = async (channel: number, state: boolean) => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/modbus_temp/set_io', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ channel, state })
      });
    } catch (e) {
      console.error('Set heater failed', e);
    }
  };

  const handleSetEventSwitch = async (eventIdx: number, state: boolean) => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/modbus_temp/set_io', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ channel: eventIdx + 4, state })
      });
    } catch (e) {
      console.error('Set event switch failed', e);
    }
  };

  const [valveProgram, setValveProgram] = useState<any[]>([]);

  useEffect(() => {
    const fetchValveProgram = async () => {
      try {
        const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
        const res = await fetch(baseUrl + '/api/v1/valve/program');
        const data = await res.json();
        if (Array.isArray(data)) {
          setValveProgram(data);
        }
      } catch (e) {
        console.error('Failed to fetch valve program', e);
      }
    };
    fetchValveProgram();
  }, []);

  const saveValveProgram = async (prog: any[]) => {
    try {
      const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
      await fetch(baseUrl + '/api/v1/valve/program', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(prog)
      });
      setValveProgram(prog);
    } catch (e) {
      console.error('Failed to save valve program', e);
    }
  };

  const toggleRun = async () => {
    if (isRunning) {
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
      setStatus("Running");
      
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
    }
  };

  useEffect(() => {
    let isMounted = true;
    let timeoutId: any;
    let startTime = Date.now();
    
    const pollData = async () => {
      try {
        const baseUrl = window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '';
        const [tcdRes, tempRes, epcRes, voltRes] = await Promise.all([
          fetch(baseUrl + '/api/v1/tcd/state'),
          fetch(baseUrl + '/api/v1/modbus_temp/state'),
          fetch(baseUrl + '/api/v1/epc/state'),
          fetch(baseUrl + '/api/v1/voltage/state')
        ]);
        
        const tcdData = await tcdRes.json();
        const tempData = await tempRes.json();
        const epcData = await epcRes.json();
        const voltData = await voltRes.json();

        if (!isMounted) return;

          let tcdVoltage = voltData.connected ? voltData.voltage : 0;
          tcdVoltage = Math.round(tcdVoltage * 1000) / 1000;
          let tcdBridgeCurrentNum = tcdData.connected ? tcdData.bridge_current : 0;
          let tcdResistance = 0;
          let tcdFilamentTemp = 0;

          if (tcdVoltage > 0 && tcdBridgeCurrentNum > 0) {
             tcdResistance = (tcdVoltage / tcdBridgeCurrentNum) * 1000;
             tcdResistance = Math.round(tcdResistance * 100) / 100;
             tcdFilamentTemp = 2.5458 * tcdResistance - 285.5878;
             tcdFilamentTemp = Math.round(tcdFilamentTemp * 10) / 10;
          }
          
          setHardwareData(prev => {
            const newData = {
              pressure: Math.round((epcData.real_pressure || 0) * 100) / 100,
              oven_temp: Math.round((tempData.temperatures ? tempData.temperatures[0] : 0) * 10) / 10,
              inlet_temp: Math.round((tempData.temperatures ? tempData.temperatures[1] : 0) * 10) / 10,
              tcd_block_temp: Math.round((tempData.temperatures ? tempData.temperatures[2] : 0) * 10) / 10,
              aux_temp: Math.round((tempData.temperatures ? tempData.temperatures[3] : 0) * 10) / 10,
              tcd_bridge_current: tcdData.connected ? (tcdData.bridge_current > 0 ? `${tcdData.bridge_current} mA` : 'OFF') : 'Offline',
              tcd_voltage: tcdVoltage,
              tcd_resistance: tcdResistance,
              tcd_filament_temp: tcdFilamentTemp,
              tcd_polarity: tcdData.connected ? 'Positive' : 'Unknown',
              ms_vacuum: 'Offline',
              prep_valve: 'WASTE',
            };
          
          let changed = false;
          for (const key in newData) {
            if ((prev as any)[key] !== (newData as any)[key]) {
              changed = true;
              break;
            }
          }
          
          if (changed) {
            return { ...prev, ...newData };
          }
          return prev;
        });

        if (isRunning) {
          const totalSeconds = (Date.now() - startTime) / 1000.0;
          const mm = Math.floor(totalSeconds / 60).toString().padStart(2, '0');
          const ss = Math.floor(totalSeconds % 60).toString().padStart(2, '0');
          setRunTime(`00:${mm}:${ss}.000`);
          
          if (tcdData.connected && tcdData.values && tcdData.values.length > 0) {
            setTraceData(prev => {
              const newTrace = [...prev];
              const val = tcdData.values[tcdData.values.length - 1];
              newTrace.push({ 
                time: (Date.now() - startTime) / 60000.0, // Convert ms to minutes
                value: val,
                oven_temp: tempData.temperatures ? tempData.temperatures[0] : 0,
                pressure: epcData.real_pressure || 0
              });
              if (newTrace.length > 50000000000) newTrace.shift(); // Extended to support 40+ minutes tests
              return newTrace;
            });
          }
        }
      } catch (e) {
        // Ignore polling errors
      } finally {
        if (isMounted) {
          timeoutId = setTimeout(pollData, 500); // 500ms = 2 times per second
        }
      }
    };

    pollData();

    return () => {
      isMounted = false;
      if (timeoutId) clearTimeout(timeoutId);
    };
  }, [isRunning]);

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
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => openModal('setupMethod')}>{t('Setup Instrument Method...')}</div>
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

            {/* Temperatures Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center gap-1">
              <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
              {t('Thermal Zones (4-Ch)')}
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Oven Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.oven_temp.toFixed(1)} °C</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Inlet Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.inlet_temp.toFixed(1)} °C</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('TCD Block Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.tcd_block_temp.toFixed(1)} °C</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('AUX Temp:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.aux_temp.toFixed(1)} °C</div>
            </div>

            {/* TCD Detector Section */}
            <div className="bg-[#f5f5f5] font-bold p-1 border-b border-gray-300 flex items-center justify-between">
              <div className="flex items-center gap-1">
                <span className="w-3 h-3 border border-gray-400 flex items-center justify-center bg-white text-[8px]">-</span>
                {t('TCD Signal 1')}
              </div>
              <button 
                onClick={handleZeroing}
                className="bg-white border border-gray-400 px-2 py-0.5 text-[10px] hover:bg-gray-100 active:bg-gray-200"
              >
                {t('Auto Zero')}
              </button>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Bridge Current:')}</div>
              <div className={`w-1/2 p-1 font-bold ${hardwareData.tcd_bridge_current.includes('mA') ? 'bg-[#00ff00] text-black' : ''}`}>{t(hardwareData.tcd_bridge_current)}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Voltage:')}</div>
              <div className="w-1/2 p-1 font-bold text-yellow-600">{hardwareData.tcd_voltage > 0 ? hardwareData.tcd_voltage.toFixed(4) + ' V' : '-- V'}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Resistance:')}</div>
              <div className="w-1/2 p-1 font-bold">{hardwareData.tcd_resistance > 0 ? hardwareData.tcd_resistance.toFixed(2) + ' Ω' : '-- Ω'}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Filament Temp:')}</div>
              <div className="w-1/2 p-1 font-bold text-red-600">{hardwareData.tcd_filament_temp > 0 ? hardwareData.tcd_filament_temp.toFixed(2) + ' °C' : '-- °C'}</div>
            </div>
            <div className="flex border-b border-gray-200">
              <div className="w-1/2 p-1 border-r border-gray-300 pl-6">{t('Polarity:')}</div>
              <div className="w-1/2 p-1 font-bold">{t(hardwareData.tcd_polarity)}</div>
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
              <TraceChart1 traceData={traceData} />
            </div>
          </div>

          {/* EPC / Temperature Chart */}
          <div className="flex-1 bg-white border border-gray-600 flex flex-col min-h-[200px]">
            <div className="flex-1 p-2 pb-6 relative">
              <div className="absolute left-2 top-4 text-xs font-bold rotate-90 origin-left translate-y-16">{t('Temperature (°C) / Pressure (bar)')}</div>
              <div className="absolute bottom-1 w-full text-center text-xs">{t('Time (min)')}</div>
              <TraceChart2 traceData={traceData} />
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
                  {['Pump', 'Injector', 'Thermal (4-Ch)', 'Detector (TCD)', 'Valves/Events'].map(tab => (
                    <li 
                      key={tab}
                      className={`px-2 py-1 cursor-pointer ${methodTab === tab ? 'bg-blue-200 font-bold' : 'hover:bg-gray-100'}`}
                      onClick={() => setMethodTab(tab)}
                    >
                      {t(tab)}
                    </li>
                  ))}
                </ul>
              </div>
              <div className="w-2/3 border border-gray-400 bg-white p-4 overflow-y-auto">
                {methodTab === 'Pump' && (
                  <>
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
                  </>
                )}
                {methodTab === 'Thermal (4-Ch)' && (
                  <>
                    <h3 className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Thermal Zones Setup')}</h3>
                    <div className="flex flex-col gap-4 text-sm mt-4">
                      {['Oven (Ch 1)', 'Inlet (Ch 2)', 'TCD Block (Ch 3)', 'AUX (Ch 4)'].map((zone, idx) => (
                        <div key={idx} className="flex items-center justify-between border-b border-gray-200 pb-2">
                          <span className="font-semibold w-1/3">{t(zone)}</span>
                          <div className="flex items-center gap-2">
                            <label>{t('Set Point:')}</label>
                            <input 
                              type="number" 
                              className="border border-gray-400 px-1 py-1 w-20 text-right"
                              placeholder="°C"
                              defaultValue="0"
                              onBlur={(e) => handleSetTemp(idx, parseInt(e.target.value) || 0)}
                            />
                            <span className="text-gray-500">°C</span>
                          </div>
                          <div className="flex items-center gap-2">
                            <input 
                              type="checkbox" 
                              className="w-4 h-4" 
                              defaultChecked 
                              onChange={(e) => handleSetHeater(idx, e.target.checked)}
                            />
                            <label>{t('Heater On')}</label>
                          </div>
                        </div>
                      ))}
                    </div>
                  </>
                )}
                {methodTab === 'Detector (TCD)' && (
                  <>
                    <h3 className="font-bold border-b border-gray-300 pb-1 mb-2">{t('TCD Settings')}</h3>
                    <div className="grid grid-cols-2 gap-4 text-sm mt-4">
                      <label className="flex items-center">{t('Bridge Current (mA):')}</label>
                      <div className="flex items-center gap-2">
                        <input 
                          type="number" 
                          className="border border-gray-400 px-1 py-1 w-20 text-right"
                          defaultValue={hardwareData.tcd_bridge_current.replace(' mA', '')}
                          onBlur={(e) => handleSetBridge(parseInt(e.target.value) || 0)}
                          min="0"
                          max="255"
                          step="1"
                        />
                        <span className="text-gray-500 text-xs">mA (0 = {t('Off')})</span>
                      </div>

                      <label className="flex items-center">{t('Polarity:')}</label>
                      <select className="border border-gray-400 px-1 py-1">
                        <option value="Positive">{t('Positive')}</option>
                        <option value="Negative">{t('Negative')}</option>
                      </select>
                      
                      <label className="flex items-center">{t('Auto Zero before run:')}</label>
                      <input type="checkbox" className="w-4 h-4" defaultChecked />
                    </div>
                  </>
                )}
                {methodTab === 'Valves/Events' && (
                  <>
                    <h3 className="font-bold border-b border-gray-300 pb-1 mb-2">{t('Valves / Events Control')}</h3>
                    
                    <div className="mb-4">
                      <h4 className="font-semibold text-gray-700 mb-2">{t('Immediate Control')}</h4>
                      <div className="grid grid-cols-2 gap-4">
                        {[1, 2, 3, 4].map(eventNum => (
                          <div key={eventNum} className="flex items-center justify-between border border-gray-300 p-2 bg-gray-50">
                            <span>{t(`Event ${eventNum}`)}</span>
                            <div className="flex items-center gap-2">
                              <button 
                                className="px-2 py-1 bg-white border border-gray-400 hover:bg-gray-100 text-xs"
                                onClick={() => handleSetEventSwitch(eventNum - 1, true)}
                              >{t('ON')}</button>
                              <button 
                                className="px-2 py-1 bg-white border border-gray-400 hover:bg-gray-100 text-xs"
                                onClick={() => handleSetEventSwitch(eventNum - 1, false)}
                              >{t('OFF')}</button>
                            </div>
                          </div>
                        ))}
                      </div>
                    </div>

                    <div>
                      <h4 className="font-semibold text-gray-700 mb-2">{t('Time Program')}</h4>
                      <table className="w-full text-xs text-left border-collapse border border-gray-300">
                        <thead className="bg-[#e0e0e0]">
                          <tr>
                            <th className="border border-gray-400 p-1">{t('Time (min)')}</th>
                            <th className="border border-gray-400 p-1">{t('Event')}</th>
                            <th className="border border-gray-400 p-1">{t('State')}</th>
                            <th className="border border-gray-400 p-1 w-10"></th>
                          </tr>
                        </thead>
                        <tbody>
                          {valveProgram.map((row, idx) => (
                            <tr key={idx}>
                              <td className="border border-gray-300 p-1">
                                <input 
                                  type="number" 
                                  className="w-full text-right border border-gray-300 px-1" 
                                  value={row.time} 
                                  onChange={(e) => {
                                    const newProg = [...valveProgram];
                                    newProg[idx].time = parseFloat(e.target.value) || 0;
                                    setValveProgram(newProg);
                                  }}
                                />
                              </td>
                              <td className="border border-gray-300 p-1">
                                <select 
                                  className="w-full border border-gray-300 px-1"
                                  value={row.event_id}
                                  onChange={(e) => {
                                    const newProg = [...valveProgram];
                                    newProg[idx].event_id = parseInt(e.target.value) || 1;
                                    setValveProgram(newProg);
                                  }}
                                >
                                  <option value={1}>{t('Event 1')}</option>
                                  <option value={2}>{t('Event 2')}</option>
                                  <option value={3}>{t('Event 3')}</option>
                                  <option value={4}>{t('Event 4')}</option>
                                </select>
                              </td>
                              <td className="border border-gray-300 p-1">
                                <select 
                                  className="w-full border border-gray-300 px-1"
                                  value={row.state ? 'ON' : 'OFF'}
                                  onChange={(e) => {
                                    const newProg = [...valveProgram];
                                    newProg[idx].state = e.target.value === 'ON';
                                    setValveProgram(newProg);
                                  }}
                                >
                                  <option value="ON">{t('ON')}</option>
                                  <option value="OFF">{t('OFF')}</option>
                                </select>
                              </td>
                              <td className="border border-gray-300 p-1 text-center">
                                <button 
                                  className="text-red-600 font-bold hover:bg-gray-200 px-1"
                                  onClick={() => {
                                    const newProg = valveProgram.filter((_, i) => i !== idx);
                                    setValveProgram(newProg);
                                  }}
                                >✕</button>
                              </td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                      <div className="flex justify-between mt-2">
                        <button 
                          className="text-xs px-2 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300"
                          onClick={() => {
                            setValveProgram([...valveProgram, { time: 0, event_id: 1, state: true }]);
                          }}
                        >+ {t('Add Event')}</button>
                        <button 
                          className="text-xs px-2 py-1 bg-blue-100 border border-blue-400 hover:bg-blue-200 text-blue-800"
                          onClick={() => saveValveProgram(valveProgram)}
                        >{t('Save Program')}</button>
                      </div>
                    </div>
                  </>
                )}
                {['Injector', 'Detector (DAD)'].includes(methodTab) && (
                  <div className="text-gray-500 italic text-sm mt-4">
                    {t('Settings for')} {t(methodTab)} {t('are not available in this demo.')}
                  </div>
                )}
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
              <div className="flex justify-between border-b border-gray-300 py-1"><span>{t('Column Comp.')} {t('Temp:')}</span><span className="font-bold">{hardwareData.oven_temp.toFixed(1)} °C</span></div>
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
