using Entitas;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Lifetime
{
    public class UpdateHpSliderSystem : IExecuteSystem 
    {
        private readonly IGroup<GameEntity> _placed;

        public UpdateHpSliderSystem(GameContext game)
        {
            _placed = game
                .GetGroup(GameMatcher
                .AllOf(matchers: new[]
            {
                GameMatcher.MaxHp, 
                GameMatcher.Placed,
                GameMatcher.CurrentHp,
                GameMatcher.StatsSliderHolder
            })
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _placed)
            {
                if (entity.isDead)
                {
                    continue;
                }
                
                entity.StatsSliderHolder.UpdateSlider(stat: Stats.MaxHp, entity.CurrentHp, maxValue: entity.MaxHp);
            }
        }
    }
}