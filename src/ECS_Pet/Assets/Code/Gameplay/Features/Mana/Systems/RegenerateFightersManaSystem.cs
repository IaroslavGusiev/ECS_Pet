using Entitas;
using UnityEngine;
using Code.Common.Extensions;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Mana
{
    public class RegenerateManaSystem : IExecuteSystem 
    {
        private readonly IGroup<GameEntity> _tick;
        private readonly IGroup<GameEntity> _combatants;

        public RegenerateManaSystem(GameContext gameContext)
        {
            _tick = gameContext.GetGroup(GameMatcher.Tick);
            
            _combatants = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Placed,
                GameMatcher.MaxMana,
                GameMatcher.BaseStats,
                GameMatcher.CurrentMana
            }));
        }

        public void Execute()
        {
            if (_tick.IsEmpty())
            {
                return;
            }
            
            foreach (GameEntity combatant in _combatants)
            {
                float regenRate = combatant.BaseStats[Stats.ManaRegen];
                combatant.ReplaceCurrentMana(Mathf.Min(combatant.CurrentMana + regenRate, combatant.MaxMana));
            }
        }
    }
}
