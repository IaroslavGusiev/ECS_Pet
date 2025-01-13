using UnityEngine;

namespace Code.Gameplay.Common
{
    public interface IPhysicsService
    {
        GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask);
    }
}