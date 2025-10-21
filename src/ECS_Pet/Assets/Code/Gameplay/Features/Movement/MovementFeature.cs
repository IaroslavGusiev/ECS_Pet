using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Movement
{
    public sealed class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systemFactory)
        {
            // Execute systems
            Add(systemFactory.Create<CalculateMovementDirectionSystem>());
            Add(systemFactory.Create<CalculateDistanceToTargetSystem>());
            Add(systemFactory.Create<ConvertCurrentTargetToMovementTargetSystem>());
            Add(systemFactory.Create<ApplyMovementToTargetSystem>());
            Add(systemFactory.Create<RotateTowardDirectionSystem>());
            Add(systemFactory.Create<StopMovementOnArrivalSystem>());
            
            Add(systemFactory.Create<UpdateTransformPositionSystem>());
            Add(systemFactory.Create<UpdateWorldRotationSystem>());
        }
    }
}