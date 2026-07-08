import React from 'react';
import { BrowserRouter, Routes, Route, Link, useLocation } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import ConfigEditor from './pages/ConfigEditor';
import MethodRun from './pages/MethodRun';
import SequenceTable from './pages/SequenceTable';
import DataAnalysis from './pages/DataAnalysis';
import Calibration from './pages/Calibration';
import BatchReview from './pages/BatchReview';
import ReportLayout from './pages/ReportLayout';
import AuditTrail from './pages/AuditTrail';
import UserMgmt from './pages/UserMgmt';
import Diagnostics from './pages/Diagnostics';
import SpectralAnalysis from './pages/SpectralAnalysis';
import SamplePrep from './pages/SamplePrep';
import ValveControl from './pages/ValveControl';
import ControlCharts from './pages/ControlCharts';
import CustomCalculator from './pages/CustomCalculator';
import ECM from './pages/ECM';
import MassSpectrometry from './pages/MassSpectrometry';
import GPCAnalysis from './pages/GPCAnalysis';
import PrepLC from './pages/PrepLC';

function Sidebar() {
  const { t, i18n } = useTranslation();
  const location = useLocation();

  const toggleLanguage = () => {
    const nextLang = i18n.language === 'zh' ? 'en' : 'zh';
    i18n.changeLanguage(nextLang);
  };

  const navItems = [
    { path: '/method-run', label: 'nav.method' },
    { path: '/sample-prep', label: 'nav.sample_prep' },
    { path: '/valve', label: 'nav.valve' },
    { path: '/data-analysis', label: 'nav.analysis' },
    { path: '/spectral', label: 'nav.spectral' },
    { path: '/sequence', label: 'nav.sequence' },
    { path: '/calibration', label: 'nav.calibration' },
    { path: '/charts', label: 'nav.charts' },
    { path: '/calc', label: 'nav.calc' },
    { path: '/batch-review', label: 'nav.batch' },
    { path: '/report', label: 'nav.report' },
    { path: '/diagnostics', label: 'nav.diagnostics' },
    { path: '/ecm', label: 'nav.ecm' },
    { path: '/audit', label: 'nav.audit' },
    { path: '/users', label: 'nav.users' },
    { path: '/', label: 'nav.config' },
  ];

  return (
    <div className="w-64 bg-slate-800 text-white flex flex-col h-full">
      <div className="p-4 text-xl font-bold border-b border-slate-700">
        ChemStation <span className="text-sm font-normal text-slate-400">Clone</span>
      </div>
      <nav className="flex-1 overflow-y-auto p-2 flex flex-col gap-1">
        {navItems.map((item) => (
          <Link 
            key={item.path}
            to={item.path}
            className={`block px-4 py-3 rounded text-sm transition-colors ${
              location.pathname === item.path 
                ? 'bg-blue-600 font-semibold' 
                : 'hover:bg-slate-700 text-slate-300 hover:text-white'
            }`}
          >
            {t(item.label)}
          </Link>
        ))}
      </nav>
      <div className="p-4 border-t border-slate-700">
        <button 
          onClick={toggleLanguage}
          className="w-full bg-slate-700 hover:bg-slate-600 text-sm py-2 rounded transition-colors"
        >
          🌐 {i18n.language === 'zh' ? 'Switch to English' : '切换至中文'}
        </button>
      </div>
    </div>
  );
}

export default function App() {
  return (
    <BrowserRouter>
      <div className="flex h-screen w-full bg-gray-100 font-sans">
        <Sidebar />
        <div className="flex-1 overflow-hidden relative">
          <Routes>
            <Route path="/" element={<ConfigEditor />} />
            <Route path="/method-run" element={<MethodRun />} />
            <Route path="/sample-prep" element={<SamplePrep />} />
            <Route path="/valve" element={<ValveControl />} />
            <Route path="/sequence" element={<SequenceTable />} />
            <Route path="/data-analysis" element={<DataAnalysis />} />
            <Route path="/ms" element={<MassSpectrometry />} />
            <Route path="/gpc" element={<GPCAnalysis />} />
            <Route path="/prep-lc" element={<PrepLC />} />
            <Route path="/spectral" element={<SpectralAnalysis />} />
            <Route path="/calibration" element={<Calibration />} />
            <Route path="/charts" element={<ControlCharts />} />
            <Route path="/calc" element={<CustomCalculator />} />
            <Route path="/batch-review" element={<BatchReview />} />
            <Route path="/report" element={<ReportLayout />} />
            <Route path="/ecm" element={<ECM />} />
            <Route path="/diagnostics" element={<Diagnostics />} />
            <Route path="/audit" element={<AuditTrail />} />
            <Route path="/users" element={<UserMgmt />} />
          </Routes>
        </div>
      </div>
    </BrowserRouter>
  );
}