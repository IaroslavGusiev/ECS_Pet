using Entitas;
using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class MoveFighterOnSelectionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly IGroup<InputEntity> _inputs;

        private readonly Vector3 _offsetForGameBoard = new(0f, 0.5f, 0f);
        private readonly List<GameEntity> _buffer = new(capacity: 4);

        public MoveFighterOnSelectionSystem(
            GameContext gameContext,
            InputContext inputContext)
        {
            _gameContext = gameContext;

            _selectedFighters = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterTypeId,
                GameMatcher.Selected,
                GameMatcher.Fighter
            }));
            
            _inputs = inputContext.GetGroup(InputMatcher.AllOf(InputMatcher.PointerOverCellId));
        }

        public void Execute()
        {
            InputEntity input = _inputs.GetSingleEntity();
            
            if (input == null || _selectedFighters.count == 0)
            {
                return;
            }

            GameEntity hitCell = _gameContext.GetEntityWithId(input.PointerOverCellId);
            
            if (hitCell == null)
            {
                return;
            }

            foreach (GameEntity fighter in _selectedFighters.GetEntities(_buffer))
            {
                if (fighter.hasCellId && fighter.CellId == hitCell.Id)
                {
                    continue;
                }

                fighter.ReplaceWorldPosition(hitCell.WorldPosition + _offsetForGameBoard);

                if (fighter.hasCellId)
                {
                    fighter.ReplaceCellId(hitCell.Id);
                    continue;
                }

                fighter.AddCellId(hitCell.Id);
            }
        }
    }
}
