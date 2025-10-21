using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Cysharp.Threading.Tasks;
using Code.Gameplay.Features.GameBoard;
using Code.Gameplay.Monster;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        
        WindowsConfig GetWindowsConfig();
        GameBoardConfig GetGameBoardConfig();
        
        FighterConfig GetFighterConfig(FighterTypeId fighterTypeId);
        MonsterConfig GetMonsterConfig(MonsterTypeId monsterTypeId);
    }
}