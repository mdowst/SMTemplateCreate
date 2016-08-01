# SMTemplateCreate
A light weight executable that will allow you to create a Work Item in Service Manager 2012 from a template with prefixes for all child activities.

You can download just the executable [here](/SMTemplateCreate.zip)

To create a Work Item from a template you need to pass two parameters. 
- TemplateId - Should be the Guid of the template to create the Work Item from
- ComputerName (optional) - The name of your management server. Defaults to localhost if not specified.

EXAMPLE:   SMTemplateCreate.exe /computername sm1 /templateid e0287ab6-089e-5172-0534-49edbd841f34

You can get the TemplateId by using Service Manager PowerShell Cmdlet Get-SCSMObjectTemplate. 

Get-SCObjectTemplate -DisplayName "Major Change Request" | FT Id
