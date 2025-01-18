using Entitas;
using PrimeTween;
using UnityEngine;
using Code.Gameplay.Common;

namespace Code.Gameplay.Features.GameBoard
{
   public class ProcessClickOnGameBoardSystem : IExecuteSystem
   {
      private readonly GameContext _gameContext;
      
      private readonly IPhysicsService _physicsService;
      private readonly IGroup<InputEntity> _inputs;
      private readonly IGroup<GameEntity> _cells;

      public ProcessClickOnGameBoardSystem(GameContext gameContext, InputContext inputContext, IPhysicsService physicsService)
      {
         _gameContext = gameContext;
         
         _physicsService = physicsService;
         
         _inputs = inputContext.GetGroup(InputMatcher.AllOf(InputMatcher.Input));
         
         _cells = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
         {
            GameMatcher.GameBoardCell, 
            GameMatcher.LayerMask
         }));
      }

      public void Execute()
      {
         foreach (InputEntity input in _inputs)
         foreach (GameEntity cell in _cells)
         {
            if (input.hasClickInput)
            {
               GameEntity entity = _physicsService.RaycastFromScreen(input.ClickInput, cell.LayerMask);
               if (entity.hasId)
               {
                  GameEntity gameEntity = _gameContext.GetEntityWithId(entity.Id);
                  Tween.PunchScale(gameEntity.Transform, new Vector3(0.35f, 0.35f, 0.35f), 0.5f);
               }
            }
         }
      }
   }
}