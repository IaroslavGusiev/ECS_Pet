using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.Abilities;
using System.Collections.Generic;
using Code.Gameplay.CharacterStats;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Fighter
{
    public class FighterFactory : IFighterFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IAbilityFactory _abilityFactory;
        private readonly IStaticDataService _staticDataService;

        public FighterFactory(
            IEntityFactory entityFactory, 
            IAbilityFactory abilityFactory, 
            IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _abilityFactory = abilityFactory;
            _staticDataService = staticDataService;
        }
        
        public GameEntity CreateFighter(FighterTypeId fighterTypeId, Vector3 at)
        {
            FighterConfig fighterConfig = _staticDataService.GetFighterConfig(fighterTypeId);

            Dictionary<Stats, float> baseStats = FillBaseStatsFromConfig(fighterConfig);

            GameEntity fighter = _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddViewPath(fighterConfig.ViewPath)
                .AddFighterTypeId(fighterTypeId)
                .AddPurchasePrice(fighterConfig.Price)
                .AddWorldPosition(at)
                .AddWorldRotation(Quaternion.identity)
                .AddBaseStats(baseStats)
                .AddStatModifiers(InitStats.EmptyStatDictionary())
                .AddMaxHp(baseStats[Stats.MaxHp])
                .AddCurrentHp(baseStats[Stats.MaxHp])
                .AddMaxMana(baseStats[Stats.MaxMana])
                .AddCurrentMana(0)
                .AddAttackRange(fighterConfig.AttackRange)
                .AddTargetBuffer(new List<int>(capacity: 16))
                .AddRadius(fighterConfig.TargetDetectionRadius)
                .AddSpeed(baseStats[Stats.Speed])
                .With(entity => entity.isFighter = true)
                .With(entity => entity.isSelected = true);
            
            CreateAbilities(fighterConfig, fighter);
            
            return fighter;
        }

        private void CreateAbilities(FighterConfig fighterConfig, GameEntity fighter)
        {
            GameEntity basicAbility = _abilityFactory.CreateBasicAbility(fighterConfig.BasicAbilityConfig, fighter.Id);
            fighter.AddBasicAbilityId(basicAbility.Id);

            if (fighterConfig.SpecialAbilityConfig == null)
            {
                return;
            }
            
            GameEntity special = _abilityFactory.CreateSpecialAbility(fighterConfig.SpecialAbilityConfig, fighter.Id);
            fighter.AddSpecialAbilityId(special.Id);
        }

        private static Dictionary<Stats, float> FillBaseStatsFromConfig(FighterConfig fighterConfig)
        {
            return InitStats.EmptyStatDictionary()
                .With(dictionary => dictionary[Stats.MaxHp] = fighterConfig.MaxHp)
                .With(dictionary => dictionary[Stats.Damage] = fighterConfig.Damage)
                .With(dictionary => dictionary[Stats.Speed] = fighterConfig.MoveSpeed)
                .With(dictionary => dictionary[Stats.MaxMana] = fighterConfig.MaxMana)
                .With(dictionary => dictionary[Stats.ManaRegen] = fighterConfig.ManaRegen);
        }
    }
}
