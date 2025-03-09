using Code.Gameplay.Features.Movables.Factory;
using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoopState : IState
  {
    private readonly IInputService _inputService;
    private ICircleFactory _circleFactory;

    public LevelLoopState(IInputService inputService, ICircleFactory circleFactory)
    {
      _circleFactory = circleFactory;
      _inputService = inputService;
    }

    public void Enter() =>
      _inputService.Enable();

    public void Exit()
    {
      _circleFactory.ClearPool();
      _inputService.Disable();
    }
  }
}