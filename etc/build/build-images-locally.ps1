param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"
$appFolder = Join-Path $slnFolder "ChoreoSample"


Write-Host "********* BUILDING Application *********" -ForegroundColor Green

$hostFolder = Join-Path $slnFolder "ChoreoSample.Host"
Set-Location $hostFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t choreosample:$version .

### ALL COMPLETED
Write-Host "********* COMPLETED *********" -ForegroundColor Green
Set-Location $currentFolder
exit $LASTEXITCODE