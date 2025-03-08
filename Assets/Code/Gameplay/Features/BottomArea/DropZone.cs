using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.Movables;
using Code.Gameplay.Features.Movables.Factory;
using Code.Gameplay.Input.Service;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Gameplay.Features.BottomArea
{
  public class DropZone : MonoBehaviour
  {
    [SerializeField] private List<Well> _wells;
    private DroppedCircles _droppedCircles;
    private IInputService _inputService;
    private Transform _circlesInWells;
    private Circle _currentCircle;
    private InputActions _controls;
    private ICircleFactory _circleFactory;

    [Inject]
    public void Construct(IInputService inputService, ICircleFactory circleFactory)
    {
      _circleFactory = circleFactory;
      _inputService = inputService;
    }
    
    private void Awake() => 
      TryGetComponent(out _droppedCircles);

    public void SetControls() => 
      _controls = _inputService.GetActions();
    
    public void Subscribe() =>
      _controls.UI.Drop.performed += DropCircle;

    public void SetContainer(Transform wells) =>
      _circlesInWells = wells;

    public void SetCircle(Circle circle) =>
      _currentCircle = circle;

    private Well FindClosestWell(float currentX)
    {
      var result = _wells[1];
      foreach (var well in _wells.Where(well =>
                 Mathf.Abs(well.transform.position.x - currentX) < Mathf.Abs(result.transform.position.x - currentX)))
        result = well;
      return result;
    }

    private void DropCircle(InputAction.CallbackContext obj)
    {
      _controls.Disable();
      _currentCircle.transform.SetParent(_circlesInWells);
      var well = FindClosestWell(_currentCircle.transform.position.x);
      var slot = well.GetFreeSlot();
      _droppedCircles.Matrix[well.Index, slot.Index] = _currentCircle;
      _currentCircle.transform
        .DOMove(well.transform.position, _currentCircle.Speed)
        .SetEase(Ease.Linear)
        .SetSpeedBased()
        .OnComplete(() =>
        {
          _currentCircle.transform
            .DOMove(slot.transform.position, _currentCircle.Speed)
            .SetEase(Ease.Linear)
            .SetSpeedBased()
            .OnComplete(() =>
            {
              _droppedCircles.Check();
              _currentCircle = _circleFactory.GetCircle();
              _controls.Enable();
            });
        });
    }
  }
}