using UnityEngine;

namespace Code.Infrastructure.Services
{
    public interface IResourcesAssetProvider
    {
        T LoadFromResources<T>(string path) where T : Object;
    }
}