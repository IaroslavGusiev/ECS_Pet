using Code.Infrastructure.Systems;

namespace Code.Gameplay.Combat
{
    public sealed class CombatFeature : Feature
    {
        public CombatFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<AttackRangeCheckSystem>());
            Add(systemFactory.Create<AttackStartDecisionSystem>());

            // cleanup
        }
    }
}