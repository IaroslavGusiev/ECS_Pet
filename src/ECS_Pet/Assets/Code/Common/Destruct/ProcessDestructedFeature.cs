using Code.Infrastructure.Systems;

namespace Code.Common.Destruct
{
    public sealed class ProcessDestructedFeature : Feature
    {
        public ProcessDestructedFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CleanupGameEntitySystem>());
            Add(systemFactory.Create<CleanupGameDestructedViewSystem>());
        }
    }
}