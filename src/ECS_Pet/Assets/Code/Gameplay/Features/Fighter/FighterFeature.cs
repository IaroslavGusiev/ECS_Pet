using Code.Gameplay.Abilities;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Fighter
{
    public sealed class FighterFeature : Feature
    {
        public FighterFeature(ISystemFactory systemFactory)
        {
            // execute systems
            Add(systemFactory.Create<AnimateFighterMovementSystem>());
            Add(systemFactory.Create<DestroyAbilitiesOnOwnerDestroySystem>());
        }
    }
}
