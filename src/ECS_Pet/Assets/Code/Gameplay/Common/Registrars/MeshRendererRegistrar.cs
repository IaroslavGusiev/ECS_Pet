using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Common.Time
{
    public class MeshRendererRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private MeshRenderer meshRenderer;
        
        public override void RegisterComponents() => 
            Entity.AddMeshRenderer(meshRenderer);

        public override void UnregisterComponents()
        {
            if (Entity.hasMeshRenderer)
            {
                Entity.RemoveMeshRenderer();
            }
        }
    }
}