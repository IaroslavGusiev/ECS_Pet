using Entitas;
using UnityEngine;
using Code.Common.Extensions;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Mana
{
    public class RegenerateFightersManaSystem : IExecuteSystem 
    {
        private readonly IGroup<GameEntity> _fighters;
        private readonly IGroup<GameEntity> _tick;

        public RegenerateFightersManaSystem(GameContext game)
        {
            _tick = game.GetGroup(GameMatcher.Tick);
            
            _fighters = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Placed,
                GameMatcher.MaxMana,
                GameMatcher.Fighter,
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
            
            foreach (GameEntity fighter in _fighters)
            {
                float regenRate = fighter.BaseStats[Stats.ManaRegen];
                fighter.ReplaceCurrentMana(Mathf.Min(fighter.CurrentMana + regenRate, fighter.MaxMana));
            }
        }
    }
}