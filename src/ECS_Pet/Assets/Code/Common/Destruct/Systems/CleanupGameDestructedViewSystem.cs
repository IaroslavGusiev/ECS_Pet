using Entitas;
using UnityEngine;

namespace Code.Common.Destruct
{
    public class CleanupGameDestructedViewSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupGameDestructedViewSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Destructed, 
                GameMatcher.View
            }));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _entities)
            {
                entity.View.ReleaseEntity();
                Object.Destroy(entity.View.GameObject);
            }
        }
    }
}