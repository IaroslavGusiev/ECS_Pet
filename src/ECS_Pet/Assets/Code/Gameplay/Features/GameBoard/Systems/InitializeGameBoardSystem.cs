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
            
            var gameBoardObject = new GameObject("GameBoard");
            
            Vector2Int boardSize = config.BoardSize;

            int minX = -boardSize.x / 2;
            int maxX = boardSize.x / 2 - 1;
            
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = 0; y < boardSize.y; y++)
                {
                    _gameBoardFactory.CreateGameBoardCell(config, new Vector3(x, 0, y));
                }
            }
        }
    }

    public class ProcessBoardCellUponInitSystem : IExecuteSystem
    {
        // UpdateTransformPositionSystem
        
        private readonly IGroup<GameEntity> _cells;

        public ProcessBoardCellUponInitSystem(GameContext game)
        {
            _cells = game.GetGroup(GameMatcher.AllOf(GameMatcher.GameBoardCell, GameMatcher.GameBoardCell, GameMatcher.WorldPosition, GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _cells)
            {
                
            }
        }
    }
}