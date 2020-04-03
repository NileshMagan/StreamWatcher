REM Powershell.exe -executionpolicy remotesigned -File  DeleteScheduledTasks.ps1
REM powershell -noprofile -command "&{ start-process powershell -ArgumentList '-noprofile -file DeleteScheduledTasks.ps1' -verb RunAs}"
REM powershell -command "&{ start-process powershell -ArgumentList '-file DeleteScheduledTasks.ps1' -verb RunAs}"

REM powershell -command "&{ start-process powershell -ArgumentList '-file C:\Users\Nilesh Magan\Documents\Code\Powershell\Test\DeleteScheduledTasks.ps1' -verb RunAs}"
REM powershell -command "&{ $CurrentDirectory = (Get-Item -Path '.\').FullName; $ScriptPath = `"$CurrentDirectory\Test\DeleteScheduledTasks.ps1`";}"

REM powershell -command "&{ $CurrentDirectory = (Get-Item -Path '.\').FullName; $ScriptPath = Join-Path -Path $CurrentDirectory -ChildPath 'DeleteScheduledTasks.ps1'; $FormattedPath = Resolve-Path $ScriptPath;start-process powershell -ArgumentList '-file $($FormattedPath.Path)' -verb RunAs}"
REM powershell -command "&{ start-process powershell -ArgumentList '-file $($FormattedPath.Path)' -verb RunAs}"
REM powershell -command "&{ start-process powershell -ArgumentList '-file $($FormattedPath.Path)' -verb RunAs}"

@ECHO OFF
SET ThisScriptsDirectory=%~dp0
SET PowerShellScriptPath=%ThisScriptsDirectory%DeleteScheduledTasks.ps1
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& {Start-Process PowerShell -ArgumentList '-NoProfile -ExecutionPolicy Bypass -File ""%PowerShellScriptPath%""' -Verb RunAs}";
REM powershell -command "&{ start-process powershell -ArgumentList '-file $($FormattedPath.Path)' -verb RunAs}"
REM powershell -command "&{ start-process powershell -verb RunAs}"
