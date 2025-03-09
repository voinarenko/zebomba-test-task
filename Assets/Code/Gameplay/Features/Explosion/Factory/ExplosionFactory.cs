using Code.Gameplay.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Explosion.Factory
{
  public class ExplosionFactory : IExplosionFactory
  {
    private readonly Queue<GameObject> _explosions = new();
    private readonly IStaticDataService _staticData;
    private Transform _pool;

    public ExplosionFactory(IStaticDataService staticData) =>
      _staticData = staticData;

    public void SetContainers(Transform pool) =>
      _pool = pool;

    public GameObject GetExplosion(Transform parent)
    {
      GameObject explosion;
      if (_explosions.Count > 0)
      {
        explosion = _explosions.Dequeue();
        explosion.SetActive(true);
      }
      else
      {
        explosion = CreateExplosion();
        explosion.TryGetComponent<Explosion>(out var explosionComponent);
        explosionComponent.Initialize(this);
      }
      explosion.transform.SetParent(_pool);
      explosion.transform.position = parent.position;
      return explosion;
    }

    public void PutExplosion(GameObject explosion)
    {
      explosion.transform.SetParent(_pool);
      _explosions.Enqueue(explosion);
      explosion.SetActive(false);
    }

    public void ClearPool() => 
      _explosions.Clear();
    
    private GameObject CreateExplosion() =>
      Object.Instantiate(_staticData.GetExplosionPrefab());
  }
}