using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Time.Systems
{
    public class MeshRendererMaterialChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _meshRenderers;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public MeshRendererMaterialChangeSystem(GameContext gameContext)
        {
            _meshRenderers = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
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