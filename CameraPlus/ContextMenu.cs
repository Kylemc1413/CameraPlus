using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LogLevel = IPA.Logging.Logger.Level;

namespace CameraPlus
{
    public class ContextMenu : MonoBehaviour
    {
        internal Vector2 menuPos
        {
            get
            {
                return new Vector2(
                   Mathf.Min(mousePosition.x / (Screen.width / 1600f), (Screen.width * (0.806249998f / (Screen.width / 1600f)))),
                   Mathf.Min(mousePosition.y / (Screen.height / 900f), (Screen.height * (0.555555556f / (Screen.height / 900f)))));
            }
        }

        internal Vector2 mousePosition;
        internal bool showMenu;
        internal bool layoutMode = false;
        internal bool verify38 = false;
        internal CameraPlusBehaviour parentBehaviour;

        public void Awake()
        {
        }

        public void EnableMenu(Vector2 mousePos, CameraPlusBehaviour parentBehaviour)
        {
            this.enabled = true;
            // Console.WriteLine("Enable Menu");
            mousePosition = mousePos;
            showMenu = true;
            this.parentBehaviour = parentBehaviour;
            layoutMode = false;
            verify38 = false;
        }

        public void DisableMenu()
        {
            this.enabled = false;
            // Console.WriteLine("Disable Menu");
            showMenu = false;
        }

        void OnGUI()
        {

            if (showMenu)
            {
                Vector3 scale;
                float originalWidth = 1600f;
                float originalHeight = 900f;


                scale.x = Screen.width / originalWidth;
                scale.y = Screen.height / originalHeight;
                scale.z = 1;
                Matrix4x4 originalMatrix = GUI.matrix;
                GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);
                // Layer boxes for Opacity
                GUI.Box(new Rect(menuPos.x - 5, menuPos.y, 310, 400), "CameraPlus");
                GUI.Box(new Rect(menuPos.x - 5, menuPos.y, 310, 400), "CameraPlus");
                GUI.Box(new Rect(menuPos.x - 5, menuPos.y, 310, 400), "CameraPlus");
                if (verify38)
                {
                    GUI.Box(new Rect(menuPos.x, menuPos.y + 35, 290, 70), "Are You Sure?");
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 75, 140, 30), new GUIContent("Yes.")))
                    {
                        parentBehaviour.StartCoroutine(CameraUtilities.Spawn38Cameras());
                        verify38 = false;
                        parentBehaviour.CloseContextMenu();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 155, menuPos.y + 75, 140, 30), new GUIContent("No.")))
                    {
                        verify38 = false;
                    }
                }
                else if (!layoutMode)
                {
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 25, 120, 30), new GUIContent("Add New Camera")))
                    {
                        lock (Plugin.Instance.Cameras)
                        {
                            string cameraName = CameraUtilities.GetNextCameraName();
                            Logger.Log($"Adding new config with name {cameraName}.cfg");
                            CameraUtilities.AddNewCamera(cameraName);
                            CameraUtilities.ReloadCameras();
                            parentBehaviour.CloseContextMenu();
                        }
                    }
                    if (GUI.Button(new Rect(menuPos.x + 130, menuPos.y + 25, 170, 30), new GUIContent("Remove Selected Camera")))
                    {
                        lock (Plugin.Instance.Cameras)
                        {
                            if (CameraUtilities.RemoveCamera(parentBehaviour))
                            {
                                parentBehaviour._isCameraDestroyed = true;
                                parentBehaviour.CreateScreenRenderTexture();
                                parentBehaviour.CloseContextMenu();
                                Logger.Log("Camera removed!", LogLevel.Notice);
                            }
                        }
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 65, 170, 30), new GUIContent("Duplicate Selected Camera")))
                    {
                        lock (Plugin.Instance.Cameras)
                        {
                            string cameraName = CameraUtilities.GetNextCameraName();
                            Logger.Log($"Adding {cameraName}", LogLevel.Notice);
                            CameraUtilities.AddNewCamera(cameraName, parentBehaviour.Config);
                            CameraUtilities.ReloadCameras();
                            parentBehaviour.CloseContextMenu();
                        }
                    }
                    if (GUI.Button(new Rect(menuPos.x + 180, menuPos.y + 65, 120, 30), new GUIContent("Layout")))
                    {
                        layoutMode = true;
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 105, 120, 30), new GUIContent(parentBehaviour.Config.thirdPerson ? " First Person" : "Third Person")))
                    {
                        parentBehaviour.Config.thirdPerson = !parentBehaviour.Config.thirdPerson;
                        parentBehaviour.ThirdPerson = parentBehaviour.Config.thirdPerson;
                        parentBehaviour.ThirdPersonPos = parentBehaviour.Config.Position;
                        parentBehaviour.ThirdPersonRot = parentBehaviour.Config.Rotation;
                        // FirstPersonOffset = Config.FirstPersonPositionOffset;
                        // FirstPersonRotationOffset = Config.FirstPersonRotationOffset;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.CloseContextMenu();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 130, menuPos.y + 105, 170, 30), new GUIContent(parentBehaviour.Config.showThirdPersonCamera ? "Hide Third Person Camera" : "Show Third Person Camera")))
                    {

                        parentBehaviour.Config.showThirdPersonCamera = !parentBehaviour.Config.showThirdPersonCamera;
                        parentBehaviour.Config.Save();
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.CloseContextMenu();
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 145, 170, 30), new GUIContent(parentBehaviour.Config.forceFirstPersonUpRight ? "Don't Force Camera Upright" : "Force Camera Upright")))
                    {

                        parentBehaviour.Config.forceFirstPersonUpRight = !parentBehaviour.Config.forceFirstPersonUpRight;
                        parentBehaviour.Config.Save();
                        parentBehaviour.CloseContextMenu();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 180, menuPos.y + 145, 120, 30), new GUIContent(parentBehaviour.Config.transparentWalls ? "Solid Walls" : "Transparent Walls")))
                    {
                        parentBehaviour.Config.transparentWalls = !parentBehaviour.Config.transparentWalls;
                        parentBehaviour.SetCullingMask();
                        parentBehaviour.CloseContextMenu();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 185, 300, 30), new GUIContent("Close Menu")))
                    {
                        parentBehaviour.CloseContextMenu();
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 355, 300, 30), new GUIContent("Spawn 38 Cameras")))
                    {
                        verify38 = true;
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 25, 290, 30), new GUIContent("Reset Camera Position and Rotation")))
                    {

                        parentBehaviour.Config.Position = parentBehaviour.Config.DefaultPosition;
                        parentBehaviour.Config.Rotation = parentBehaviour.Config.DefaultRotation;
                        parentBehaviour.Config.FirstPersonPositionOffset = parentBehaviour.Config.DefaultFirstPersonPositionOffset;
                        parentBehaviour.Config.FirstPersonRotationOffset = parentBehaviour.Config.DefaultFirstPersonRotationOffset;
                        parentBehaviour.ThirdPersonPos = parentBehaviour.Config.DefaultPosition;
                        parentBehaviour.ThirdPersonRot = parentBehaviour.Config.DefaultRotation;
                        parentBehaviour.Config.Save();
                        parentBehaviour.CloseContextMenu();
                    }
                    // Layer
                    GUI.Box(new Rect(menuPos.x, menuPos.y + 65, 290, 70), "Layer: " + parentBehaviour.Config.layer);
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 85, 140, 30), new GUIContent("-")))
                    {
                        parentBehaviour.Config.layer--;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 155, menuPos.y + 85, 140, 30), new GUIContent("+")))
                    {
                        parentBehaviour.Config.layer++;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    // FOV
                    GUI.Box(new Rect(menuPos.x, menuPos.y + 125, 290, 70), "FOV: " + parentBehaviour.Config.fov);
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 145, 140, 30), new GUIContent("-")))
                    {
                        parentBehaviour.Config.fov--;
                        parentBehaviour.SetFOV();
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 155, menuPos.y + 145, 140, 30), new GUIContent("+")))
                    {
                        parentBehaviour.Config.fov++;
                        parentBehaviour.SetFOV();
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    // Render Scale
                    GUI.Box(new Rect(menuPos.x, menuPos.y + 185, 290, 70), "Render Scale: " + parentBehaviour.Config.renderScale.ToString("F1"));
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 205, 140, 30), new GUIContent("-")))
                    {
                        parentBehaviour.Config.renderScale -= 0.1f;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x + 155, menuPos.y + 205, 140, 30), new GUIContent("+")))
                    {
                        parentBehaviour.Config.renderScale += 0.1f;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 255, 290, 30), new GUIContent(parentBehaviour.Config.fitToCanvas ? " Don't Fit To Canvas" : "Fit To Canvas")))
                    {
                        parentBehaviour.Config.fitToCanvas = !parentBehaviour.Config.fitToCanvas;
                        parentBehaviour.CreateScreenRenderTexture();
                        parentBehaviour.Config.Save();
                    }
                    if (GUI.Button(new Rect(menuPos.x, menuPos.y + 305, 290, 30), new GUIContent("Close Layout Menu")))
                    {
                        layoutMode = false;
                    }


                }

                GUI.matrix = originalMatrix;
            }
        }
    }
}
