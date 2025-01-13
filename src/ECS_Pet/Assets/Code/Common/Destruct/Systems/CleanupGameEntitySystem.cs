using Entitas;
using System.Collections.Generic;

namespace Code.Common.Destruct 
{
    public class CleanupGameEntitySystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(capacity: 128);

        public CleanupGameEntitySystem(GameContext gameContext) => 
            _entities = gameContext.GetGroup(GameMatcher.Destructed);

        public void Cleanup()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
                entity.Destroy();
        }
    }
}