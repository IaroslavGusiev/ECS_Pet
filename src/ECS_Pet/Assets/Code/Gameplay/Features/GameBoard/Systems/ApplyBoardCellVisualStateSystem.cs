using Entitas;
using UnityEngine;
using Code.StaticData;
using System.Collections.Generic;

namespace Code.Gameplay.Features.GameBoard
{
    public class ApplyBoardCellVisualStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameBoardService _gameBoardService;

        public ApplyBoardCellVisualStateSystem(
            GameContext gameContext,
            IGameBoardService gameBoardService)
            : base(gameContext)
        {
            _gameBoardService = gameBoardService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.CellVisualState.Added());

        protected override bool Filter(GameEntity entity) =>
            entity.isGameBoardCell && entity.hasCellVisualState;

        protected override void Execute(List<GameEntity> cells)
        {
            foreach (GameEntity cell in cells)
            {
                RequestMaterialChange(cell, MaterialFor(cell.CellVisualState));
            }
        }

        private Material MaterialFor(BoardCellVisualState state) =>
            state switch
            {
                BoardCellVisualState.Available => _gameBoardService.GetGreenCellMaterial(),
                BoardCellVisualState.Blocked => _gameBoardService.GetRedCellMaterial(),
                _ => _gameBoardService.GetCurrentCellMaterial()
            };

        private static void RequestMaterialChange(GameEntity cell, Material material)
        {
            if (cell.hasMaterialChangeRequest)
            {
                cell.ReplaceMaterialChangeRequest(material);
                return;
            }

            cell.AddMaterialChangeRequest(material);
        }
    }
}
