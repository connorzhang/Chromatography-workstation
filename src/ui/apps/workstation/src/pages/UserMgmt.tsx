import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

const mockUsers = [
  { id: 1, name: 'Admin', role: 'Administrator', status: 'Active', lastLogin: '2026-06-20 08:00:00' },
  { id: 2, name: 'Manager_A', role: 'Manager', status: 'Active', lastLogin: '2026-06-19 14:22:10' },
  { id: 3, name: 'Operator1', role: 'Operator', status: 'Active', lastLogin: '2026-06-20 09:10:05' },
];

export default function UserMgmt() {
  const { t } = useTranslation();
  const [showESignModal, setShowESignModal] = useState(false);
  const [showNewUserModal, setShowNewUserModal] = useState(false);
  const [showEditUserModal, setShowEditUserModal] = useState(false);
  const [showPasswordPolicyModal, setShowPasswordPolicyModal] = useState(false);
  const [showAccountLockoutModal, setShowAccountLockoutModal] = useState(false);
  const [showLDAPModal, setShowLDAPModal] = useState(false);

  const [activeMenu, setActiveMenu] = useState<string | null>(null);

  const toggleMenu = (e: React.MouseEvent, menuName: string) => {
    e.stopPropagation();
    setActiveMenu(activeMenu === menuName ? null : menuName);
  };

  const handleMenuClick = (action: string) => {
    setActiveMenu(null);
    if (action === 'esign') setShowESignModal(true);
    else if (action === 'New User') setShowNewUserModal(true);
    else if (action === 'Edit User') setShowEditUserModal(true);
    else if (action === 'Password Policy') setShowPasswordPolicyModal(true);
    else if (action === 'Account Lockout') setShowAccountLockoutModal(true);
    else if (action === 'LDAP Sync') setShowLDAPModal(true);
    else if (action === 'Disable User') alert(t('User Disabled'));
    else alert(`${t('Feature in development')}: ${action}`);
  };

  return (
    <div className="h-full flex flex-col bg-[#f0f0f0] text-sm font-sans select-none border border-gray-400 relative" onContextMenu={(e) => e.preventDefault()} onClick={() => setActiveMenu(null)}>
      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-2 text-black border-b border-gray-300 text-xs relative">
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'user' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'user')}>{t('User(U)')}</div>
          {activeMenu === 'user' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('New User')}>{t('New User...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Edit User')}>{t('Edit User...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer text-red-600 hover:text-white" onClick={() => handleMenuClick('Disable User')}>{t('Disable User')}</div>
              <div className="h-px bg-gray-400 my-1"></div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => window.close()}>{t('Exit')}</div>
              {/* New User Modal */}
      {showNewUserModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('New User')}</span>
              <span className="cursor-pointer" onClick={() => setShowNewUserModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <div className="flex items-center"><label className="w-24">{t('Username')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-24">{t('Full Name')}:</label><input type="text" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-24">{t('Password')}:</label><input type="password" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center">
                <label className="w-24">{t('Role')}:</label>
                <select className="border border-gray-400 p-1 flex-1">
                  <option>{t('Administrator')}</option>
                  <option>{t('Manager')}</option>
                  <option>{t('Operator')}</option>
                  <option>{t('Guest')}</option>
                </select>
              </div>
              <label className="flex items-center gap-2 mt-2"><input type="checkbox" defaultChecked /> {t('User must change password at next login')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowNewUserModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowNewUserModal(false)}>{t('Create User')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Password Policy Modal */}
      {showPasswordPolicyModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[450px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Password Policy')}</span>
              <span className="cursor-pointer" onClick={() => setShowPasswordPolicyModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center justify-between"><span>{t('Minimum Password Length')}:</span><input type="number" defaultValue={8} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <label className="flex items-center justify-between"><span>{t('Password Expiration (Days)')}:</span><input type="number" defaultValue={90} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <div className="border-t border-gray-300 my-2"></div>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Require Uppercase Letters')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Require Lowercase Letters')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Require Numbers')}</label>
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Require Special Characters')}</label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowPasswordPolicyModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowPasswordPolicyModal(false)}>{t('Save Policy')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Account Lockout Modal */}
      {showAccountLockoutModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[400px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Account Lockout Policy')}</span>
              <span className="cursor-pointer" onClick={() => setShowAccountLockoutModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Enable Account Lockout')}</label>
              <div className="border-t border-gray-300 my-2"></div>
              <label className="flex items-center justify-between"><span>{t('Lockout Threshold (Attempts)')}:</span><input type="number" defaultValue={5} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <label className="flex items-center justify-between"><span>{t('Lockout Duration (Minutes)')}:</span><input type="number" defaultValue={30} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <label className="flex items-center justify-between"><span>{t('Reset Counter After (Minutes)')}:</span><input type="number" defaultValue={30} className="border border-gray-400 p-1 w-20 text-right" /></label>
              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowAccountLockoutModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowAccountLockoutModal(false)}>{t('Save Policy')}</button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* LDAP Sync Modal */}
      {showLDAPModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('LDAP/AD Synchronization')}</span>
              <span className="cursor-pointer" onClick={() => setShowLDAPModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-3">
              <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Enable LDAP/AD Authentication')}</label>
              <div className="border-t border-gray-300 my-2"></div>
              <div className="flex items-center"><label className="w-32">{t('Server Address')}:</label><input type="text" defaultValue="ldap://corp.local:389" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Base DN')}:</label><input type="text" defaultValue="DC=corp,DC=local" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Bind User')}:</label><input type="text" defaultValue="CN=Admin,CN=Users,DC=corp,DC=local" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex items-center"><label className="w-32">{t('Bind Password')}:</label><input type="password" defaultValue="********" className="border border-gray-400 p-1 flex-1" /></div>
              <div className="flex justify-between items-center mt-2">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300">{t('Test Connection')}</button>
                <div className="flex gap-2">
                  <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowLDAPModal(false)}>{t('Cancel')}</button>
                  <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowLDAPModal(false)}>{t('Save Settings')}</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'policy' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'policy')}>{t('Policy(P)')}</div>
          {activeMenu === 'policy' && (
            <div className="absolute top-full left-0 mt-0 w-48 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Password Policy')}>{t('Password Policy...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('Account Lockout')}>{t('Account Lockout...')}</div>
            </div>
          )}
        </div>
        <div className="relative">
          <div className={`px-2 py-1 cursor-pointer ${activeMenu === 'compliance' ? 'bg-blue-200' : 'hover:bg-blue-100'}`} onClick={(e) => toggleMenu(e, 'compliance')}>{t('Compliance(C)')}</div>
          {activeMenu === 'compliance' && (
            <div className="absolute top-full left-0 mt-0 w-56 bg-[#f0f0f0] border border-gray-400 shadow-lg z-50 py-1 flex flex-col">
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('esign')}>{t('E-Signature Setup...')}</div>
              <div className="px-4 py-1 hover:bg-blue-500 hover:text-white cursor-pointer" onClick={() => handleMenuClick('LDAP Sync')}>{t('LDAP/AD Sync Settings...')}</div>
            </div>
          )}
        </div>
      </div>

      <div className="flex bg-[#f0f0f0] px-2 py-1 gap-1 items-center border-b border-gray-300 shadow-sm">
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('New User')}>➕ {t('New User...')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs" onClick={() => handleMenuClick('Edit User')}>✏️ {t('Edit User...')}</button>
        <button className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs text-red-600" onClick={() => handleMenuClick('Disable User')}>🗑️ {t('Disable User')}</button>
        <div className="w-px h-5 bg-gray-400 mx-1"></div>
        <button 
          className="px-2 py-1 hover:bg-gray-200 border border-transparent hover:border-gray-400 rounded text-xs font-bold text-blue-700"
          onClick={() => handleMenuClick('esign')}
        >
          🔐 {t('E-Signature Setup...')}
        </button>
      </div>

      <div className="flex flex-1 overflow-hidden p-2 gap-2 bg-[#a0a0a0]">
        <div className="flex-1 bg-white border border-gray-500 shadow-md flex flex-col">
          <div className="bg-blue-800 text-white font-bold px-2 py-1 text-xs flex justify-between">
            <span>{t('User List')}</span>
            <span className="bg-green-600 px-2 rounded text-[10px]">{t('LDAP/AD Sync: Active')}</span>
          </div>
          <div className="overflow-auto flex-1">
            <table className="w-full text-xs text-left border-collapse whitespace-nowrap">
              <thead className="bg-[#e0e0e0] sticky top-0">
                <tr>
                  <th className="border border-gray-400 p-1 w-32">{t('Username')}</th>
                  <th className="border border-gray-400 p-1 w-32">{t('Role')}</th>
                  <th className="border border-gray-400 p-1 w-24">{t('Status')}</th>
                  <th className="border border-gray-400 p-1">{t('Last Login')}</th>
                </tr>
              </thead>
              <tbody>
                {mockUsers.map(u => (
                  <tr key={u.id} className="hover:bg-blue-50 cursor-pointer">
                    <td className="border border-gray-300 p-1 font-bold">{u.name}</td>
                    <td className="border border-gray-300 p-1">{u.role}</td>
                    <td className="border border-gray-300 p-1 text-green-700">{u.status}</td>
                    <td className="border border-gray-300 p-1 font-mono">{u.lastLogin}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>

      {/* Advanced E-Signature Modal */}
      {showESignModal && (
        <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-[#f0f0f0] border-2 border-blue-800 shadow-2xl w-[500px] flex flex-col">
            <div className="bg-blue-800 text-white font-bold px-3 py-1 flex justify-between">
              <span>{t('Electronic Signature Policy (21 CFR Part 11)')}</span>
              <span className="cursor-pointer" onClick={() => setShowESignModal(false)}>✕</span>
            </div>
            <div className="p-4 flex flex-col gap-4">
              <div className="flex flex-col gap-2">
                <label className="font-bold border-b border-gray-300 pb-1">{t('Signature Workflow Policy')}</label>
                <label className="flex items-center gap-2"><input type="radio" name="flow" /> {t('Single Sign-off (Reviewer)')}</label>
                <label className="flex items-center gap-2"><input type="radio" name="flow" /> {t('Two-Step (Submitter -> Reviewer)')}</label>
                <label className="flex items-center gap-2"><input type="radio" name="flow" defaultChecked /> {t('Three-Step (Submitter -> Reviewer -> Approver)')}</label>
              </div>

              <div className="flex flex-col gap-2 mt-2">
                <label className="font-bold border-b border-gray-300 pb-1">{t('Authentication Requirements')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Require Password for every signature')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Lock data file upon final approval')}</label>
                <label className="flex items-center gap-2"><input type="checkbox" defaultChecked /> {t('Force signature meaning selection')}</label>
              </div>

              <div className="flex justify-end gap-2 mt-4 pt-4 border-t border-gray-300">
                <button className="px-4 py-1 bg-gray-200 border border-gray-400 hover:bg-gray-300" onClick={() => setShowESignModal(false)}>{t('Cancel')}</button>
                <button className="px-4 py-1 bg-blue-600 text-white border border-blue-800 hover:bg-blue-700" onClick={() => setShowESignModal(false)}>{t('Save Policy')}</button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}