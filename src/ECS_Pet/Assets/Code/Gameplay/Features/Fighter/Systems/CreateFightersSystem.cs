using Entitas;
using Code.StaticData;
using Code.Common.Extensions;
using System.Collections.Generic;

namespace Code.Gameplay.Fighter
{
   public class CreateFightersSystem : IExecuteSystem
   {
      private readonly GameContext _gameContext;
      private readonly IFighterFactory _fighterFactory;
      private readonly IFighterPlacementService _fighterPlacementService;

      private readonly IGroup<GameEntity> _successfulRequests;
      private readonly List<GameEntity> _buffer = new(capacity: 8);

      public CreateFightersSystem(
         GameContext gameContext, 
         IFighterFactory fighterFactory, 
         IFighterPlacementService fighterPlacementService)
      {
         _gameContext = gameContext;
         _fighterPlacementService = fighterPlacementService;
         _fighterFactory = fighterFactory;

         _successfulRequests = _gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
         {
            GameMatcher.TargetId,
            GameMatcher.SuccessfulCellRequest
         }));
      }

      public void Execute()
      {
         foreach (GameEntity request in _successfulRequests.GetEntities(_buffer))
         {
            int idOfCell = request.TargetId;
            GameEntity cell = _gameContext.GetEntityWithId(idOfCell);
            
            if (cell.hasWorldPosition)
            {
               GameEntity fighter = _fighterFactory.CreateFighter(FighterTypeId.EvilWitch, cell.WorldPosition);
               _fighterPlacementService.RegisterFighter(fighter.Id, idOfCell);
            }

            CleanupRequest(request);
         }
      }

      private void CleanupRequest(GameEntity request)
      {
         request
            .With(entity => entity.isSuccessfulCellRequest = false)
            .With(entity => entity.isDestructed = true);
      }
   }
}