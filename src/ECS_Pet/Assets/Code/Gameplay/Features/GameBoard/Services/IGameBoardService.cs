using UnityEngine;

namespace Code.Gameplay.Features.GameBoard
{
    public interface IGameBoardService
    {
        void Initialize();
        Vector2Int GetBoardSize();
        string GetCellPrefabPath();
        
        Material GetCurrentCellMaterial();
        Material GetGreenCellMaterial();
        Material GetRedCellMaterial();
    }
}