using Code.Infrastructure.Systems;

namespace Code.Gameplay.Armaments
{
    public sealed class ArmamentFeature : Feature
    {
        public ArmamentFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            
            // react
            Add(systemFactory.Create<CreateProjectileHitVfxUponReachTargetSystem>());
            Add(systemFactory.Create<CreateEffectsUponReachTargetSystem>());
            
            // cleanup
            
        }
    }
}
