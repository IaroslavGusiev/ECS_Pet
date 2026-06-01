using Entitas;
using Code.Gameplay.Fighter;

namespace Code.Gameplay.FighterSelection
{
    public class FighterAffordabilityService : IFighterAffordabilityService
    {
        private readonly IGroup<GameEntity> _storages;

        public FighterAffordabilityService(GameContext gameContext)
        {
            _storages = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Storage,
                GameMatcher.Gold));
        }

        public bool CanAfford(FighterConfig fighterConfig) =>
            Storage() is { } storage && CanAfford(storage, fighterConfig);

        private GameEntity Storage() =>
            _storages.GetSingleEntity();
        
        private static bool CanAfford(GameEntity storage, FighterConfig config) =>
            storage.Gold >= config.Price;
    }
}
