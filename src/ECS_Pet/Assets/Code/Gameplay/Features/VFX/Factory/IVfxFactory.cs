using UnityEngine;

namespace Code.Gameplay.Features.Vfx.Factory
{
    public interface IVfxFactory
    {
        GameEntity CreateVfx(string path, Vector3 position, Quaternion rotation, float lifetime);
    }
}
