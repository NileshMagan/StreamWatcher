REM schtasks /create /tn "My App" /tr c:\apps\myapp.exe /sc once /st 14:18 /sd 11/13/2002
REM SCHTASKS /CREATE /SC DAILY /st 14:18 /sd format(as.Date("2020-03-25"), "%m/%d/%Y") /TN "MyTasks\StreamWatcher task" /TR "C:\Users\Nilesh Magan\Documents\Code\Powershell\StreamWatcher.ps1" 
REM SCHTASKS /CREATE /SC DAILY /TN "MyTasks\StreamWatcher task" /TR "C:\Users\Nilesh Magan\Documents\Code\Powershell\StreamWatcher.ps1" /ST 13:42 /SD 03/25/2020
REM SCHTASKS /CREATE /SC DAILY /TN "MyTasks\StreamWatcher task1" /TR "C:\Users\Nilesh Magan\Documents\Code\Powershell\StreamWatcher.ps1" /ST 14:20

REM $StartDate = New-Object -TypeName DateTime -ArgumentList:(2020,03,25)
REM $FormatHack = ($([System.Globalization.DateTimeFormatInfo]::CurrentInfo.ShortDatePattern) -replace 'M+/', 'MM/') -replace 'd+/', 'dd/'

REM schtasks.exe /create `
REM     /tn "MyTasks\StreamWatcher task" `
REM     /tr "C:\Users\Nilesh Magan\Documents\Code\Powershell\StreamWatcher.ps1" `
REM     /sc DAILY `
REM     /st 14:13 `
REM     /sd $StartDate.ToString($FormatHack) `
REM     /ru System `
REM     /rl HIGHEST


REM Powershell.exe -executionpolicy remotesigned -File  CreateStreamWatcherTasks.ps1
@ECHO OFF
SET ThisScriptsDirectory=%~dp0
SET PowerShellScriptPath=%ThisScriptsDirectory%CreateScheduledTasks.ps1
REM SET PowerShellScriptPath=%ThisScriptsDirectory%mydir.ps1
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& {Start-Process PowerShell -ArgumentList '-NoProfile -ExecutionPolicy Bypass -File ""%PowerShellScriptPath%""' -Verb RunAs}";
