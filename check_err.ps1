Get-WinEvent -FilterHashtable @{LogName='Application'; ProviderName='Application Error'} -MaxEvents 5 | Select-Object TimeCreated, Message | Format-List
