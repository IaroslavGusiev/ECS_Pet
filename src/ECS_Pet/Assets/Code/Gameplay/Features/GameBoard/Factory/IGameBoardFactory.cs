using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Features.GameBoard
{
    public interface IGameBoardFactory
    {
        UniTask<GameBoardBehaviour> CreateGameBoard();
    }
}