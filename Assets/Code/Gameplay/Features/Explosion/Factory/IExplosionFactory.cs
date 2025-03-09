using Code.Gameplay.Features.Movables;
using UnityEngine;

namespace Code.Gameplay.Features.Explosion.Factory
{
  public interface IExplosionFactory
  {
    GameObject GetExplosion(Transform parent);
    void SetContainers(Transform pool);
    void ClearPool();
    void PutExplosion(GameObject explosion);
  }
}