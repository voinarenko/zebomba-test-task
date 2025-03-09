using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.Menu.UI
{
  public class MenuWindow : BaseWindow
  {
    [SerializeField] private Button PlayButton;

    private IGameStateMachine _gameStateMachine;
    private IWindowService _windowService;

    [Inject]
    private void Construct(IGameStateMachine stateMachine, IWindowService windowService)
    {
      Id = WindowId.MenuWindow;

      _gameStateMachine = stateMachine;
      _windowService = windowService;
    }

    private void OnDestroy() => 
      PlayButton.onClick.RemoveAllListeners();
    
    protected override void Initialize() =>
      PlayButton.onClick.AddListener(Play);

    private void Play()
    {
      _windowService.Close(Id);
      _gameStateMachine.Enter<LevelLoadState>();
    }
  }
}