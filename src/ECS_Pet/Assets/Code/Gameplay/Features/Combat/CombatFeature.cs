using Code.Infrastructure.Systems;

namespace Code.Gameplay.Combat
{
    public sealed class CombatFeature : Feature
    {
        public CombatFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<ClearDeadTargetSystem>());
            Add(systemFactory.Create<AttackRangeCheckSystem>());
            Add(systemFactory.Create<AbilityActivationSystem>());
            Add(systemFactory.Create<StopAttackOnNoTargetSystem>());
            
            // reactive systems
            Add(systemFactory.Create<EnableStatsSliderOnPlacementSystem>());
            Add(systemFactory.Create<DisableStatsSliderOnWindowSelectionSystem>());
            Add(systemFactory.Create<DisableStatsSliderOnDeathSystem>());

            // cleanup
        }
    }
}