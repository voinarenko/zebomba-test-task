using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Result.UI
{
  public class ResultWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private Button ReplayButton;
    [SerializeField] private Button MenuButton;

    private IGameStateMachine _gameStateMachine;
    private IWindowService _windowService;
    private IProgressProvider _progress;

    [Inject]
    private void Construct(IGameStateMachine stateMachine, IWindowService windowService, IProgressProvider progress)
    {
      _gameStateMachine = stateMachine;
      _windowService = windowService;
      _progress = progress;

      Id = WindowId.ResultWindow;
    }

    private void OnDestroy()
    {
      ReplayButton.onClick.RemoveAllListeners();
      MenuButton.onClick.RemoveAllListeners();
    }
    
    protected override void Initialize()
    {
      SetScore();
      ReplayButton.onClick.AddListener(Replay);
      MenuButton.onClick.AddListener(GoToMenu);
    }

    private void SetScore() => 
      _score.text = $"{_progress.ProgressData.Score}";

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