using Code.Gameplay.Mana;
using Code.Gameplay.Lifetime;
using Code.Gameplay.Abilities;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Fighter
{
    public sealed class FighterFeature : Feature
    {
        public FighterFeature(ISystemFactory systemFactory)
        {
            // execute systems
            Add(systemFactory.Create<UpdateManaSliderSystem>());
            Add(systemFactory.Create<RegenerateFightersManaSystem>());
            Add(systemFactory.Create<AnimateFighterMovementSystem>());
            Add(systemFactory.Create<DestroyAbilitiesOnOwnerDestroySystem>());
            
            // reactive systems
            Add(systemFactory.Create<EnableStatsSliderOnPlacementSystem>());
            Add(systemFactory.Create<DisableStatsSliderOnWindowSelectionSystem>());
        }
    }
}