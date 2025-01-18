using UnityEngine;

namespace Code.Gameplay.Common
{
    public class PhysicsService : IPhysicsService
    {
        private readonly ICollisionRegistry _collisionRegistry;
        private readonly Camera _camera = Camera.main;

        public PhysicsService(ICollisionRegistry collisionRegistry) => 
            _collisionRegistry = collisionRegistry;

        public GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            
            return Physics.Raycast(ray, out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask) 
                ? _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID()) 
                : default;
        }
    }
}