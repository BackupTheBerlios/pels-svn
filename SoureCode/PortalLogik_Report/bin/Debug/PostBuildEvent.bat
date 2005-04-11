@echo off
copy PortalLogik_Report.dll ..\..\..\Server\bin\debug\PortalLogik
copy ..\..\*.rpt ..\..\..\Server\bin\debug\ReportVorlagen
del ..\..\..\Server\bin\debug\ReportVorlagen\{*}.rpt 
if errorlevel 1 goto CSharpReportError
goto CSharpEnd
:CSharpReportError
echo Project error: A tool returned an error code from the build event
exit 1
:CSharpEnd