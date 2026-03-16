using Code.Infrastructure.Systems;

namespace Code.Gameplay.CharacterStats
{
    public sealed class StatsFeature : Feature
    {
        public StatsFeature(ISystemFactory systems)
        {
            Add(systems.Create<StatChangeSystem>());
            Add(systems.Create<ApplySpeedFromStatsSystem>());
        }
    }
}