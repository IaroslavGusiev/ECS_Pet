using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Gameplay.Features.GameBoard;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        
        WindowsConfig GetWindowsConfig();
        GameBoardConfig GetGameBoardConfig();

        List<FighterConfig> GetAllFighterConfigs();
        FighterConfig GetFighterConfig(FighterTypeId fighterTypeId);
    }
}