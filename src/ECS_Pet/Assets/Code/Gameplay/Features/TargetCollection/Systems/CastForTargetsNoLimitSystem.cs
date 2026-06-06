using Entitas;
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
                    GameMatcher.LayerMask,
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
                FillTargetBuffer(entity);
            }
        }
    
        private void FillTargetBuffer(GameEntity entity)
        {
            foreach (GameEntity target in _physicsService.SphereRaycast(entity.WorldPosition, entity.Radius, entity.LayerMask))
            {
                if (IsValidTarget(target))
                {
                    entity.TargetBuffer.Add(target.Id);
                }
            }
        }

        private static bool IsValidTarget(GameEntity target) =>
            target is { isDead: false, isPlaced: true };
    }
}
