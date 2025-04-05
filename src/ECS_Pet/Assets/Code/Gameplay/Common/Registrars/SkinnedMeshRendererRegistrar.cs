using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Common.Time
{
    public class SkinnedMeshRendererRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
        
        public override void RegisterComponents() => 
            Entity.AddSkinnedMeshRenderer(skinnedMeshRenderer);

        public override void UnregisterComponents()
        {
            if (Entity.hasMeshRenderer)
            {
                Entity.RemoveMeshRenderer();
            }
        }
    }
}