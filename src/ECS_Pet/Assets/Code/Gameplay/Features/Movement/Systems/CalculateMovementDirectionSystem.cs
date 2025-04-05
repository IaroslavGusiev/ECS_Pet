using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
    public class CalculateMovementDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CalculateMovementDirectionSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.WorldRotation, 
                GameMatcher.MovementTarget
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                Vector3 direction = CalculateNormalizedDirection(entity);

                if (entity.hasDirection) 
                {
                    entity.ReplaceDirection(direction);
                } 
                else 
                {
                    entity.AddDirection(direction);
                }
            }
        }

        private static Vector3 CalculateNormalizedDirection(GameEntity entity)
        {
            Vector3 currentPosition = entity.WorldPosition;
            Vector3 targetPosition = entity.MovementTarget;
            
            return new Vector3(targetPosition.x - currentPosition.x, 0, targetPosition.z - currentPosition.z).normalized;
        }
    }
}