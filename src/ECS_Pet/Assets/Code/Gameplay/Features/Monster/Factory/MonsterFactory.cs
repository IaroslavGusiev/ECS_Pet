using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using System.Collections.Generic;
using Code.Gameplay.CharacterStats;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Monster
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IStaticDataService _staticDataService;

        public MonsterFactory(
            IEntityFactory entityFactory, 
            IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateMonster(MonsterTypeId monsterTypeId, Vector3 at)
        {
            MonsterConfig monsterConfig = _staticDataService.GetMonsterConfig(monsterTypeId);
            
            Dictionary<Stats, float> baseStates = FillBaseStatsFromConfig(monsterConfig); 
            
            return _entityFactory
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
                .With(entity => entity.isFighter = true)
                .With(entity => entity.isSelected = true);
        }
        
        private static Dictionary<Stats, float> FillBaseStatsFromConfig(MonsterConfig monsterConfig)
        {
            return InitStats.EmptyStatDictionary()
                .With(dictionary => dictionary[Stats.MaxHp] = monsterConfig.MaxHp)
                .With(dictionary => dictionary[Stats.Damage] = monsterConfig.Damage)
                .With(dictionary => dictionary[Stats.MaxMana] = monsterConfig.MaxMana)
                .With(dictionary => dictionary[Stats.ManaRegen] = monsterConfig.ManaRegen);
        }
    }
}