@echo off
for /f %%i in ('gh release view -R Vicizlat/SquirrelDeploymentTest --json tagName --jq .tagName') do set var=%%i
echo Latest version is %var%
set var2=%1
echo Current version is %var2%
if %var% geq %var2% goto :END
set /p answer=Do you want to create a new release [y/n]?:
if %answer% neq y goto :END
echo Making a new release...
set title="%3 v.%var2%"
echo %title%
gh release create -R Vicizlat/SquirrelDeploymentTest %var2% %2\Setup.exe -t %title% --generate-notes
gh release upload -R Vicizlat/SquirrelDeploymentTest %var2% %2\RELEASES
gh release upload -R Vicizlat/SquirrelDeploymentTest %var2% %2\SquirrelDeploymentTest-%var2%-full.nupkg
gh release upload -R Vicizlat/SquirrelDeploymentTest %var2% %2\SquirrelDeploymentTest-%var2%-delta.nupkg
:END
echo Script finished