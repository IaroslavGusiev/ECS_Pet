using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.UI.Gold;
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
        private readonly IEntityFactory _entityFactory;
        private readonly IWindowService _windowService;
        private readonly IGameBoardService _gameBoardService;

        public BattleEnterState(
            AppStateMachine stateMachine, 
            IEntityFactory entityFactory,
            IWindowService windowService,
            IGameBoardService gameBoardService)
        {
            _stateMachine = stateMachine;
            _entityFactory = entityFactory;
            _windowService = windowService;
            _gameBoardService = gameBoardService;
        }

        public override async UniTask Enter()
        {
            CreateStorageEntity();
            
            await CreateGameplayWindows();
            
            _gameBoardService.Initialize();
            
            _stateMachine
                .Enter<BattleLoopState>()
                .Forget();
        }

        private void CreateStorageEntity()
        {
            _entityFactory
                .CreateEntity<GameEntity>()
                .With(entity => entity.isStorage = true)
                .AddGold(100);
        }

        private async UniTask CreateGameplayWindows()
        {
            //List<FighterTypeId> randomFighters = EnumExtensions.GetRandomEnumValues(count: 6, excludeValues: FighterTypeId.Unknown);
            
            var randomFighters = new List<FighterTypeId> { FighterTypeId.Minotaur, FighterTypeId.Rat, FighterTypeId.Eagle, FighterTypeId.Ent, FighterTypeId.EvilWitch, FighterTypeId.Golem };
            
            var data = new SelectFighterWindowData(randomFighters);
            
            await _windowService.ShowWindow<SelectFighterWindow, SelectFighterWindowData>(data);
            await _windowService.ShowWindow<GoldBadgeWindow>();
        }
    }
}
