using PrimeTween;
using UnityEngine;
using Code.Gameplay.Features.GameBoard;

namespace Code.Common.Physics
{
    public class PhysicsService : IPhysicsService
    {
        private readonly Camera _camera = Camera.main;
        
        public GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            
            if (UnityEngine.Physics.Raycast(ray, out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask))
            {
                var cell = hit.collider.GetComponent<CellBehaviour>();
                if (cell)
                {
                    Tween.PunchScale(cell.transform, Vector3.one * 0.4f, 0.5f);
                }
            }
            
            return default;
        }
    }
}