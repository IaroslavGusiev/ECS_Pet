using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Abilities
{
    public interface IAbilityFactory
    {
        GameEntity CreateBasicAbility(AbilityConfig abilityConfig, int fighterId);
        GameEntity CreateSpecialAbility(AbilityConfig abilityConfig, int fighterId);
    }
}