using UnityEngine;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Infrastructure.Services;
using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Armaments.Factory
{
    public class ArmamentFactory : IArmamentFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IStaticDataService _staticDataService;

        public ArmamentFactory(IEntityFactory entityFactory, IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }
        
        public GameEntity CreateProjectile(GameEntity owner, GameEntity ability)
        {
            AbilityConfig abilityConfig = ability.isBasicAbility
                ? _staticDataService.GetBasicAbilityConfig(owner.FighterTypeId)
                : _staticDataService.GetSpecialAbilityConfig(owner.FighterTypeId);
            
            Vector3 spawnPosition = owner.CombatantProjectileHolder.GetPosition(); 

            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddEffectConfigs(abilityConfig.EffectConfigs)
                .AddOwnerLink(owner.Id)
                .AddViewPath("RatStone")
                .AddTargetId(ability.TargetId)
                .AddWorldPosition(spawnPosition)
                .AddWorldRotation(Quaternion.identity)
                .AddSpeed(8f)
                .With(entity => entity.isProjectileArmament = true)
                .With(entity => entity.isMovementAvailable = true)
                .With(entity => entity.isMoving = true);
        }
    }
}