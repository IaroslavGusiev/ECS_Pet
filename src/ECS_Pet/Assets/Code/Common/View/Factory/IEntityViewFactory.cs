using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Code.Common.View.Factory
{
    public interface IEntityViewFactory
    {
        UniTask<EntityBehaviour> CreateViewForEntity(GameEntity entity);
    }
}