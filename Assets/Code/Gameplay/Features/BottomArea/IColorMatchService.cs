using Code.Gameplay.Features.Movables;
using System.Collections.Generic;

namespace Code.Gameplay.Features.BottomArea
{
  public interface IColorMatchService
  {
    List<Circle> Check();
    void SetMatrixElement(int row, int column, Circle circle);
    bool CheckMatrixFull();
  }
}