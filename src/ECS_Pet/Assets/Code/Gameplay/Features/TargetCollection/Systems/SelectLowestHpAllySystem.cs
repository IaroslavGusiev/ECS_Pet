using Entitas;

namespace Code.Gameplay.TargetCollection
{
    public class SelectLowestHpAllySystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _allies;
        private readonly IGroup<GameEntity> _healingAbilities;

        public SelectLowestHpAllySystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _healingAbilities = _gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.HealingAbility, 
                GameMatcher.OwnerLink));
            
            _allies = _gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.MaxHp,
                GameMatcher.CurrentHp, 
                GameMatcher.Fighter));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _healingAbilities)
            {
                GameEntity owner = _gameContext.GetEntityWithId(ability.OwnerLink);

                if (owner == null || owner.isDead)
                {
                    continue;
                }
                
                GameEntity lowestHpAlly = FindLowestHpAlly(owner);

                if (lowestHpAlly != null)
                {
                    ability.ReplaceTargetId(lowestHpAlly.Id);
                }
                else if (ability.hasTargetId)
                {
                    ability.RemoveTargetId();
                }
            }
        }
        
        private GameEntity FindLowestHpAlly(GameEntity healer)
        {
            GameEntity lowest = null;
            var lowestRatio = float.MaxValue;

            foreach (GameEntity ally in _allies)
            {
                if (ally.isDead || ally.Id == healer.Id)
                {
                    continue;
                }

                float hpRatio = ally.CurrentHp / ally.MaxHp;

                if (hpRatio < lowestRatio)
                {
                    lowestRatio = hpRatio;
                    lowest = ally;
                }
            }

            return lowest;
        }
    }
}