using Entitas;
using UnityEngine;
using System.Collections.Generic;
using Code.Gameplay.Features.Vfx.Factory;

namespace Code.Gameplay.Features.Vfx
{
    public class CreateProjectileHitVfxUponReachTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        private readonly IVfxFactory _vfxFactory;

        public CreateProjectileHitVfxUponReachTargetSystem(
            GameContext gameContext,
            IVfxFactory vfxFactory)
            : base(gameContext)
        {
            _gameContext = gameContext;
            _vfxFactory = vfxFactory;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.ReachedTarget.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.isProjectileArmament && entity.hasTargetId;

        protected override void Execute(List<GameEntity> projectiles)
        {
            foreach (GameEntity projectile in projectiles)
            {
                GameEntity target = _gameContext.GetEntityWithId(projectile.TargetId);

                if (target is not
                    {
                        isDead: false,
                        hasWorldPosition: true
                    })
                {
                    continue;
                }

                CreateVfxEntity(target);
            }
        }

        private void CreateVfxEntity(GameEntity target)
        {
            _vfxFactory.CreateVfx(
                VfxConstants.ProjectileHit.Path,
                target.WorldPosition + VfxConstants.ProjectileHit.PositionOffset,
                target.hasWorldRotation ? target.WorldRotation : Quaternion.identity,
                VfxConstants.ProjectileHit.Lifetime);
        }
    }
}
