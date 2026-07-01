using Entitas;

namespace Code.Gameplay.Combat
{
    public class AttackRangeCheckSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _fighters;

        public AttackRangeCheckSystem(GameContext game)
        {
            _fighters = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.TargetId,
                GameMatcher.AttackRange,
                GameMatcher.DistanceToTarget
            }));
        }

        public void Execute()
        {
            foreach (GameEntity fighter in _fighters)
            {
                if (fighter.isStunned)
                {
                    fighter.isAttacking = false;
                    fighter.isMoving = false;
                    fighter.isMovementAvailable = false;
                    continue;
                }

                float distance = fighter.DistanceToTarget;

                if (distance <= fighter.AttackRange)
                {
                    fighter.isAttacking = true;
                    fighter.isMoving = false;
                    fighter.isMovementAvailable = false;
                }
                else
                {
                    fighter.isAttacking = false;
                    fighter.isMovementAvailable = true;
                }
            }
        }
    }
}
