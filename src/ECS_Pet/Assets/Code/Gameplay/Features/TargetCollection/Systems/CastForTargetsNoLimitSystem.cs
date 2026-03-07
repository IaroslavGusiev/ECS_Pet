using Entitas;
using System.Linq;
using Code.StaticData;
using Code.Common.Extensions;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;

namespace Code.Gameplay.TargetCollection
{
    public class CastForTargetsNoLimitSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<GameEntity> _ready;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public CastForTargetsNoLimitSystem(GameContext gameContext, IPhysicsService physicsService)
        {
            _physicsService = physicsService;
            
            _ready = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]  
                {
                    GameMatcher.Radius,
                    GameMatcher.TargetBuffer,
                    GameMatcher.WorldPosition,
                    GameMatcher.ReadyToCollectTargets
                })
            );
        }
        
        public void Execute()
        {
            foreach (GameEntity entity in _ready.GetEntities(_buffer))
            {
                List<int> targetsInRadius = TargetsInRadius(entity);

                // foreach (int targetId in targetsInRadius)
                // {
                //     Debug.Log($"<color=yellow>{targetId}</color>");
                // }

                entity.TargetBuffer.AddRange(targetsInRadius);
            }
        }
    
        private List<int> TargetsInRadius(GameEntity entity)
        {
            return _physicsService
                .SphereRaycast(entity.WorldPosition, entity.Radius, CollisionLayer.Monster.AsMask())
                .Where(target => target.isDead == false) 
                .Select(target => target.Id)
                .ToList();
        }
    }
}