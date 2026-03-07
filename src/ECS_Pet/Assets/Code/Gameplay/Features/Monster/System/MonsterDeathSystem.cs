using Entitas;

namespace Code.Gameplay.Monster
{
    public class MonsterDeathSystem : IExecuteSystem
    {
        private const float DeathAnimationTime = 2.5f; // TODO: take it from config
        
        private readonly IGroup<GameEntity> _monsters;

        public MonsterDeathSystem(GameContext game)
        {
            _monsters = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Monster, 
                GameMatcher.Dead, 
                GameMatcher.ProcessingDeath
            }));
        }

        public void Execute()
        {
            foreach (GameEntity monster in _monsters)
            {
                monster.isMovementAvailable = false;
                
                // monster.RemoveTargetCollectionComponents();

                if (monster.hasFighterAnimator)
                {
                    monster.FighterAnimator.PlayDied();
                }

                monster.ReplaceSelfDestructTimer(DeathAnimationTime);
            }
        }
    }
}