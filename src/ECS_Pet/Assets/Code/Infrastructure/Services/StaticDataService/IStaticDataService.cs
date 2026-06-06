using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Code.Gameplay.Monster;
using Cysharp.Threading.Tasks;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Features.GameBoard;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        
        WindowsConfig GetWindowsConfig();
        GameBoardConfig GetGameBoardConfig();
        
        FighterConfig GetFighterConfig(FighterTypeId fighterTypeId);
        MonsterConfig GetMonsterConfig(MonsterTypeId monsterTypeId);
        
        AbilityConfig GetBasicAbilityConfig(FighterTypeId fighterTypeId);
        AbilityConfig GetSpecialAbilityConfig(FighterTypeId fighterTypeId);
        AbilityConfig GetBasicAbilityConfig(MonsterTypeId monsterTypeId);
        AbilityConfig GetSpecialAbilityConfig(MonsterTypeId monsterTypeId);
    }
}
