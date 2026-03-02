using Code.Infrastructure.Systems;

namespace Code.Gameplay.Monster
{
    public sealed class MonsterFeature : Feature
    {
        public MonsterFeature(ISystemFactory systemFactory)
        {
            // initialize
            Add(systemFactory.Create<SpawnMonsterOnInitSystem>());

            // execute
            Add(systemFactory.Create<MonsterDeathSystem>());
            Add(systemFactory.Create<FinalizeMonsterDeathProcessingSystem>());

            // cleanup
        }
    }
}