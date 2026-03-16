using Entitas;

namespace Code.Gameplay.Abilities
{
    public class MarkSpecialAbilityReadyOnFullManaSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _combatants;

        public MarkSpecialAbilityReadyOnFullManaSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _combatants = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.MaxMana, 
                GameMatcher.CurrentMana, 
                GameMatcher.SpecialAbilityId
            }));
        }

        public void Execute()
        {
            foreach (GameEntity combatant in _combatants)
            {
                GameEntity specialAbility = _gameContext.GetEntityWithId(combatant.SpecialAbilityId);
                specialAbility.isManaSufficient = combatant.CurrentMana >= combatant.MaxMana;
            }
        }
    }
}