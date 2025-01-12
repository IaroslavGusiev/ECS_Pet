using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.Services
{
    public interface IAddressablesAssetProvider
    {
        UniTask InitializeAsync();
        
        UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : MonoBehaviour;
        UniTask<T> LoadAndGetComponent<T>(string key) where T : MonoBehaviour;
        UniTask<T[]> LoadAll<T>(List<string> keys) where T : class;

        UniTask WarmupAssetsByLabel(string label);
        UniTask ReleaseAssetsByLabel(string label);
        UniTask<List<string>> FetchAssetKeysByLabel<T>(string label);
    }
}