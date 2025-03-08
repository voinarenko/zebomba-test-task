using Code.Gameplay.Features.Movables;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.BottomArea
{
  public class DroppedCircles : MonoBehaviour
  {
    public readonly Circle[,] Matrix = new Circle[3, 3];

    public void Check()
    {
      var horizontalCount = Matrix.GetLength(0);
      var verticalCount = Matrix.GetLength(1);

      for (var i = 0; i < horizontalCount; i++)
      {
        var matchedCircles = new List<Circle>();
        for (var j = 0; j < verticalCount; j++)
        {
          if (!Matrix[i, j])
            break;
          
          if (Matrix[i, j].CurrentColorIndex != Matrix[i, 0].CurrentColorIndex)
            break;
          
          matchedCircles.Add(Matrix[i, j]);
        }
        if (matchedCircles.Count == verticalCount) 
          print($"vertical {matchedCircles[0].CurrentColorIndex}");
      }
    }
  }
}