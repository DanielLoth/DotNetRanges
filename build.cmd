@REM **************************************************
@REM * Push the current working directory onto the
@REM * stack.
@REM **************************************************
@pushd %CD%


@REM **************************************************
@REM * Setting environment to VsMSBuildCmd so that we
@REM * can make use of the msbuild command.
@REM **************************************************
@call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsMSBuildCmd.bat"


@REM **************************************************
@REM * Pop working directory from the stack so that
@REM * we're running msbuild in the right location.
@REM **************************************************
@popd REM %CD%

@REM Perform the build
@REM msbuild DotNetRanges.sln /t:Clean /p:Configuration=Release

msbuild DotNetRanges.sln /t:Rebuild /p:Configuration=Release

pause.