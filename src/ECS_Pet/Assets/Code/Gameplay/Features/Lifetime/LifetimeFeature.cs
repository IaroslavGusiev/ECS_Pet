using Code.Infrastructure.Systems;

namespace Code.Gameplay.Lifetime
{
    public sealed class LifetimeFeature : Feature
    {
        public LifetimeFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<MarkDeadSystem>());
            Add(systemFactory.Create<UpdateHpSliderSystem>());

            // cleanup
            
        }
    }
}