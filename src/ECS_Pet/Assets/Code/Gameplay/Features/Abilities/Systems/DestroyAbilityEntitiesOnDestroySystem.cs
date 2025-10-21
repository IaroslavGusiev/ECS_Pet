using Entitas;

namespace Code.Gameplay.Abilities
{
    public class DestroyAbilitiesOnOwnerDestroySystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _fighters;

        public DestroyAbilitiesOnOwnerDestroySystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _fighters = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Fighter, 
                GameMatcher.Destructed
            }));
        }

        public void Execute()
        {
            foreach (GameEntity fighter in _fighters)
            {
                foreach (GameEntity entity in _gameContext.GetEntitiesWithOwnerLink(fighter.Id))
                {
                    entity.isDestructed = true;
                }
            }
        }
    }
}