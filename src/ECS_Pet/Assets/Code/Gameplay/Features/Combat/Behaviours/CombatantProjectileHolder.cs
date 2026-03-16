using UnityEngine;

namespace Code.Gameplay.Features.Combat
{
    public class CombatantProjectileHolder : MonoBehaviour
    {
        public Vector3 GetPosition() => 
            transform.position;
    }
}