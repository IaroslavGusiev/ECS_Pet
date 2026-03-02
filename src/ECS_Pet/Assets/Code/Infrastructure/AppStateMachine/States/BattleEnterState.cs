using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Common.Extensions;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Gameplay.FighterSelection;
using Code.Gameplay.Features.GameBoard;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public class BattleEnterState : SimpleState
    {
        private readonly AppStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IGameBoardService _gameBoardService;

        public BattleEnterState(
            AppStateMachine stateMachine, 
            IWindowService windowService,
            IGameBoardService gameBoardService)
        {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _gameBoardService = gameBoardService;
        }

        public override async UniTask Enter()
        {
            await CreateGameplayWindows();
            
            _gameBoardService.Initialize();
            
            _stateMachine
                .Enter<BattleLoopState>()
                .Forget();
        }

        private async UniTask CreateGameplayWindows()
        {
            //List<FighterTypeId> randomFighters = EnumExtensions.GetRandomEnumValues(count: 6, excludeValues: FighterTypeId.Unknown);
            
            List<FighterTypeId> randomFighters = new List<FighterTypeId> { FighterTypeId.Minotaur, FighterTypeId.Bee, FighterTypeId.Eagle, FighterTypeId.Ent, FighterTypeId.EvilWitch, FighterTypeId.Golem };
            
            var data = new SelectFighterWindowData(randomFighters);
            await _windowService.ShowWindow<SelectFighterWindow, SelectFighterWindowData>(data);
        }
    }
}