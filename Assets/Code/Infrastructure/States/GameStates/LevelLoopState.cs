using Code.Gameplay.Features.Explosion.Factory;
using Code.Gameplay.Features.Movables.Factory;
using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelLoopState : IState
  {
    private readonly IInputService _inputService;
    private readonly ICircleFactory _circleFactory;
    private readonly IExplosionFactory _explosionFactory;

    public LevelLoopState(IInputService inputService, ICircleFactory circleFactory, IExplosionFactory explosionFactory)
    {
      _explosionFactory = explosionFactory;
      _circleFactory = circleFactory;
      _inputService = inputService;
    }

    public void Enter() =>
      _inputService.Enable();

    public void Exit()
    {
      _circleFactory.ClearPool();
      _explosionFactory.ClearPool();
      _inputService.Disable();
    }
  }
}