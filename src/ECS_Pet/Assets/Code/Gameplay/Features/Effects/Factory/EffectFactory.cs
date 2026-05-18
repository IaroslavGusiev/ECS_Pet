using System;
using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;

namespace Code.GameplayEffects
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IEntityFactory _entityFactory;
        
        public EffectFactory(IEntityFactory entityFactory) => 
            _entityFactory = entityFactory;

        public GameEntity CreateEffect(EffectConfig effectConfig, int producerId, int targetId)
        {
            switch (effectConfig.EffectTypeId)
            {
                case EffectTypeId.Damage:
                    return CreateDamage(producerId, targetId, effectConfig.Value);
                
                case EffectTypeId.Heal:
                    return CreateHeal(producerId, targetId, effectConfig.Value);
            }
            
            throw new Exception($"Effect with type id {effectConfig.EffectTypeId} does not exist");
        }

        private GameEntity CreateDamage(int producerId, int targetId, float value)
        {
            Debug.Log($"<color=yellow>Create Damage Effect. Producer id: {producerId}, targetId: {targetId}, value: {value}</color>");
            
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .With(x => x.isEffect = true)
                .With(x => x.isDamageEffect = true)
                .AddEffectValue(value)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }

        private GameEntity CreateHeal(int producerId, int targetId, float value)
        {
            Debug.Log($"<color=yellow>Create Heal Effect. Producer id: {producerId}, targetId: {targetId}, value: {value}</color>");

            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .With(x => x.isEffect = true)
                .With(x => x.isHealEffect = true)
                .AddEffectValue(value)
                .AddProducerId(producerId)
                .AddTargetId(targetId);
        }
    }
}