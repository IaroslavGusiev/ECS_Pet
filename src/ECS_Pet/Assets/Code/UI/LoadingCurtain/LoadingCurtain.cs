using PrimeTween;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Code.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const float TweenDuration = 0.33f;
        [SerializeField] private CanvasGroup curtain;
        
        public async UniTask Show()
        {
            await Tween
                .Alpha(curtain, endValue: 1, TweenDuration)
                .ToYieldInstruction()
                .ToUniTask();
        }

        public async UniTask Hide()
        {
            await Tween
                .Alpha(curtain, endValue: 0, TweenDuration)
                .ToYieldInstruction()
                .ToUniTask();
        }
    }
}