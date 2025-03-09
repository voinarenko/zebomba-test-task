using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class MenuEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IWindowService _windowService;

    public MenuEnterState(IGameStateMachine stateMachine, IWindowService windowService)
    {
      _windowService = windowService;
      _stateMachine = stateMachine;
    }

    public void Enter()
    {
      _windowService.Open(WindowId.MenuWindow);
      _stateMachine.Enter<MenuLoopState>();
    }

    public void Exit()
    {
      
    }
  }
}