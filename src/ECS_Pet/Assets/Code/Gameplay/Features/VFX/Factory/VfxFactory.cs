using UnityEngine;
using Code.Infrastructure;

namespace Code.Gameplay.Features.Vfx.Factory
{
    public class VfxFactory : IVfxFactory
    {
        private readonly IEntityFactory _entityFactory;

        public VfxFactory(IEntityFactory entityFactory) =>
            _entityFactory = entityFactory;

        public GameEntity CreateVfx(string path, Vector3 position, Quaternion rotation, float lifetime)
        {
            return _entityFactory
                .CreateEntity<GameEntity>()
                .AddViewPath(path)
                .AddWorldPosition(position)
                .AddWorldRotation(rotation)
                .AddSelfDestructTimer(lifetime);
        }
    }
}
