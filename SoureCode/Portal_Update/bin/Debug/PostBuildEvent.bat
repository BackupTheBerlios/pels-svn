@echo off
copy Portal_Update.dll ..\..\..\Server\bin\debug\PortalLogik
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd