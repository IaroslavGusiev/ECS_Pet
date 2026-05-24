using Entitas;
using Code.Gameplay.Fighter;

namespace Code.Gameplay.FighterSelection
{
    public class FighterPurchaseService : IFighterPurchaseService
    {
        private readonly IGroup<GameEntity> _storages;

        public FighterPurchaseService(GameContext gameContext)
        {
            _storages = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Storage,
                GameMatcher.Gold));
        }

        public bool CanPurchase(FighterConfig fighterConfig) =>
            Storage() is { } storage && CanAfford(storage, fighterConfig);

        public bool TryPurchase(FighterConfig fighterConfig)
        {
            if (Storage() is not { } storage || CanAfford(storage, fighterConfig) == false)
            {
                return false;
            }
            
            storage.ReplaceGold(storage.Gold - fighterConfig.Price);
            return true;
        }

        private GameEntity Storage() =>
            _storages.GetSingleEntity();
        
        private static bool CanAfford(GameEntity storage, FighterConfig config) =>
            storage.Gold >= config.Price;
    }
}
