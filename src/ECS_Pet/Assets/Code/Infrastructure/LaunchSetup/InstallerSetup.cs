using Zenject;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public abstract class InstallerSetup<TStateMachine, TBootstrapper> : MonoInstaller 
        where TStateMachine : StateMachine 
        where TBootstrapper : Bootstrapper<TStateMachine>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<TBootstrapper>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<TStateMachine>()
                .AsSingle();
            
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();
            
            Bind();
            BindStates();
        }

        protected abstract void Bind();
        
        protected abstract void BindStates();
    }
}