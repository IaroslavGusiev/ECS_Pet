using Code.Infrastructure.Systems;

namespace Code.Gameplay.TargetCollection
{
    public sealed class CollectTargetsFeature : Feature
    {
        public CollectTargetsFeature(ISystemFactory systemFactory)
        {
            // execute
            Add(systemFactory.Create<CastForTargetsNoLimitSystem>());
            Add(systemFactory.Create<SelectNearestTargetSystem>());
            Add(systemFactory.Create<SelectLowestHpAllySystem>());

            // cleanup
            Add(systemFactory.Create<CleanupTargetBuffersSystem>());
        }
    }
}