using Code.Infrastructure.Systems;

namespace Code.Gameplay.Fighter
{
    public sealed class FighterFeature : Feature
    {
        public FighterFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<CreateFightersSystem>());
        }
    }
}