using Code.Gameplay.Common.Random;
using Code.Gameplay.Features.BottomArea.Services;
using Code.Gameplay.Features.Explosion.Factory;
using Code.Gameplay.Features.Movables.Factory;
using Code.Gameplay.Input.Service;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Infrastructure.Async.Service;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    public override void InstallBindings()
    {
      BindInputService();
      BindInfrastructureServices();
      BindCommonServices();
      BindGameplayServices();
      BindUIServices();
      BindUIFactories();
      BindGameplayFactories();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
      BindProgressServices();
    }

    private void BindInputService() =>
      Container.Bind<IInputService>().To<InputService>().AsSingle();

    private void BindInfrastructureServices() =>
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindCommonServices()
    {
      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<IAsyncService>().To<AsyncService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
    }

    private void BindGameplayServices()
    {
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
      Container.Bind<IColorMatchService>().To<ColorMatchService>().AsSingle();
      Container.Bind<ICirclesRemoveService>().To<CirclesRemoveService>().AsSingle();
    }

    private void BindUIServices() =>
      Container.Bind<IWindowService>().To<WindowService>().AsSingle();

    private void BindUIFactories() =>
      Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();

    private void BindGameplayFactories()
    {
      Container.Bind<ICircleFactory>().To<CircleFactory>().AsSingle();
      Container.Bind<IExplosionFactory>().To<ExplosionFactory>().AsSingle();
    }

    private void BindStateMachine() =>
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

    private void BindStateFactory() =>
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelEnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MenuEnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MenuLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<MenuLoopState>().AsSingle();
      Container.BindInterfacesAndSelfTo<ResultEnterState>().AsSingle();
      Container.BindInterfacesAndSelfTo<ResultLoadState>().AsSingle();
      Container.BindInterfacesAndSelfTo<ResultLoopState>().AsSingle();
    }

    private void BindProgressServices() =>
      Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
  }
}