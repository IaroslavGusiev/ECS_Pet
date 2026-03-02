using Entitas;

namespace Code.Gameplay.Animation
{
    public class AnimateFighterAbilitiesSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        
        private readonly IGroup<GameEntity> _basicAbilityAnimationRequests;
        private readonly IGroup<GameEntity> _specialAbilityAnimationRequests;

        public AnimateFighterAbilitiesSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _basicAbilityAnimationRequests = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.AnimationRequest, 
                GameMatcher.BasicAbility,
                GameMatcher.ProducerId
            }));
            
            _specialAbilityAnimationRequests = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.AnimationRequest, 
                GameMatcher.SpecialAbility,
                GameMatcher.ProducerId
            }));
        }

        public void Execute()
        {
            HandleBasicAbilityAnimationRequest();
            HandleSpecialAbilityAnimationRequest();
        }

        private void HandleBasicAbilityAnimationRequest()
        {
            foreach (GameEntity request in _basicAbilityAnimationRequests)
            {
                GameEntity producer = _gameContext.GetEntityWithId(request.ProducerId);
                
                if (producer is { hasFighterAnimator: true })
                {
                    producer.FighterAnimator.AnimateBasicAbility();
                }

                request.isDestructed = true;
            }
        }

        private void HandleSpecialAbilityAnimationRequest()
        {
            foreach (GameEntity request in _specialAbilityAnimationRequests)
            {
                GameEntity producer = _gameContext.GetEntityWithId(request.ProducerId);
        
                if (producer is { hasFighterAnimator: true })
                {
                    producer.FighterAnimator.AnimateSpecialAbility();} 

                request.isDestructed = true;
            }
        }
    }
}