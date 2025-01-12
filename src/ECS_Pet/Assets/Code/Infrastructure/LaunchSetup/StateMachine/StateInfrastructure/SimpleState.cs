using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public class SimpleState : IState
    {
        public virtual UniTask Enter() => 
            UniTask.CompletedTask;

        protected virtual UniTask Exit() => 
            UniTask.CompletedTask;

        async UniTask IExitableState.BeginExit() => 
            await Exit();

        void IExitableState.EndExit() { }
    }
}