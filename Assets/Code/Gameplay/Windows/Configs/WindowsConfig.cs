using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Windows.Configs
{
  [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Zebomba/Windows Config")]
  public class WindowsConfig : ScriptableObject
  {
    public List<WindowConfig> WindowConfigs;
  }
}