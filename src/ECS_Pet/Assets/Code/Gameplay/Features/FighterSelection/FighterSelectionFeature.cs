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
            Add(systemFactory.Create<MoveFighterOnSelectionSystem>());
            Add(systemFactory.Create<FighterPlacementOnClickSystem>());
            Add(systemFactory.Create<DestroyFighterOnCancelSystem>());

            // cleanup
        }
    }
}