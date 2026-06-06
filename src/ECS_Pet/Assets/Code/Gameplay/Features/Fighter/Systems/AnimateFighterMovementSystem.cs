using Entitas;

namespace Code.Gameplay.Fighter
{
    public class AnimateFighterMovementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _combatants;

        public AnimateFighterMovementSystem(GameContext game)
        {
            _combatants = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.CombatantAnimator
            }));
        }

        public void Execute()
        {
            foreach (GameEntity combatant in _combatants)
            {
                if (combatant.isDead)
                {
                    continue;
                }

                if (combatant.isMoving)
                {
                    combatant.CombatantAnimator.Walk();
                }
                else
                {
                    combatant.CombatantAnimator.Idle();
                }
            }
        }
    }
}
