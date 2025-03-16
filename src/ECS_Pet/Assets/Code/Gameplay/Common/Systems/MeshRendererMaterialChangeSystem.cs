using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Systems
{
    public class MeshRendererMaterialChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _meshRenderers;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public MeshRendererMaterialChangeSystem(GameContext game)
        {
            _meshRenderers = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.MeshRenderer, 
                GameMatcher.MaterialChangeRequest
            }));
        }

        public void Execute()
        {
            foreach (GameEntity meshRenderer in _meshRenderers.GetEntities(_buffer))
            {
                meshRenderer.MeshRenderer.material = meshRenderer.MaterialChangeRequest;
                meshRenderer.RemoveMaterialChangeRequest();
            }
        }
    }
}