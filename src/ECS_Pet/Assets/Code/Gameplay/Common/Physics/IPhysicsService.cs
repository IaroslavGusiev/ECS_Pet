using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Time
{
    public interface IPhysicsService
    {
        bool EnableDebugDrawing { get; set; }
        
        GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask);
        List<GameEntity> RaycastInBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask);
        List<GameEntity> SphereRaycast(Vector3 position, float radius, int layerMask);
    }
}