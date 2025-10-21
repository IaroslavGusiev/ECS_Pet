using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Input;
using Code.GameplayEffects;
using Code.Gameplay.Fighter;
using Code.Gameplay.Monster;
using Code.UI.LoadingCurtain;
using Code.Common.View.Factory;
using Code.Gameplay.Abilities;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.Systems;
using Code.Infrastructure.Services;
using Code.Gameplay.Features.GameBoard;
using Code.Gameplay.Common.Time.EntityIndices;

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
            BindEntityIndices();
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
            Container.Bind<IGameBoardFactory>().To<GameBoardFactory>().AsSingle();
            Container.Bind<IFighterFactory>().To<FighterFactory>().AsSingle();
            Container.Bind<IMonsterFactory>().To<MonsterFactory>().AsSingle();
            Container.Bind<IEffectFactory>().To<EffectFactory>().AsSingle();
            Container.Bind<IAbilityFactory>().To<AbilityFactory>().AsSingle();
        }

        protected override void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleEnterState>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleLoopState>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IGameBoardService>().To<GameBoardService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IFighterPlacementService>().To<FighterPlacementService>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IIdProvider>().To<IdProvider>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsSingle(); // can be moved to gameplay installer
            Container.Bind<ICollisionRegistry>().To<CollisionRegistry>().AsSingle(); // can be moved to gameplay installer
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
        }

        private void BindEntityIndices()
        {
            Container.BindInterfacesAndSelfTo<GameEntityIndices>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<ILoadingCurtain>().FromComponentInNewPrefabResource(CorePrefabPath.LoadingCurtainPath).AsSingle();
            Container.Bind<IHUDRoot>().FromComponentInNewPrefabResource(CorePrefabPath.HudRootPath).AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
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