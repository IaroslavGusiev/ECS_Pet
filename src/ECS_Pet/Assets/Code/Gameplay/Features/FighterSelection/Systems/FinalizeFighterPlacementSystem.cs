using Entitas;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class FinalizeFighterPlacementSystem : ReactiveSystem<GameEntity>
    {
        public FinalizeFighterPlacementSystem(GameContext gameContext)
            : base(gameContext) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Placed.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.isFighter && entity.isPlaced && entity.hasCellId;

        protected override void Execute(List<GameEntity> fighters)
        {
            foreach (GameEntity fighter in fighters)
            {
                int cellId = fighter.CellId;

                fighter
                    .AddPlacedCellId(cellId)
                    .RemoveCellId()
                    .With(entity => entity.isSelected = false)
                    .With(entity => entity.isMovementAvailable = true)
                    .With(entity => entity.isReadyToCollectTargets = true);
            }
        }
    }
}
