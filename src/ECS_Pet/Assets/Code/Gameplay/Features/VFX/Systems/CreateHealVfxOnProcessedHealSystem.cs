using Entitas;
using UnityEngine;
using Code.GameplayEffects;
using Code.Gameplay.Features.Vfx.Factory;

namespace Code.Gameplay.Features.Vfx
{
    public class CreateHealVfxOnProcessedHealSystem : IExecuteSystem
    {
        private readonly IVfxFactory _vfxFactory;
        private readonly IGroup<GameEntity> _healEffects;

        public CreateHealVfxOnProcessedHealSystem(GameContext gameContext, IVfxFactory vfxFactory)
        {
            _vfxFactory = vfxFactory;

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
            _vfxFactory.CreateVfx(
                VfxConstants.Healing.Path,
                target.WorldPosition + VfxConstants.Healing.PositionOffset,
                target.hasWorldRotation ? target.WorldRotation : Quaternion.identity,
                VfxConstants.Healing.Lifetime);
        }
    }
}
