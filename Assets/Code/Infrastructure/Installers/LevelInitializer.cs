using Code.Gameplay.Features.BottomArea;
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

    [Inject]
    private void Construct(ICircleFactory circleFactory) =>
      _circleFactory = circleFactory;

    public void Initialize()
    {
      _circleFactory.SetContainers(_circleContainer, _circlesInPool);
      _dropZone.Subscribe();
      _dropZone.SetContainer(_circlesInWells);
      _dropZone.SetCircle(_circleFactory.GetCircle());
    }
  }
}