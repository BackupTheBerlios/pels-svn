@echo off
copy PortalLogik_ToDo.dll ..\..\..\Server\bin\debug\PortalLogik
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd