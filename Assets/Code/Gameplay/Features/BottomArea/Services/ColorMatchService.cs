using Code.Gameplay.Features.Movables;
using System.Collections.Generic;
using System.Linq;

namespace Code.Gameplay.Features.BottomArea.Services
{
  public class ColorMatchService : IColorMatchService
  {
    private readonly Circle[,] _matrix = new Circle[3, 3];

    public void SetMatrixElement(int column, int row, Circle circle = null) =>
      _matrix[column, row] = circle;

    public bool CheckMatrixFull() =>
      _matrix.Cast<Circle>().Count(circle => circle) == _matrix.Length;

    public List<Circle> Check()
    {
      var size = _matrix.GetLength(0);

      var matchedCircles = new List<Circle>();
      var matchedLine = new List<Circle>();
      var matchedDiagonal = new List<Circle>();

      matchedLine = CheckLine(size, (i, j) => _matrix[i, j]);
      if (matchedLine.Count == size) matchedCircles = matchedLine;
      else
      {
        matchedLine = CheckLine(size, (i, j) => _matrix[j, i]);
        if (matchedLine.Count == size) matchedCircles = matchedLine;
      }

      matchedDiagonal = CheckDiagonal(size, (i) => _matrix[i, i]);
      if (matchedDiagonal.Count < size)
        matchedDiagonal = CheckDiagonal(size, (i) => _matrix[i, size - 1 - i]);
      if (matchedDiagonal.Count == size)
        foreach (var circle in matchedDiagonal.Where(circle => !matchedCircles.Contains(circle)))
          matchedCircles.Add(circle);

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