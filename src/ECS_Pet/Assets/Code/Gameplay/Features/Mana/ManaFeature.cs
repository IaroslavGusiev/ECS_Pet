using Code.Infrastructure.Systems;

namespace Code.Gameplay.Mana
{
    public sealed class ManaFeature : Feature
    {
        public ManaFeature(ISystemFactory systemFactory)
        {
            // initialize   

            // execute
            Add(systemFactory.Create<UpdateManaSliderSystem>());
            Add(systemFactory.Create<RegenerateManaSystem>());

            // cleanup
            
        }
    }
}