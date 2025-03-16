using Zenject;
using Code.Common.Extensions;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Services;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAddressablesAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly IHUDRoot _hudRoot;

        public WindowFactory(
            IHUDRoot hudRoot, 
            IInstantiator instantiator,
            IAddressablesAssetProvider assetProvider)
        {
            _hudRoot = hudRoot;
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<TWindow> CreateWindow<TWindow>(AssetReference assetReference) where TWindow : BaseWindow
        {
            var prefab = await _assetProvider.Load<TWindow>(assetReference);
            
            return _instantiator
                .InstantiatePrefabForComponent<TWindow>(prefab, _hudRoot.HudRoot)
                .With(window => window.SetupOnInstantiate());
        }

        public async UniTask<TWindow> CreateWindow<TWindow, TArg>(AssetReference assetReference, TArg arg) where TWindow : BaseWindow<TArg>
        {
            var prefab = await _assetProvider.Load<TWindow>(assetReference);
            
            return _instantiator
                .InstantiatePrefabForComponent<TWindow>(prefab, _hudRoot.HudRoot)
                .With(window => window.SetupOnInstantiate(arg));
        }
    }
}