using System;
using Code.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Services
{
    public interface ISceneLoader
    {
        UniTask Load(SceneName scene, Action onLoaded = null);
        UniTask UnloadScene(SceneName scene, Action onUnloaded = null);
        UniTask LoadInAdditiveMode(SceneName scene, Action onLoaded = null);
    }
}