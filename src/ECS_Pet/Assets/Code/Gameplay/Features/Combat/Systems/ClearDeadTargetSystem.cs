using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Combat
{
    public class ClearDeadTargetSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _entitiesWithTarget;
        private readonly List<GameEntity> _buffer = new(capacity: 16);

        public ClearDeadTargetSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _entitiesWithTarget = _gameContext.GetGroup(GameMatcher.AllOf(matchers: GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (GameEntity target in _entitiesWithTarget.GetEntities(_buffer))
            {
                GameEntity targetEntity = _gameContext.GetEntityWithId(target.TargetId);
                
                if (targetEntity == null || targetEntity.isDead)
                {
                    target.RemoveTargetId();
                }
            }
        }
    }
}