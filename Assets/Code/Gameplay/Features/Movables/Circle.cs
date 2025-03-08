using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class Circle : MonoBehaviour
  {
    public int CurrentColorIndex => _color.CurrentColorIndex;
    public float Speed = 10;

    private CircleColor _color;
    
    private void Awake() =>
      TryGetComponent(out _color);

    public void Init() =>
      _color.SetColor();
  }
}