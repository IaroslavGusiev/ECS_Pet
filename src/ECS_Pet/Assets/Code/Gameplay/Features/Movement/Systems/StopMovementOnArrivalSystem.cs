using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.Movement
{
    public class StopMovementOnArrivalSystem : IExecuteSystem
    {
        private const float ReachDistance = 0.3f; 
        
        private readonly IGroup<GameEntity> _movers;
        private readonly List<GameEntity> _buffer = new(capacity: 32);

        public StopMovementOnArrivalSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Direction,
                GameMatcher.MovementTarget,
                GameMatcher.DistanceToTarget,
                GameMatcher.MovementAvailable,
            }));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers.GetEntities(_buffer))
            {
                float distanceToTarget = mover.DistanceToTarget;
                
                if (distanceToTarget <= ReachDistance)
                {
                    mover.isMoving = false;
                    mover.isMovementAvailable = false;
                }
            }
        }
    }
}