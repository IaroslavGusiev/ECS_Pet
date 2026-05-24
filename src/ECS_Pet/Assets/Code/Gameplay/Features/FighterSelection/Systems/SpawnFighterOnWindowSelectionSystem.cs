using Entitas;
using UnityEngine;
using Code.Gameplay.Fighter;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class SpawnFighterOnWindowSelectionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IFighterFactory _fighterFactory;

        private readonly IGroup<GameEntity> _fighterRequests;
        private readonly List<GameEntity> _buffer = new(capacity: 4);

        private readonly Vector3 _middleOfBoard = new(0, 0, 4); // TODO: later calculate this value
        private int _fighterId = -1;

        public SpawnFighterOnWindowSelectionSystem(
            GameContext gameContext, 
            IFighterFactory fighterFactory)
        {
            _gameContext = gameContext;
            _fighterFactory = fighterFactory;

            _fighterRequests = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterTypeId, 
                GameMatcher.FighterRequest
            }));
        }

        public void Execute()
        {
            foreach (GameEntity request in _fighterRequests.GetEntities(_buffer))
            {
                CleanupPreviousFighter();
                CreateFighterForSelection(request);
                CleanUpRequest(request);
            }
        }

        private void CleanupPreviousFighter()
        {
            GameEntity fighter = _gameContext.GetEntityWithId(_fighterId);
            
            if (fighter == null || fighter.isPlaced)
            {
                return;
            }
            
            fighter.isSelected = false;
            fighter.isDestructed = true;
        }

        private void CreateFighterForSelection(GameEntity request)
        {
            GameEntity fighter = _fighterFactory.CreateFighter(request.FighterTypeId, _middleOfBoard);
            _fighterId = fighter.Id;
        }

        private static void CleanUpRequest(GameEntity request)
        {
            request.isFighterRequest = false;
            request.isDestructed = true;
        }
    }
}
