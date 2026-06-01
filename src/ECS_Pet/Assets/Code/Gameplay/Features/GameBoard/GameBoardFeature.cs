using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.GameBoard
{
    public sealed class GameBoardFeature : Feature
    {
        public GameBoardFeature(ISystemFactory systemFactory)
        {
            // Initialize
            Add(systemFactory.Create<InitializeGameBoardSystem>());
            
            // Execute
            Add(systemFactory.Create<ApplyBoardCellVisualStateSystem>());
        }
    }
}
