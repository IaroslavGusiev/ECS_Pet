using Entitas;
using Cysharp.Threading.Tasks;
using Code.Common.View.Factory;
using System.Collections.Generic;

namespace Code.Common.View
{
    public class BindEntityViewFromPathSystem : IExecuteSystem
    {
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(capacity: 32);

        public BindEntityViewFromPathSystem(GameContext game, IEntityViewFactory entityViewFactory)
        {
            _entityViewFactory = entityViewFactory;
            
            _entities = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.ViewPath)
                .NoneOf(GameMatcher.View));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                _entityViewFactory
                    .CreateViewForEntity(entity)
                    .Forget();
            }
        }
    }
}