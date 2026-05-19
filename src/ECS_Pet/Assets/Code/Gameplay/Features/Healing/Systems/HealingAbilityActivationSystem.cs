using Entitas;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Cooldowns;

namespace Code.Gameplay.Healing
{
    public class HealingAbilityActivationSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IEntityFactory _entityFactory;
        private readonly IGroup<GameEntity> _healingAbilities;

        public HealingAbilityActivationSystem(GameContext gameContext, IEntityFactory entityFactory)
        {
            _gameContext = gameContext;
            _entityFactory = entityFactory;

            _healingAbilities = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.Ability,
                GameMatcher.HealingAbility,
                GameMatcher.OwnerLink,
                GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _healingAbilities)
            {
                if (CanActivate(ability) == false)
                {
                    continue;
                }

                ability
                    .PutOnCooldown()
                    .With(entity => entity.isReadyToUse = true);

                CreateAnimationRequest(ability);
            }
        }

        private bool CanActivate(GameEntity ability)
        {
            if (ability is not { isCooldownUp: true })
            {
                return false;
            }

            if (ability.isSpecialAbility && ability.isManaSufficient == false)
            {
                return false;
            }

            GameEntity owner = _gameContext.GetEntityWithId(ability.OwnerLink);

            if (owner == null || owner.isDead)
            {
                return false;
            }

            GameEntity target = _gameContext.GetEntityWithId(ability.TargetId);

            return IsValidTarget(target) && IsHealingNeeded(target);
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

        private static bool IsValidTarget(GameEntity target) => 
            target is { isDead: false, hasCurrentHp: true, hasMaxHp: true };

        private static bool IsHealingNeeded(GameEntity target) => 
            target.CurrentHp < target.MaxHp;
    }
}
