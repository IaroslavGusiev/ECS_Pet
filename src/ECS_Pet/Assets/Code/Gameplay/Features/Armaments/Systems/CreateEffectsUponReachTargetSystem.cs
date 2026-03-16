using Entitas;
using Code.GameplayEffects;
using System.Collections.Generic;

namespace Code.Gameplay.Armaments
{
    public class CreateEffectsUponReachTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IEffectFactory _effectFactory;

        public CreateEffectsUponReachTargetSystem(
            GameContext gameContext, 
            IEffectFactory effectFactory) 
            : base(gameContext)
        {
            _gameContext = gameContext;
            _effectFactory = effectFactory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.ReachedTarget.Added());

        protected override bool Filter(GameEntity entity)
        {
            return entity.isProjectileArmament &&
                   entity.hasOwnerLink &&
                   entity.hasTargetId &&
                   entity.hasEffectConfigs;
        }

        protected override void Execute(List<GameEntity> projectiles)
        {
            foreach (GameEntity projectile in projectiles)
            {
                GameEntity target = _gameContext.GetEntityWithId(projectile.TargetId);

                if (target == null || target.isDead)
                {
                    continue;
                }

                foreach (EffectConfig config in projectile.EffectConfigs)
                {
                    _effectFactory.CreateEffect(config, projectile.OwnerLink, target.Id);
                }

                projectile.isDestructed = true;
            }
        }
    }
}