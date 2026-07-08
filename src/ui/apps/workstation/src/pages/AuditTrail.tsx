import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';

interface AuditLog {
  id?: string;
  time?: string;
  user: string;
  module: string;
  action: string;
  details: string;
}

export default function AuditTrail() {
  const { t } = useTranslation();
  const [logs, setLogs] = useState<AuditLog[]>([]);
  const [activeMenu, setActiveMenu] = useState<string | null>(null);
  const [filterType, setFilterType] = useState<string>('All');
  const [showExportModal, setShowExportModal] = useState(false);
  const [showCustomFilterModal, setShowCustomFilterModal] = useState(false);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'refresh') fetchLogs();
    else if (action === 'export' || action === 'print') setShowExportModal(true);
    else if (action.startsWith('filter_')) {
      if (action === 'filter_all') setFilterType('All');
      else if (action === 'filter_system') setFilterType('System');
      else if (action === 'filter_methods') setFilterType('Methods');
      else if (action === 'filter_sequences') setFilterType('Sequences');
      else if (action === 'filter_custom') setShowCustomFilterModal(true);
    }
  };

  const fetchLogs = async () => {
    try {
      const hostname = window.location.hostname === 'localhost' ? '127.0.0.1' : window.location.hostname;
      const res = await fetch((window.location.port === '5173' ? `http://${window.location.hostname}:8082` : '') + `/api/v1/audit/logs`);
      const data = await res.json();
      setLogs(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    fetchLogs();
  }, []);

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'file' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'file')}>{t('File(F)')}</div>
          {activeMenu === 'file' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('refresh')}>{t('Refresh Logs')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('export')}>{t('Export Log...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('print')}>{t('Print Log...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'filter' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'filter')}>{t('Filter(L)')}</div>
          {activeMenu === 'filter' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('filter_all')}><span className="w-3">{filterType === 'All' ? '✓' : ''}</span>{t('All')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('filter_system')}><span className="w-3">{filterType === 'System' ? '✓' : ''}</span>{t('System')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('filter_methods')}><span className="w-3">{filterType === 'Methods' ? '✓' : ''}</span>{t('Methods')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer flex items-center gap-2" onClick={() => handleMenuClick('filter_sequences')}><span className="w-3">{filterType === 'Sequences' ? '✓' : ''}</span>{t('Sequences')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('filter_custom')}>{t('Custom Filter...')}</div>
            </div>
          )}
        </div>
      </div>

      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={fetchLogs}>{t('Refresh Logs')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs">{t('Export PDF')}</button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        <div className="flex-1 bg-white border border-gray-500 shadow-md flex flex-col">      
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs">{t('Audit Trail Logs (21 CFR Part 11 Compliant)')}</div>
          <div className="overflow-auto flex-1">
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap">    
              <thead className="bg-[#e0e0e0] sticky top-0">
                <tr>
                  <th className="border border-gray-400 p-1 w-40">{t('Date/Time')}</th>     
                  <th className="border border-gray-400 p-1 w-24">{t('User')}</th>
                  <th className="border border-gray-400 p-1 w-32">{t('Module')}</th>
                  <th className="border border-gray-400 p-1 w-48">{t('Action')}</th>        
                  <th className="border border-gray-400 p-1">{t('Details')}</th>
                </tr>
              </thead>
              <tbody>
                {logs.filter(log => filterType === 'All' || log.module === filterType).map((log, idx) => (
                  <tr key={log.id || idx} className="hover:bg-blue-50">
                    <td className="border border-gray-300 p-1 font-mono">{log.time}</td>      
                    <td className="border border-gray-300 p-1">{log.user}</td>
                    <td className="border border-gray-300 p-1">{log.module}</td>
                    <td className="border border-gray-300 p-1 font-bold">{log.action}</td>    
                    <td className="border border-gray-300 p-1">{log.details}</td>
                  </tr>
                ))}
                {logs.filter(log => filterType === 'All' || log.module === filterType).length === 0 && (
                  <tr>
                    <td colSpan={5} className="p-4 text-center text-gray-500 italic">No audit logs found.</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>

      {/* Export/Print Modal */}
      {showExportModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Export/Print Logs')}</span>
              <span className="cursor-pointer" onClick={() => setShowExportModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="radio" name="exportFormat" defaultChecked /> {t('PDF Format (Secure)')}</label>
              <label className="flex items-center gap-2"><input type="radio" name="exportFormat" /> {t('CSV / Excel')}</label>
              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Include E-Signature history')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowExportModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowExportModal(false)}>{t('Execute')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Custom Filter Modal */}
      {showCustomFilterModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Custom Filter')}</span>
              <span className="cursor-pointer" onClick={() => setShowCustomFilterModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-24">{t('Date Range')}:</label><input type="date" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-24">{t('User')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-24">{t('Action')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowCustomFilterModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowCustomFilterModal(false)}>{t('Apply Filter')}</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}