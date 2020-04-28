Import-Module powerGate
#Disconnect-ERP -Service "http://$($ENV:Computername):8080/coolOrange/DynamicsNav"
Connect-ERP -Service "http://$($ENV:Computername):8080/coolOrange/DynamicsNav"


$categories = Get-ERPObjects -EntitySet "Categories"
$unitsOfMeasure = Get-ERPObjects -EntitySet "UnitsOfMeasure"
$vendors = Get-ERPObjects -EntitySet "Vendors"


$materials = Get-ERPObjects -EntitySet "Materials"
$material = Get-ERPObject -EntitySet "Materials" -Keys @{Number="1000"}
$newMaterial = New-ERPObject -EntityType "Material" -Properties @{ Number="4711";Description="test";UnitOfMeasure="KG";MaterialType="Alu"}
$material = Add-ERPObject -EntitySet "Materials" -Properties $newMaterial
$material = Update-ERPObject -EntitySet "Materials" -Keys @{Number="4711"} -Properties @{Description="changed"}

$bom = Get-ERPObject -EntitySet "BomHeaders" -Keys @{Number="1000"} -Expand @('BomRows')
$newBomRow = New-ERPObject -EntityType "BomRow" -Properties @{ParentNumber = "1000"; ChildNumber = "LS-100"; Quantity = 4.3; Position = 12}
$newBom = New-ERPObject -EntityType "BomHeader" -Properties @{Number="1000"; BomRows = @($newBomRow)}
$bom = Add-ERPObject -EntitySet "BomHeaders" -Properties $newBom

Add-ERPObject -EntitySet "BomRows" -Keys 


$d = New-ERPObject -EntityType "Document"
$d.Number = "1000"
$d.Description = "Blower.idw.pdf"
$d.FileName = "Blower.idw.pdf"
Add-ERPMedia -EntitySet "Documents" -Properties $d -ContentType "application/pdf" -File "C:\TEMP\Blower.idw.pdf"

$d = New-ERPObject -EntityType "Document"
$d.Number = "1000"
$d.Description = "TEST"
$d.FileName = "test.pdf"
Add-ERPMedia -EntitySet "Documents" -Properties $d -ContentType "application/pdf" -File "C:\TEMP\test.pdf"

$documents = Get-ERPObjects -EntitySet "Documents" -Filter "Number eq '1000'"
Get-ERPMedia -EntitySet "Documents" -Keys @{Number="1000"; FileName="Blower.idw.pdf"} -File "C:\TEMP\Download_MC.pdf"

Update-ERPMedia -EntitySet "Documents" -File "C:\TEMP\Download_MC.pdf" -Keys @{Number="1000"; FileName="test.pdf"}
Update-ERPObject -EntitySet "Documents" -Keys @{Number="1000"; FileName="test.pdf"} -Properties @{Description="Test.pdf"}

Remove-ERPObject -EntitySet "Documents" -Keys @{Number="1000"; FileName="test.pdf"}