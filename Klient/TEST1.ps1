Import-AzureRmContext -Path "C:\Program Files\azureprofile.json" 
$PowerState = ((Get-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska -Status).Statuses[1]).code
If ( $PowerState -contains "PowerState/running")
{
Write-Host "PowerState1: running"
}
ElseIf ( $PowerState -contains "PowerState/deallocated")
{
Start-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska
$PowerState = ((Get-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska -Status).Statuses[1]).code
}
Write-Host "PowerState2: $PowerState"