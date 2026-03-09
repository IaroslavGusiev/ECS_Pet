using UnityEngine;

namespace Code.Gameplay.Features.Combat
{
    public class CombatantAnimator : MonoBehaviour
    {
        private static readonly int MovingHash = Animator.StringToHash("Walk");
        private static readonly int DeathHash = Animator.StringToHash("Death");
        private static readonly int BasicAbilityHash = Animator.StringToHash("FirstAttack");
        private static readonly int SpecialAbilityHash = Animator.StringToHash("SecondAttack");
        
        [SerializeField] private Animator animator;

        public void Walk() => 
            animator.SetBool(MovingHash, true);

        public void Idle() => 
            animator.SetBool(MovingHash, false);

        public void AnimateBasicAbility() => 
            animator.SetTrigger(BasicAbilityHash);

        public void AnimateSpecialAbility() => 
            animator.SetTrigger(SpecialAbilityHash);

        public void PlayDied() => 
            animator.SetTrigger(DeathHash);
    }
}