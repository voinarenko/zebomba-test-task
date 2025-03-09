using Code.Gameplay.Features.Explosion.Factory;
using UnityEngine;

namespace Code.Gameplay.Features.Explosion
{
  public class Explosion : MonoBehaviour
  {
    private IExplosionFactory _explosionFactory;
    
    public void OnParticleSystemStopped() =>
      _explosionFactory.PutExplosion(gameObject);

    public void Initialize(IExplosionFactory explosionFactory) =>
      _explosionFactory = explosionFactory;
  }
}