using Entitas;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Code.Common.Extensions;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay.FighterSelection
{
    public class FighterPlacementOnClickSystem : IExecuteSystem
    {
        private readonly GameContext _gameContext;
        private readonly IWindowService _windowService;
        private readonly IGameBoardService _gameBoardService;
        private readonly IStaticDataService _staticDataService;
        private readonly IFighterPurchaseService _fighterPurchaseService;
        private readonly IFighterPlacementService _fighterPlacementService;
        
        private readonly IGroup<GameEntity> _selectedFighters;
        private readonly IGroup<InputEntity> _inputs;

        private readonly List<GameEntity> _buffer = new(capacity: 4);

        public FighterPlacementOnClickSystem(
            GameContext gameContext, 
            InputContext inputContext, 
            IWindowService windowService, 
            IGameBoardService gameBoardService, 
            IStaticDataService staticDataService,
            IFighterPurchaseService fighterPurchaseService,
            IFighterPlacementService fighterPlacementService)
        {
            _gameContext = gameContext;
            _windowService = windowService;
            _gameBoardService = gameBoardService;
            _staticDataService = staticDataService;
            _fighterPurchaseService = fighterPurchaseService;
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
                if (input.hasClickInput && fighter.hasCellId)
                {
                    GameEntity cell = GetEntityById(fighter.CellId);

                    if (cell is not { isOccupied: false })
                    {
                        continue;
                    }

                    FighterConfig fighterConfig = _staticDataService.GetFighterConfig(fighter.FighterTypeId);

                    if (_fighterPurchaseService.TryPurchase(fighterConfig) == false)
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
                .RemoveCellId()
                .With(entity => entity.isPlaced = true)
                .With(entity => entity.isSelected = false)
                .With(entity => entity.isMovementAvailable = true)
                .With(entity => entity.isReadyToCollectTargets = true)
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
