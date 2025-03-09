using UnityEngine;

namespace Code.Gameplay.Features.Movables.Factory
{
  public interface ICircleFactory
  {
    Circle GetCircle();
    void SetContainers(Transform container, Transform pool);
    void PutCircle(Circle circle);
  }
}