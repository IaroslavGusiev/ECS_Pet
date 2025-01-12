using UnityEngine;

namespace Code.Gameplay.Features.GameBoard
{
    public class CellBehaviour : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        
        public void UpdateMaterial(Material material) => 
            meshRenderer.material = material;
    }
}