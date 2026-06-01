using Entitas;
using Code.Infrastructure;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class RequestFighterPlacementOnClickSystem : IExecuteSystem
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly IGroup<InputEntity> _inputs;
        private readonly List<GameEntity> _buffer = new(capacity: 4);

        public RequestFighterPlacementOnClickSystem(
            GameContext gameContext,
            InputContext inputContext,
            IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
            
            _inputs = inputContext.GetGroup(InputMatcher.ClickInput);

            _selectedFighters = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Fighter,
                GameMatcher.Selected,
                GameMatcher.CellId
            }));
        }

        public void Execute()
        {
            if (_inputs.count == 0)
            {
                return;
            }

            foreach (GameEntity fighter in _selectedFighters.GetEntities(_buffer))
            {
                _entityFactory
                    .CreateEntity<GameEntity>()
                    .With(entity => entity.isFighterPlacementRequest = true)
                    .AddFighterId(fighter.Id)
                    .AddCellId(fighter.CellId);
            }
        }
    }
}
