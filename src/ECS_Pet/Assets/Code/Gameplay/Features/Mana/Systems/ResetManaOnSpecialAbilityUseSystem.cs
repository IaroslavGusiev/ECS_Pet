using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Mana
{
    public class ResetManaOnSpecialAbilityUseSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _gameContext;
        
        public ResetManaOnSpecialAbilityUseSystem(GameContext gameContext) 
            : base(gameContext) => 
            _gameContext = gameContext;

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.ReadyToUse.Removed());
        
        protected override bool Filter(GameEntity entity) =>
            entity.isSpecialAbility && entity.hasOwnerLink;

        protected override void Execute(List<GameEntity> abilities)
        {
            foreach (GameEntity ability in abilities)
            {
                GameEntity owner = _gameContext.GetEntityWithId(ability.OwnerLink);

                if (owner == null || owner.isDead)
                {
                    continue;
                }
                
                owner.ReplaceCurrentMana(0);
            }
        }
    }
}