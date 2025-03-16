using Entitas;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Code.Common.Extensions;
using System.Collections.Generic;
using Code.Gameplay.Features.GameBoard;
using UnityEngine;

namespace Code.Gameplay.FighterSelection
{
    public class FighterPlacementOnClickSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IWindowService _windowService;
        private readonly IGameBoardService _gameBoardService;
        private readonly IFighterPlacementService _fighterPlacementService;
        
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly IGroup<InputEntity> _inputs;

        private readonly List<GameEntity> _buffer = new(capacity: 4);

        public FighterPlacementOnClickSystem(
            GameContext gameContext, 
            InputContext inputContext, 
            IWindowService windowService, 
            IGameBoardService gameBoardService, 
            IFighterPlacementService fighterPlacementService)
        {
            _gameContext = gameContext;
            _windowService = windowService;
            _gameBoardService = gameBoardService;
            _fighterPlacementService = fighterPlacementService;

            _inputs = inputContext.GetGroup(InputMatcher.AllOf(matchers: new[]
            {
                InputMatcher.ClickInput
            }));
            
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
            
            foreach (GameEntity fighter in _selectedFighters.GetEntities(_buffer))
            foreach (InputEntity input in _inputs)
            {
                if (input.hasClickInput && fighter.hasTargetId)
                {
                    GameEntity cell = GetEntityById(fighter.TargetId);

                    if (cell is not { isOccupied: false })
                    {
                        continue;
                    }
                    
                    _windowService
                        .GetWindowFromActive<SelectFighterWindow>()
                        .DeselectAll();
                    
                    PlaceFighterToCell(fighter, cell.Id);
                    SwitchCellToDefaultState(cell.Id);
                }
            }
        }

        private void PlaceFighterToCell(GameEntity fighter, int cellId)
        {
            fighter
                .RemoveTargetId()
                .With(entity => entity.isSelected = false)
                .With(entity => _fighterPlacementService.RegisterFighter(entity.Id, cellId));
        }

        private void SwitchCellToDefaultState(int cellId)
        {
            _gameContext
                .GetEntityWithId(cellId)
                .AddMaterialChangeRequest(_gameBoardService.GetCurrentCellMaterial())
                .With(entity => entity.isOccupied = true);
        }
        
        private GameEntity GetEntityById(int id) => 
            _gameContext.GetEntityWithId(id);
    }
}