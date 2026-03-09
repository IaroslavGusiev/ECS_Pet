using Entitas;
using Code.Common.Extensions;

namespace Code.Gameplay.CharacterStats
{
    public class ApplySpeedFromStatsSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _statsOwner;

        public ApplySpeedFromStatsSystem(GameContext gameContext)
        {
            _statsOwner = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Speed, 
                GameMatcher.BaseStats, 
                GameMatcher.StatModifiers
            }));
        }

        public void Execute()
        {
            foreach (GameEntity statOwner in _statsOwner)
            {
                statOwner.ReplaceSpeed(MoveSpeed(statOwner).ZeroIfNegative());
            }
        }
        
        private static float MoveSpeed(GameEntity statOwner) => 
            statOwner.BaseStats[Stats.Speed] + statOwner.StatModifiers[Stats.Speed];
    }
}