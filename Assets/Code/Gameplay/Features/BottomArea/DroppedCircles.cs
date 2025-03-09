using Code.Gameplay.Features.Movables;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.BottomArea
{
  public class DroppedCircles : MonoBehaviour
  {
    public readonly Circle[,] Matrix = new Circle[3, 3];

    public List<Circle> Check()
    {
      var size = Matrix.GetLength(0);

      var matchedCircles = CheckLine(size, (i, j) => Matrix[i, j]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckLine(size, (i, j) => Matrix[j, i]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckDiagonal(size, (i) => Matrix[i, i]);
      if (matchedCircles.Count == size) return matchedCircles;

      matchedCircles = CheckDiagonal(size, (i) => Matrix[i, size - 1 - i]);
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
        var isWin = true;

        for (var j = 0; j < size; j++)
        {
          var circle = getElement(i, j);
          if (!circle || circle.CurrentColorIndex != startColor)
          {
            isWin = false;
            break;
          }
          matchedCircles.Add(circle);
        }

        if (isWin && matchedCircles.Count == size)
        {
          print($"Winning line: {startColor}");
          return matchedCircles;
        }
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

      print($"Winning diagonal: {startColor}");
      return matchedCircles;
    }
  }
}