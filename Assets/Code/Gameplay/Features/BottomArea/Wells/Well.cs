using Code.Gameplay.Features.BottomArea.Wells.Slots;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Features.BottomArea.Wells
{
  public class Well : MonoBehaviour
  {
    [SerializeField] private List<Slot> _slots;

    public Transform GetFreeSlot(Transform parent)
    {
      foreach (var slot in _slots.Where(slot => !slot.IsOccupied))
        return slot.transform;
      return parent;
    }
  }
}