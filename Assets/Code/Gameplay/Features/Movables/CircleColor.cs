using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class CircleColor : MonoBehaviour
  {
    [SerializeField] private SpriteRenderer _renderer;

    public void SetColor(Color color)
    {
      if (!_renderer) 
        return;

      _renderer.color = color;
    }
  }
}