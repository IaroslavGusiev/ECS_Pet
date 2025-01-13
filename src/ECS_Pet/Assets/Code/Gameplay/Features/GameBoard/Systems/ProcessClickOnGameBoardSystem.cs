using Entitas;
using Code.StaticData;
using Code.Gameplay.Common;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.GameBoard
{
   public class ProcessClickOnGameBoardSystem : IExecuteSystem
   {
      private readonly IPhysicsService _physicsService;
      private readonly IGroup<InputEntity> _inputs;
      private readonly IGroup<GameEntity> _cells;

      public ProcessClickOnGameBoardSystem(GameContext gameContext, InputContext inputContext, IPhysicsService physicsService)
      {
         _physicsService = physicsService;
         _cells = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Destructed));
         _inputs = inputContext.GetGroup(InputMatcher.AllOf(InputMatcher.Input));
      }

      public void Execute()
      {
         foreach (InputEntity input in _inputs)
         {
            if (input.hasClickInput)
            {
               _physicsService.RaycastFromScreen(input.ClickInput, CollisionLayer.GameBoard.AsMask());
            }
         }
      }
   }
}