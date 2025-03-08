using UnityEngine.InputSystem;

namespace Code.Gameplay.Input.Service
{
  public class InputService : IInputService
  {
    private readonly InputActions _controls = new();

    public void Enable()
    {
      _controls.Enable();
      ActivateTouch();
    }

    public InputActions GetActions() =>
      _controls;

    public void Disable() =>
      _controls.Disable();

    private void ActivateTouch()
    {
      InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
      Touchscreen.current?.MakeCurrent();
      InputSystem.EnableDevice(Touchscreen.current);
    }
  }
}