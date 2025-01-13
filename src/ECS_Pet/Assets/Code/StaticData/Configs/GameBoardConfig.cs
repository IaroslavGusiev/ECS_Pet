using UnityEngine;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GameBoardConfig", menuName = "ScriptableObject/GameBoardConfig")]
    public class GameBoardConfig : ScriptableObject
    {
        [Header("--- Prefabs Paths ---")]
        [field: SerializeField] public string CellPrefabPath { get; private set; }
        
        [Header("--- Other ---")]
        [SerializeField] private List<Material> cellMaterials;
        [field: SerializeField] public Vector2Int BoardSize { get; private set; }
        
        public Material GetRandomCellMaterial() => 
            cellMaterials.PickRandom();
    }
}