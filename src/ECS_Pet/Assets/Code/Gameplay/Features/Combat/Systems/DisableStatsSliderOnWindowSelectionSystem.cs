using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Combat
{
    public class DisableStatsSliderOnWindowSelectionSystem : ReactiveSystem<GameEntity>
    {
        public DisableStatsSliderOnWindowSelectionSystem(GameContext gameContext) 
            : base(gameContext) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(triggers:
                GameMatcher.AllOf(matchers: new[]
                {
                    GameMatcher.Selected,
                    GameMatcher.StatsSliderHolder
                }).Added());
        }

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> combatants)
        {
            foreach (GameEntity combatant in combatants)
            {
                combatant.StatsSliderHolder.Disable();
            }
        }
    }
}