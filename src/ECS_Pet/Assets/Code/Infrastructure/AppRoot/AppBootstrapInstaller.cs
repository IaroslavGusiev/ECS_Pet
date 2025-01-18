using Code.StaticData;
using Code.Gameplay.Input;
using Code.Gameplay.Common;
using Code.UI.LoadingCurtain;
using Code.Common.View.Factory;
using Code.Infrastructure.Systems;
using Code.Infrastructure.Services;
using Code.Gameplay.Features.GameBoard;

namespace Code.Infrastructure.CompositionRoot
{
    public class AppBootstrapInstaller : InstallerSetup<AppStateMachine, AppBootstrapper>
    {
        protected override void Bind()
        {
            BindInfrastructureServices();
            BindGameplayFactories();
            BindGameplayServices();
            BindCommonServices();
            BindUIServices();
            BindFactories();
            BindContexts();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IAddressablesAssetProvider>().To<AddressablesAssetProvider>().AsSingle();
            Container.Bind<IResourcesAssetProvider>().To<ResourcesAssetProvider>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.BindInterfacesAndSelfTo<GameBoardFactory>().AsSingle();
        }

        protected override void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleEnterState>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IIdProvider>().To<IdProvider>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle(); // може поїхати до інсталеру геймплею
            Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle(); // може поїхати до інсталеру геймплею

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
            Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
        }

        private void BindContexts()
        {
            Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
            Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
        }
    }
}