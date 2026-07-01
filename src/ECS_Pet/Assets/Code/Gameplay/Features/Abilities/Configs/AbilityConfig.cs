using UnityEngine;
using Code.StaticData;
using Code.GameplayEffects;
using System.Collections.Generic;
using Code.Gameplay.Statuses;

namespace Code.Gameplay.Abilities.Configs
{
    [CreateAssetMenu(fileName = "FighterConfig", menuName = "Configs/AbilityConfig")]
    public class AbilityConfig : ScriptableObject
    {
        [field: SerializeField] public AbilityTypeId AbilityTypeId { get; private set; }
        
        [field: SerializeField] public List<EffectConfig> EffectConfigs { get; private set; } = new();
        [field: SerializeField] public List<StatusSetup> StatusSetups { get; private set; } = new();
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; } = new();
        
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AnimationDelay { get; private set; }
    }
}
