using UnityEngine;
using Code.Common.View;

namespace Code.Gameplay.Fighter.Registrars
{
    public class StatsSliderHolderRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private StatsSliderHolder holder;

        public override void RegisterComponents() => 
            Entity.AddStatsSliderHolder(holder);

        public override void UnregisterComponents()
        {
            if (Entity.hasStatsSliderHolder)
            {
                Entity.RemoveStatsSliderHolder();
            }
        }
    }
}