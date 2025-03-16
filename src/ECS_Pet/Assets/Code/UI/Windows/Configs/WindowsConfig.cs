using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [SerializeField] private WindowPrefabsMap windowPrefabsMap;
        
        private void OnValidate() => 
            CreateScreenPrefabMap();

        public AssetReference GetPrefabReference<TScreen>() where TScreen : CommonWindow => 
            windowPrefabsMap.GetPrefabReference<TScreen>();

        private void CreateScreenPrefabMap()
        {
#if UNITY_EDITOR
            windowPrefabsMap.CreateMap();
#endif
        }
    }
}