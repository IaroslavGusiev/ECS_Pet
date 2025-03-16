using UnityEngine;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.Gameplay.Features.GameBoard
{
    [CreateAssetMenu(fileName = "GameBoardConfig", menuName = "Configs/GameBoardConfig")]
    public class GameBoardConfig : ScriptableObject
    {
        [Header("--- Prefabs Paths ---")]
        [field: SerializeField] public string CellPrefabPath { get; private set; }
        [field: Space(10)]
        
        [Header("--- Materials ---")]
        [field: SerializeField] public Material GreenCellMaterial { get; private set; }
        [field: SerializeField] public Material RedCellMaterial { get; private set; }
        [SerializeField] private List<Material> cellMaterials;
        [field: Space(10)]
        
        [Header("--- BoardSize ---")]
        [field: SerializeField] public Vector2Int BoardSize { get; private set; }
        
        public Material GetRandomCellMaterial() => 
            cellMaterials.PickRandom();
    }
}