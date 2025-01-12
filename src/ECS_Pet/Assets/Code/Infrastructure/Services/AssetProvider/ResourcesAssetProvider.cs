using UnityEngine;
using System.Collections.Generic;

namespace Code.Infrastructure.Services
{
    public class ResourcesAssetProvider : IResourcesAssetProvider
    {
        private readonly Dictionary<string, Object> _cachedResourcesObjects = new();
        
        public T LoadFromResources<T>(string path) where T : Object 
        {
            if (_cachedResourcesObjects.TryGetValue(path, out Object prefab))
            {
                return prefab as T;
            }

            Object loadedPrefab = Resources.Load<T>(path);
            _cachedResourcesObjects[path] = loadedPrefab;
            return (T) loadedPrefab;
        }
    }
}