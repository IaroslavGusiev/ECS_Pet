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
            Add(systemFactory.Create<ApplyEffectsOnTargetsSystem>());

            // cleanup
        }
    }
}