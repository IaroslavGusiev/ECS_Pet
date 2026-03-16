using System.Linq;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Cooldowns;
using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Abilities
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IEntityFactory _entityFactory;

        public AbilityFactory(IEntityFactory entityFactory) => 
            _entityFactory = entityFactory;

        public GameEntity CreateBasicAbility(AbilityConfig abilityConfig, int fighterId)
        {
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .With(entity => entity.isAbility = true)
                .With(entity => entity.isBasicAbility = true)
                .AddOwnerLink(fighterId)
                .AddCooldown(abilityConfig.Cooldown)
                .AddEffectConfigs(abilityConfig.EffectConfigs)
                .AddAbilityTypeId(abilityConfig.AbilityTypeId)
                .AddAnimationDelay(abilityConfig.AnimationDelay)
                .AddAnimationDelayLeft(abilityConfig.AnimationDelay)
                .With(entity => entity.isMeleeAttackAbility = true, when: abilityConfig.AbilityTypeId == AbilityTypeId.MeleeAttack)
                .With(entity => entity.isRangedAttackAbility = true, when: abilityConfig.AbilityTypeId == AbilityTypeId.RangedAttack)
                .With(entity => entity.isHealingAbility = true, when: abilityConfig.EffectConfigs.First().EffectTypeId == EffectTypeId.Heal)
                .PutOnCooldown();
        }

        public GameEntity CreateSpecialAbility(AbilityConfig abilityConfig, int fighterId)
        {
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .With(entity => entity.isAbility = true)
                .With(entity => entity.isSpecialAbility = true)
                .AddOwnerLink(fighterId)
                .AddCooldown(abilityConfig.Cooldown)
                .AddEffectConfigs(abilityConfig.EffectConfigs)
                .AddAbilityTypeId(abilityConfig.AbilityTypeId)
                .AddAnimationDelay(abilityConfig.AnimationDelay)
                .AddAnimationDelayLeft(abilityConfig.AnimationDelay)
                .With(entity => entity.isMeleeAttackAbility = true, when: abilityConfig.AbilityTypeId == AbilityTypeId.MeleeAttack)
                .With(entity => entity.isRangedAttackAbility = true, when: abilityConfig.AbilityTypeId == AbilityTypeId.RangedAttack)
                .With(entity => entity.isHealingAbility = true, when: abilityConfig.EffectConfigs.First().EffectTypeId == EffectTypeId.Heal)
                .PutOnCooldown();
        }
    }
}