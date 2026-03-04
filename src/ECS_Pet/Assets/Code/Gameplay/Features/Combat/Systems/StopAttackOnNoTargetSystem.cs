using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Combat
{
    public class StopAttackOnNoTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _attackers;
        private readonly List<GameEntity> _buffer = new(16);
        
        public StopAttackOnNoTargetSystem(GameContext gameContext)
        {
            _attackers = gameContext.GetGroup(GameMatcher
                    .AllOf(GameMatcher.Attacking)
                    .NoneOf(GameMatcher.TargetId));
        }
        
        public void Execute()
        {
            foreach (GameEntity attacker in _attackers.GetEntities(_buffer))
            {
                attacker.isAttacking = false;
            }
        }
    }
}