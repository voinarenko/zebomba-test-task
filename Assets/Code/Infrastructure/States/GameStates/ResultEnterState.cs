using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class ResultEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IWindowService _windowService;

    public ResultEnterState(IGameStateMachine stateMachine, IWindowService windowService)
    {
      _windowService = windowService;
      _stateMachine = stateMachine;
    }

    public void Enter()
    {
      _windowService.Open(WindowId.ResultWindow);
      _stateMachine.Enter<ResultLoopState>();
    }

    public void Exit()
    {
      
    }
  }
}