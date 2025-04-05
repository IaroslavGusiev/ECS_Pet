using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using Code.Common.Extensions;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Fighter
{
    public class StatsSliderBehaviour : MonoBehaviour
    {
        [field: SerializeField] public Stats Stats { get; private set; }
        [SerializeField] private Slider slider;

        private Tween _sliderTween;
        private float _targetValue;

        public void Enable() => 
            gameObject.SetActive(true);

        public void Disable() => 
            gameObject.SetActive(false);

        public void UpdateSlider(float value, float maxValue)
        {
            float newTargetValue = value / maxValue;
            
            if (CheckForSameValue(newTargetValue))
            {
                return;
            }

            _targetValue = newTargetValue; 
            
            _sliderTween.StopIfAlive();
            _sliderTween = Tween.UISliderValue(slider, endValue: newTargetValue, duration: 0.3f);
        }

        private bool CheckForSameValue(float newTargetValue) => 
            Mathf.Approximately(slider.value, newTargetValue) || Mathf.Approximately(_targetValue, newTargetValue);
    }
}