using Entitas;
using UnityEngine;
using Code.StaticData;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Features.GameBoard
{
    public class InitializeGameBoardSystem : IInitializeSystem
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameBoardFactory _gameBoardFactory;

        public InitializeGameBoardSystem(IGameBoardFactory gameBoardFactory, IStaticDataService staticDataService)
        {
            _gameBoardFactory = gameBoardFactory;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            GameBoardConfig config = _staticDataService.GetGameBoardConfig();
            Material randomMaterial = config.GetRandomCellMaterial();
            CreateGameBoard(config, randomMaterial);
        }

        private void CreateGameBoard(GameBoardConfig config, Material randomMaterial)
        {
            Vector2Int boardSize = config.BoardSize;

            int minX = -boardSize.x / 2;
            int maxX = boardSize.x / 2 - 1;
            
            for (int x = minX; x <= maxX; x++)
            {
                for (var y = 0; y < boardSize.y; y++)
                {
                    _gameBoardFactory.CreateGameBoardCell(config.CellPrefabPath, new Vector3(x, 0, y), randomMaterial);
                }
            }
        }
    }
}