Import-AzureRmContext -Path "C:\Program Files\azureprofile.json"
$PowerState = ((Get-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska -
Status).Statuses[1]).code
If ( $PowerState -contains "PowerState/running")
{
Write-Host "PowerState12: $PowerState"
Stop-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska -Force
$PowerState = ((Get-AzureRmVM -Name Janusz -ResourceGroupName Inzynierska -
Status).Statuses[1]).code
}
Write-Host "PowerState13: $PowerState
