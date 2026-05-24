using Entitas;
using Code.UI.BaseWindow;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class DeselectFighterWindowOnPlacementSystem : ReactiveSystem<GameEntity>
    {
        private readonly IWindowService _windowService;

        public DeselectFighterWindowOnPlacementSystem(
            GameContext gameContext,
            IWindowService windowService)
            : base(gameContext)
        {
            _windowService = windowService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Placed.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.isFighter;

        protected override void Execute(List<GameEntity> fighters)
        {
            _windowService
                .GetWindowFromActive<SelectFighterWindow>()
                ?.DeselectAll();
        }
    }
}
