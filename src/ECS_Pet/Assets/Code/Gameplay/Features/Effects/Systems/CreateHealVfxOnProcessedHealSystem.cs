using Entitas;
using UnityEngine;
using Code.Infrastructure;

namespace Code.GameplayEffects.Systems
{
    public class CreateHealVfxOnProcessedHealSystem : IExecuteSystem
    {
        private const string HealingVfxPath = "VFX/Healing_VFX.prefab";
        private const float VfxLifetime = 2f;

        private readonly IEntityFactory _entityFactory;
        private readonly IGroup<GameEntity> _healEffects;
        
        private readonly Vector3 _vfxPositionOffset = new(0, 1f, 0);

        public CreateHealVfxOnProcessedHealSystem(GameContext gameContext, IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;

            _healEffects = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.HealEffect,
                GameMatcher.Processed,
                GameMatcher.TargetId));
        }

        public void Execute()
        {
            foreach (GameEntity healEffect in _healEffects)
            {
                GameEntity target = healEffect.Target();

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
                .AddViewPath(HealingVfxPath)
                .AddWorldPosition(target.WorldPosition + _vfxPositionOffset)
                .AddWorldRotation(target.hasWorldRotation 
                    ? target.WorldRotation 
                    : Quaternion.identity)
                .AddSelfDestructTimer(VfxLifetime);
        }
    }
}
