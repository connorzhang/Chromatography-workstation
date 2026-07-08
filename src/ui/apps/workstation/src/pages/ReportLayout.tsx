import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, ResponsiveContainer } from 'recharts';

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

export default function ReportLayout() {
  const { t } = useTranslation();
  const [chartData, setChartData] = useState<{time: number, value: number}[]>([]);
  const [report, setReport] = useState<IntegrationReport | null>(null);
  const [fileName, setFileName] = useState<string>('');

  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  
  const fetchAnalysis = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      
      // Get first file
      const filesRes = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/data/files`);
      const files = await filesRes.json();
      if (files.length === 0) return;
      
      const targetFile = files[0];
      setFileName(targetFile);

      // Analyze
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/analyze`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          file_name: targetFile,
          events: {
            initial_area_reject: 100.0,
            initial_peak_width: 0.04,
            tangent_skim_mode: false
          }
        })
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
      console.error(err);
    }
  };

  useEffect(() => {
    fetchAnalysis();
  }, []);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'refresh') {
      fetchAnalysis();
    } else {
      alert(`${t('Feature in development')}: ${action}`);
    }
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'report' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'report')}>{t('Report(R)')}</div>
          {activeMenu === 'report' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Specify Report')}>{t('Specify Report...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print Report')}>{t('Print Report...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'edit' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'edit')}>{t('Edit(E)')}</div>
          {activeMenu === 'edit' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Add Element')}>{t('Add Element...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Remove Element')}>{t('Remove Element')}</div>
            </div>
          )}
        </div>
      </div>
      
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('refresh')}>{t('Refresh Data')}</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('Print')}>{t('Print (Print)')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('Export PDF')}>{t('Export PDF')}</button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        <div className="flex-1 bg-white border border-gray-500 shadow-md flex flex-col items-center overflow-auto py-8">
          {/* A4 Paper Mockup */}
          <div className="w-[210mm] min-h-[297mm] bg-white shadow-xl border border-gray-300 p-12 flex flex-col gap-6">
            <div className="border-b-2 border-black pb-4 flex justify-between items-end">
              <div>
                <h1 className="text-2xl font-bold font-serif mb-1">Analysis Report</h1>
                <div className="text-sm font-mono text-gray-700">Data File: {fileName || 'N/A'}</div>
              </div>
              <div className="text-right text-xs text-gray-600">
                CDS Workstation<br/>
                Printed: {new Date().toLocaleString()}
              </div>
            </div>
            
            {/* Chromatogram Graphic Element */}
            <div className="h-64 border border-gray-300 p-2 relative">
              <div className="absolute top-1 left-2 text-xs font-bold text-gray-700 z-10">FID1 A, Front Signal</div>
              <ResponsiveContainer width="100%" height="100%">
                <LineChart data={chartData}>
                  <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
                  <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(1)} tick={{fontSize: 10}} tickCount={10} />
                  <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} />
                  <Line type="monotone" dataKey="value" stroke="#0000ff" strokeWidth={1.5} dot={false} isAnimationActive={false} />
                </LineChart>
              </ResponsiveContainer>
            </div>
            
            {/* Integration Results Table Element */}
            <div>
              <h2 className="text-lg font-bold font-serif border-b border-gray-400 mb-2">{t('Integration Results')}</h2>
              <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
                <thead className="bg-[#f5f5f5]">
                  <tr>
                    <th className="border-b-2 border-black p-1 text-center">Peak #</th>
                    <th className="border-b-2 border-black p-1 text-right">{t('Ret Time')}</th>
                    <th className="border-b-2 border-black p-1 text-center">{t('Type')}</th>
                    <th className="border-b-2 border-black p-1 text-right">{t('Width')}</th>
                    <th className="border-b-2 border-black p-1 text-right">{t('Area')}</th>
                    <th className="border-b-2 border-black p-1 text-right">{t('Height')}</th>
                    <th className="border-b-2 border-black p-1 text-right">{t('Area %')}</th>
                  </tr>
                </thead>
                <tbody>
                  {report?.peaks.map(p => (
                    <tr key={p.num}>
                      <td className="border-b border-gray-200 p-1 text-center">{p.num}</td>
                      <td className="border-b border-gray-200 p-1 text-right font-mono">{p.rt_min.toFixed(3)}</td>
                      <td className="border-b border-gray-200 p-1 text-center">{p.baseline_type}</td>
                      <td className="border-b border-gray-200 p-1 text-right font-mono">{p.width.toFixed(3)}</td>
                      <td className="border-b border-gray-200 p-1 text-right font-mono">{p.area.toFixed(1)}</td>
                      <td className="border-b border-gray-200 p-1 text-right font-mono">{p.height.toFixed(1)}</td>
                      <td className="border-b border-gray-200 p-1 text-right font-mono">{p.area_percent.toFixed(2)}</td>
                    </tr>
                  ))}
                  <tr className="font-bold">
                    <td colSpan={4} className="border-t-2 border-black p-1 text-right">Totals:</td>
                    <td className="border-t-2 border-black p-1 text-right font-mono">{report?.total_area.toFixed(1)}</td>
                    <td className="border-t-2 border-black p-1 text-right font-mono">{report?.total_height.toFixed(1)}</td>
                    <td className="border-t-2 border-black p-1 text-right font-mono">100.00</td>
                  </tr>
                </tbody>
              </table>
            </div>
            
            {/* Signature Area */}
            <div className="mt-16 flex justify-end">
              <div className="w-64 border-t border-black pt-1 text-center text-sm font-serif">
                Analyst Signature
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}