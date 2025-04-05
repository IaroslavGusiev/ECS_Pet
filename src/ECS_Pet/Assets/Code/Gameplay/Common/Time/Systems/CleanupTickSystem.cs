using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Time
{
    public class CleanupTickSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _ticks;
        private readonly List<GameEntity> _buffer = new(capacity: 1);

        public CleanupTickSystem(GameContext gameContext)
        {
            _ticks = gameContext.GetGroup(GameMatcher.Tick);
        }

        public void Cleanup()
        {
            foreach (GameEntity tick in _ticks.GetEntities(_buffer))
            {
                tick.Destroy();
            }
        }
    }
}