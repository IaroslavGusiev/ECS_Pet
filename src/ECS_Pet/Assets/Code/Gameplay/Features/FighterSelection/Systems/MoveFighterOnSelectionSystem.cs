using Entitas;
using UnityEngine;
using Code.StaticData;
using Code.Gameplay.Input;
using Code.Gameplay.Common.Time;
using Code.Common.Extensions;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay.FighterSelection
{
    public class MoveFighterOnSelectionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IInputService _inputService;
        private readonly IPhysicsService _physicsService;
        private readonly IGameBoardService _gameBoardService;
        private readonly IGroup<GameEntity> _selectedFighters;

        private readonly Vector3 _offsetForGameBoard = new(0f, 0.5f, 0f);
        private int _lastHitCellIndex = -1;

        public MoveFighterOnSelectionSystem(
            GameContext gameContext,
            IInputService inputService, 
            IPhysicsService physicsService, 
            IGameBoardService gameBoardService)
        {
            _gameContext = gameContext;
            _inputService = inputService;
            _physicsService = physicsService;
            _gameBoardService = gameBoardService;

            _selectedFighters = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterTypeId,
                GameMatcher.Selected,
                GameMatcher.Fighter
            }));
        }

        public void Execute()
        {
            if (_selectedFighters.IsEmpty())
            {
                return;
            }
            
            foreach (GameEntity fighter in _selectedFighters)
            {
                GameEntity hitCell = _physicsService.RaycastFromScreen(_inputService.GetScreenPosition(), CollisionLayer.GameBoard.AsMask());
                
                if (hitCell == null)
                {
                    return;
                }

                if (_lastHitCellIndex != -1 && _lastHitCellIndex == hitCell.Id)
                {
                    return;
                }

                SwitchPreviousCellToDefaultState();
                
                HandleMaterialChangeOfCurrentHitCell(hitCell);
                
                fighter
                    .ReplaceWorldPosition(hitCell.WorldPosition + _offsetForGameBoard)
                    .ReplaceTargetId(hitCell.Id); 
                
                _lastHitCellIndex = hitCell.Id;
            }
        }

        private void SwitchPreviousCellToDefaultState()
        {
            GameEntity lastHitCell = GetEntityById(_lastHitCellIndex);
            lastHitCell?.AddMaterialChangeRequest(_gameBoardService.GetCurrentCellMaterial());
        }

        private void HandleMaterialChangeOfCurrentHitCell(GameEntity hitCell)
        {
            hitCell.AddMaterialChangeRequest(hitCell.isOccupied == false
                ? _gameBoardService.GetGreenCellMaterial()
                : _gameBoardService.GetRedCellMaterial());
        }

        private GameEntity GetEntityById(int id) => 
            _gameContext.GetEntityWithId(id);
    }
}