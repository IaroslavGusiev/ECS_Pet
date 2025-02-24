using Cysharp.Threading.Tasks;
using Code.Gameplay.Features.GameBoard;
using Code.Gameplay.Fighter;
using Code.StaticData;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        GameBoardConfig GetGameBoardConfig();
        FighterConfig GetFighterConfig(FighterTypeId fighterTypeId);
    }
}