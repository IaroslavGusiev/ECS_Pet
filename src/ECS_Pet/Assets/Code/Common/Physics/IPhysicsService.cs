using UnityEngine;

namespace Code.Common.Physics
{
    public interface IPhysicsService
    {
        GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask);
    }
}