using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.CompositionRoot
{
    public class AppBootstrapper : Bootstrapper<AppStateMachine>
    {
        public AppBootstrapper(AppStateMachine stateMachine) : base(stateMachine) { }
        
        protected override async UniTask OnInitialize()
        {
            Debug.Log("<color=green>App is launched!</color>");
            await StateMachine.Enter<BootstrapState>();
        }
    }
}