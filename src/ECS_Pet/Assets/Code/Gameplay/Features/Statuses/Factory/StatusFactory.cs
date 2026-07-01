using System;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;

namespace Code.Gameplay.Statuses.Factory
{
    public class StatusFactory : IStatusFactory
    {
        private readonly IEntityFactory _entityFactory;

        public StatusFactory(IEntityFactory entityFactory) =>
            _entityFactory = entityFactory;

        public GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId)
        {
            GameEntity status = setup.StatusTypeId switch
            {
                StatusTypeId.Stun => CreateStunStatus(setup, producerId, targetId),
                _ => throw new Exception($"Status with type id {setup.StatusTypeId} does not exist")
            };

            status
                .With(entity => entity.AddStatusDuration(setup.Duration), when: setup.Duration > 0)
                .With(entity => entity.AddTimeLeft(setup.Duration), when: setup.Duration > 0);

            return status;
        }

        private GameEntity CreateStunStatus(StatusSetup setup, int producerId, int targetId)
        {
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddStatusTypeId(StatusTypeId.Stun)
                .AddEffectValue(setup.Value)
                .AddProducerId(producerId)
                .AddTargetId(targetId)
                .With(entity => entity.isStatus = true)
                .With(entity => entity.isStunStatus = true);
        }
    }
}
