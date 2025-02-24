using UnityEngine;
using Code.StaticData;

namespace Code.Gameplay.Fighter
{
    [CreateAssetMenu(fileName = "FighterConfig", menuName = "Configs/FighterConfig")]
    public class FighterConfig : ScriptableObject
    {
        [field: SerializeField] public FighterTypeId FighterTypeId { get; private set; }
        [field: SerializeField] public string ViewPath { get; private set; }
    }
}