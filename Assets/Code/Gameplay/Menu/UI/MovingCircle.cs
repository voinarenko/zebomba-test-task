using Code.Gameplay.Common.Random;
using Code.Gameplay.Features.Movables;
using Code.Gameplay.StaticData;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Menu.UI
{
  public class MovingCircle : MonoBehaviour
  {
    private const float Duration = 10;
    private IRandomService _randomService;
    private Vector3 _startPosition;
    private SpriteRenderer _renderer;
    private IStaticDataService _staticData;

    [Inject]
    public void Construct(IRandomService randomService, IStaticDataService staticData)
    {
      _staticData = staticData;
      _randomService = randomService;
    }

    private void Awake()
    {
      _startPosition = transform.position;
      TryGetComponent(out _renderer);
      Move();
      Shimmer();
    }

    private void Move()
    {
      transform
        .DOMove(new Vector3(transform.position.x, -_startPosition.y, transform.position.z), Duration)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }

    private void Shimmer()
    {
      _renderer.DOColor(_staticData.GetCircleConfig(_randomService.Range(0, (int)CircleId.Count)).Color, Duration)
        .SetEase(Ease.Linear)
        .OnComplete(Shimmer);
    }
  }
}