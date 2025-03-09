using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Movables.Configs
{
  [CreateAssetMenu(fileName = "CirclesConfig", menuName = "Zebomba/Circles Config")]
  public class CirclesConfig : ScriptableObject
  {
    public List<CircleConfig> CircleConfigs;
  }
}