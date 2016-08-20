$parentDirectory = split-path $PSScriptRoot -parent
$nugetPath = join-path $parentDirectory "tools\nuget.exe"
$nuspecSource = join-path $PSScriptRoot "MyAir3Api.nuspec"
$MF43buildPath = join-path $parentDirectory "MFMyAir3Api\bin\Release\*"
$netstandardbuildPath = join-path $parentDirectory "MyAir3Api\bin\Release\*"


$outputDirectory = join-path $parentDirectory -childpath "nuget output"
if (Test-Path -Path $outputDirectory){
    Remove-Item -Recurse -Force $outputDirectory
}
New-Item -ItemType directory -Path $outputDirectory | Out-Null

$tempName = [System.Guid]::NewGuid().ToString()
$packagePath = join-path $env:temp $tempName
$corePackagePath = join-path $packagePath -childpath "core"
$MF43libPath = join-path $corePackagePath -childpath "lib\netmf43"
$netstandardlibPath = join-path $corePackagePath -childpath "lib"

New-Item -ItemType directory -Path $packagePath | Out-Null
New-Item -ItemType directory -Path $corePackagePath | Out-Null
New-Item -ItemType directory -Path $MF43libPath | Out-Null
Copy-Item $nuspecSource $corePackagePath
Copy-Item $MF43buildPath $MF43libPath -Recurse
Copy-Item $netstandardbuildPath $netstandardlibPath -Recurse

Push-Location -Path $corePackagePath

& $nugetPath Pack $nuspec
Copy-Item "*.nupkg" $outputDirectory

Pop-Location

Remove-Item -Recurse -Force $packagePath
