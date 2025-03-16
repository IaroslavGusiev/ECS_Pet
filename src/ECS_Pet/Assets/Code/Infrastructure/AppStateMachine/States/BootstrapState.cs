using Code.StaticData;
using Code.UI.BaseWindow;
using Code.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public class BootstrapState : SimpleState
    {
       private readonly AppStateMachine _stateMachine;
       private readonly IWindowService _windowService;
       private readonly ILoadingCurtain _loadingCurtain;
       private readonly IStaticDataService _staticDataService;
       private readonly IAddressablesAssetProvider _assetProvider;

       public BootstrapState(
           AppStateMachine stateMachine, 
           IWindowService windowService,
           ILoadingCurtain loadingCurtain, 
           IStaticDataService staticDataService, 
           IAddressablesAssetProvider assetProvider)
       {
           _stateMachine = stateMachine;
           _assetProvider = assetProvider;
           _windowService = windowService;
           _loadingCurtain = loadingCurtain;
           _staticDataService = staticDataService;
       }
       
       public override async UniTask Enter()
       {
           _loadingCurtain
               .Show()
               .Forget();
           
           await _assetProvider.InitializeAsync();
           await _assetProvider.WarmupAssetsByLabel(AssetLabels.Configs);
           await _staticDataService.Initialize();
           
           _windowService.Initialize();
           _stateMachine
               .Enter<BattleEnterState>()
               .Forget();
       }
       
         protected override async UniTask Exit() => 
             await _assetProvider.ReleaseAssetsByLabel(AssetLabels.Configs);
    }
}