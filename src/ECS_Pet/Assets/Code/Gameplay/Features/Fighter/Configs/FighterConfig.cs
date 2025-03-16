using UnityEngine;
using Code.StaticData;
using NaughtyAttributes;

namespace Code.Gameplay.Fighter
{
    [CreateAssetMenu(fileName = "FighterConfig", menuName = "Configs/FighterConfig")]
    public class FighterConfig : ScriptableObject
    {
        [field: SerializeField] public FighterTypeId FighterTypeId { get; private set; }
        [field: SerializeField] public string ViewPath { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        [field: ShowAssetPreview]
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}