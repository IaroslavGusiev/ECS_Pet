using Entitas;

namespace Code.GameplayEffects.Systems
{
    public class ApplyEffectsOnTargetsSystem : IExecuteSystem
    {
        private readonly IEffectFactory _effectFactory;
        private readonly IGroup<GameEntity> _entities;

        public ApplyEffectsOnTargetsSystem(GameContext gameContext, IEffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            
            _entities = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.TargetBuffer, 
                GameMatcher.EffectConfigs
            }));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities)
            foreach (int targetId in entity.TargetBuffer)
            foreach (EffectConfig config in entity.EffectConfigs)
            {
                _effectFactory.CreateEffect(config, ProducerId(entity), targetId);
            }
        }
        
        private static int ProducerId(GameEntity entity) => 
            entity.hasProducerId 
                ? entity.ProducerId 
                : entity.Id;
    }
}