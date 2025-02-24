using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Systems
{
    public class SkinnedMeshRendererMaterialChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public SkinnedMeshRendererMaterialChangeSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.MeshRenderer, 
                GameMatcher.MaterialChangeRequest
            }));
        }

        public void Execute()
        {
            foreach (GameEntity cell in _entities.GetEntities(_buffer))
            {
                cell.SkinnedMeshRenderer.material = cell.MaterialChangeRequest;
                cell.RemoveMaterialChangeRequest();
            }
        }
    }
}