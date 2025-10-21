using Code.Gameplay.Cooldowns;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Abilities
{
    public sealed class AbilitiesFeature : Feature
    {
        public AbilitiesFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<ProcessEffectTimersSystem>());
            Add(systemFactory.Create<CooldownSystem>());

            // cleanup
        }
    }
}