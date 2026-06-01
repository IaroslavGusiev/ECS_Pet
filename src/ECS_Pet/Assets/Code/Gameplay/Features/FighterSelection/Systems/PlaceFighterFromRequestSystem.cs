using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class PlaceFighterFromRequestSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _requests;
        private readonly List<GameEntity> _buffer = new(capacity: 8);

        public PlaceFighterFromRequestSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            _requests = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterPlacementRequest,
                GameMatcher.Processed,
                GameMatcher.FighterId,
                GameMatcher.CellId
            }));
        }

        public void Execute()
        {
            foreach (GameEntity request in _requests.GetEntities(_buffer))
            {
                if (request.isDestructed)
                {
                    continue;
                }

                GameEntity fighter = _gameContext.GetEntityWithId(request.FighterId);
                GameEntity cell = _gameContext.GetEntityWithId(request.CellId);

                if (fighter == null || cell == null)
                {
                    request.isDestructed = true;
                    continue;
                }

                fighter.isPlaced = true;
                cell.isOccupied = true;
                request.isDestructed = true;
            }
        }
    }
}
