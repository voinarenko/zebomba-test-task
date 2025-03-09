using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.Movables;
using System.Collections.Generic;

namespace Code.Gameplay.Features.BottomArea
{
  public interface ICirclesRemoveService
  {
    void RemoveCircles(List<Circle> circles, List<Well> wells, List<Circle> totalCircles);
  }
}