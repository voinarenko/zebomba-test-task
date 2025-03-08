using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.StateInfrastructure;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
  public class MenuLoopState : IState
  {
    private readonly IInputService _inputService;

    public MenuLoopState(IInputService inputService) =>
      _inputService = inputService;

    public void Enter() =>
      _inputService.Enable();

    public void Exit() =>
      _inputService.Disable();
  }
}