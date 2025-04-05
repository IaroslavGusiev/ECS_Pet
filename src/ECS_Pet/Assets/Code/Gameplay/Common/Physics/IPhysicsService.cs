using UnityEngine;

namespace Code.Gameplay.Common.Time
{
    public interface IPhysicsService
    {
        GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask);
    }
}