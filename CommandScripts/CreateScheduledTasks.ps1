function GetConfigData() {
    # from https://serverfault.com/questions/186030/how-to-use-a-config-file-ini-conf-with-a-powershell-script

    Get-Content "$($CurrentDirectory)\config.txt" | foreach-object -begin {$data=@{}} -process { $k = [regex]::split($_,'='); if(($k[0].CompareTo("") -ne 0) -and ($k[0].StartsWith("[") -ne $True)) { $data.Add($k[0], $k[1]) } }
    return $data
}

# Get the current directory of code
$InvocatedScriptpath = $MyInvocation.MyCommand.Path
$CurrentDirectory = Split-Path $InvocatedScriptpath

# Get data from, config file
$Config = GetConfigData $CurrentDirectory

# $StartDate = New-Object -TypeName DateTime -ArgumentList:(2020,04,02)

# $StartDateRaw = Get-Date -Format "yyyy,MM,dd"
# $StartDate = New-Object -TypeName DateTime -ArgumentList:($StartDateRaw)

$StartDate = Get-Date -DisplayHint DateTime
$FormatHack = ($([System.Globalization.DateTimeFormatInfo]::CurrentInfo.ShortDatePattern) -replace 'M+/', 'MM/') -replace 'd+/', 'dd/'
$LaunchTargetTaskName = $Config.LaunchTargetTaskName

$CloseTargetTaskName = $Config.CloseTargetTaskName
# $ScriptPath = "C:\Users\Nilesh Magan\Documents\Code\Powershell\StreamWatcher.ps1"
# $ScriptPath = "C:\Users\Nilesh Magan\Documents\Code\Powershell\test.ps1"

# $CurrentDirectory = (Get-Item -Path ".\").FullName
$LaunchTargetScriptPath = "$($CurrentDirectory)\$($Config.LaunchTargetScriptName)"
$CloseTargetScriptPath = "$($CurrentDirectory)\$($Config.CloseTargetScriptName)"
$LaunchTargetTaskName -replace ' ', '` '
$CloseTargetTaskName -replace ' ', '` '
$LaunchTargetScriptPath -replace ' ', '` '
$CloseTargetScriptPath -replace ' ', '` '

$Users = Get-LocalGroupMember -Group "Administrators"
$UserName = ""
for($i = 0; $i -lt $Users.Count; $i++) {
    if ($Users[$i].Name.Contains("Administrator") -eq $false) {
        $UserName = $Users[$i].Name
    }
}
# /ru "DESKTOP-AU9EAQO\Nilesh Magan" `
# /ru System `
# /ru -RunAsAdministrator `

schtasks.exe /create `
    /tn $LaunchTargetTaskName `
    /tr $LaunchTargetScriptPath `
    /sc DAILY `
    /st $Config.StartTime `
    /sd $StartDate.ToString($FormatHack) `
    /ru $UserName `
    /rl HIGHEST 

schtasks.exe /create `
    /tn $CloseTargetTaskName `
    /tr $CloseTargetScriptPath `
    /sc ONCE `
    /st $Config.StopTime `
    /sd $StartDate.ToString($FormatHack) `
    /ru $UserName `
    /rl HIGHEST 