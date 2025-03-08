using UnityEngine;

namespace Code.Gameplay.Features.Pendulum
{
  public class PendulumSwing : MonoBehaviour
  {
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _amplitude = 45f;
    private float _angle;

    private void Update()
    {
      _angle = Mathf.Sin(Time.time * _speed) * _amplitude;
      transform.rotation = Quaternion.Euler(0, 0, _angle);
    }
  }
}