using Code.Infrastructure.Systems;

namespace Code.Gameplay.Animation
{
    public sealed class AnimationFeature : Feature
    {
        public AnimationFeature(ISystemFactory systemFactory)
        {
            // initialize

            // execute
            Add(systemFactory.Create<AnimateFighterAbilitiesSystem>());

            // cleanup
        }
    }
}