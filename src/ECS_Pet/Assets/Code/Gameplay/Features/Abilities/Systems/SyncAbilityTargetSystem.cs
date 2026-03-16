using Entitas;

namespace Code.Gameplay.Abilities
{
    public class SyncAbilityTargetSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _abilities;

        public SyncAbilityTargetSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _abilities = _gameContext.GetGroup(GameMatcher.
                AllOf(
                    GameMatcher.Ability, 
                    GameMatcher.OwnerLink)
                .NoneOf(
                    GameMatcher.HealingAbility));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities)
            {
                GameEntity owner = _gameContext.GetEntityWithId(ability.OwnerLink);

                if (owner == null || owner.isDead)
                {
                    continue;
                }

                if (owner.hasTargetId)
                {
                    ability.ReplaceTargetId(owner.TargetId);
                }
                else
                {
                    if (ability.hasTargetId)
                        ability.RemoveTargetId();
                }
            }
        }
    }
}