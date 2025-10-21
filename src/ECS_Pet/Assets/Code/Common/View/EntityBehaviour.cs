using Zenject;
using UnityEngine;
using Code.Gameplay.Common.Time;

namespace Code.Common.View
{
    public class EntityBehaviour : MonoBehaviour, IEntityView
    {
        public GameEntity Entity { get; private set; }
        public GameObject GameObject => gameObject;

        private ICollisionRegistry _collisionRegistry;

        [Inject]
        public void Construct(ICollisionRegistry collisionRegistry) => 
            _collisionRegistry = collisionRegistry;

        public void SetEntity(GameEntity entity)
        {
            Entity = entity;
            Entity.AddView(this);
            Entity.Retain(owner: this);

            RegisterEntityComponents();
            RegisterColliders();
        }

        public void ReleaseEntity()
        {
            UnregisterEntityComponents();
            UnregisterColliders();
            
            Entity.Release(owner: this);
            Entity = null;
        }
        
        private void RegisterEntityComponents()
        {
            foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
            {
                registrar.RegisterComponents();
            }
        }

        private void UnregisterEntityComponents()
        {
            foreach (IEntityComponentRegistrar registrar in GetComponentsInChildren<IEntityComponentRegistrar>())
            {
                registrar.UnregisterComponents();
            }
        }

        private void RegisterColliders()
        {
            foreach (Collider collider2d in GetComponentsInChildren<Collider>(includeInactive: true))
            {
                _collisionRegistry.Register(collider2d.GetInstanceID(), Entity);
            }
        }

        private void UnregisterColliders()
        {
            foreach (Collider collider2d in GetComponentsInChildren<Collider>(includeInactive: true))
            {
                _collisionRegistry.Unregister(collider2d.GetInstanceID());
            }
        }
    }
}