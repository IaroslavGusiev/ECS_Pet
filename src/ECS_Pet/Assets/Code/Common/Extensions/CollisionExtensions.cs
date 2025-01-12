using UnityEngine;
using Code.StaticData;

namespace Code.Common.Extensions
{
    public static class CollisionExtensions
    {
        public static bool Matches(this Collider collider, LayerMask layerMask) =>
            ((1 << collider.gameObject.layer) & layerMask) != 0;

        public static int AsMask(this CollisionLayer layer) =>
            1 << (int)layer;
    }
}