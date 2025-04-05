using Entitas;
using Zenject;
using Code.Gameplay.CharacterStats;
using Code.Gameplay.TargetCollection;
using Code.Gameplay.CharacterStats.Indexing;

namespace Code.Gameplay.Common.Time.EntityIndices
{
    public class GameEntityIndices : IInitializable
    {
        public const string StatChangeKey = "StatChange"; 
        
        private readonly GameContext _gameContext;

        public GameEntityIndices(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Initialize()
        {
            _gameContext.AddEntityIndex(new EntityIndex<GameEntity, StatKey>(
                name: StatChangeKey,
                _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
                {
                    GameMatcher.StatChange, 
                    GameMatcher.TargetId
                })),
                getKey: GetTargetStatKey, 
                new StatKeyEqualityComparer()));
        }
        
        private StatKey GetTargetStatKey(GameEntity entity, IComponent component)
        {
            return new StatKey(
                (component as TargetId)?.Value ?? entity.TargetId,
                (component as StatChange)?.Value ?? entity.StatChange);
        }
    }
}