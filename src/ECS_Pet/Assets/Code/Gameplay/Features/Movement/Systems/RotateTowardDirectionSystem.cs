using Entitas;
using UnityEngine;
using Code.Gameplay.Common.Time;

namespace Code.Gameplay.Features.Movement
{
    public class RotateTowardDirectionSystem : IExecuteSystem
    {
        private const float RotationSpeed = 10f;
        
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public RotateTowardDirectionSystem(GameContext game, ITimeService timeService)
        {
            _timeService = timeService;
            
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Direction,
                GameMatcher.WorldRotation,
                GameMatcher.MovementAvailable
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.Direction.magnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(entity.Direction);
                    Quaternion currentRotation = entity.WorldRotation;
                    
                    Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, _timeService.DeltaTime * RotationSpeed);
                    entity.ReplaceWorldRotation(newRotation);
                }
            }
        }
    }
}