using System.Linq;
using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Gameplay.Fighter;
using Code.Gameplay.Monster;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Features.GameBoard;

namespace Code.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAddressablesAssetProvider _assetProvider;
        
        private List<WindowsConfig> _windowConfigs;
        private List<GameBoardConfig> _gameBoardConfigs;
        private Dictionary<FighterTypeId, FighterConfig> _fighterConfigs;
        private Dictionary<MonsterTypeId, MonsterConfig> _monsterConfigs;

        public StaticDataService(IAddressablesAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public async UniTask Initialize()
        {
            var tasks = new List<UniTask>
            {
                LoadWindowsConfig(),
                LoadFighterConfigs(),
                LoadMonsterConfigs(),
                LoadGameBoardConfigs()
            };

           await UniTask.WhenAll(tasks);
        }

        public WindowsConfig GetWindowsConfig() =>
            _windowConfigs.FirstOrDefault();

        public GameBoardConfig GetGameBoardConfig() =>
            _gameBoardConfigs.FirstOrDefault();

        public FighterConfig GetFighterConfig(FighterTypeId fighterTypeId) => 
            _fighterConfigs.GetValueOrDefault(fighterTypeId);
        
        public MonsterConfig GetMonsterConfig(MonsterTypeId monsterTypeId) => 
            _monsterConfigs.GetValueOrDefault(monsterTypeId);

        public AbilityConfig GetBasicAbilityConfig(FighterTypeId fighterTypeId) => 
            GetFighterConfig(fighterTypeId).BasicAbilityConfig;

        public AbilityConfig GetSpecialAbilityConfig(FighterTypeId fighterTypeId) => 
            GetFighterConfig(fighterTypeId).SpecialAbilityConfig;
        
        public AbilityConfig GetBasicAbilityConfig(MonsterTypeId monsterTypeId) => 
            GetMonsterConfig(monsterTypeId).BasicAbilityConfig;

        public AbilityConfig GetSpecialAbilityConfig(MonsterTypeId monsterTypeId) => 
            GetMonsterConfig(monsterTypeId).SpecialAbilityConfig;

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
        
        private async UniTask LoadMonsterConfigs()
        {
            MonsterConfig[] configs = await GetConfigs<MonsterConfig>();
            _monsterConfigs = configs.ToDictionary(config => config.MonsterTypeId);
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
