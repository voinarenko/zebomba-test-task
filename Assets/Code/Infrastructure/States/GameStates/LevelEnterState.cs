using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
  public class LevelEnterState : IState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly IProgressProvider _progressProvider;

    public LevelEnterState(IGameStateMachine stateMachine, IProgressProvider progressProvider)
    {
      _progressProvider = progressProvider;
      _stateMachine = stateMachine;
    }

    public void Enter()
    {
      _progressProvider.SetProgressData(new ProgressData());
      _stateMachine.Enter<LevelLoopState>();
    }

    public void Exit()
    {
      
    }
  }
}