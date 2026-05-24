using Entitas;
using Code.Gameplay.Fighter;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class FinalizeFighterPlacementSystem : ReactiveSystem<GameEntity>
    {
        private readonly IFighterPlacementService _fighterPlacementService;

        public FinalizeFighterPlacementSystem(
            GameContext gameContext,
            IFighterPlacementService fighterPlacementService)
            : base(gameContext)
        {
            _fighterPlacementService = fighterPlacementService;
        }

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
                    .RemoveCellId()
                    .With(entity => entity.isSelected = false)
                    .With(entity => entity.isMovementAvailable = true)
                    .With(entity => entity.isReadyToCollectTargets = true)
                    .With(entity => _fighterPlacementService.RegisterFighter(entity.Id, cellId));
            }
        }
    }
}
