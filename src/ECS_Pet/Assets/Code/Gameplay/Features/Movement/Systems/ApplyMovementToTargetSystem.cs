using Entitas;
using UnityEngine;
using Code.Gameplay.Common.Time;

namespace Code.Gameplay.Features.Movement
{
    public class ApplyMovementToTargetSystem : IExecuteSystem
    {
        private const float MoveSpeed = 2.0f;

        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _movers;

        public ApplyMovementToTargetSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            
            _movers = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Moving,
                GameMatcher.Direction,
                GameMatcher.MovementTarget,
                GameMatcher.DistanceToTarget,
                GameMatcher.MovementAvailable
            }));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                Vector3 currentPosition = mover.WorldPosition;
                float distanceToTarget = mover.DistanceToTarget;
                
                float moveDistance = MoveSpeed * _timeService.DeltaTime;
                
                float actualMoveDistance = Mathf.Min(moveDistance, distanceToTarget);
                
                Vector3 direction = mover.Direction;
                
                var newPosition = new Vector3
                (
                    currentPosition.x + direction.x * actualMoveDistance,
                    currentPosition.y, 
                    currentPosition.z + direction.z * actualMoveDistance
                );
                
                mover.ReplaceWorldPosition(newPosition);
            }
        }
    }
}