using Code.Gameplay.Features.BottomArea.Wells.Slots;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Features.BottomArea.Wells
{
  public class Well : MonoBehaviour
  {
    public int Index;
    
    [SerializeField] private List<Slot> _slots;

    public List<Slot> GetSlots() =>
      _slots;

    public Slot GetFreeSlot()
    {
      Slot result = null;
      foreach (var slot in _slots.Where(slot => !slot.IsOccupied))
      {
        slot.IsOccupied = true;
        result = slot;
        break;
      }
      return result;
    }

    public Vector3 GetSlotPosition(int emptySlot) =>
      _slots[emptySlot].transform.position;
  }
}