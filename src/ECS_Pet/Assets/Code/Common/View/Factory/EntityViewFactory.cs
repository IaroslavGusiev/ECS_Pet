using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Services;

namespace Code.Common.View.Factory
{
    public class EntityViewFactory : IEntityViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAddressablesAssetProvider _assetProvider;
        private readonly Vector3 _farAway = new(-999, 999, 0);

        public EntityViewFactory(IAddressablesAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }
        
        public async UniTask<EntityBehaviour> CreateViewForEntity(GameEntity entity)
        {
            var viewPrefab = await _assetProvider.LoadAndGetComponent<EntityBehaviour>(entity.ViewPath);

            if (entity.hasView)
            {
                return entity.View as EntityBehaviour;
            }
            
            var view = _instantiator.InstantiatePrefabForComponent<EntityBehaviour>(
                viewPrefab,
                position: _farAway, 
                Quaternion.identity, 
                parentTransform: null);

            view.SetEntity(entity);
            return view;
        }
    }
}
