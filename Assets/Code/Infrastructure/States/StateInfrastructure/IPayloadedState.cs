namespace Code.Infrastructure.States.StateInfrastructure
{
  public interface IPayloadedState<in TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
}