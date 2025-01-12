using Zenject;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardFactory : IGameBoardFactory, IInitializable
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticDataService;
        private readonly IAddressablesAssetProvider _assetProvider;

        public GameBoardFactory(
            IInstantiator instantiator, 
            IStaticDataService staticDataService, 
            IAddressablesAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        
        public async UniTask<GameBoardBehaviour> CreateGameBoard()
        {
            GameBoardConfig config = _staticDataService.GetGameBoardConfig();
            var prefab = await _assetProvider.LoadAndGetComponent<GameBoardBehaviour>(config.BoardPrefabPath);
            var gameBoard = _instantiator.InstantiatePrefabForComponent<GameBoardBehaviour>(prefab);
            return await gameBoard.Initialize(config);
        }

        public void Initialize()
        {
            CreateGameBoard().Forget();
        }
    }
}