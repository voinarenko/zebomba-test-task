using Code.Gameplay.Features.BottomArea.Wells;
using Code.Gameplay.Features.Explosion.Factory;
using Code.Gameplay.Features.Movables;
using Code.Gameplay.Features.Movables.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.Features.BottomArea.Services
{
  public class CirclesRemoveService : ICirclesRemoveService
  {
    private readonly IColorMatchService _colorMatchService;
    private readonly ICircleFactory _circleFactory;
    private readonly IExplosionFactory _explosionFactory;

    public CirclesRemoveService(IColorMatchService colorMatchService, ICircleFactory circleFactory,
      IExplosionFactory explosionFactory)
    {
      _explosionFactory = explosionFactory;
      _colorMatchService = colorMatchService;
      _circleFactory = circleFactory;
    }

    public void RemoveCircles(List<Circle> circles, List<Well> wells, List<Circle> totalCircles)
    {
      var isVertical = CheckIfVertical(circles);

      foreach (var circle in circles)
      {
        _explosionFactory.GetExplosion(circle.transform);
        _circleFactory.PutCircle(circle);
        totalCircles.Remove(circle);
      }

      if (!isVertical) UpdateCirclesPositions(totalCircles, wells);

      for (var i = 0; i < wells.Count; i++)
      {
        for (var j = 0; j < wells[i].GetSlots().Count; j++)
        {
          wells[i].GetSlots()[j].IsOccupied = totalCircles.Any(c => c.CurrentWell == i && c.CurrentSlot == j);
          var circle = totalCircles.FirstOrDefault(c => c.CurrentWell == i && c.CurrentSlot == j);
          _colorMatchService.SetMatrixElement(i, j, circle);
        }
      }
    }

    private static void UpdateCirclesPositions(List<Circle> totalCircles, List<Well> wells)
    {
      var circlesByWell = totalCircles.GroupBy(c => c.CurrentWell)
        .ToDictionary(g => g.Key, g => g.OrderBy(c => c.CurrentSlot).ToList());

      foreach (var wellIndex in circlesByWell.Keys)
      {
        var circlesInWell = circlesByWell[wellIndex];

        var emptySlot = 0;

        foreach (var circle in circlesInWell)
        {
          if (circle.CurrentSlot != emptySlot)
          {
            Vector3 newPosition = wells[wellIndex].GetSlotPosition(emptySlot);

            circle.Shift(newPosition);
            circle.CurrentSlot = emptySlot;
          }
          emptySlot++;
        }
      }
    }

    private static bool CheckIfVertical(List<Circle> circles) =>
      circles.All(c => c.CurrentWell == circles[0].CurrentWell);
  }
}