using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class ValidateFighterPlacementRequestSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _requests;
        private readonly List<GameEntity> _buffer = new(capacity: 8);

        public ValidateFighterPlacementRequestSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _requests = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.FighterPlacementRequest, 
                GameMatcher.FighterId, 
                GameMatcher.CellId));
        }

        public void Execute()
        {
            foreach (GameEntity request in _requests.GetEntities(_buffer))
            {
                if (request.isProcessed || request.isSuccessfulCellRequest || request.isDestructed)
                {
                    continue;
                }

                if (IsValid(request) == false)
                {
                    request.isDestructed = true;
                    continue;
                }

                request.isSuccessfulCellRequest = true;
            }
        }

        private bool IsValid(GameEntity request)
        {
            GameEntity fighter = _gameContext.GetEntityWithId(request.FighterId);
            
            GameEntity cell = _gameContext.GetEntityWithId(request.CellId);

            return fighter is { isFighter: true, isSelected: true, hasPurchasePrice: true }
                   && cell is { isGameBoardCell: true, isOccupied: false };
        }
    }
}
