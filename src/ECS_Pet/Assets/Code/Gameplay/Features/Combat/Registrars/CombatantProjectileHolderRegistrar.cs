using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Features.Combat
{
    public class CombatantProjectileHolderRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private CombatantProjectileHolder projectileHolder;
        
        public override void RegisterComponents()
        {
            if (projectileHolder != null)
            {
                Entity.AddCombatantProjectileHolder(projectileHolder);
            }
        }

        public override void UnregisterComponents()
        {
            if (projectileHolder != null)
            {
                Entity.RemoveCombatantProjectileHolder();
            }
        }
    }
}