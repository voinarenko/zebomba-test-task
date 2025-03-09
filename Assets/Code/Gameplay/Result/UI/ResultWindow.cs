using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Result.UI
{
  public class ResultWindow : BaseWindow
  {
    [SerializeField] private Button ReplayButton;
    [SerializeField] private Button MenuButton;

    private IGameStateMachine _gameStateMachine;
    private IWindowService _windowService;

    [Inject]
    private void Construct(IGameStateMachine stateMachine, IWindowService windowService)
    {
      Id = WindowId.ResultWindow;

      _gameStateMachine = stateMachine;
      _windowService = windowService;
    }

    protected override void Initialize()
    {
      ReplayButton.onClick.AddListener(Replay);
      MenuButton.onClick.AddListener(GoToMenu);
    }

    private void Replay()
    {
      _windowService.Close(Id);
      _gameStateMachine.Enter<LevelLoadState>();
    }

    private void GoToMenu()
    {
      _windowService.Close(Id);
      _gameStateMachine.Enter<MenuLoadState>();
    }
  }
}