using Entitas;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;

namespace Code.Common.Destruct
{
    public class SelfDestructTimerSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new (64);

        public SelfDestructTimerSystem(GameContext gameContext, ITimeService time)
        {
            _time = time;
            _entities = gameContext.GetGroup(GameMatcher.SelfDestructTimer);
        }
    
        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                if (entity.SelfDestructTimer > 0)
                {
                    entity.ReplaceSelfDestructTimer(entity.SelfDestructTimer - _time.DeltaTime);
                }
                else
                {
                    entity.RemoveSelfDestructTimer();
                    entity.isDestructed = true;
                }
            }
        }
    }
}