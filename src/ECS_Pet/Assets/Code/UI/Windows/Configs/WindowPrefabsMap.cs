using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    [Serializable]
    public class WindowPrefabsMap
    {
#if UNITY_EDITOR
        [SerializeField] private List<CommonWindow> windows = new();
#endif
        private Dictionary<Type, AssetReference> _screenReferenceMap;
        
        public AssetReference GetPrefabReference<TScreen>() where TScreen : CommonWindow
        {
            if (_screenReferenceMap.TryGetValue(typeof(TScreen), out AssetReference screenReference))
            {
                return screenReference;
            }
            
            throw new IndexOutOfRangeException($"Can't find asset reference for window of type { typeof(TScreen) }");
        }
        
#if UNITY_EDITOR
        public void CreateMap()
        {
            _screenReferenceMap = new Dictionary<Type, AssetReference>();

            foreach (CommonWindow window in windows)
            {
                if (_screenReferenceMap.ContainsKey(window.GetType()))
                {
                    Debug.LogError($" Duplicate type found: { window.GetType() }");
                }
                else
                {
                    _screenReferenceMap.Add(window.GetType(), window.AssetReference);
                }
            }
        }
#endif
    }
}