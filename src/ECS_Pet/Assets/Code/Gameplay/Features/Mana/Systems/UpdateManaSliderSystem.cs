using Entitas;
using Code.Gameplay.CharacterStats;

namespace Code.Gameplay.Mana
{
    public class UpdateManaSliderSystem : IExecuteSystem 
    {
        private readonly IGroup<GameEntity> _heroes;

        public UpdateManaSliderSystem(GameContext game)
        {
            _heroes = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Placed,
                GameMatcher.MaxMana, 
                GameMatcher.CurrentMana,
                GameMatcher.StatsSliderHolder
            })); 
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                hero.StatsSliderHolder.UpdateSlider(stat: Stats.MaxMana, hero.CurrentMana, maxValue: hero.MaxMana);
            }
        }
    }
}