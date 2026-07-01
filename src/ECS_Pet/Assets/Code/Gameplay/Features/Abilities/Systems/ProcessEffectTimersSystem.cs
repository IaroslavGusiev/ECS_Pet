using Entitas;
using Code.GameplayEffects;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;
using Code.Gameplay.Statuses;
using Code.Gameplay.Statuses.Applier;
using Code.Gameplay.Armaments.Factory;

namespace Code.Gameplay.Abilities
{
    public class ProcessEffectTimersSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly ITimeService _timeService;
        private readonly IEffectFactory _effectFactory;
        private readonly IStatusApplier _statusApplier;
        private readonly IArmamentFactory _armamentFactory;

        private readonly IGroup<GameEntity> _abilities;
        private readonly List<GameEntity> _buffer = new(16);

        public ProcessEffectTimersSystem(
            GameContext gameContext, 
            ITimeService timeService, 
            IEffectFactory effectFactory, 
            IStatusApplier statusApplier,
            IArmamentFactory armamentFactory)
        {
            _gameContext = gameContext;
            _timeService = timeService;
            _effectFactory = effectFactory;
            _statusApplier = statusApplier;
            _armamentFactory = armamentFactory;

            _abilities = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Ability, 
                GameMatcher.TargetId, 
                GameMatcher.ReadyToUse, 
                GameMatcher.AnimationDelay, 
                GameMatcher.AnimationDelayLeft));
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

                    if (owner == null || owner.isDead || owner.isStunned)
                    {
                        continue;
                    }

                    if (ability.isRangedAttackAbility)
                    {
                        _armamentFactory.CreateProjectile(owner, ability);
                    }
                    else
                    {
                        foreach (EffectConfig config in ability.EffectConfigs)
                        {
                            _effectFactory.CreateEffect(config, ProducerId(owner), ability.TargetId);
                        }

                        if (ability.hasStatusSetups)
                        {
                            foreach (StatusSetup setup in ability.StatusSetups)
                            {
                                _statusApplier.ApplyStatus(setup, ProducerId(owner), ability.TargetId);
                            }
                        }
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
