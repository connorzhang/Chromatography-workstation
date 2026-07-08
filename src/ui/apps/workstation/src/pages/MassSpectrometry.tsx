import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, BarChart, Bar } from 'recharts';

export default function MassSpectrometry() {
  const { t } = useTranslation();
  const [targetMz, setTargetMz] = useState('91.0');
  const [ticData, setTicData] = useState<any[]>([]);
  const [spectrumData, setSpectrumData] = useState<any[]>([]);
  const [searchResults, setSearchResults] = useState<any[]>([
    { hit: 1, name: "Toluene", formula: "C7H8", mw: 92.14, match: 98, cas: "108-88-3" },
    { hit: 2, name: "Ethylbenzene", formula: "C8H10", mw: 106.17, match: 75, cas: "100-41-4" }
  ]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
        const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/ms/data`);
        if (res.ok) {
          const data = await res.json();
          setTicData(data.tic);
          setSpectrumData(data.spectrum);
        }
      } catch (e) {
        console.error("Failed to fetch MS data", e);
      }
    };
    fetchData();
  }, []);

  const handleDeconv = async () => {
    setLoading(true);
    try {
      const unknown = spectrumData.map(d => ({ mz: d.mz, intensity: d.abundance }));
      // Mock library spectrum
      const library = [
        { mz: 50, intensity: 12 },
        { mz: 77, intensity: 40 },
        { mz: 91, intensity: 100 },
        { mz: 105, intensity: 18 },
      ];

      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/ms/deconvolute`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ unknown, library })
      });

      if (res.ok) {
        const data = await res.json();
        // Insert at the top of results
        setSearchResults(prev => [
          { 
            hit: 0, 
            name: "AMDIS Extracted Component", 
            formula: "Unknown", 
            mw: "-", 
            match: Math.round(data.match_factor / 10), // Convert 0-1000 to 0-100%
            cas: data.cas_number 
          },
          ...prev
        ]);
      }
    } catch (e) {
      console.error("Deconvolution failed", e);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-6 h-full flex flex-col bg-white overflow-y-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-slate-800">{t('ms.title', 'LC/GC-MS Integration')}</h1>
        <div className="flex gap-2">
          <button 
            onClick={handleDeconv}
            disabled={loading}
            className="bg-slate-200 text-slate-700 px-4 py-2 rounded shadow hover:bg-slate-300 disabled:opacity-50"
          >
            {loading ? t('common.loading', 'Loading...') : t('ms.amdis', 'Auto Deconvolution (AMDIS)')}
          </button>
          <button className="bg-blue-600 text-white px-4 py-2 rounded shadow hover:bg-blue-700">
            {t('ms.nist_search', 'NIST Library Search')}
          </button>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-6 h-64">
        {/* TIC / EIC Chart */}
        <div className="bg-slate-50 border border-slate-200 rounded p-4 flex flex-col">
          <div className="flex justify-between items-center mb-4">
            <h3 className="font-semibold text-slate-700">{t('ms.tic_eic', 'TIC & Extracted Ion Chromatogram')}</h3>
            <div className="flex items-center gap-2">
              <span className="text-xs text-slate-500">{t('ms.extract_mz', 'Extract m/z:')}</span>
              <input 
                type="text" 
                value={targetMz} 
                onChange={e => setTargetMz(e.target.value)}
                className="border border-slate-300 rounded px-2 py-1 text-xs w-16" 
              />
            </div>
          </div>
          <div className="flex-1">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={ticData} margin={{ top: 5, right: 20, left: 0, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                <XAxis dataKey="time" type="number" domain={['dataMin', 'dataMax']} />
                <YAxis />
                <Tooltip />
                <Line type="monotone" dataKey="intensity" stroke="#2563eb" dot={false} strokeWidth={2} name="TIC" />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </div>

        {/* Mass Spectrum Chart */}
        <div className="bg-slate-50 border border-slate-200 rounded p-4 flex flex-col">
          <div className="flex justify-between items-center mb-4">
            <h3 className="font-semibold text-slate-700">{t('ms.spectrum', 'Mass Spectrum (RT: 3.10 min)')}</h3>
            <span className="text-xs font-mono bg-blue-100 text-blue-800 px-2 py-1 rounded">Base Peak: 91.0</span>
          </div>
          <div className="flex-1">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={spectrumData} margin={{ top: 5, right: 20, left: 0, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" vertical={false} />
                <XAxis dataKey="mz" />
                <YAxis />
                <Tooltip cursor={{fill: 'transparent'}} />
                <Bar dataKey="abundance" fill="#0f172a" barSize={4} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>
      </div>

      {/* NIST Search Results */}
      <div className="flex-1 bg-white border border-slate-200 rounded flex flex-col">
        <div className="bg-slate-100 border-b border-slate-200 p-3 font-semibold text-slate-700">
          {t('ms.search_results', 'NIST Library Search Results')}
        </div>
        <div className="flex-1 overflow-auto">
          <table className="w-full text-left text-sm border-collapse">
            <thead className="bg-white border-b border-slate-200 sticky top-0">
              <tr>
                <th className="p-3 font-semibold text-slate-600">{t('ms.hit', 'Hit')}</th>
                <th className="p-3 font-semibold text-slate-600">{t('ms.compound', 'Compound Name')}</th>
                <th className="p-3 font-semibold text-slate-600">{t('ms.formula', 'Formula')}</th>
                <th className="p-3 font-semibold text-slate-600">{t('ms.mw', 'MW')}</th>
                <th className="p-3 font-semibold text-slate-600">{t('ms.match', 'Match Quality')}</th>
                <th className="p-3 font-semibold text-slate-600">{t('ms.cas', 'CAS#')}</th>
              </tr>
            </thead>
            <tbody>
              {searchResults.map((row, idx) => (
                <tr key={idx} className="border-b border-slate-100 hover:bg-blue-50">
                  <td className="p-3 text-slate-600">{row.hit === 0 ? '*' : row.hit}</td>
                  <td className="p-3 font-medium text-blue-700">{row.name}</td>
                  <td className="p-3">{row.formula}</td>
                  <td className="p-3">{row.mw}</td>
                  <td className="p-3 font-bold text-green-600">{row.match}%</td>
                  <td className="p-3 text-slate-500">{row.cas}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
