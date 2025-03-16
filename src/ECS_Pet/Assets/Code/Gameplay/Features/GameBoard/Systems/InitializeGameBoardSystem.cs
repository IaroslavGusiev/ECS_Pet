using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.GameBoard
{
    public class InitializeGameBoardSystem : IInitializeSystem
    {
        private readonly IGameBoardFactory _gameBoardFactory;
        private readonly IGameBoardService _gameBoardService;

        public InitializeGameBoardSystem(
            IGameBoardFactory gameBoardFactory, 
            IGameBoardService gameBoardService)
        {
            _gameBoardFactory = gameBoardFactory;
            _gameBoardService = gameBoardService;
        }

        public void Initialize()
        {
            Vector2Int boardSize = _gameBoardService.GetBoardSize();

            int minX = -boardSize.x / 2;
            int maxX = (boardSize.x - 1) / 2; 
            
            for (int x = minX; x <= maxX; x++)
            {
                for (var y = 0; y < boardSize.y; y++)
                {
                    _gameBoardFactory.CreateGameBoardCell(_gameBoardService.GetCellPrefabPath(), new Vector3(x, GameBoardService.FixedYForCells, y), _gameBoardService.GetCurrentCellMaterial());
                }
            }
        }
    }
}