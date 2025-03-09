using UnityEngine;

namespace Code.Gameplay.Features.Camera
{
  public class CameraCorrector : MonoBehaviour
  {
    private const float MinCameraSize = 5;
    private const float MaxCameraSize = 8.3f;
    private const int BaseScreenWidth = 1080;
    private const int MinBaseScreenHeight = 1698;
    private const int MaxBaseScreenHeight = 3000;

    private const float MinAspectRatio = 1.572222f;

    private UnityEngine.Camera _camera;

    public void SetCameraSize(float ratio)
    {
      _camera ??= UnityEngine.Camera.main;
      if (ratio > MinAspectRatio)
        _camera.orthographicSize = MinCameraSize + (MaxCameraSize - MinCameraSize) /
          (MaxBaseScreenHeight - MinBaseScreenHeight) * (ratio * BaseScreenWidth - MinBaseScreenHeight);
      else
        _camera.orthographicSize = MinCameraSize;
    }
  }
}