using System.Linq;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAddressablesAssetProvider _assetProvider;
        private List<GameBoardConfig> _gameBoardConfigs;

        public StaticDataService(IAddressablesAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Initialize()
        {
            await LoadGameBoardConfigs();
        }
        
        public GameBoardConfig GetGameBoardConfig() =>
            _gameBoardConfigs.FirstOrDefault();

        private async UniTask LoadGameBoardConfigs()
        {
            GameBoardConfig[] configs = await GetConfigs<GameBoardConfig>();
            _gameBoardConfigs = configs.ToList();
        }

        private async UniTask<T[]> GetConfigs<T>() where T : class
        {
            List<string> keys = await GetConfigsKeys<T>();
            T[] loadedConfigs = await _assetProvider.LoadAll<T>(keys);
            return loadedConfigs;
        }

        private async UniTask<List<string>> GetConfigsKeys<T>() =>
            await _assetProvider.FetchAssetKeysByLabel<T>(AssetLabels.Configs);
    }
}