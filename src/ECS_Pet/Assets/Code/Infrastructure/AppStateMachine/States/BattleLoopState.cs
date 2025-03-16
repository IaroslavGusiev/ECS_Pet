using Code.Gameplay;
using Code.StaticData;
using Code.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Systems;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public class BattleLoopState : EndOfFrameExitState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISystemFactory _systemFactory;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAddressablesAssetProvider _assetProvider;

        private GameContext _gameContext; 
        private GameFeature _gameFeature;

        public BattleLoopState(
            ISceneLoader sceneLoader, 
            ISystemFactory systemFactory, 
            ILoadingCurtain loadingCurtain, 
            IAddressablesAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _assetProvider = assetProvider;
            _systemFactory = systemFactory;
            _loadingCurtain = loadingCurtain;
        }

        public override async UniTask Enter()
        {
            await _assetProvider.WarmupAssetsByLabel(AssetLabels.Gameplay);
            await _sceneLoader.Load(SceneName.EcsWorld);
            
            _gameFeature = _systemFactory.Create<GameFeature>();
            _gameFeature.Initialize();
            
            await _loadingCurtain.Hide();
        }

        protected override async UniTask OnBeginExit() => 
            await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Gameplay);

        protected override void OnUpdate()
        {
            _gameFeature.Execute();
            _gameFeature.Cleanup();
        }

        protected override void ExitOnEndOfFrame()
        {
            _gameFeature.DeactivateReactiveSystems();
            _gameFeature.ClearReactiveSystems();

            DestructEntities();
            
            _gameFeature.Cleanup();
            _gameFeature.TearDown();
            _gameFeature = null;
        }

        private void DestructEntities()
        {
            foreach (GameEntity entity in _gameContext.GetEntities())
            {
                entity.isDestructed = true;
            }
        }
    }
}