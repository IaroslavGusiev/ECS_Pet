using Entitas;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Cooldowns;

namespace Code.Gameplay.Combat
{
    public class AbilityActivationSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _attackers;
        private readonly IEntityFactory _entityFactory;

        public AbilityActivationSystem(GameContext gameContext, IEntityFactory entityFactory)
        {
            _gameContext = gameContext;
            _entityFactory = entityFactory;

            _attackers = _gameContext
                .GetGroup(GameMatcher
                    .AllOf(
                GameMatcher.TargetId, 
                GameMatcher.Attacking)
                    .AnyOf(
                GameMatcher.BasicAbilityId, 
                GameMatcher.SpecialAbilityId));
        }

        public void Execute()
        {
            foreach (GameEntity attacker in _attackers)
            {
                if (attacker.isDead)
                {
                    continue;
                }
                
                if (attacker.hasSpecialAbilityId)
                {
                    GameEntity specialAbility = _gameContext.GetEntityWithId(attacker.SpecialAbilityId);
                    if (TryActivateAbility(specialAbility))
                    {
                        continue;
                    }
                }
                
                if (attacker.hasBasicAbilityId)
                {
                    GameEntity basicAbility = _gameContext.GetEntityWithId(attacker.BasicAbilityId);
                    TryActivateAbility(basicAbility);
                }
            }
        }
        
        private bool TryActivateAbility(GameEntity ability)
        {
            if (ability is not { isCooldownUp: true })
            {
                return false;
            }

            if (ability.isSpecialAbility && ability.isManaSufficient == false)
            {
                return false;
            }

            ability
                .PutOnCooldown()
                .With(entity => entity.isReadyToUse = true);

            CreateAnimationRequest(ability);
            return true;
        }

        private void CreateAnimationRequest(GameEntity ability)
        {
            _entityFactory
                .CreateEntity<GameEntity>()
                .AddProducerId(ability.OwnerLink)
                .With(request => request.isBasicAbility = true, when: ability.isBasicAbility)
                .With(request => request.isSpecialAbility = true, when: ability.isSpecialAbility)
                .With(request => request.isAnimationRequest = true);
        }
    }
}