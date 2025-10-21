using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Common.Time
{
    public class CommonComponents
    {
        [Game, Meta] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
        [Game] public class WorldPosition : IComponent { public Vector3 Value; }
        [Game] public class WorldRotation : IComponent { public Quaternion Value; }
        
        [Game] public class TransformComponent : IComponent { public Transform Value; }
        [Game] public class MeshRendererComponent : IComponent { public MeshRenderer Value; }
        [Game] public class SkinnedMeshRendererComponent : IComponent { public SkinnedMeshRenderer Value; }
        
        [Game] public class MaterialChangeRequest : IComponent { public Material Value; }
        [Game] public class ReadyToUse : IComponent { }
        
        [Game] public class LayerMask : IComponent { public int Value; } // TODO: later move to Target Collection
        [Game] public class Tick : IComponent { public float Value; } // TODO: moved to some time based feature
    }
}