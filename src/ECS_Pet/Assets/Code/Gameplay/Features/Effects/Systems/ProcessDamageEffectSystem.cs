using Entitas;

namespace Code.GameplayEffects.Systems
{
    public class ProcessDamageEffectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _effects;

        public ProcessDamageEffectSystem(GameContext game)
        {
            _effects = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.DamageEffect,
                GameMatcher.EffectValue,
                GameMatcher.TargetId
            }));
        }
    
        public void Execute()
        {
            foreach (GameEntity effect in _effects)
            {
                GameEntity target = effect.Target();
                
                effect.isProcessed = true;

                if (target.isDead) 
                {
                    continue;
                }
                
                target.ReplaceCurrentHp(target.CurrentHp - effect.EffectValue);
            }
        }
    }
}