using Code.Gameplay.Features.Movables;
using Code.Gameplay.Features.Movables.Configs;
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
    private List<CircleConfig> _circleConfigs;

    public void LoadAll()
    {
      LoadWindows();
      LoadCirclesData();
      LoadCirclePrefab();
    }

    public CircleConfig GetCircleConfig(int id) =>
      _circleConfigs.FirstOrDefault(x => x.Id == (CircleId)id);
    
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

    private void LoadCirclesData() =>
      _circleConfigs = Resources
        .Load<CirclesConfig>("Configs/Circles/CirclesConfig")
        .CircleConfigs
        .ToList();

    private void LoadCirclePrefab() =>
      _circlePrefab = Resources.Load<GameObject>("Gameplay/Pendulum/Circle");
  }
}