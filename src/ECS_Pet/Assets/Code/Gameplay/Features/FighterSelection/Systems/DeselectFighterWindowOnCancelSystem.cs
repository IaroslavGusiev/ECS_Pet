using Entitas;
using Code.UI.BaseWindow;
using Code.Common.Extensions;

namespace Code.Gameplay.FighterSelection
{
    public class DeselectFighterWindowOnCancelSystem : IExecuteSystem
    {
        private readonly IWindowService _windowService;
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly IGroup<InputEntity> _inputs;

        public DeselectFighterWindowOnCancelSystem(
            GameContext gameContext,
            InputContext inputContext,
            IWindowService windowService)
        {
            _windowService = windowService;

            _selectedFighters = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Fighter,
                GameMatcher.Selected));

            _inputs = inputContext.GetGroup(InputMatcher.AllOf(
                InputMatcher.EscKeyInput));
        }

        public void Execute()
        {
            if (_selectedFighters.IsEmpty() || _inputs.IsEmpty())
            {
                return;
            }

            foreach (InputEntity input in _inputs)
            {
                if (input.isEscKeyInput)
                {
                    _windowService
                        .GetWindowFromActive<SelectFighterWindow>()
                        ?.DeselectAll();
                }
            }
        }
    }
}
