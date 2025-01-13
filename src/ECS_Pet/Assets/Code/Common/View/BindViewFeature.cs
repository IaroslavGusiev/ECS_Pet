using Code.Infrastructure.Systems;

namespace Code.Common.View
{
    public sealed class BindViewFeature : Feature
    {
        public BindViewFeature(ISystemFactory systemFactory)
        {
           Add(systemFactory.Create<BindEntityViewFromPathSystem>());
        }
    }
}