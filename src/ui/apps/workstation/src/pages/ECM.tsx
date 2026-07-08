import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

export default function ECM() {
  const { t } = useTranslation();
  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  // Modal states
  const [showCheckout, setShowCheckout] = useState(false);
  const [showSettings, setShowSettings] = useState(false);
  const [showESign, setShowESign] = useState(false);
  const [showVersionCompare, setShowVersionCompare] = useState(false);
  
  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'Checkout') setShowCheckout(true);
    else if (action === 'Settings') setShowSettings(true);
    else if (action === 'ESign') setShowESign(true);
    else if (action === 'Compare') setShowVersionCompare(true);
    else alert(`${t('Feature in development')}: ${action}`);
  };

  return (
    <div className="h-full flex flex-col bg-white" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      {/* Menu Bar */}
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'vault' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'vault')}>{t('Vault(V)')}</div>
          {activeMenu === 'vault' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Sync')}>{t('Sync to Vault')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Checkout')}>{t('Checkout Dataset...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Settings')}>{t('Connection Settings...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'compliance' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'compliance')}>{t('Compliance(C)')}</div>
          {activeMenu === 'compliance' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Lock')}>{t('Lock Dataset')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('ESign')}>{t('Electronic Signature...')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Compare')}>{t('Version Comparison...')}</div>
            </div>
          )}
        </div>
      </div>

      {/* Checkout Dataset Modal */}
      {showCheckout && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Checkout Dataset')}</span>
              <span className="cursor-pointer" onClick={() => setShowCheckout(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="font-bold">{t('Select Dataset to Checkout')}:</label>
              <select className="border border-gray-400 p-2" size={4}>
                <option>ProjectA/001-0101.D (v1.0)</option>
                <option>ProjectA/002-0102.D (v1.2)</option>
                <option>ProjectB/Calibration_2026.D (v2.0)</option>
              </select>
              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('Lock on server to prevent concurrent edits')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowCheckout(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowCheckout(false)}>{t('Checkout')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Connection Settings Modal */}
      {showSettings && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[450px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('ECM Connection Settings')}</span>
              <span className="cursor-pointer" onClick={() => setShowSettings(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-32">{t('Server URL')}:</label><input type="text" defaultValue="https://ecm.corp.local/api" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Repository')}:</label><input type="text" defaultValue="Analytical_Lab" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('API Token')}:</label><input type="password" defaultValue="****************" className="border border-gray-400 p-1 flex-1" /></div>
              <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300 mt-2 self-start">{t('Test Connection')}</button>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowSettings(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowSettings(false)}>{t('Save')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* E-Signature Modal (ECM Context) */}
      {showESign && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[450px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Apply Electronic Signature')}</span>
              <span className="cursor-pointer" onClick={() => setShowESign(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('By signing this document, you acknowledge that you have reviewed the data and it complies with all regulatory requirements.')}</p>
              <div className="flex items-center"><label className="w-32">{t('Meaning')}:</label>
                <select className="border border-gray-400 p-1 flex-1">
                  <option>{t('I am the author of this data')}</option>
                  <option>{t('I have reviewed this data')}</option>
                  <option>{t('I approve this data for release')}</option>
                </select>
              </div>
              <div className="flex items-center"><label className="w-32">{t('Username')}:</label><input type="text" defaultValue="Admin" disabled className="border border-gray-400 p-1 flex-1 bg-gray-200" /></div>
              <div className="flex items-center"><label className="w-32">{t('Password')}:</label><input type="password" placeholder="Enter password" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowESign(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowESign(false)}>{t('Sign')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Version Comparison Modal */}
      {showVersionCompare && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[700px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Version Comparison (Audit Trail)')}</span>
              <span className="cursor-pointer" onClick={() => setShowVersionCompare(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <p className="text-xs text-gray-700">{t('Compare two versions of a method or dataset to highlight modified parameters, aligning with data integrity guidelines.')}</p>
              
              <div className="flex gap-4">
                <div className="flex-1 flex flex-col gap-1">
                  <label className="font-bold text-xs">{t('Source Version (Older)')}:</label>
                  <select className="border border-gray-400 p-1 text-xs">
                    <option>001-0101.D (v1.0) - 2026-06-20 10:00:00</option>
                    <option>001-0101.D (v1.1) - 2026-06-20 10:15:00</option>
                  </select>
                </div>
                <div className="flex-1 flex flex-col gap-1">
                  <label className="font-bold text-xs">{t('Target Version (Newer)')}:</label>
                  <select className="border border-gray-400 p-1 text-xs">
                    <option>001-0101.D (v1.2) - 2026-06-20 11:30:00</option>
                  </select>
                </div>
              </div>

              <div className="border border-gray-400 mt-2 bg-white">
                <table className="w-full text-left text-xs border-collapse">
                  <thead className="bg-gray-200">
                    <tr>
                      <th className="p-1 border-r border-b border-gray-400">{t('Parameter / Setting')}</th>
                      <th className="p-1 border-r border-b border-gray-400 text-red-700">{t('Source Value')}</th>
                      <th className="p-1 border-b border-gray-400 text-green-700">{t('Target Value')}</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr className="border-b border-gray-300">
                      <td className="p-1 border-r border-gray-300">Integration: Area Reject</td>
                      <td className="p-1 border-r border-gray-300 bg-red-50 text-red-800 line-through">1.0</td>
                      <td className="p-1 bg-green-50 text-green-800 font-bold">5.0</td>
                    </tr>
                    <tr className="border-b border-gray-300">
                      <td className="p-1 border-r border-gray-300">Pump: Flow Rate</td>
                      <td className="p-1 border-r border-gray-300 bg-red-50 text-red-800 line-through">1.000 mL/min</td>
                      <td className="p-1 bg-green-50 text-green-800 font-bold">1.200 mL/min</td>
                    </tr>
                    <tr className="border-b border-gray-300">
                      <td className="p-1 border-r border-gray-300">Oven: Temperature</td>
                      <td className="p-1 border-r border-gray-300">40.0 °C</td>
                      <td className="p-1">40.0 °C</td>
                    </tr>
                  </tbody>
                </table>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-white border border-gray-400 hover:bg-gray-100">{t('Print Diff Report')}</button>
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowVersionCompare(false)}>{t('Close')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      <div className="p-6 flex-1 flex flex-col">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-bold text-slate-800">{t('ECM Integration (Enterprise Content Management)')}</h1>
          <div className="flex gap-2">
            <button className="bg-slate-200 text-slate-700 px-4 py-2 rounded shadow hover:bg-slate-300">{t('Sync to Vault')}</button>
            <button className="bg-green-600 text-white px-4 py-2 rounded shadow hover:bg-green-700">{t('Lock Dataset')}</button>
          </div>
        </div>
      
      <div className="bg-yellow-50 border-l-4 border-yellow-400 p-4 mb-6">
        <div className="flex items-center">
          <div className="flex-shrink-0">
            <svg className="h-5 w-5 text-yellow-400" viewBox="0 0 20 20" fill="currentColor">
              <path fillRule="evenodd" d="M8.257 3.099c.765-1.36 2.722-1.36 3.486 0l5.58 9.92c.75 1.334-.213 2.98-1.742 2.98H4.42c-1.53 0-2.493-1.646-1.743-2.98l5.58-9.92zM11 13a1 1 0 11-2 0 1 1 0 012 0zm-1-8a1 1 0 00-1 1v3a1 1 0 002 0V6a1 1 0 00-1-1z" clipRule="evenodd" />
            </svg>
          </div>
          <div className="ml-3">
            <p className="text-sm text-yellow-700">
              This system is configured for 21 CFR Part 11 compliance. Data saved to the ECM Vault cannot be deleted or overwritten.
            </p>
          </div>
        </div>
      </div>

        <div className="flex-1 overflow-auto border border-slate-200 rounded">
          <table className="w-full text-left text-sm border-collapse">
            <thead className="bg-slate-100 border-b border-slate-200 sticky top-0">
              <tr>
                <th className="p-3 font-semibold text-slate-700">{t('Status')}</th>
                <th className="p-3 font-semibold text-slate-700">{t('Data File')} (.D)</th>
                <th className="p-3 font-semibold text-slate-700">{t('Version')}</th>
                <th className="p-3 font-semibold text-slate-700">{t('Last Modified')}</th>
                <th className="p-3 font-semibold text-slate-700">{t('Checksum (SHA-256)')}</th>
                <th className="p-3 font-semibold text-slate-700">{t('Actions')}</th>
              </tr>
            </thead>
            <tbody>
              <tr className="border-b border-slate-100 hover:bg-slate-50">
                <td className="p-3"><span className="text-green-600 font-bold">🔒 {t('Locked')}</span></td>
                <td className="p-3 font-medium text-slate-700">001-0101.D</td>
                <td className="p-3">v1.0</td>
                <td className="p-3">2026-06-20 10:00:00</td>
                <td className="p-3 font-mono text-xs text-slate-500">e3b0c44298fc1c149afbf4c8996fb924</td>
                <td className="p-3"><button className="text-blue-600 hover:underline">{t('View History')}</button></td>
              </tr>
              <tr className="border-b border-slate-100 hover:bg-slate-50">
                <td className="p-3"><span className="text-orange-500 font-bold">⚠️ {t('Draft')}</span></td>
                <td className="p-3 font-medium text-slate-700">002-0102.D</td>
                <td className="p-3">-</td>
                <td className="p-3">2026-06-20 11:30:00</td>
                <td className="p-3 font-mono text-xs text-slate-500">{t('Pending')}</td>
                <td className="p-3"><button className="text-blue-600 hover:underline">{t('Commit')}</button></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}