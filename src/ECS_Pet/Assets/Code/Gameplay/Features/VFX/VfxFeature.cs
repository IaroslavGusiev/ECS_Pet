using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Vfx
{
    public sealed class VfxFeature : Feature
    {
        public VfxFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<CreateHealVfxOnProcessedHealSystem>());

            // react
            Add(systemFactory.Create<CreateProjectileHitVfxUponReachTargetSystem>());

            // cleanup
        }
    }
}
