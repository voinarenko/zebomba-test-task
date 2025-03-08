using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
  public class ResultEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;

    public ResultEnterState(
      IGameStateMachine stateMachine) =>
      _stateMachine = stateMachine;

    public void Enter() =>
      _stateMachine.Enter<ResultLoopState>();

    public void Exit()
    {
      
    }
  }
}