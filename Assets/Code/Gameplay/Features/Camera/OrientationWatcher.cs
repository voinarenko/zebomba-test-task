using UnityEngine;

namespace Code.Gameplay.Features.Camera
{
  public class OrientationWatcher : MonoBehaviour
  {
    [SerializeField] private RectTransform _screen;
    [SerializeField] private CameraCorrector _cameraCorrector;
    private float _lastScreenWidth;
    private float _currentRatio;
    private float _previousRatio;

    private void Start()
    {
      GetCurrentRatio();
      _cameraCorrector.SetCameraSize(_currentRatio);
      _previousRatio = _currentRatio;
    }

    private void OnRectTransformDimensionsChange()
    {
      var currentScreenWidth = _screen.rect.width;
      if (Mathf.Approximately(currentScreenWidth, _lastScreenWidth)) return;
      _lastScreenWidth = currentScreenWidth;
      GetCurrentRatio();
      if (Mathf.Approximately(_currentRatio, _previousRatio))
        return;

      _cameraCorrector.SetCameraSize(_currentRatio);
      _previousRatio = _currentRatio;
    }

    private void GetCurrentRatio() =>
      _currentRatio = (float)Screen.height / Screen.width;
  }
}