using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.GameBoard
{
    public sealed class GameBoardFeature : Feature
    {
        public GameBoardFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<ProcessClickOnGameBoardSystem>());
        }
    }
}