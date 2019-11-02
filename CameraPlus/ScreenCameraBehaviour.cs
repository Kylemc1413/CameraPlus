using UnityEngine;

namespace CameraPlus
{
    /// <summary>
    /// This is the monobehaviour that goes on the camera that handles
    /// displaying the actual feed from the camera to the screen.
    /// </summary>
    public class ScreenCameraBehaviour : MonoBehaviour
    {
        private Camera _cam;
        private RenderTexture _renderTexture;

        public void SetRenderTexture(RenderTexture renderTexture)
        {
            _renderTexture = renderTexture;
        }

        public void SetCameraInfo(Vector2 position, Vector2 size, int layer)
        {
            _cam.pixelRect = new Rect(position, size);
            _cam.depth = layer;
        }

        public void Awake()
        {
            Logger.Log("Created new screen camera behaviour component!");
            DontDestroyOnLoad(gameObject);

            _cam = gameObject.AddComponent<Camera>();
            _cam.clearFlags = CameraClearFlags.Nothing;
            _cam.cullingMask = 0;
            _cam.stereoTargetEye = StereoTargetEyeMask.None;
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (_renderTexture == null) return;
            Graphics.Blit(_renderTexture, dest);
        }
    }
}
