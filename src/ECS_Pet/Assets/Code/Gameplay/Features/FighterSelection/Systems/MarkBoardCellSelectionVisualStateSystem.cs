using Entitas;
using Code.StaticData;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class MarkBoardCellSelectionVisualStateSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _boardCells;
        private readonly IGroup<GameEntity> _selectedFighters;
        
        private readonly List<GameEntity> _cellBuffer = new(capacity: 64);
        private readonly List<GameEntity> _fighterBuffer = new(capacity: 4);
        
        private readonly HashSet<int> _selectedCellIds = new();

        public MarkBoardCellSelectionVisualStateSystem(GameContext gameContext)
        {
            _boardCells = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.GameBoardCell,
                GameMatcher.Id));

            _selectedFighters = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Fighter,
                GameMatcher.Selected,
                GameMatcher.CellId
            }));
        }

        public void Execute()
        {
            CollectSelectedCellIds();

            foreach (GameEntity cell in _boardCells.GetEntities(_cellBuffer))
            {
                SetVisualState(cell, ResolveVisualState(cell));
            }
        }

        private void CollectSelectedCellIds()
        {
            _selectedCellIds.Clear();

            foreach (GameEntity fighter in _selectedFighters.GetEntities(_fighterBuffer))
            {
                _selectedCellIds.Add(fighter.CellId);
            }
        }

        private BoardCellVisualState ResolveVisualState(GameEntity cell)
        {
            if (_selectedCellIds.Contains(cell.Id) == false)
            {
                return BoardCellVisualState.Default;
            }

            return cell.isOccupied
                ? BoardCellVisualState.Blocked
                : BoardCellVisualState.Available;
        }

        private static void SetVisualState(GameEntity cell, BoardCellVisualState state)
        {
            if (cell.CellVisualState == state)
            {
                return;
            }

            cell.RemoveCellVisualState();
            cell.AddCellVisualState(state);
        }
    }
}
