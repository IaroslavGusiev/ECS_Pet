using Entitas;

namespace Code.Gameplay.Features.Movement
{
    public class ConvertCurrentTargetToMovementTargetSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IGroup<GameEntity> _movers;

        public ConvertCurrentTargetToMovementTargetSystem(GameContext gameContext)
        {
            _gameContext = gameContext;
            
            _movers = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.TargetId, 
                GameMatcher.WorldPosition
            }));
        }

        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                GameEntity target = _gameContext.GetEntityWithId(mover.TargetId);

                if (target is { hasWorldPosition: true, isDead: false })
                {
                    mover.ReplaceMovementTarget(target.WorldPosition);

                    if (mover.isMoving == false)
                    {
                        mover.isMoving = true;
                    }
                }
                else
                {
                    if (mover.hasMovementTarget)
                    {
                        mover.RemoveMovementTarget();
                    }

                    if (mover.hasDistanceToTarget)
                    {
                        mover.RemoveDistanceToTarget();
                    }

                    mover.isMoving = false;
                }
            }
        }
    }
}