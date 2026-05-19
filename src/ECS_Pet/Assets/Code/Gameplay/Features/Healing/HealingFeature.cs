using Code.Infrastructure.Systems;

namespace Code.Gameplay.Healing
{
    public sealed class HealingFeature : Feature
    {
        public HealingFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<SelectHealingTargetSystem>());
            Add(systemFactory.Create<HealingAbilityActivationSystem>());
        }
    }
}
