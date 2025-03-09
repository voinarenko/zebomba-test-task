using Code.Gameplay.Features.BottomArea.Services;
using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.BottomArea.Wells.Slots;
using Code.Gameplay.Features.Movables;
using Code.Gameplay.Features.Movables.Factory;
using Code.Gameplay.Input.Service;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Features.BottomArea
{
  public class DropZone : MonoBehaviour
  {
    [SerializeField] private List<Well> _wells;
    [SerializeField] private SpriteRenderer _ropeRenderer;
    private readonly List<Circle> _totalCircles = new();
    private IColorMatchService _colorMatchService;
    private IInputService _inputService;
    private ICircleFactory _circleFactory;
    private ICirclesRemoveService _circlesRemoveService;
    private IGameStateMachine _stateMachine;
    private IProgressProvider _progress;
    private Transform _circlesInWells;
    private Circle _currentCircle;
    private InputActions _controls;

    [Inject]
    public void Construct(
      IInputService inputService, ICircleFactory circleFactory,
      IColorMatchService colorMatchService, ICirclesRemoveService circlesRemoveService,
      IGameStateMachine stateMachine, IProgressProvider progress)
    {
      _progress = progress;
      _stateMachine = stateMachine;
      _circlesRemoveService = circlesRemoveService;
      _inputService = inputService;
      _circleFactory = circleFactory;
      _colorMatchService = colorMatchService;
    }

    private void OnDestroy() =>
      _controls.UI.Drop.performed -= DropCircle;

    public void SetControls() =>
      _controls = _inputService.GetActions();

    public void Subscribe() =>
      _controls.UI.Drop.performed += DropCircle;

    public void SetContainer(Transform wells) =>
      _circlesInWells = wells;

    public void SetCircle(Circle circle) =>
      _currentCircle = circle;

    private Well FindClosestWell(float currentX) =>
      _wells.OrderBy(well => Mathf.Abs(well.transform.position.x - currentX)).First();

    private void DropCircle(InputAction.CallbackContext obj)
    {
      _ropeRenderer.enabled = false;
      _controls.Disable();
      _currentCircle.transform.SetParent(_circlesInWells);

      var well = FindClosestWell(_currentCircle.transform.position.x);
      var slot = well.GetFreeSlot();

      MoveToWellOrNext(well, slot);
    }

    private void MoveToWellOrNext(Well well, Slot slot)
    {
      _currentCircle.transform
        .DOMove(well.transform.position, _currentCircle.Speed)
        .SetEase(Ease.Linear)
        .SetSpeedBased()
        .OnComplete(() =>
        {
          if (slot)
            FallIntoWell(well, slot);
          else
            FindAlternativeWellAndJump(well);
        });
    }

    private void FindAlternativeWellAndJump(Well initialWell)
    {
      var alternativeWells = _wells.Where(w => w != initialWell).ToList();

      foreach (var nextWell in alternativeWells)
      {
        var slot = nextWell.GetFreeSlot();
        if (slot)
        {
          _currentCircle.transform
            .DOJump(nextWell.transform.position, 1f, 1, 1)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => FallIntoWell(nextWell, slot));
          return;
        }
      }
    }

    private void FallIntoWell(Well well, Slot slot)
    {
      _currentCircle.CurrentWell = well.Index;
      _currentCircle.CurrentSlot = slot.Index;
      _colorMatchService.SetMatrixElement(well.Index, slot.Index, _currentCircle);
      _totalCircles.Add(_currentCircle);

      _currentCircle.transform
        .DOMove(slot.transform.position, _currentCircle.Speed)
        .SetEase(Ease.Linear)
        .SetSpeedBased()
        .OnComplete(OnCircleSettled);
    }

    private void OnCircleSettled()
    {
      var matchedCircles = _colorMatchService.Check();
      if (matchedCircles.Count > 0)
      {
        _circlesRemoveService.RemoveCircles(matchedCircles, _wells, _totalCircles);
        _progress.ProgressData.Score += _currentCircle.Value * matchedCircles.Count;
      }

      _currentCircle = _circleFactory.GetCircle();
      _controls.Enable();
      if (_ropeRenderer) _ropeRenderer.enabled = true;

      if (_colorMatchService.CheckMatrixFull())
        _stateMachine.Enter<ResultLoadState>();
    }
  }
}