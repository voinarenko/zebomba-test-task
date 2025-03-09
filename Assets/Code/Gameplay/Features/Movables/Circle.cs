using UnityEngine;

namespace Code.Gameplay.Features.Movables
{
  public class Circle : MonoBehaviour
  {
    public int Value { get; private set; }
    public int CurrentWell { get; set; }
    public int CurrentSlot { get; set; }
    public int CurrentColorIndex { get; private set; }
    public float Speed = 10;

    private CircleColor _color;
    private CircleMove _move;

    private void Awake()
    {
      TryGetComponent(out _color);
      TryGetComponent(out _move);
    }

    public void Init(int id, Color color, int value)
    {
      CurrentColorIndex = id;
      _color.SetColor(color);
      Value = value;
    }

    public void Shift(Vector3 newPosition) =>
      _move.Shift(newPosition, Speed);
  }
}