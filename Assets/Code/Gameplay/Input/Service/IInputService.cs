namespace Code.Gameplay.Input.Service
{
  public interface IInputService
  {
    void Enable();
    InputActions GetActions();
    void Disable();
  }
}