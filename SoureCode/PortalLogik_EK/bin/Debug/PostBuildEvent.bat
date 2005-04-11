@echo off
copy PortalLogik_EK.dll ..\..\..\Server\bin\debug\PortalLogik\PortalLogik_EK.dll
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd