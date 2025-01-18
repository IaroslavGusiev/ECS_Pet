using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.GameBoard
{
    public class ProcessCellsMaterialChangeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cells;
        private readonly List<GameEntity> _buffer = new(capacity: 64);

        public ProcessCellsMaterialChangeSystem(GameContext game)
        {
            _cells = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.MeshRenderer, 
                GameMatcher.GameBoardCell, 
                GameMatcher.MaterialChangeRequest
            }));
        }

        public void Execute()
        {
            foreach (GameEntity cell in _cells.GetEntities(_buffer))
            {
                cell.MeshRenderer.material = cell.MaterialChangeRequest;
                cell.RemoveMaterialChangeRequest();
            }
        }
    }
}