using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    public abstract class CommonWindow : MonoBehaviour
    {
        [field: SerializeField] public AssetReference AssetReference { get; private set; }
        private bool _isInAnimation;
        
        public async UniTask Show()
        {
            if (_isInAnimation)
            {
                return;
            }
            _isInAnimation = true;
            await PlayShowAnimation();
            _isInAnimation = false;
        }

        public async UniTask Hide()
        {
            if (_isInAnimation)
            {
                return;
            }
            _isInAnimation = true;
            await PlayHideAnimation();
            _isInAnimation = false;
        }
        
        protected virtual async UniTask PlayShowAnimation()
        {
            await UniTask.CompletedTask;
        }
        
        protected virtual async UniTask PlayHideAnimation()
        {
            await UniTask.CompletedTask;
        }
    }
}