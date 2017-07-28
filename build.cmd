@REM ****************************************************************************************************
@REM * Push the current working directory onto the stack.
@REM ****************************************************************************************************
@pushd %CD%


@REM ****************************************************************************************************
@REM * Setting environment to VsMSBuildCmd so that we can make use of the msbuild command.
@REM ****************************************************************************************************
@call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsMSBuildCmd.bat"


@REM ****************************************************************************************************
@REM * Pop working directory from the stack so that we're running msbuild in the right location.
@REM ****************************************************************************************************
@popd REM %CD%


@REM ****************************************************************************************************
@REM * Perform the build
@REM *
@REM * Note: The following link provides some insight into the build process:
@REM * http://conceptf1.blogspot.com.au/2013/11/visual-studio-clean-build-and-rebuild-solution.html
@REM *
@REM * To summarise the blog post, it states the following:
@REM * msbuild /t:Clean performs the following steps:
@REM * 1. Clean project 1, project 2, ..., project N
@REM * 2. Build project 1, project 2, ..., project N.
@REM *
@REM * On the other hand, msbuild /t:Rebuild performs the following sequence of steps:
@REM * 1. Clean project 1
@REM * 2. Build project 1
@REM * 3. Clean project 2
@REM * 4. Build project 2
@REM * 5. ...
@REM * 6. Clean project N
@REM * 7. Clean project N
@REM ****************************************************************************************************
@REM msbuild DotNetRanges.sln /t:Clean /p:Configuration=Release

IF "%BuildConfiguration%"=="" (set BuildConfiguration="Release")

msbuild DotNetRanges.sln /t:Rebuild /p:Configuration=%BuildConfiguration%