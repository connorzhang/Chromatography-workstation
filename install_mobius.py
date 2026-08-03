import urllib.request
import os
import winreg

bin_dir = os.path.expanduser('~/.mobius/bin')
os.makedirs(bin_dir, exist_ok=True)

url = 'http://mobius.rry.net/mobius.exe'
dest = os.path.join(bin_dir, 'mobius.exe')
urllib.request.urlretrieve(url, dest)
print('Downloaded to ' + dest)

key = winreg.OpenKey(winreg.HKEY_CURRENT_USER, 'Environment', 0, winreg.KEY_ALL_ACCESS)
try:
    path_val, _ = winreg.QueryValueEx(key, 'PATH')
    if bin_dir not in path_val:
        new_path = path_val.rstrip(';') + ';' + bin_dir
        winreg.SetValueEx(key, 'PATH', 0, winreg.REG_EXPAND_SZ, new_path)
        print('Added to PATH')
except Exception as e:
    print('Registry error:', e)
finally:
    winreg.CloseKey(key)
