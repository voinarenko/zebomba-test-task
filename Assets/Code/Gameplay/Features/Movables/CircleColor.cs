using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class CircleColor : MonoBehaviour
  {
    public int CurrentColorIndex { get; private set; }

    [SerializeField] private List<Color> _colors;
    [SerializeField] private SpriteRenderer _renderer;

    public void SetColor()
    {
      if (!_renderer) 
        return;

      CurrentColorIndex = Random.Range(0, _colors.Count);
      _renderer.color = _colors[CurrentColorIndex];
    }
  }
}