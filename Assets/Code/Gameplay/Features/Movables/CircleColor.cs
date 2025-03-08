using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class CircleColor : MonoBehaviour
  {
    [SerializeField] private List<Color> _colors;
    [SerializeField] private SpriteRenderer _renderer;
    
    public void SetColor()
    {
      if (_renderer) 
        _renderer.color = _colors[Random.Range(0, _colors.Count)];
    }
  }
}