using Code.Gameplay.Fighter;

namespace Code.Gameplay.FighterSelection
{
    public interface IFighterAffordabilityService
    {
        bool CanAfford(FighterConfig fighterConfig);
    }
}
