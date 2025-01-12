using Code.StaticData;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        GameBoardConfig GetGameBoardConfig();
    }
}