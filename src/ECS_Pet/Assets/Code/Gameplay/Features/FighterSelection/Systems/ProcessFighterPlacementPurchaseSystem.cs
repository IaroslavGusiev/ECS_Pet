using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class ProcessFighterPlacementPurchaseSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _requests;
        private readonly IGroup<GameEntity> _storages;
        private readonly List<GameEntity> _buffer = new(capacity: 8);

        public ProcessFighterPlacementPurchaseSystem(GameContext gameContext)
        {
            _gameContext = gameContext;

            _requests = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterPlacementRequest,
                GameMatcher.SuccessfulCellRequest,
                GameMatcher.FighterId
            }));

            _storages = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Storage,
                GameMatcher.Gold));
        }

        public void Execute()
        {
            foreach (GameEntity request in _requests.GetEntities(_buffer))
            {
                if (request.isProcessed || request.isDestructed)
                {
                    continue;
                }

                GameEntity fighter = _gameContext.GetEntityWithId(request.FighterId);
                GameEntity storage = _storages.GetSingleEntity();

                if (fighter is not { hasPurchasePrice: true } ||
                    storage == null ||
                    storage.Gold < fighter.PurchasePrice)
                {
                    request.isDestructed = true;
                    continue;
                }

                storage.ReplaceGold(storage.Gold - fighter.PurchasePrice);
                request.isProcessed = true;
            }
        }
    }
}
