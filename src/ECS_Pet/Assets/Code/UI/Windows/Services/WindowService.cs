using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    public class WindowService : IWindowService
    {
        private readonly IWindowFactory _widowFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly Dictionary<Type, CommonWindow> _activeWindows = new();
        
        private WindowsConfig _config;

        public WindowService(IWindowFactory widowFactory, IStaticDataService staticDataService)
        {
            _widowFactory = widowFactory;
            _staticDataService = staticDataService;
        }
        
        public void Initialize() => 
            _config = _staticDataService.GetWindowsConfig();
        
        public async UniTask<TScreen> ShowWindow<TScreen>() where TScreen : BaseWindow
        {
            AssetReference assetReference = _config.GetPrefabReference<TScreen>();
            var screenInstance = await CreateWindow<TScreen>(assetReference);
            RegisterWindow(screenInstance);
            return await PlayShowScreenAnimation(screenInstance);
        }
        
        public async UniTask<TScreen> ShowWindow<TScreen, TArg>(TArg arg) where TScreen : BaseWindow<TArg>
        {
            AssetReference assetReference = _config.GetPrefabReference<TScreen>();
            TScreen screenInstance = await CreateWindow<TScreen, TArg>(arg, assetReference);
            RegisterWindow(screenInstance);
            return await PlayShowScreenAnimation(screenInstance);
        }
        
        public T GetWindowFromActive<T>() where T : CommonWindow
        {
            return _activeWindows.TryGetValue(typeof(T), out CommonWindow window)
                ? window as T
                : null;
        }

        private async UniTask<TScreen> PlayShowScreenAnimation<TScreen>(TScreen screenInstance) where TScreen : CommonWindow
        {
            await screenInstance.Show();
            return screenInstance;
        }
        
        private void RegisterWindow<TScreen>(TScreen screenInstance) where TScreen : CommonWindow => 
            _activeWindows[typeof(TScreen)] = screenInstance;

        private async UniTask<TScreen> CreateWindow<TScreen>(AssetReference assetReference) where TScreen : BaseWindow => 
            await _widowFactory.CreateWindow<TScreen>(assetReference);

        private async UniTask<TScreen> CreateWindow<TScreen, TArg>(TArg arg, AssetReference assetReference) where TScreen : BaseWindow<TArg> => 
            await _widowFactory.CreateWindow<TScreen, TArg>(assetReference, arg);
    }
}