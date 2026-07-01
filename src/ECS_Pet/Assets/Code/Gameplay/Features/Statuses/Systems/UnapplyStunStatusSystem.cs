using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Statuses.Systems
{
    public class UnapplyStunStatusSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _unappliedStuns;
        private readonly IGroup<GameEntity> _allStuns;
        private readonly List<GameEntity> _buffer = new(32);

        public UnapplyStunStatusSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _unappliedStuns = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Status,
                GameMatcher.StunStatus,
                GameMatcher.TargetId,
                GameMatcher.Unapplied));
            
            _allStuns = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Status,
                GameMatcher.StunStatus,
                GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (GameEntity stun in _unappliedStuns.GetEntities(_buffer))
            {
                GameEntity target = _gameContext.GetEntityWithId(stun.TargetId);

                if (target != null && HasActiveStun(target.Id) == false)
                {
                    target.isStunned = false;
                }
            }
        }

        private bool HasActiveStun(int targetId)
        {
            foreach (GameEntity stun in _allStuns)
            {
                if (stun.TargetId == targetId && stun.isUnapplied == false)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
