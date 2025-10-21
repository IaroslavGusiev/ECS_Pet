using UnityEngine;
using Code.StaticData;
using Code.GameplayEffects;
using System.Collections.Generic;

namespace Code.Gameplay.Abilities.Configs
{
    [CreateAssetMenu(fileName = "FighterConfig", menuName = "Configs/AbilityConfig")]
    public class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityTypeId AbilityTypeId { get; set; }
        [field: SerializeField] public List<EffectConfig> EffectConfigs = new();
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public float AnimationDelay { get; set; }
    }
}