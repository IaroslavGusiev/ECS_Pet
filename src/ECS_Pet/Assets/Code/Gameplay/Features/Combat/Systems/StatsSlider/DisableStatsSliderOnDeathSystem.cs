using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Combat
{
    public class DisableStatsSliderOnDeathSystem : ReactiveSystem<GameEntity>
    {
        public DisableStatsSliderOnDeathSystem(GameContext gameContext)
            : base(gameContext) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Dead.Added());

        protected override bool Filter(GameEntity entity) => 
            entity.isDead && entity.hasStatsSliderHolder;

        protected override void Execute(List<GameEntity> combatants)
        {
            foreach (GameEntity combatant in combatants)
            {
                combatant.StatsSliderHolder.Disable();
            }
        }
    }
}