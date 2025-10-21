using Entitas;
using UnityEngine;

namespace Code.Gameplay.TargetCollection
{
    public class SelectNearestTargetSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _entities;

        public SelectNearestTargetSystem(GameContext game, GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.TargetBuffer, 
                GameMatcher.WorldPosition
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            {
                if (entity.TargetBuffer.Count == 0)
                {
                    continue;
                }

                int nearestTargetId = FindNearestTarget(entity);
                entity.ReplaceTargetId(nearestTargetId);
            }
        }
        
        private int FindNearestTarget(GameEntity entity)
        {
            var minDistanceSqr = float.MaxValue;
            int nearestId = -1;

            Vector3 origin = entity.WorldPosition;

            foreach (int targetId in entity.TargetBuffer)
            {
                GameEntity target = _gameContext.GetEntityWithId(targetId);

                if (target is not { hasWorldPosition: true })
                {
                    continue;
                }

                float sqrDistance = (target.WorldPosition - origin).sqrMagnitude;
                if (sqrDistance < minDistanceSqr == false)
                {
                    continue;
                }
                
                minDistanceSqr = sqrDistance;
                nearestId = targetId;
            }

            return nearestId;
        }
    }
}