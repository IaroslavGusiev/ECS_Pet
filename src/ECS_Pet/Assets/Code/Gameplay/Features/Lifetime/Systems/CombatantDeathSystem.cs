using Entitas;

namespace Code.Gameplay.Lifetime
{
    public class CombatantDeathSystem : IExecuteSystem
    {
        private const float DeathAnimationTime = 2f; // TODO: take it from config
        
        private readonly IGroup<GameEntity> _combatants;

        public CombatantDeathSystem(GameContext game)
        {
            _combatants = game.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Dead,
                    GameMatcher.ProcessingDeath)
                .AnyOf(
                    GameMatcher.Fighter,
                    GameMatcher.Monster));
        }

        public void Execute()
        {
            foreach (GameEntity combatant in _combatants)
            {
                combatant.isMovementAvailable = false;
                combatant.isMoving = false;
                combatant.isAttacking = false;

                if (combatant.hasCombatantAnimator)
                {
                    combatant.CombatantAnimator.PlayDied();
                }

                combatant.ReplaceSelfDestructTimer(DeathAnimationTime);
            }
        }
    }
}
