﻿using Code.Gameplay.Windows;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public interface IStaticDataService
  {
    void LoadAll();
    
    GameObject GetWindowPrefab(WindowId id);
  }
}