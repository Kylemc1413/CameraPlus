# CameraPlus
CameraPlus is a Beat Saber mod that allows for multiple wide FOV cameras with smoothed movement, which makes for a much more pleasant overall spectator experience.
### Make sure to disable the base game's "Smooth Camera" If you wish to use CameraPlus
[Video Comparison](https://youtu.be/MysLXKSXGTY)  
[Third Person Preview](https://youtu.be/ltIhpt-n6b8)
### Right Click Menu Recently Renovated, Appearance may differ from what is shown in this video
[![How to use CameraPlus](https://i.imgur.com/UbKrHAF.png)](https://www.youtube.com/watch?v=RpYoMiKJygQ)

# Installing
1. Use the Beat Saber Mod Manager installer: https://github.com/beat-saber-modding-group/BeatSaberModInstaller/releases
		It is the easiest method, it will do all these steps below in 1 click.
	
### To install manually:
	1b. Make sure that Beat Saber is not running.
	2b. Extract the contents of the zip into Beat Saber's installation folder.
		For Oculus Home: \Oculus Apps\Software\hyperbolic-magnetism-beat-saber\
		For Steam: \steamapps\common\Beat Saber\
		(The folder that contains Beat Saber.exe)
	3b. Done! You've installed the CameraPlus Plugin.
# Usage
To edit the settings of any camera in real time, right click on the Beat Saber game window! A context menu will appear with options specific to the camera that you right clicked on!

Press <kbd>F1</kbd> to toggle the main camera between first and third person.

After you run the game once, a `cameraplus.cfg` file is created within the folder Beat Saber\UserData\CameraPlus. Any cfg files located in this folder will be used to render additional cameras.
Edit that file to configure CameraPlus:

| Parameter                   | Description                                                                                  |
|-----------------------------|----------------------------------------------------------------------------------------------|
| **fov**                     | Horizontal field of view of the camera                                                       |
| **antiAliasing**            | Anti-aliasing setting for the camera (1, 2, 4 or 8 only)                                     |
| **renderScale**             | The resolution scale of the camera relative to game window (similar to supersampling for VR) |
| **positionSmooth**          | How much position should smooth **(SMALLER NUMBER = SMOOTHER)**                              |
| **rotationSmooth**          | How much rotation should smooth **(SMALLER NUMBER = SMOOTHER)**                              |
| **cam360Smoothness**        | How smoothly the 360 cam snaps to lane changes **(SMALLER NUMBER = SMOOTHER)**               |
| **thirdPerson**             | Whether third person camera is enabled                                                       |
| **showThirdPersonCamera**   | Whether or not the third person camera is visible                                            |
| **use360Camera**            | Whether the 360 camera is used                                                               |
| **posx**                    | X position of third person camera                                                            |
| **posy**                    | Y position of third person camera                                                            |
| **posz**                    | Z position of third person camera                                                            |
| **angx**                    | X rotation of third person camera                                                            |
| **angy**                    | Y rotation of third person camera                                                            |
| **angz**                    | Z rotation of third person camera                                                            |
| **firstPersonPosOffsetX**   | X position offset of first person camera                                                     |
| **firstPersonPosOffsetY**   | Y position offset of first person camera                                                     |
| **firstPersonPosOffsetZ**   | Z position offset of first person camera                                                     |
| **firstPersonRotOffsetX**   | X rotation offset of first person camera                                                     |
| **firstPersonRotOffsetY**   | Y rotation offset of first person camera                                                     |
| **firstPersonRotOffsetZ**   | Z rotation offset of first person camera                                                     |
| **cam360ForwardOffset**     | 360 camera position offset forwards                                                          |
| **cam360UpOffset**          | 360 camera position offset upwards                                                           |
| **cam360RightOffset**       | 360 camera position offset to the right                                                      |
| **cam360XTilt**             | X tilt rotation relative to the forward of 360 camera                                        |
| **cam360ZTilt**             | Z tilt rotation relative to the forward of 360 camera                                        |
| **cam360YTilt**             | The Y tilt rotation OFFSET on the 360 camera                                                 |
| **screenWidth**             | Width of the camera render area                                                              |
| **screenHeight**            | Height of the camera render area                                                             |
| **screenPosX**              | X position of the camera in the Beat Saber window                                            |
| **screenPosY**              | Y position of the camera in the Beat Saber window                                            |
| **layer**                   | Layer to render the camera on **(HIGHER NUMBER = top)**                                      |
| **fitToCanvas**             | Force camera to stretch to fit window                                                        |
| **transparentWalls**        | Make Walls appear transparent on the camera                                                  |
| **forceFirstPersonUpRight** | Lock rotation of first person camera on Z axis to keep the camera upright                    |
| **movementScriptPath**      | Full path to the movement script file to associate with the camera                           |

If you need help, ask us at the Beat Saber Mod Group Discord Server:  
https://discord.gg/BeatSaberMods

## For developers

### Contributing to CameraPlus
In order to build this project, please create the file `CameraPlus.csproj.user` and add your Beat Saber directory path to it in the project directory.
This file should not be uploaded to GitHub and is in the .gitignore.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Set "YOUR OWN" Beat Saber folder here to resolve most of the dependency paths! -->
    <BeatSaberDir>E:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
  </PropertyGroup>
</Project>
```

If you plan on adding any new dependencies which are located in the Beat Saber directory, it would be nice if you edited the paths to use `$(BeatSaberDir)` in `CameraPlus.csproj`

```xml
...
<Reference Include="BS_Utils">
  <HintPath>$(BeatSaberDir)\Plugins\BS_Utils.dll</HintPath>
</Reference>
<Reference Include="IPA.Loader">
  <HintPath>$(BeatSaberDir)\Beat Saber_Data\Managed\IPA.Loader.dll</HintPath>
</Reference>
...
```