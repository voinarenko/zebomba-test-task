using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class Circle : MonoBehaviour
  {
    public int CurrentWell { get; set; }
    public int CurrentSlot { get; set; }
    public int CurrentColorIndex => _color.CurrentColorIndex;
    public float Speed = 10;

    private CircleColor _color;
    private CircleMove _move;

    private void Awake()
    {
      TryGetComponent(out _color);
      TryGetComponent(out _move);
    }

    public void Init() =>
      _color.SetColor();

    public void Shift(Vector3 newPosition) =>
      _move.Shift(newPosition, Speed);
  }
}