using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Common
{
    public class CommonComponents
    {
        [Game, Meta] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
        [Game] public class WorldPosition : IComponent { public Vector3 Value; }
        
        [Game] public class TransformComponent : IComponent { public Transform Value; }
        [Game] public class MeshRendererComponent : IComponent { public MeshRenderer Value; }
        
        [Game] public class LayerMask : IComponent { public int Value; } // TODO: можливо пізніше винисте TargetCollection фічу
    }
}