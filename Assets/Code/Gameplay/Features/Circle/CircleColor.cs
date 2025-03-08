using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Circle
{
  public class CircleColor : MonoBehaviour
  {
    [SerializeField] private List<Color> _colors;
    [SerializeField] private SpriteRenderer _renderer;
    
    private void Awake() =>
      SetColor();

    private void SetColor()
    {
      if (_renderer) 
        _renderer.color = _colors[Random.Range(0, _colors.Count)];
    }
  }
}