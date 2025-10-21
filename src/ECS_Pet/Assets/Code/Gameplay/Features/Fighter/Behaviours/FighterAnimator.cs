using UnityEngine;

namespace Code.Gameplay.Fighter
{
    public class FighterAnimator : MonoBehaviour
    {
        private static readonly int MovingHash = Animator.StringToHash("Walk");
        
        [SerializeField] private Animator animator;

        public void Walk() => 
            animator.SetBool(MovingHash, true);

        public void Idle() => 
            animator.SetBool(MovingHash, false);
    }
}