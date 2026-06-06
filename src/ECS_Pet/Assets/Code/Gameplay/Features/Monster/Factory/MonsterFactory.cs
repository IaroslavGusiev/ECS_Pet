using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Abilities;
using System.Collections.Generic;
using Code.Gameplay.CharacterStats;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Monster
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IAbilityFactory _abilityFactory;
        private readonly IStaticDataService _staticDataService;

        public MonsterFactory(
            IEntityFactory entityFactory, 
            IAbilityFactory abilityFactory, 
            IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _abilityFactory = abilityFactory;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateMonster(MonsterTypeId monsterTypeId, Vector3 at)
        {
            MonsterConfig monsterConfig = _staticDataService.GetMonsterConfig(monsterTypeId);
            
            Dictionary<Stats, float> baseStates = FillBaseStatsFromConfig(monsterConfig);

            GameEntity monster = _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddViewPath(monsterConfig.ViewPath)
                .AddMonsterTypeId(monsterTypeId)
                .AddWorldPosition(at)
                .AddWorldRotation(Quaternion.Euler(new Vector3(0f, 180f, 0f)))
                .AddBaseStats(baseStates)
                .AddStatModifiers(InitStats.EmptyStatDictionary())
                .AddMaxHp(baseStates[Stats.MaxHp])
                .AddCurrentHp(baseStates[Stats.MaxHp])
                .AddMaxMana(baseStates[Stats.MaxMana])
                .AddCurrentMana(0)
                .AddAttackRange(monsterConfig.AttackRange)
                .AddTargetBuffer(new List<int>(capacity: 16))
                .AddRadius(monsterConfig.TargetDetectionRadius)
                .AddLayerMask(CollisionLayer.Fighter.AsMask())
                .AddSpeed(baseStates[Stats.Speed])
                .With(entity => entity.isMonster = true)
                .With(entity => entity.isPlaced = true)
                .With(entity => entity.isMovementAvailable = true)
                .With(entity => entity.isReadyToCollectTargets = true);
            
            CreateAbilities(monsterConfig, monster);
            
            return monster;
        }

        private void CreateAbilities(MonsterConfig monsterConfig, GameEntity monster)
        {
            if (monsterConfig.BasicAbilityConfig != null)
            {
                GameEntity basicAbility = _abilityFactory.CreateBasicAbility(monsterConfig.BasicAbilityConfig, monster.Id);
                monster.AddBasicAbilityId(basicAbility.Id);
            }

            if (monsterConfig.SpecialAbilityConfig == null)
            {
                return;
            }
            
            GameEntity special = _abilityFactory.CreateSpecialAbility(monsterConfig.SpecialAbilityConfig, monster.Id);
            monster.AddSpecialAbilityId(special.Id);
        }
        
        private static Dictionary<Stats, float> FillBaseStatsFromConfig(MonsterConfig monsterConfig)
        {
            return InitStats.EmptyStatDictionary()
                .With(dictionary => dictionary[Stats.MaxHp] = monsterConfig.MaxHp)
                .With(dictionary => dictionary[Stats.Damage] = monsterConfig.Damage)
                .With(dictionary => dictionary[Stats.Speed] = monsterConfig.MoveSpeed)
                .With(dictionary => dictionary[Stats.MaxMana] = monsterConfig.MaxMana)
                .With(dictionary => dictionary[Stats.ManaRegen] = monsterConfig.ManaRegen);
        }
    }
}
