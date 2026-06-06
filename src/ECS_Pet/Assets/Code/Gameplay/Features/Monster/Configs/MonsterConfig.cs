using UnityEngine;
using Code.StaticData;
using NaughtyAttributes;
using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Monster
{
    [CreateAssetMenu(fileName = "MonsterConfig", menuName = "Configs/MonsterConfig")]
    public class MonsterConfig : ScriptableObject
    {
        [field: SerializeField] public MonsterTypeId MonsterTypeId { get; private set; }
        [field: SerializeField] public string ViewPath { get; private set; }
        
        [field: BoxGroup("Stats")][field: SerializeField] public float MaxHp { get; private set; }
        [field: BoxGroup("Stats")][field: SerializeField] public float MaxMana { get; private set; }
        [field: BoxGroup("Stats")][field: SerializeField] public float Damage { get; private set; }
        [field: BoxGroup("Stats")][field: SerializeField] public float ManaRegen { get; private set; }

        [field: BoxGroup("Parameters")] [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: BoxGroup("Parameters")] [field: SerializeField] public float AttackRange { get; private set; }
        [field: BoxGroup("Parameters")] [field: SerializeField] public float TargetDetectionRadius { get; private set; }

        [field: Space(15)]
        [field: BoxGroup("Abilities")] [field: SerializeField] public AbilityConfig BasicAbilityConfig { get; private set; }
        [field: BoxGroup("Abilities")] [field: SerializeField] public AbilityConfig SpecialAbilityConfig { get; private set; }
    }
}
