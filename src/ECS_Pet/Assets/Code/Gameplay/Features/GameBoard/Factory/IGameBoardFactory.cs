using UnityEngine;

namespace Code.Gameplay.Features.GameBoard
{
    public interface IGameBoardFactory
    {
        public GameEntity CreateGameBoardCell(string viewPath, Vector3 position, Material material);
    }
}