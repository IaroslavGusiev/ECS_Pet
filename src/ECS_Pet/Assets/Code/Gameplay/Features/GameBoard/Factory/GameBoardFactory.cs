using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardFactory : IGameBoardFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IAddressablesAssetProvider _assetProvider;

        public GameBoardFactory(
            IEntityFactory entityFactory, 
            IStaticDataService staticDataService, 
            IAddressablesAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateGameBoardCell(GameBoardConfig config, Vector3 position)
        {
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddViewPath(config.CellPrefabPath)
                .AddWorldPosition(position)
                .With(entity => entity.isGameBoardCell = true)
                .With(entity => entity.isProcessMaterialChange = true); 
            
            // TODO: LayerMask
            // TODO: система повинна поставити матеріал та позицію
        }
        
        public async UniTask<GameBoardBehaviour> CreateGameBoard()
        {
            // GameBoardConfig config = _staticDataService.GetGameBoardConfig();
            // var gameBoard = _instantiator.InstantiatePrefabForComponent<GameBoardBehaviour>(prefab);
            // return await gameBoard.Initialize(config);

            return default;
        }
    }
}