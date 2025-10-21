using UnityEngine;
using Code.StaticData;
using NaughtyAttributes;
using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Fighter
{
    [CreateAssetMenu(fileName = "FighterConfig", menuName = "Configs/FighterConfig")]
    public class FighterConfig : ScriptableObject
    {
        [field: SerializeField] public FighterTypeId FighterTypeId { get; private set; }
        [field: SerializeField] public string ViewPath { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        [field: ShowAssetPreview] [field: SerializeField] public Sprite Icon { get; private set; }
        
        [field: BoxGroup("Stats")] [field: SerializeField] public float MaxHp { get; private set; }
        [field: BoxGroup("Stats")] [field: SerializeField] public float MaxMana { get; private set; }
        [field: BoxGroup("Stats")] [field: SerializeField] public float Damage { get; private set; }
        [field: BoxGroup("Stats")] [field: SerializeField] public float ManaRegen { get; private set; }
        [field: BoxGroup("Stats")] [field: SerializeField] public float AttackRange { get; private set; }
        
        [field: Space(15)]
        [field: BoxGroup("Abilities")] [field: SerializeField] public AbilityConfig BasicAbilityConfig { get; private set; }
        [field: BoxGroup("Abilities")] [field: SerializeField] public AbilityConfig SpecialAbilityConfig { get; private set; }
    }
}