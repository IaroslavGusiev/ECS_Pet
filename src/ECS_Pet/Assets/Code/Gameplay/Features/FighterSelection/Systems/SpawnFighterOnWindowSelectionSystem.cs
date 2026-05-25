using Entitas;
using UnityEngine;
using Code.StaticData;
using Code.Gameplay.Input;
using Code.Gameplay.Fighter;
using Code.Common.Extensions;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay.FighterSelection
{
    public class SpawnFighterOnWindowSelectionSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IInputService _inputService;
        private readonly IPhysicsService _physicsService;
        private readonly IFighterFactory _fighterFactory;
        private readonly IGameBoardService _gameBoardService;

        private readonly IGroup<GameEntity> _fighterRequests;
        private readonly IGroup<GameEntity> _boardCells;
        private readonly List<GameEntity> _buffer = new(capacity: 4);

        private Camera _camera;
        private int _fighterId = -1;
        
        private readonly Vector3 _offsetForGameBoard = new(0f, 0.5f, 0f);

        private Camera MainCamera => _camera ??= Camera.main;

        public SpawnFighterOnWindowSelectionSystem(
            GameContext gameContext, 
            IInputService inputService,
            IPhysicsService physicsService,
            IFighterFactory fighterFactory,
            IGameBoardService gameBoardService)
        {
            _gameContext = gameContext;
            _inputService = inputService;
            _physicsService = physicsService;
            _fighterFactory = fighterFactory;
            _gameBoardService = gameBoardService;

            _fighterRequests = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.FighterTypeId, 
                GameMatcher.FighterRequest
            }));

            _boardCells = gameContext.GetGroup(GameMatcher.AllOf(matchers: new[]
            {
                GameMatcher.GameBoardCell,
                GameMatcher.WorldPosition
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
            
            if (ShouldDestroyPreviousFighter(fighter) == false)
            {
                return;
            }

            if (fighter.hasCellId)
            {
                SwitchCellToDefaultState(fighter.CellId);
            }
            
            fighter.isSelected = false;
            fighter.isDestructed = true;
        }

        private void CreateFighterForSelection(GameEntity request)
        {
            GameEntity initialCell = GetInitialCell();
            
            Vector3 initialPosition = initialCell != null
                ? initialCell.WorldPosition + _offsetForGameBoard
                : Vector3.zero; 

            GameEntity fighter = _fighterFactory.CreateFighter(request.FighterTypeId, initialPosition);

            if (initialCell != null)
            {
                fighter.AddCellId(initialCell.Id);
                MarkCellForSelection(initialCell);
            }

            _fighterId = fighter.Id;
        }

        private void MarkCellForSelection(GameEntity cell)
        {
            RequestMaterialChange(cell, cell.isOccupied == false
                ? _gameBoardService.GetGreenCellMaterial()
                : _gameBoardService.GetRedCellMaterial());
        }

        private void SwitchCellToDefaultState(int cellId)
        {
            GameEntity cell = _gameContext.GetEntityWithId(cellId);

            if (cell == null)
            {
                return;
            }

            RequestMaterialChange(cell, _gameBoardService.GetCurrentCellMaterial());
        }

        private GameEntity GetInitialCell()
        {
            Vector3 screenPosition = _inputService.GetScreenPosition();
            GameEntity hitCell = _physicsService.RaycastFromScreen(screenPosition, CollisionLayer.GameBoard.AsMask());

            return hitCell ?? GetClosestBottomRowCell(screenPosition);
        }

        private GameEntity GetClosestBottomRowCell(Vector3 screenPosition)
        {
            if (_boardCells.IsEmpty() || MainCamera == null)
            {
                return null;
            }

            float bottomRowZ = FindBottomRowZ();

            return FindClosestCellInBottomRow(bottomRowZ, screenPosition);
        }

        private float FindBottomRowZ()
        {
            var bottomRowZ = float.MaxValue;

            foreach (GameEntity cell in _boardCells)
            {
                if (cell.WorldPosition.z < bottomRowZ)
                {
                    bottomRowZ = cell.WorldPosition.z;
                }
            }

            return bottomRowZ;
        }

        private GameEntity FindClosestCellInBottomRow(float bottomRowZ, Vector3 screenPosition)
        {
            GameEntity closestCell = null;
            
            var closestScreenDistance = float.MaxValue;

            foreach (GameEntity cell in _boardCells)
            {
                if (Mathf.Approximately(cell.WorldPosition.z, bottomRowZ) == false)
                {
                    continue;
                }

                float cellScreenX = MainCamera.WorldToScreenPoint(cell.WorldPosition).x;
                float screenDistance = Mathf.Abs(screenPosition.x - cellScreenX);

                if (screenDistance < closestScreenDistance)
                {
                    closestScreenDistance = screenDistance;
                    closestCell = cell;
                }
            }

            return closestCell;
        }

        private static void CleanUpRequest(GameEntity request)
        {
            request.isFighterRequest = false;
            request.isDestructed = true;
        }

        private static void RequestMaterialChange(GameEntity cell, Material material)
        {
            if (cell.hasMaterialChangeRequest)
            {
                cell.ReplaceMaterialChangeRequest(material);
                return;
            }

            cell.AddMaterialChangeRequest(material);
        }

        private static bool ShouldDestroyPreviousFighter(GameEntity fighter) => 
            fighter is { isPlaced: false };
    }
}
