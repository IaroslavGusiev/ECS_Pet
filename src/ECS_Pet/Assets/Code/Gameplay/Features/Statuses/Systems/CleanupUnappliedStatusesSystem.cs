using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Statuses.Systems
{
    public class CleanupUnappliedStatusesSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _statuses;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupUnappliedStatusesSystem(GameContext gameContext)
        {
            _statuses = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Status,
                GameMatcher.Unapplied));
        }

        public void Cleanup()
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                status.isDestructed = true;
            }
        }
    }
}
