using Entitas;
using System.Collections.Generic;

namespace Code.GameplayEffects.Systems
{
    public class CleanupProcessedEffects : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupProcessedEffects(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Effect,
                GameMatcher.Processed
            }));
        }

        public void Cleanup()
        {
            foreach (GameEntity effect in _effects.GetEntities(_buffer))
            {
                effect.Destroy();
            }
        }
    }
}