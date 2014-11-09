$parentDirectory = split-path $PSScriptRoot -parent
$nugetPath = join-path $parentDirectory ".nuget\nuget.exe"

write-host "nuget" $nugetPath

$outputDirectory = join-path $parentDirectory -childpath "nuget output"

Function Package($nuspec, $buildSource, $coreLibPath)
{
	$tempName = [System.Guid]::NewGuid().ToString()
	$packagePath = join-path $env:temp $tempName
	$corePackagePath = join-path $packagePath -childpath "core"
	$coreLibPath = join-path $corePackagePath -childpath $coreLibPath
	$nuspecSource = join-path $PSScriptRoot $nuspec

	New-Item -ItemType directory -Path $packagePath | Out-Null
	New-Item -ItemType directory -Path $corePackagePath | Out-Null
	New-Item -ItemType directory -Path $coreLibPath | Out-Null
	Copy-Item $nuspecSource $corePackagePath
	Copy-Item $buildSource $coreLibPath -Recurse

	Push-Location -Path $corePackagePath

	& $nugetPath Pack $nuspec
	Copy-Item "*.nupkg" $outputDirectory

	Pop-Location

	Remove-Item -Recurse -Force $packagePath
}



if (Test-Path -Path $outputDirectory){
    Remove-Item -Recurse -Force $outputDirectory
}
New-Item -ItemType directory -Path $outputDirectory | Out-Null

$45buildPath = join-path $parentDirectory "MyAir3Api\bin\Release\*"
Package "MyAir3Api.nuspec" $45buildPath "lib\net45"

$MF43buildPath = join-path $parentDirectory "MFMyAir3Api\bin\Release\*"
Package "MFMyAir3Api.nuspec" $MF43buildPath "lib\netmf43"