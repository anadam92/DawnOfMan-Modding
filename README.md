# DawnOfMan-Modding
## Dawn Of Man Mods. UMM (Unity Mod Manager) required.

This repository contains mods for Dawn Of Man.
It uses [UMM](https://github.com/newman55/unity-mod-manager) for modding assistance and [Harmony](https://github.com/pardeike/Harmony) for patching the main Assembly-CSharp.dll

- ### CameraHigh  
  Less restrictions in the main CameraMode (CameraModeInteractive). Can now go far higher and view top-down.
- ### EnableDevelopmentScenarios  
  The game has built-in development scenarios. This mod activates them.
- ### IncreaseChartUpdateSpeed  
  10 times more chart updates.
- ### IndependentPause  
  Now you can pause regardless of GamePlay mod (Normal, Hardcore) and regardless of whether Primal Vision is activated. You can even fast-forward while Primal Vision is activated.
- ### MoreInputKeys  
  This mod makes most of the keyboard keys valid to enter as keyboard shortcuts.
- ### OutputGeneratedTerrain  
  Creates a sub-folder named "OutputGeneratedTerrain_Data" in the game main folder (%userprofile%\Documents\DawnOfMan) and outputs there gzipped serialized generated terrain data. Can be used to create overview images for any map.
- ### OutputXmlResources  
  Creates a sub-folder named "OutputXmlResources_Data" in the game main folder (%userprofile%\Documents\DawnOfMan) and outputs there built-in xml data. Useful for finding all the numeric parameters of the gameplay.
- ### RaiderAlert  
  Automatically turns on Alert when a raider attack is launched and also when tension starts. Normally the player gets notified about the raider attack when tension starts, but raiders appear on a size of the map long before.

## Usage
To use these mods:
1. [Install UMM](https://www.nexusmods.com/site/mods/21)
2. Download a release from this repository. [1.0.0](https://github.com/anadam92/DawnOfMan-Modding/releases/download/1.0.0/DawnOfMan_mods_umm.7z)
3. Drag & Drop the mods (zip files) you want in the "Mods" tab of UMM.

While playing press Ctrl+F10 to toggle on/off any mod.
