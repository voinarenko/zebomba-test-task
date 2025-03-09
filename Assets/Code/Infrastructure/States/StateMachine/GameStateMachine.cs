using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.StateMachine
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly IStateFactory _stateFactory;
    private IState _activeState;

    public GameStateMachine(IStateFactory stateFactory) =>
      _stateFactory = stateFactory;

    public void Enter<TState>() where TState : class, IState
    {
      _activeState?.Exit();
      var state = _stateFactory.GetState<TState>();
      _activeState = state;
      _activeState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
      _stateFactory.GetState<TState>().Enter(payload);
  }
}