using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public class SimplePayloadState<TPayload> : IPayloadState<TPayload>
    {
        public virtual UniTask Enter(TPayload payload) => 
            UniTask.CompletedTask;

        protected virtual UniTask Exit() => 
            UniTask.CompletedTask;

        async UniTask IExitableState.BeginExit() => 
            await Exit();

        void IExitableState.EndExit(){}
    }
}