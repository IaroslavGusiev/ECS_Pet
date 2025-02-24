using Entitas;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.GameBoard
{
    public class HandleSuccessfulClickOnBoardCellSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _successfulRequests;

        public HandleSuccessfulClickOnBoardCellSystem(GameContext game)
        {
            _successfulRequests = game.GetGroup(GameMatcher.AllOf(GameMatcher.SuccessfulCellRequest));
        }

        public void Execute()
        {
            foreach (GameEntity request in _successfulRequests)
            {
                int idOfCell = request.Id;
                
                // TODO: For debug we will spawn random tower
                
                request
                    .With(entity => entity.isSuccessfulCellRequest = false)
                    .With(entity => entity.isDestructed = true);
            }
        }
    }
}