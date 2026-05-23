using UnityEngine;

namespace Code.Gameplay.Features.Vfx
{
    public static class VfxConstants
    {
        public static class Healing
        {
            public const string Path = "VFX/Healing_VFX.prefab";
            public const float Lifetime = 2f;
            public static readonly Vector3 PositionOffset = new(0, 1f, 0);
        }

        public static class ProjectileHit
        {
            public const string Path = "VFX/Poof_VFX.prefab";
            public const float Lifetime = 2f;
            public static readonly Vector3 PositionOffset = new(0, 0.5f, 0);
        }
    }
}
