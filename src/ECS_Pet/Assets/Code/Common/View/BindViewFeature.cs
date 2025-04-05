using Code.Infrastructure.Systems;
using Code.Gameplay.Common.Time.Systems;

namespace Code.Common.View
{
    public sealed class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systemFactory)
        { 
            // ExecuteSystem
           Add(systemFactory.Create<BindEntityViewFromPathSystem>());
           Add(systemFactory.Create<MeshRendererMaterialChangeSystem>());
           Add(systemFactory.Create<SkinnedMeshRendererMaterialChangeSystem>());
        }
    }
}