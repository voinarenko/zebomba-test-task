using Code.Gameplay.Features.Movables;
using System.Collections.Generic;

namespace Code.Gameplay.Features.BottomArea
{
  public class ColorMatchService : IColorMatchService
  {
    private readonly Circle[,] _matrix = new Circle[3, 3];
    
    public void SetMatrixElement(int row, int column, Circle circle = null) =>
      _matrix[row, column] = circle;

    public List<Circle> Check()
    {
      var size = _matrix.GetLength(0);

      var matchedCircles = CheckLine(size, (i, j) => _matrix[i, j]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckLine(size, (i, j) => _matrix[j, i]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckDiagonal(size, (i) => _matrix[i, i]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckDiagonal(size, (i) => _matrix[i, size - 1 - i]);
      return matchedCircles;
    }

    private static List<Circle> CheckLine(int size, System.Func<int, int, Circle> getElement)
    {
      for (var i = 0; i < size; i++)
      {
        var matchedCircles = new List<Circle>();
        var startCircle = getElement(i, 0);
        if (!startCircle) continue;

        var startColor = startCircle.CurrentColorIndex;
        var isFull = true;

        for (var j = 0; j < size; j++)
        {
          var circle = getElement(i, j);
          if (!circle || circle.CurrentColorIndex != startColor)
          {
            isFull = false;
            break;
          }
          matchedCircles.Add(circle);
        }

        if (isFull && matchedCircles.Count == size)
          return matchedCircles;
      }
      return new List<Circle>();
    }

    private static List<Circle> CheckDiagonal(int size, System.Func<int, Circle> getElement)
    {
      var matchedCircles = new List<Circle>();
      var startCircle = getElement(0);
      if (!startCircle) return matchedCircles;

      var startColor = startCircle.CurrentColorIndex;
      for (var i = 0; i < size; i++)
      {
        var circle = getElement(i);
        if (!circle || circle.CurrentColorIndex != startColor) 
          return new List<Circle>();
        matchedCircles.Add(circle);
      }
      return matchedCircles;
    }
  }
}