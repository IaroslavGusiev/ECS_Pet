using UnityEngine;

namespace Code.Common.View
{
    public abstract class EntityDependant : MonoBehaviour
    {
        [SerializeField] private EntityBehaviour entityView;

        public GameEntity Entity => entityView 
            ? entityView.Entity 
            : null;

        private void Awake()
        {
            if (entityView == false)
            {
                entityView = GetComponent<EntityBehaviour>();
            }
        }
    }
}