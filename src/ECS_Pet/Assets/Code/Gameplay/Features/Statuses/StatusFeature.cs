using Code.Infrastructure.Systems;
using Code.Gameplay.Statuses.Systems;

namespace Code.Gameplay.Statuses
{
    public sealed class StatusFeature : Feature
    {
        public StatusFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<StatusDurationSystem>());
            Add(systemFactory.Create<ApplyStunStatusSystem>());
            Add(systemFactory.Create<UnapplyStunStatusSystem>());

            Add(systemFactory.Create<CleanupUnappliedStatusesSystem>());
        }
    }
}
