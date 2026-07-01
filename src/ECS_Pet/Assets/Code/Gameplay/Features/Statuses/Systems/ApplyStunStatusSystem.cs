using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Statuses.Systems
{
    public class ApplyStunStatusSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _statuses;
        private readonly List<GameEntity> _buffer = new(32);

        public ApplyStunStatusSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            _statuses = gameContext.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Status,
                    GameMatcher.StunStatus,
                    GameMatcher.TargetId)
                .NoneOf(GameMatcher.Affected));
        }

        public void Execute()
        {
            foreach (GameEntity status in _statuses.GetEntities(_buffer))
            {
                GameEntity target = _gameContext.GetEntityWithId(status.TargetId);

                if (target == null || target.isDead)
                {
                    status.isUnapplied = true;
                    continue;
                }

                target.isStunned = true;
                target.isMoving = false;
                target.isAttacking = false;
                target.isMovementAvailable = false;

                status.isAffected = true;
            }
        }
    }
}
