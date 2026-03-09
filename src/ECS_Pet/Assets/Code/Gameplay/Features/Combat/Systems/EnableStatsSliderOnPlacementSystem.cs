using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Combat
{
    public class EnableStatsSliderOnPlacementSystem : ReactiveSystem<GameEntity>
    {
        public EnableStatsSliderOnPlacementSystem(GameContext gameContext) 
            : base(gameContext) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(triggers: GameMatcher.Placed.Added());

        protected override bool Filter(GameEntity entity) => 
            entity.isPlaced;

        protected override void Execute(List<GameEntity> fighters)
        {
            foreach (GameEntity fighter in fighters)
            {
                fighter.StatsSliderHolder.Enable();
            }
        }
    }
}