using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Progress.Provider;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class ResultLoopState : IState
  {
    private readonly IInputService _inputService;
    private readonly IProgressProvider _progress;

    public ResultLoopState(IInputService inputService, IProgressProvider progress)
    {
      _inputService = inputService;
      _progress = progress;
    }

    public void Enter() =>
      _inputService.Enable();

    public void Exit()
    {
      _progress.ProgressData.Reset();
      _inputService.Disable();
    }
  }
}