using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Lifetime
{
    public class MarkDeadSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(64);

        public MarkDeadSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher
                .AllOf(matchers: new[]
                {
                    GameMatcher.CurrentHp,
                    GameMatcher.MaxHp
                })
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                if (entity.CurrentHp <= 0)
                {
                    entity.isDead = true;
                    entity.isProcessingDeath = true;
                }
            }
        }
    }
}