using Code.Infrastructure.Systems;
using Code.GameplayEffects.Systems;

namespace Code.GameplayEffects
{
    public sealed class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<RemoveEffectsWithoutTargetsSystem>());
            Add(systemFactory.Create<ApplyEffectsOnTargetsSystem>());
            Add(systemFactory.Create<ProcessDamageEffectSystem>());

            // cleanup
            Add(systemFactory.Create<CleanupProcessedEffects>());
        }
    }
}