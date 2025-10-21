using Entitas;

namespace Code.Gameplay.Fighter
{
    public class AnimateFighterMovementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _fighters;

        public AnimateFighterMovementSystem(GameContext game)
        {
            _fighters = game.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.Fighter, 
                GameMatcher.FighterAnimator
            }));
        }

        public void Execute()
        {
            foreach (GameEntity fighter in _fighters)
            {
                if (fighter.isMoving)
                {
                    fighter.FighterAnimator.Walk();
                }
                else
                {
                    fighter.FighterAnimator.Idle();
                }
            }
        }
    }
}