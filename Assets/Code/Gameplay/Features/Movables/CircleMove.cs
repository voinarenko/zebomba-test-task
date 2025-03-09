using DG.Tweening;
using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class CircleMove : MonoBehaviour
  {
    public void Shift(Vector3 newPosition, float speed)
    {
      transform.DOMove(newPosition, speed)
        .SetEase(Ease.Linear)
        .SetSpeedBased();
    }
  }
}