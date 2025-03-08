using Code.Gameplay.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Movables.Factory
{
  public class CircleFactory : ICircleFactory
  {
    private readonly Queue<Circle> _circles = new();
    private readonly IStaticDataService _staticData;
    private Transform _container;
    private Transform _pool;

    public CircleFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public void SetContainers(Transform container, Transform pool)
    {
      _pool = pool;
      _container = container;
    }

    public Circle GetCircle()
    {
      Circle circle;
      if (_circles.Count > 0)
      {
        circle = _circles.Dequeue();
        circle.gameObject.SetActive(true);
      }
      else
        CreateCircle().TryGetComponent(out circle);
      circle.transform.SetParent(_container);
      circle.transform.localPosition = Vector3.zero;
      circle.Init();
      return circle;
    }

    public void PutCircle(Circle circle)
    {
      circle.transform.SetParent(_pool);
      _circles.Enqueue(circle);
      circle.gameObject.SetActive(false);
    }

    private GameObject CreateCircle() =>
      Object.Instantiate(_staticData.GetCirclePrefab());
  }
}