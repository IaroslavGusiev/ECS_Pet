using Code.Infrastructure.Systems;

namespace Code.Gameplay.FighterSelection
{
    public sealed class FighterSelectionFeature : Feature
    {
        public FighterSelectionFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<SpawnFighterOnWindowSelectionSystem>());
            Add(systemFactory.Create<RaycastBoardCellUnderPointerSystem>());
            Add(systemFactory.Create<MoveFighterOnSelectionSystem>());
            Add(systemFactory.Create<RequestFighterPlacementOnClickSystem>());
            Add(systemFactory.Create<ValidateFighterPlacementRequestSystem>());
            Add(systemFactory.Create<ProcessFighterPlacementPurchaseSystem>());
            Add(systemFactory.Create<PlaceFighterFromRequestSystem>());
            Add(systemFactory.Create<FinalizeFighterPlacementSystem>());
            Add(systemFactory.Create<DeselectFighterWindowOnPlacementSystem>());
            Add(systemFactory.Create<DeselectFighterWindowOnCancelSystem>());
            Add(systemFactory.Create<DestroyFighterOnCancelSystem>());
            Add(systemFactory.Create<MarkBoardCellSelectionVisualStateSystem>());

            // cleanup
        }
    }
}
