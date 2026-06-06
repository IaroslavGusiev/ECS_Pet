using Entitas;

namespace Code.Gameplay.Abilities
{
    public class DestroyAbilitiesOnOwnerDestroySystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _owners;

        public DestroyAbilitiesOnOwnerDestroySystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _owners = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Destructed)
                .AnyOf(
                    GameMatcher.BasicAbilityId, 
                    GameMatcher.SpecialAbilityId));
        }

        public void Execute()
        {
            foreach (GameEntity owner in _owners)
            {
                foreach (GameEntity entity in _gameContext.GetEntitiesWithOwnerLink(owner.Id))
                {
                    entity.isDestructed = true;
                }
            }
        }
    }
}
