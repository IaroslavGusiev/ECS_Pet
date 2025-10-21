using Entitas;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;

namespace Code.Gameplay.Cooldowns
{
    public class CooldownSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _cooldownables;
        private readonly List<GameEntity> _buffer = new(32);

        public CooldownSystem(GameContext gameContext, ITimeService timeService)
        {
            _timeService = timeService;
            
            _cooldownables = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Cooldown, 
                GameMatcher.CooldownLeft
            }));
        }

        public void Execute()
        {
            foreach (GameEntity cooldownable in _cooldownables.GetEntities(_buffer))
            {
                cooldownable.ReplaceCooldownLeft(cooldownable.CooldownLeft - _timeService.DeltaTime);

                if (cooldownable.CooldownLeft <= 0)
                {
                    cooldownable.isCooldownUp = true;
                    cooldownable.RemoveCooldownLeft();
                }
            }
        }
    }
}