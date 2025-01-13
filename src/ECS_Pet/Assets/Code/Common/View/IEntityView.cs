using UnityEngine;

namespace Code.Common.View
{
    public interface IEntityView
    {
        GameEntity Entity { get; }
        GameObject GameObject { get; }
        
        void SetEntity(GameEntity entity);
        void ReleaseEntity();
    }
}