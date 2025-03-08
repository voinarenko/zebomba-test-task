using Code.Gameplay.Features.BottomArea;
using Code.Gameplay.Features.Movables;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private Circle _circle;
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private Transform _circlesInWells;
    [SerializeField] private Transform _circlesInPool;
    
    public void Initialize()
    {
      _dropZone.Subscribe();
      _dropZone.SetContainer(_circlesInWells);
      _dropZone.SetCircle(_circle);
    }
  }
}