"%~dp0..\tools\Nuget.exe" restore "%~dp0..\MyAir3Api.sln"
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" "%~dp0..\MyAir3Api.sln" /target:Clean,Build /p:Configuration=Release;VisualStudioVersion=12.0 /maxcpucount
powershell.exe %~dp0PackageNuget.ps1