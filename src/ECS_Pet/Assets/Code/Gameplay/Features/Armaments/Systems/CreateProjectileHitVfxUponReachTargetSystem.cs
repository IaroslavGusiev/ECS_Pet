using Entitas;
using UnityEngine;
using Code.Infrastructure;
using System.Collections.Generic;

namespace Code.Gameplay.Armaments
{
    public class CreateProjectileHitVfxUponReachTargetSystem : ReactiveSystem<GameEntity>
    {
        private const string PoofVfxPath = "VFX/Poof_VFX.prefab";
        private const float VfxLifetime = 2f;

        private readonly GameContext _gameContext;
        private readonly IEntityFactory _entityFactory;
        
        private readonly Vector3 _vfxPositionOffset = new(0, 0.5f, 0);

        public CreateProjectileHitVfxUponReachTargetSystem(
            GameContext gameContext,
            IEntityFactory entityFactory)
            : base(gameContext)
        {
            _gameContext = gameContext;
            _entityFactory = entityFactory;
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
            _entityFactory
                .CreateEntity<GameEntity>()
                .AddViewPath(PoofVfxPath)
                .AddWorldPosition(target.WorldPosition + _vfxPositionOffset)
                .AddWorldRotation(target.hasWorldRotation
                    ? target.WorldRotation
                    : Quaternion.identity)
                .AddSelfDestructTimer(VfxLifetime);
        }
    }
}
