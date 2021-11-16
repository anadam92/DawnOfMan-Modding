REM Notepad++ Regex Replace:	ROBOCOPY %___dev_dir___%\\\1\\bin\\Debug %___output_dir___%\\\1 \1\.dll Info\.json /E
REM Notepad++ Regex Replace:	"C:\\Program Files\\7-Zip\\7z\.exe" a -tzip %___output_dir___%\\\1\.zip %___output_dir___%\\\1
REM Notepad++ Regex Replace:	RMDIR %___output_dir___%\\\1 /S /Q

SET ___dev_dir___=%~dp0
SET ___output_dir___=%userprofile%\Downloads\DawnOfMan_mods_umm

ROBOCOPY %___dev_dir___%\CameraHigh\bin\Debug %___output_dir___%\CameraHigh CameraHigh.dll Info.json /E
ROBOCOPY %___dev_dir___%\EnableDevelopmentScenarios\bin\Debug %___output_dir___%\EnableDevelopmentScenarios EnableDevelopmentScenarios.dll Info.json /E
ROBOCOPY %___dev_dir___%\IncreaseChartUpdateSpeed\bin\Debug %___output_dir___%\IncreaseChartUpdateSpeed IncreaseChartUpdateSpeed.dll Info.json /E
ROBOCOPY %___dev_dir___%\IndependentPause\bin\Debug %___output_dir___%\IndependentPause IndependentPause.dll Info.json /E
ROBOCOPY %___dev_dir___%\MoreInputKeys\bin\Debug %___output_dir___%\MoreInputKeys MoreInputKeys.dll Info.json /E
ROBOCOPY %___dev_dir___%\OutputGeneratedTerrain\bin\Debug %___output_dir___%\OutputGeneratedTerrain OutputGeneratedTerrain.dll Info.json /E
ROBOCOPY %___dev_dir___%\OutputXmlResources\bin\Debug %___output_dir___%\OutputXmlResources OutputXmlResources.dll Info.json /E
ROBOCOPY %___dev_dir___%\RaiderAlert\bin\Debug %___output_dir___%\RaiderAlert RaiderAlert.dll Info.json /E

"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\CameraHigh.zip %___output_dir___%\CameraHigh
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\EnableDevelopmentScenarios.zip %___output_dir___%\EnableDevelopmentScenarios
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\IncreaseChartUpdateSpeed.zip %___output_dir___%\IncreaseChartUpdateSpeed
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\IndependentPause.zip %___output_dir___%\IndependentPause
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\MoreInputKeys.zip %___output_dir___%\MoreInputKeys
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\OutputGeneratedTerrain.zip %___output_dir___%\OutputGeneratedTerrain
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\OutputXmlResources.zip %___output_dir___%\OutputXmlResources
"C:\Program Files\7-Zip\7z.exe" a -tzip %___output_dir___%\RaiderAlert.zip %___output_dir___%\RaiderAlert

RMDIR %___output_dir___%\CameraHigh /S /Q
RMDIR %___output_dir___%\EnableDevelopmentScenarios /S /Q
RMDIR %___output_dir___%\IncreaseChartUpdateSpeed /S /Q
RMDIR %___output_dir___%\IndependentPause /S /Q
RMDIR %___output_dir___%\MoreInputKeys /S /Q
RMDIR %___output_dir___%\OutputGeneratedTerrain /S /Q
RMDIR %___output_dir___%\OutputXmlResources /S /Q
RMDIR %___output_dir___%\RaiderAlert /S /Q