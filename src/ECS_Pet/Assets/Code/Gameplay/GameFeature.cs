using Code.Gameplay.Input;
using Code.Infrastructure.Systems;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay
{
    public sealed class GameFeature : Feature
    {
        public GameFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<GameBoardFeature>());
        }
    }
}