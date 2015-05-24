$parentDirectory = split-path $PSScriptRoot -parent
$nugetPath = join-path $parentDirectory "tools\nuget.exe"
$nuspecSource = join-path $PSScriptRoot "MyAir3Api.nuspec"
$45buildPath = join-path $parentDirectory "MyAir3Api\bin\Release\*"
$MF43buildPath = join-path $parentDirectory "MFMyAir3Api\bin\Release\*"
$UAPbuildPath = join-path $parentDirectory "MyAir3Api.UAP\bin\Release\*"


$outputDirectory = join-path $parentDirectory -childpath "nuget output"
if (Test-Path -Path $outputDirectory){
    Remove-Item -Recurse -Force $outputDirectory
}
New-Item -ItemType directory -Path $outputDirectory | Out-Null

$tempName = [System.Guid]::NewGuid().ToString()
$packagePath = join-path $env:temp $tempName
$corePackagePath = join-path $packagePath -childpath "core"
$45libPath = join-path $corePackagePath -childpath "lib\net45"
$UAPlibPath = join-path $corePackagePath -childpath "lib\netcore45"
$MF43libPath = join-path $corePackagePath -childpath "lib\netmf43"

New-Item -ItemType directory -Path $packagePath | Out-Null
New-Item -ItemType directory -Path $corePackagePath | Out-Null
New-Item -ItemType directory -Path $45libPath | Out-Null
New-Item -ItemType directory -Path $MF43libPath | Out-Null
New-Item -ItemType directory -Path $UAPlibPath | Out-Null
Copy-Item $nuspecSource $corePackagePath
Copy-Item $45buildPath $45libPath -Recurse
Copy-Item $MF43buildPath $MF43libPath -Recurse
Copy-Item $UAPbuildPath $UAPlibPath -Recurse

Push-Location -Path $corePackagePath

& $nugetPath Pack $nuspec
Copy-Item "*.nupkg" $outputDirectory

Pop-Location

Remove-Item -Recurse -Force $packagePath
