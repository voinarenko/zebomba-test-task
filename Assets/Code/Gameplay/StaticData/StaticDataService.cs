using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WindowId, GameObject> _windowPrefabsById;
    private GameObject _circlePrefab;

    public void LoadAll()
    {
      LoadWindows();
      LoadCircle();
    }

    public GameObject GetCirclePrefab() =>
      _circlePrefab;

    public GameObject GetWindowPrefab(WindowId id) =>
      _windowPrefabsById.TryGetValue(id, out var prefab)
        ? prefab
        : throw new Exception($"Prefab config for window {id} was not found");

    private void LoadWindows() =>
      _windowPrefabsById = Resources
        .Load<WindowsConfig>("Configs/Windows/WindowsConfig")
        .WindowConfigs
        .ToDictionary(x => x.Id, x => x.Prefab);

    private void LoadCircle() => 
      _circlePrefab = Resources.Load<GameObject>("Gameplay/Pendulum/Circle");
  }
}