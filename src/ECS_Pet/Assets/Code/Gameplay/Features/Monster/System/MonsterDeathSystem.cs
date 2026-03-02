using Entitas;

namespace Code.Gameplay.Monster
{
    public class MonsterDeathSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _monsters;

        public MonsterDeathSystem(GameContext game)
        {
            _monsters = game.GetGroup(GameMatcher.AllOf(GameMatcher.Monster));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _monsters)
            {
                entity.isMovementAvailable = false;
                
                // TODO: play died animation
            }
        }
    }
}