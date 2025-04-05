using UnityEngine;
using System.Collections.Generic;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Fighter
{
    public class StatsSliderHolder : MonoBehaviour
    {
        [SerializeField] private List<StatsSliderBehaviour> statsSliders;
        
        public void Enable() => 
            statsSliders.ForEach(slider => slider.Enable());

        public void Disable() => 
            statsSliders.ForEach(slider => slider.Disable());
        
        public void UpdateSlider(Stats stat, float value, float maxValue)
        {
            StatsSliderBehaviour slider = statsSliders.Find(slider => slider.Stats == stat);
            
            if (slider == false)
            {
                Debug.LogError($"No slider found for stat: {stat}");
                return;
            }
            
            slider.UpdateSlider(value, maxValue);
        }
    }
}