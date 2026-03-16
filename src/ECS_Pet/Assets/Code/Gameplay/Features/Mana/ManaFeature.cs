using Code.Infrastructure.Systems;

namespace Code.Gameplay.Mana
{
    public sealed class ManaFeature : Feature
    {
        public ManaFeature(ISystemFactory systemFactory)
        {
            // initialize   

            // execute
            Add(systemFactory.Create<RegenerateManaSystem>());
            Add(systemFactory.Create<UpdateManaSliderSystem>());

            // reactive
            Add(systemFactory.Create<ResetManaOnSpecialAbilityUseSystem>());

            // cleanup
        }
    }
}