using Entitas;
using Code.UI.BaseWindow;
using Code.Common.Extensions;
using System.Collections.Generic;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay.FighterSelection
{
    public class DestroyFighterOnCancelSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IWindowService _windowService;
        private readonly IGameBoardService _gameBoardService;
        
        private readonly IGroup<InputEntity> _inputs;
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly List<GameEntity> _buffer = new(capacity: 4);

        public DestroyFighterOnCancelSystem(
            GameContext gameContext, 
            InputContext inputContext, 
            IWindowService windowService, 
            IGameBoardService gameBoardService)
        {
            _gameContext = gameContext;
            _windowService = windowService;
            _gameBoardService = gameBoardService;

            _inputs = inputContext.GetGroup(InputMatcher.AllOf(matchers: new[]
            {
                InputMatcher.EscKeyInput
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
                if (input.isEscKeyInput == false)
                {
                    continue;
                }
                
                CleanUpFighter(fighter);

                _windowService
                    .GetWindowFromActive<SelectFighterWindow>()
                    .DeselectAll();

                if (fighter.hasCellId == false)
                {
                    continue;
                }
                    
                SwitchAttachedCellToDefaultState(fighter);
            }
        }

        private void CleanUpFighter(GameEntity fighter)
        {
            fighter.isSelected = false;
            fighter.isDestructed = true;
        }

        private void SwitchAttachedCellToDefaultState(GameEntity fighter)
        {
            _gameContext
                .GetEntityWithId(fighter.CellId)
                .AddMaterialChangeRequest(_gameBoardService.GetCurrentCellMaterial());
        }
    }
}