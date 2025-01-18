using Entitas;

namespace Code.Gameplay.Features.Movement
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateTransformPositionSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.WorldPosition, 
                GameMatcher.Transform
            }));
        }
    
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.Transform.position = mover.WorldPosition;
            }
        }
    }
}