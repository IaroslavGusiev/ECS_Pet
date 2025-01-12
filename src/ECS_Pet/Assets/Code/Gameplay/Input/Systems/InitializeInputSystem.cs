using Entitas;
using Code.Infrastructure;

namespace Code.Gameplay.Input
{
    public class InitializeInputSystem : IInitializeSystem
    {
        private readonly IEntityFactory _entityFactory;

        public InitializeInputSystem(IEntityFactory entityFactory) => 
            _entityFactory = entityFactory;

        public void Initialize()
        {
            _entityFactory
                .CreateEntity<InputEntity>()
                .isInput = true;
        }
    }
}