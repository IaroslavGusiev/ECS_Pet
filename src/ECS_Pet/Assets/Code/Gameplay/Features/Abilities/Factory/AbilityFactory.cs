using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Cooldowns;
using Code.Infrastructure.Services;
using Code.Gameplay.Abilities.Configs;
using Code.StaticData;

namespace Code.Gameplay.Abilities
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IStaticDataService _staticDataService;

        public AbilityFactory(
            IEntityFactory entityFactory, 
            IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }
        
        public GameEntity CreateBasicAbility(AbilityConfig abilityConfig, int fighterId)
        {
            GameEntity ability = _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .With(entity => entity.isAbility = true)
                .With(entity => entity.isBasicAbility = true)
                .AddOwnerLink(fighterId)
                .AddCooldown(abilityConfig.Cooldown)
                .AddEffectConfigs(abilityConfig.EffectConfigs)
                .AddAbilityTypeId(abilityConfig.AbilityTypeId)
                .AddAnimationDelay(abilityConfig.AnimationDelay)
                .AddAnimationDelayLeft(abilityConfig.AnimationDelay)
                .PutOnCooldown();

            if (abilityConfig.AbilityTypeId == AbilityTypeId.MeleeAttack) // melee attack
            {
                ability.isMeleeAttackAbility = true;
            }
            
            return ability; 
        }
    }
}