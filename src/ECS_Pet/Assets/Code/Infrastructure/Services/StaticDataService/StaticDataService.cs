using System.Linq;
using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Gameplay.Features.GameBoard;

namespace Code.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAddressablesAssetProvider _assetProvider;

        private List<WindowsConfig> _windowConfigs;
        private List<GameBoardConfig> _gameBoardConfigs;
        private Dictionary<FighterTypeId, FighterConfig> _fighterConfigs;

        public StaticDataService(IAddressablesAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Initialize()
        {
            await LoadGameBoardConfigs();
            await LoadWindowsConfig();
            await LoadFighterConfigs();
        }
        
        public WindowsConfig GetWindowsConfig() =>
            _windowConfigs.FirstOrDefault();

        public GameBoardConfig GetGameBoardConfig() =>
            _gameBoardConfigs.FirstOrDefault();
        
        public FighterConfig GetFighterConfig(FighterTypeId fighterTypeId) => 
            _fighterConfigs.GetValueOrDefault(fighterTypeId);

        public List<FighterConfig> GetAllFighterConfigs() => 
            _fighterConfigs.Values.ToList();

        private async UniTask LoadGameBoardConfigs()
        {
            GameBoardConfig[] configs = await GetConfigs<GameBoardConfig>();
            _gameBoardConfigs = configs.ToList();
        }

        private async UniTask LoadWindowsConfig()
        {
            WindowsConfig[] configs = await GetConfigs<WindowsConfig>();
            _windowConfigs = configs.ToList();
        }

        private async UniTask LoadFighterConfigs()
        {
            FighterConfig[] configs = await GetConfigs<FighterConfig>();
            _fighterConfigs = configs.ToDictionary(config => config.FighterTypeId);
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