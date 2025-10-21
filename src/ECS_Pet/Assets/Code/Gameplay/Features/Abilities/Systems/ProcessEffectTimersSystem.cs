using Entitas;
using Code.GameplayEffects;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;

namespace Code.Gameplay.Abilities
{
    public class ProcessEffectTimersSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly ITimeService _timeService;
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _abilities;
        private readonly List<GameEntity> _buffer = new(16);

        public ProcessEffectTimersSystem(
            GameContext gameContext, 
            ITimeService timeService, 
            IEffectFactory effectFactory)
        {
            _gameContext = gameContext;
            _timeService = timeService;
            _effectFactory = effectFactory;

            _abilities = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Ability, 
                GameMatcher.ReadyToUse,
                GameMatcher.AnimationDelay,
                GameMatcher.AnimationDelayLeft
            }));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                ability.ReplaceAnimationDelayLeft(ability.AnimationDelayLeft - _timeService.DeltaTime);
                
                if (ability.AnimationDelayLeft <= 0)
                {
                    ability.isReadyToUse = false;
                    ability.ReplaceAnimationDelayLeft(ability.AnimationDelay);
                    
                    GameEntity owner = _gameContext.GetEntityWithId(ability.OwnerLink);

                    foreach (EffectConfig config in ability.EffectConfigs)
                    {
                        _effectFactory.CreateEffect(config, ProducerId(owner), owner.TargetId);
                    }
                }
            }
        }
        
        private static int ProducerId(GameEntity entity) => 
            entity.hasProducerId 
                ? entity.ProducerId 
                : entity.Id;
    }
}