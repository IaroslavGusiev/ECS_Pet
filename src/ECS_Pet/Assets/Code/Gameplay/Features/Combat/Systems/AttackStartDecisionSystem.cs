using Entitas;
using UnityEngine;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Cooldowns;

namespace Code.Gameplay.Combat
{
    public class AttackStartDecisionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _attackers;
        private readonly IEntityFactory _entityFactory;

        public AttackStartDecisionSystem( GameContext gameContext, IEntityFactory entityFactory)
        {
            _gameContext = gameContext;
            _entityFactory = entityFactory;

            _attackers = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Attacking,
                GameMatcher.BasicAbilityId, // GameMatcher.SpecialAbilityId
            }));
        }

        public void Execute()
        {
            foreach (GameEntity attacker in _attackers)
            {
                if (attacker.isDead)
                {
                    continue;
                }
                
                if (attacker.hasBasicAbilityId)
                {
                    GameEntity basicAbility = _gameContext.GetEntityWithId(attacker.BasicAbilityId);
                    TryActivateAbility(basicAbility);
                }
                
                if (attacker.hasSpecialAbilityId)
                {
                    GameEntity specialAbility = _gameContext.GetEntityWithId(attacker.SpecialAbilityId);
                    TryActivateAbility(specialAbility);
                }
            }
        }
        
        private void TryActivateAbility(GameEntity ability)
        {
            if (ability is not { isCooldownUp: true })
            {
                return;
            }

            ability
                .PutOnCooldown()
                .With(entity => entity.isReadyToUse = true);

            CreateAnimationRequest(ability);

            Debug.Log($"<color=green>Ability {ability.AbilityTypeId} activated (ID {ability.Id})</color>");
        }

        private void CreateAnimationRequest(GameEntity ability)
        {
            _entityFactory
                .CreateEntity<GameEntity>()
                .AddProducerId(ability.OwnerLink)
                .With(request => request.isBasicAbility = true)
                .With(request => request.isAnimationRequest = true);
        }
    }
}