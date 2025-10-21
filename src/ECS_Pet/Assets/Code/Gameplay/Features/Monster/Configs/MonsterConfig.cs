using UnityEngine;
using Code.StaticData;
using NaughtyAttributes;

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
        [field: BoxGroup("Stats")] [field: SerializeField] public float AttackRange { get; private set; }
    }
}