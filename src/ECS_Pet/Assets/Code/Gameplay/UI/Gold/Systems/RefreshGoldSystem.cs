using Entitas;

namespace Code.Gameplay.UI.Gold
{
    public class RefreshGoldSystem : IExecuteSystem
    {
        private readonly IStorageUIService _storage;
        private readonly IGroup<GameEntity> _storages;

        public RefreshGoldSystem(GameContext gameContext, IStorageUIService storage)
        {
            _storage = storage;

            _storages = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Storage,
                GameMatcher.Gold));
        }

        public void Execute()
        {
            foreach (GameEntity storage in _storages)
            {
                _storage.UpdateCurrentGold(storage.Gold);
            }
        }
    }
}
