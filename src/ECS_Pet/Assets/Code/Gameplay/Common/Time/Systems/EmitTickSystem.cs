using Code.Infrastructure;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Common.Time
{
    public class EmitTickSystem : TimerExecuteSystem
    {
        private readonly IEntityFactory _entityFactory;
        private readonly float _interval;

        public EmitTickSystem(
            float interval, 
            ITimeService timeService, 
            IEntityFactory entityFactory) 
            : base(interval, timeService)
        {
            _interval = interval;
            _entityFactory = entityFactory;
        }

        protected override void Execute()
        {
            _entityFactory
                .CreateEntity<GameEntity>()
                .AddTick(_interval);
        }
    }
}