using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Movement
{
    public sealed class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<UpdateTransformPositionSystem>());
        }
    }
}