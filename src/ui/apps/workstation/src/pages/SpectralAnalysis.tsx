import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, ResponsiveContainer } from 'recharts';

export default function SpectralAnalysis() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [spectralData, setSpectralData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);

  // Modal states
  const [showPurityOptions, setShowPurityOptions] = useState(false);
  const [showLibrarySearch, setShowLibrarySearch] = useState(false);
  const [showLibraryEdit, setShowLibraryEdit] = useState(false);
  const [show3DViewer, setShow3DViewer] = useState(false);

  const fetchSpectrum = async () => {
    setLoading(true);
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/dad/spectrum`);
      const data = await res.json();
      setSpectralData(data);
    } catch (e) {
      console.error('Failed to fetch DAD spectrum:', e);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSpectrum();
  }, []);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'extract') fetchSpectrum();
    else if (action === 'Purity Options') setShowPurityOptions(true);
    else if (action === 'Library Search') setShowLibrarySearch(true);
    else if (action === 'Edit Library') setShowLibraryEdit(true);
    else if (action === '3D Viewer') setShow3DViewer(true);
    else alert(`${t('Feature in development')}: ${action}`);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'spectrum' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'spectrum')}>{t('Spectrum(S)')}</div>
          {activeMenu === 'spectrum' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('extract')}>{t('Extract Spectrum')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Clear Background')}>{t('Clear Background')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Print Spectrum')}>{t('Print Spectrum...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('3D Viewer')}>{t('DAD 3D Viewer...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'purity' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'purity')}>{t('Purity(P)')}</div>
          {activeMenu === 'purity' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Peak Purity')}>{t('Peak Purity')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Purity Options')}>{t('Purity Options...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'library' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'library')}>{t('Library(L)')}</div>
          {activeMenu === 'library' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Library Search')}>{t('Library Search')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Edit Library')}>{t('Edit Library...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Search Options')}>{t('Search Options...')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Purity Options Modal */}
      {showPurityOptions && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Peak Purity Options')}</span>
              <span className="cursor-pointer" onClick={() => setShowPurityOptions(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center justify-between"><span>{t('Purity Threshold')}:</span><input type="number" defaultValue={990} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Include reference spectra')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Use baseline for purity')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowPurityOptions(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowPurityOptions(false)}>{t('Apply')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Library Search Modal */}
      {showLibrarySearch && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Library Search')}</span>
              <span className="cursor-pointer" onClick={() => setShowLibrarySearch(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold">{t('Select Spectral Library')}:</label>
              <select className="border border-gray-400 p-2">
                <option>NIST20</option>
                <option>Wiley Registry</option>
                <option>Custom_Toxins_Lib</option>
              </select>
              <label className="flex items-center justify-between mt-2"><span>{t('Match Threshold')}:</span><input type="number" defaultValue={800} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowLibrarySearch(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowLibrarySearch(false)}>{t('Search')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Library Edit Modal */}
      {showLibraryEdit && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[600px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Edit Library')}</span>
              <span className="cursor-pointer" onClick={() => setShowLibraryEdit(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-32">{t('Compound Name')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('CAS Number')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Molecular Weight')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="border border-gray-400 h-32 flex items-center justify-center bg-white text-gray-400 mt-2">
                {t('Select spectrum to add')}
              </div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowLibraryEdit(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowLibraryEdit(false)}>{t('Save to Library')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* 3D Viewer Modal */}
      {show3DViewer && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[800px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('DAD 3D Viewer')}</span>
              <span className="cursor-pointer" onClick={() => setShow3DViewer(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex justify-between items-center bg-[#e0e0e0] border border-gray-400 p-1">
                <div className="flex gap-4">
                  <label className="flex items-center gap-1">{t('View Angle')}: <input type="range" min="0" max="360" defaultValue="45" /></label>
                  <label className="flex items-center gap-1">{t('Elevation')}: <input type="range" min="0" max="90" defaultValue="30" /></label>
                </div>
                <button className="px-2 py-1 bg-white border border-gray-400 text-xs hover:bg-gray-100">{t('Reset View')}</button>
              </div>
              <div className="border border-gray-400 h-80 flex items-center justify-center bg-black text-green-500 font-mono flex-col relative overflow-hidden">
                <div className="absolute inset-0" style={{
                  backgroundImage: 'linear-gradient(rgba(0,255,0,0.2) 1px, transparent 1px), linear-gradient(90deg, rgba(0,255,0,0.2) 1px, transparent 1px)',
                  backgroundSize: '20px 20px',
                  transform: 'perspective(500px) rotateX(60deg) translateY(-100px) translateZ(-200px)',
                  transformStyle: 'preserve-3d'
                }}>
                  {/* Fake 3D surface plot elements */}
                  <div className="absolute top-1/2 left-1/3 w-20 h-40 bg-green-500 opacity-50" style={{transform: 'translateZ(50px) rotateX(-90deg)', borderRadius: '50% 50% 0 0'}}></div>
                  <div className="absolute top-1/3 left-1/2 w-16 h-24 bg-yellow-500 opacity-60" style={{transform: 'translateZ(30px) rotateX(-90deg)', borderRadius: '50% 50% 0 0'}}></div>
                </div>
                <div className="z-10 bg-black/50 p-2 rounded">{t('WebGL 3D Surface Plot Rendering...')}</div>
              </div>
              <div className="flex justify-end gap-2 mt-2 pt-2 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShow3DViewer(false)}>{t('Close')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Toolbar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={fetchSpectrum}>
          {t('Extract Spectrum')}
          {loading && <span className="ml-1 animate-spin">⏳</span>}
        </button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('Peak Purity')}>{t('Peak Purity')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('Library Search')}>{t('Library Search')}</button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        
        {/* Iso-plot / Contour representation */}
        <div className="w-1/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('DAD 3D Data Matrix (Iso-Plot)')}</div>
          <div className="flex-1 flex flex-col items-center justify-center bg-gray-900 text-green-400 font-mono text-xs p-4 text-center">
            <div className="mb-4">
              [Simulated Heatmap Area]<br/>
              Y-Axis: Time (min)<br/>
              X-Axis: Wavelength (nm)<br/>
              Color: Absorbance (mAU)
            </div>
            <div className="w-full h-48 bg-gradient-to-r from-blue-900 via-red-500 to-yellow-300 opacity-70 border border-gray-500 flex items-center justify-center text-white text-shadow">
              3D Matrix Data Loaded
            </div>
          </div>
        </div>

        {/* Extracted UV-Vis Spectrum */}
        <div className="w-2/3 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex justify-between">
            <span>{t('Extracted UV-Vis Spectrum')}</span>
            <span>RetTime: 4.211 min</span>
          </div>
          <div className="flex-1 p-4 pb-8 relative">
            <div className="absolute left-2 top-10 text-xs font-bold rotate-90 origin-left translate-y-16">{t('Absorbance')} (mAU)</div>
            <div className="absolute bottom-2 w-full text-center text-xs font-bold">{t('Wavelength (nm)')}</div>
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={spectralData}>
                <CartesianGrid strokeDasharray="3 3" vertical={true} horizontal={true} stroke="#e0e0e0" />
                <XAxis dataKey="wavelength" type="number" domain={['dataMin', 'dataMax']} tickFormatter={(v)=>v.toFixed(0)} tick={{fontSize: 10}} tickCount={10} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                <YAxis domain={['auto', 'auto']} tick={{fontSize: 10}} axisLine={{stroke: 'black'}} tickLine={{stroke: 'black'}} />
                <Line type="monotone" dataKey="absorbance" stroke="#ff0000" strokeWidth={1.5} dot={false} isAnimationActive={false} />
              </LineChart>
            </ResponsiveContainer>
          </div>
          
          <div className="h-32 border-t border-gray-400 bg-[#f5f5f5] p-2 flex flex-col">
            <div className="font-bold text-xs mb-1">{t('Library Match Results')}</div>
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap bg-white border border-gray-300">
              <thead className="bg-[#e0e0e0]">
                <tr>
                  <th className="border border-gray-300 p-1">{t('Match Rank')}</th>
                  <th className="border border-gray-300 p-1">{t('Compound Name')}</th>
                  <th className="border border-gray-300 p-1 text-right">{t('Match Score')}</th>
                  <th className="border border-gray-300 p-1 text-right">{t('Purity Factor')}</th>
                </tr>
              </thead>
              <tbody>
                <tr className="hover:bg-blue-50 cursor-pointer">
                  <td className="border border-gray-300 p-1">1</td>
                  <td className="border border-gray-300 p-1 font-bold text-blue-700">Caffeine</td>
                  <td className="border border-gray-300 p-1 text-right font-mono">998.4</td>
                  <td className="border border-gray-300 p-1 text-right font-mono text-green-600">99.9%</td>
                </tr>
                <tr className="hover:bg-blue-50 cursor-pointer text-gray-500">
                  <td className="border border-gray-300 p-1">2</td>
                  <td className="border border-gray-300 p-1">Theobromine</td>
                  <td className="border border-gray-300 p-1 text-right font-mono">845.2</td>
                  <td className="border border-gray-300 p-1 text-right font-mono">-</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

      </div>
    </div>
  );
}