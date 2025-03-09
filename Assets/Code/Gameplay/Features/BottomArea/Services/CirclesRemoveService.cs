using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.Movables;
using Code.Gameplay.Features.Movables.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Code.Gameplay.Features.BottomArea.Services
{
  public class CirclesRemoveService : ICirclesRemoveService
  {
    private readonly IColorMatchService _colorMatchService;
    private readonly ICircleFactory _circleFactory;

    public CirclesRemoveService(IColorMatchService colorMatchService, ICircleFactory circleFactory)
    {
      _colorMatchService = colorMatchService;
      _circleFactory = circleFactory;
    }

    public void RemoveCircles(List<Circle> circles, List<Well> wells, List<Circle> totalCircles)
    {
      var isVertical = CheckIfVertical(circles);
      foreach (var circle in circles)
      {
        _colorMatchService.SetMatrixElement(circle.CurrentWell, circle.CurrentSlot, null);
        if (!isVertical)
          UpdateNextCirclePosition(circle, totalCircles);
        _circleFactory.PutCircle(circle);
        totalCircles.Remove(circle);
      }

      for (var i = 0; i < wells.Count; i++)
      for (var j = 0; j < wells[i].GetSlots().Count; j++)
      {
        wells[i].GetSlots()[j].IsOccupied = totalCircles.Any(c => c.CurrentWell == i && c.CurrentSlot == j);
        var circle = totalCircles.FirstOrDefault(c => c.CurrentWell == i && c.CurrentSlot == j);
        _colorMatchService.SetMatrixElement(i, j, circle ? circle : null);
      }
    }

    private static void UpdateNextCirclePosition(Circle circle, List<Circle> totalCircles)
    {
      var circlesInWell = totalCircles.Where(c => c.CurrentWell == circle.CurrentWell).ToList();
      for (var i = circle.CurrentSlot + 1; i < circlesInWell.Count; i++)
      {
        var nextCircle = circlesInWell[i];
        if (nextCircle)
        {
          nextCircle.Shift(circlesInWell[i - 1].transform.position);
          nextCircle.CurrentSlot = circle.CurrentSlot;
        }
      }
    }

    private static bool CheckIfVertical(List<Circle> circles) =>
      circles.All(c => c.CurrentWell == circles[0].CurrentWell);
  }
}