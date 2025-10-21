using Entitas;
using UnityEngine;
using Code.Gameplay.Cooldowns;

namespace Code.Gameplay.Combat
{
    public class AttackStartDecisionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _attackers;
        
        public AttackStartDecisionSystem( GameContext gameContext)
        {
            _gameContext = gameContext;
            
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
        
        private static void TryActivateAbility(GameEntity ability)
        {
            if (ability is not { isCooldownUp: true })
            {
                return;
            }

            ability.PutOnCooldown();
            ability.isReadyToUse = true;

            Debug.Log($"<color=green>Ability {ability.AbilityTypeId} activated (ID {ability.Id})</color>");
        }
    }
}