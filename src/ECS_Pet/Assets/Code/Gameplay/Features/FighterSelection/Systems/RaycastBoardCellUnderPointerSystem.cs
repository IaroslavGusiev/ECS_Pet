using Entitas;
using Code.StaticData;
using Code.Gameplay.Input;
using Code.Common.Extensions;
using Code.Gameplay.Common.Time;

namespace Code.Gameplay.FighterSelection
{
    public class RaycastBoardCellUnderPointerSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IPhysicsService _physicsService;
        private readonly IGroup<InputEntity> _inputs;

        public RaycastBoardCellUnderPointerSystem(
            InputContext inputContext,
            IInputService inputService,
            IPhysicsService physicsService)
        {
            _inputService = inputService;
            _physicsService = physicsService;
            _inputs = inputContext.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            InputEntity input = _inputs.GetSingleEntity();

            if (input == null)
            {
                return;
            }

            GameEntity hitCell = _physicsService.RaycastFromScreen(
                _inputService.GetScreenPosition(),
                CollisionLayer.GameBoard.AsMask());

            if (hitCell is not { isGameBoardCell: true })
            {
                RemoveHoveredCell(input);
                return;
            }

            if (input.hasPointerOverCellId)
            {
                input.ReplacePointerOverCellId(hitCell.Id);
                return;
            }

            input.AddPointerOverCellId(hitCell.Id);
        }

        private static void RemoveHoveredCell(InputEntity input)
        {
            if (input.hasPointerOverCellId)
            {
                input.RemovePointerOverCellId();
            }
        }
    }
}
