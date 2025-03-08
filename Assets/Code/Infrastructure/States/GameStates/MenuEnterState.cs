using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class MenuEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;

    public MenuEnterState(
      IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    public void Enter() =>
      _stateMachine.Enter<MenuLoopState>();

    public void Exit()
    {
      
    }
  }
}