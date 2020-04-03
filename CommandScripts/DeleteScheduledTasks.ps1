# $Users = Get-LocalGroupMember -Group "Administrators"
# $UserName = ""
# for($i = 0; $i -lt $Users.Count; $i++) {
#     if ($Users[$i].Name.Contains("Administrator") -eq $false) {
#         $UserName = $Users[$i].Name
#     }
# }
    
schtasks /delete /tn "StreamWatcher\LaunchTargetSite" /f 
schtasks /delete /tn "StreamWatcher\CloseTargetSite" /f 