using Entitas;
using PrimeTween;
using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Gameplay.Common;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.GameBoard
{
   public class ProcessClickOnBoardCellSystem : IExecuteSystem
   {
      private readonly IPhysicsService _physicsService;
      private readonly IEntityFactory _entityFactory;
      private readonly IGroup<InputEntity> _inputs;

      public ProcessClickOnBoardCellSystem(
         InputContext inputContext, 
         IEntityFactory entityFactory, 
         IPhysicsService physicsService)
      {
         _entityFactory = entityFactory;
         _physicsService = physicsService;

         _inputs = inputContext.GetGroup(InputMatcher.AllOf(InputMatcher.Input));
      }

      public void Execute()
      {
         foreach (InputEntity input in _inputs)
         {
            if (input.hasClickInput == false)
            {
               continue;
            }
            
            GameEntity hitEntity = _physicsService.RaycastFromScreen(input.ClickInput, CollisionLayer.GameBoard.AsMask());
            if (hitEntity.isOccupied)
            {
               // TODO: show some message that cell is already occupied
               continue;
            }
            
            CreateSuccessfulClickRequest(hitEntity);
            hitEntity.isOccupied = true;
            
            Tween.PunchScale(hitEntity.Transform, new Vector3(0.35f, 0.35f, 0.35f), 0.5f); // TODO: right now only for debug
         }
      }

      private void CreateSuccessfulClickRequest(GameEntity hitEntity)
      {
         _entityFactory
            .CreateEntity<GameEntity>()
            .AddTargetId(hitEntity.Id)
            .With(entity => entity.isSuccessfulCellRequest = true);
      }
   }
}