using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Systems
{
    public class MeshRendererMaterialChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public MeshRendererMaterialChangeSystem(GameContext game)
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
                cell.MeshRenderer.material = cell.MaterialChangeRequest;
                cell.RemoveMaterialChangeRequest();
            }
        }
    }
}