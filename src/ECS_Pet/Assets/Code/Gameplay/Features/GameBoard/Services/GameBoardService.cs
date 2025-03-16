using UnityEngine;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardService : IGameBoardService
    {
        public const float FixedYForCells = -0.5f;
        
        private readonly IStaticDataService _staticDataService;
        private Material _currentCellMaterial;
        private GameBoardConfig _config;

        public GameBoardService(IStaticDataService staticDataService) => 
            _staticDataService = staticDataService;

        public void Initialize()
        {
            _config = _staticDataService.GetGameBoardConfig();
            _currentCellMaterial = _config.GetRandomCellMaterial();
        }
        
        public Material GetCurrentCellMaterial() => 
            _currentCellMaterial;
        
        public Material GetGreenCellMaterial() => 
            _config.GreenCellMaterial;

        public Material GetRedCellMaterial() => 
            _config.RedCellMaterial;
        
        public Vector2Int GetBoardSize() => 
            _config.BoardSize;
        
        public string GetCellPrefabPath() => 
            _config.CellPrefabPath;
    }
}