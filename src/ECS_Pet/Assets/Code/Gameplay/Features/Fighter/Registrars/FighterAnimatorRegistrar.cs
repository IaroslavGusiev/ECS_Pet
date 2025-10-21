using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Fighter.Registrars
{
    public class FighterAnimatorRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private FighterAnimator fighterAnimator;
        
        public override void RegisterComponents()
        {
            if (fighterAnimator)
            {
                Entity.AddFighterAnimator(fighterAnimator);
            }
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasFighterAnimator)
            {
                Entity.RemoveFighterAnimator();
            }
        }
    }
}