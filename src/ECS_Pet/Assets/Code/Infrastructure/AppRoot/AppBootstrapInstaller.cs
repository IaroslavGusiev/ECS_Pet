using Code.StaticData;
using Code.Common.Physics;
using Code.Gameplay.Input;
using Code.UI.LoadingCurtain;
using Code.Infrastructure.Systems;
using Code.Infrastructure.Services;

namespace Code.Infrastructure.CompositionRoot
{
    public class AppBootstrapInstaller : InstallerSetup<AppStateMachine, AppBootstrapper>
    {
        protected override void Bind()
        {
            BindInfrastructureServices();
            BindGameplayServices();
            BindCommonServices();
            BindUIServices();
            BindFactories();
            BindContexts();
        }

        protected override void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleEnterState>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IAddressablesAssetProvider>().To<AddressablesAssetProvider>().AsSingle();
            Container.Bind<IResourcesAssetProvider>().To<ResourcesAssetProvider>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<ILoadingCurtain>().FromComponentInNewPrefabResource(CorePrefabPath.LoadingCurtainPath).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
        }

        private void BindContexts()
        {
            Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
            Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
        }
    }
}