using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Monster
{
    public class FinalizeMonsterDeathProcessingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _monsters;
        private readonly List<GameEntity> _buffer = new(16);

        public FinalizeMonsterDeathProcessingSystem(GameContext game)
        {
            _monsters = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Dead, 
                GameMatcher.Monster, 
                GameMatcher.ProcessingDeath
            }));
        }

        public void Execute()
        {
            foreach (GameEntity monster in _monsters.GetEntities(_buffer))
            {
                monster.isProcessingDeath = false;
            }
        }
    }
}