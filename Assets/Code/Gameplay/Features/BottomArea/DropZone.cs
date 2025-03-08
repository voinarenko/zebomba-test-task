using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.Movables;
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
    private IInputService _inputService;
    private Transform _circlesInWells;
    private Circle _currentCircle;

    [Inject]
    public void Construct(IInputService inputService) =>
      _inputService = inputService;

    public void Subscribe() =>
      _inputService.GetActions().UI.Drop.performed += DropCircle;

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
      _currentCircle.transform.SetParent(_circlesInWells);
      var target = FindClosestWell(_currentCircle.transform.position.x);
      _currentCircle.transform
        .DOMove(target.transform.position, _currentCircle.Speed)
        .SetEase(Ease.Linear)
        .SetSpeedBased();
    }
  }
}