using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Features.Combat
{
    public class FighterAnimatorRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private CombatantAnimator combatantAnimator;
        
        public override void RegisterComponents()
        {
            if (combatantAnimator)
            {
                Entity.AddCombatantAnimator(combatantAnimator);
            }
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasCombatantAnimator)
            {
                Entity.RemoveCombatantAnimator();
            }
        }
    }
}