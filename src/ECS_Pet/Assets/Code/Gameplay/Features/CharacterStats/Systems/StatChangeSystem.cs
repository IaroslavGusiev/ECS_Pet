using Entitas;
using Code.Gameplay.Common.Time.EntityIndices;

namespace Code.Gameplay.CharacterStats
{
    public class StatChangeSystem : IExecuteSystem // TODO: execute in own feature
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _statOwners;

        public StatChangeSystem(GameContext game)
        {
            _game = game;

            _statOwners = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Id,
                GameMatcher.BaseStats,
                GameMatcher.StatModifiers
            }));
        }

        public void Execute()
        {
            foreach (GameEntity owner in _statOwners)
            foreach (Stats stat in owner.BaseStats.Keys)
            {
                owner.StatModifiers[stat] = 0;
                foreach (GameEntity statChange in _game.TargetStatChanges(stat, owner.Id))
                {
                    owner.StatModifiers[stat] += statChange.EffectValue;
                }
            }
        }
    }
}