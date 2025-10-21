using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Common.Time
{
    public class PhysicsService : IPhysicsService
    {
        public bool EnableDebugDrawing { get; set; } = true;
        
        private readonly ICollisionRegistry _collisionRegistry;
        private readonly Camera _camera = Camera.main;

        public PhysicsService(ICollisionRegistry collisionRegistry) => 
            _collisionRegistry = collisionRegistry;

        public GameEntity RaycastFromScreen(Vector3 screenPosition, int layerMask)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (EnableDebugDrawing)
            {
                DrawRay(ray.origin, ray.direction, Color.yellow);
            }
            
            return Physics.Raycast(ray, out RaycastHit hit, maxDistance: Mathf.Infinity, layerMask) 
                ? _collisionRegistry.Get<GameEntity>(hit.collider.GetInstanceID()) 
                : null;
        }
        
        public List<GameEntity> RaycastInBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
        {
            var buffer = new Collider[16];
            int hitCount = Physics.OverlapBoxNonAlloc(center, halfExtents, results: buffer, orientation, layerMask);

            if (EnableDebugDrawing)
            {
                DrawBox(center, halfExtents, orientation, Color.cyan);
            }
            
            var results = new List<GameEntity>(hitCount);
            
            for (var i = 0; i < hitCount; i++)
            {
                Collider collider = buffer[i];
                
                var entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
                if (entity == null)
                {
                    continue;
                }
                results.Add(entity);
            }
            return results;
        }

        public List<GameEntity> SphereRaycast(Vector3 position, float radius, int layerMask)
        {
            var buffer = new Collider[16];
            int hitCount = Physics.OverlapSphereNonAlloc(position, radius, buffer, layerMask);
    
            if (EnableDebugDrawing)
            {
                DrawSphere(position, radius, Color.red);
            }

            var results = new List<GameEntity>(hitCount);

            for (var i = 0; i < hitCount; i++)
            {
                Collider collider = buffer[i];
                
                var entity = _collisionRegistry.Get<GameEntity>(collider.GetInstanceID());
                if (entity == null)
                {
                    continue;
                }
                results.Add(entity);
            }
            return results;
        }
        
        private static void DrawRay(Vector3 origin, Vector3 direction, Color color, float length = 100f, float duration = 0.25f) => 
            Debug.DrawRay(start: origin, dir: direction.normalized * length, color, duration);
        
        private static void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color, float duration = 0.25f)
        {
            Vector3 size = halfExtents * 2f;
            Matrix4x4 matrix = Matrix4x4.TRS(center, orientation, size);
            var verts = new Vector3[8];

            var i = 0;
            for (int x = -1; x <= 1; x += 2)
            {
                for (int y = -1; y <= 1; y += 2)
                {
                    for (int z = -1; z <= 1; z += 2)
                    {
                        verts[i++] = matrix.MultiplyPoint3x4(new Vector3(x, y, z) * 0.5f);
                    }
                }
            }

            DrawEdge(0, 1); DrawEdge(1, 3); DrawEdge(3, 2); DrawEdge(2, 0); 
            DrawEdge(4, 5); DrawEdge(5, 7); DrawEdge(7, 6); DrawEdge(6, 4); 
            DrawEdge(0, 4); DrawEdge(1, 5); DrawEdge(2, 6); DrawEdge(3, 7); 
            return;

            void DrawEdge(int a, int b) => Debug.DrawLine(verts[a], verts[b], color, duration);
        }
        
        private static void DrawSphere(Vector3 center, float radius, Color color, float duration = 0.25f, int segments = 20)
        {
            float angleStep = 360f / segments;

            // Draw circles in XY plane
            for (int i = 0; i < segments; i++)
            {
                float angle1 = Mathf.Deg2Rad * i * angleStep;
                float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

                Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1), 0) * radius;
                Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0) * radius;
                Debug.DrawLine(point1, point2, color, duration);
            }

            // Draw circles in XZ plane
            for (int i = 0; i < segments; i++)
            {
                float angle1 = Mathf.Deg2Rad * i * angleStep;
                float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

                Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1)) * radius;
                Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * radius;
                Debug.DrawLine(point1, point2, color, duration);
            }

            // Draw circles in YZ plane
            for (int i = 0; i < segments; i++)
            {
                float angle1 = Mathf.Deg2Rad * i * angleStep;
                float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

                Vector3 point1 = center + new Vector3(0, Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
                Vector3 point2 = center + new Vector3(0, Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;
                Debug.DrawLine(point1, point2, color, duration);
            }
        }
    }
}