using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Lifetime
{
    public class FinalizeCombatantDeathProcessingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _combatants;
        private readonly List<GameEntity> _buffer = new(16);

        public FinalizeCombatantDeathProcessingSystem(GameContext game)
        {
            _combatants = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath)
                .AnyOf(
                    GameMatcher.Fighter,
                    GameMatcher.Monster));
        }

        public void Execute()
        {
            foreach (GameEntity combatant in _combatants.GetEntities(_buffer))
            {
                combatant.isProcessingDeath = false;
            }
        }
    }
}
