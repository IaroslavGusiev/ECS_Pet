using Entitas;

namespace Code.Gameplay.Features.Movement
{
    public class UpdateWorldRotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public UpdateWorldRotationSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Transform,
                GameMatcher.WorldRotation
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.Transform.rotation = entity.WorldRotation;
            }
        }
    }
}