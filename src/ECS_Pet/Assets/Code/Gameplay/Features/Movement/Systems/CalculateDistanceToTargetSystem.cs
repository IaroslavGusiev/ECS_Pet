using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
    public class CalculateDistanceToTargetSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CalculateDistanceToTargetSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.WorldPosition, 
                GameMatcher.MovementTarget
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                Vector3 currentPosition = entity.WorldPosition;
                Vector3 targetPosition = entity.MovementTarget;
                
                float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);

                entity.ReplaceDistanceToTarget(distanceToTarget);
            }
        }
    }
}