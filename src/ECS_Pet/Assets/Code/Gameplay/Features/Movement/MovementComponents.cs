using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Movement
{
    [Game] public class MovementTarget : IComponent { public Vector3 Value; }
    [Game] public class Direction : IComponent { public Vector3 Value; }
    [Game] public class DistanceToTarget : IComponent { public float Value; }
    
    [Game] public class Moving : IComponent { }
    [Game] public class MovementAvailable : IComponent { }
}