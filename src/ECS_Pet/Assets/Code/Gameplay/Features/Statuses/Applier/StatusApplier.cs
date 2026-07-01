using Entitas;
using System.Collections.Generic;
using Code.Gameplay.Statuses.Factory;

namespace Code.Gameplay.Statuses.Applier
{
    public class StatusApplier : IStatusApplier
    {
        private readonly IStatusFactory _statusFactory;
        private readonly IGroup<GameEntity> _statuses;
        private readonly List<GameEntity> _buffer = new(32);

        public StatusApplier(GameContext gameContext, IStatusFactory statusFactory)
        {
            _statusFactory = statusFactory;
            _statuses = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Status,
                GameMatcher.StatusTypeId,
                GameMatcher.TargetId));
        }

        public GameEntity ApplyStatus(StatusSetup setup, int producerId, int targetId)
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                if (status.StatusTypeId == setup.StatusTypeId &&
                    status.TargetId == targetId &&
                    status.isUnapplied == false)
                {
                    return status.ReplaceTimeLeft(setup.Duration);
                }
            }

            return _statusFactory.CreateStatus(setup, producerId, targetId);
        }
    }
}
