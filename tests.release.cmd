@set xunitcmd=".\packages\xunit.runner.console.2.2.0\tools\xunit.console.exe"
@set testdll=".\test\DotNetRanges.Tests\bin\Release\DotNetRanges.Tests.dll"

%xunitcmd% %testdll%