using Code.Gameplay.Features.BottomArea;
using Code.Gameplay.Features.Explosion.Factory;
using Code.Gameplay.Features.Movables.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private Transform _circleContainer;
    [SerializeField] private Transform _circlesInWells;
    [SerializeField] private Transform _circlesInPool;
    private ICircleFactory _circleFactory;
    private IExplosionFactory _explosionFactory;

    [Inject]
    private void Construct(ICircleFactory circleFactory, IExplosionFactory explosionFactory)
    {
      _explosionFactory = explosionFactory;
      _circleFactory = circleFactory;
    }

    public void Initialize()
    {
      _circleFactory.SetContainers(_circleContainer, _circlesInPool);
      _explosionFactory.SetContainers(_circlesInPool);
      _dropZone.SetControls();
      _dropZone.Subscribe();
      _dropZone.SetContainer(_circlesInWells);
      _dropZone.SetCircle(_circleFactory.GetCircle());
    }
  }
}