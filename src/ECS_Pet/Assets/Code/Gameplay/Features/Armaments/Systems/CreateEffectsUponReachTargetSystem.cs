using Entitas;
using Code.GameplayEffects;
using System.Collections.Generic;
using Code.Gameplay.Statuses;
using Code.Gameplay.Statuses.Applier;

namespace Code.Gameplay.Armaments
{
    public class CreateEffectsUponReachTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IEffectFactory _effectFactory;
        private readonly IStatusApplier _statusApplier;

        public CreateEffectsUponReachTargetSystem(
            GameContext gameContext, 
            IEffectFactory effectFactory,
            IStatusApplier statusApplier)
            : base(gameContext)
        {
            _gameContext = gameContext;
            _effectFactory = effectFactory;
            _statusApplier = statusApplier;
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

                if (projectile.hasStatusSetups)
                {
                    foreach (StatusSetup setup in projectile.StatusSetups)
                    {
                        _statusApplier.ApplyStatus(setup, projectile.OwnerLink, target.Id);
                    }
                }

                projectile.isDestructed = true;
            }
        }
    }
}
