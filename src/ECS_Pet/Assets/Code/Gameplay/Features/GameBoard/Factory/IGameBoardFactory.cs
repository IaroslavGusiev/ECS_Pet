using UnityEngine;
using Code.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Features.GameBoard
{
    public interface IGameBoardFactory
    {
        UniTask<GameBoardBehaviour> CreateGameBoard();
        GameEntity CreateGameBoardCell(GameBoardConfig config, Vector3 position);
    }
}