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

        public bool CanPurchase(FighterConfig fighterConfig)
        {
            GameEntity storage = Storage();

            return storage != null && storage.Gold >= fighterConfig.Price;
        }

        public bool TryPurchase(FighterConfig fighterConfig)
        {
            GameEntity storage = Storage();

            if (storage == null || storage.Gold < fighterConfig.Price)
            {
                return false;
            }

            storage.ReplaceGold(storage.Gold - fighterConfig.Price);
            return true;
        }

        private GameEntity Storage()
        {
            foreach (GameEntity storage in _storages)
            {
                return storage;
            }

            return null;
        }
    }
}
