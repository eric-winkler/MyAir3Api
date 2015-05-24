"%~dp0..\tools\Nuget.exe" restore "%~dp0..\MyAir3Api.sln"
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "%~dp0..\MyAir3Api.sln" /target:Clean,Build /p:Configuration=Release;VisualStudioVersion=14.0 /maxcpucount
powershell.exe %~dp0PackageNuget.ps1