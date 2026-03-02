using Entitas;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Lifetime
{
    public class UpdateHpSliderSystem : IExecuteSystem 
    {
        private readonly IGroup<GameEntity> _heroes;

        public UpdateHpSliderSystem(GameContext game)
        {
            _heroes = game
                .GetGroup(GameMatcher
                .AllOf(matchers: new[]
            {
                GameMatcher.MaxHp, 
                GameMatcher.Placed,
                GameMatcher.Fighter,
                GameMatcher.CurrentHp,
                GameMatcher.StatsSliderHolder
            })
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                if (hero.isDead)
                {
                    continue;
                }
                
                hero.StatsSliderHolder.UpdateSlider(stat: Stats.MaxHp, hero.CurrentHp, maxValue: hero.MaxHp);
            }
        }
    }
}