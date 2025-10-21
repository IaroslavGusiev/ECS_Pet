using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public class EndOfFrameExitState : IState, IUpdateable
    {
        private UniTaskCompletionSource _exitCompletionSource;

        public virtual UniTask Enter() => 
            UniTask.CompletedTask;

        public async UniTask BeginExit()
        {
            await OnBeginExit();
        }

        public void EndExit()
        {
            ExitOnEndOfFrame();
            ClearExitCompletionSource();
        }

        public void Update()
        {
            if (ExitWasRequested == false)
            {
                OnUpdate();
            }

            if (ExitWasRequested)
            {
                ResolveExitCompletionSource();
            }
        }

        protected virtual UniTask OnBeginExit() =>
            UniTask.CompletedTask;

        protected virtual void ExitOnEndOfFrame() { }

        protected virtual void OnUpdate() { }

        private bool ExitWasRequested => 
            _exitCompletionSource != null;

        private void ClearExitCompletionSource() =>
            _exitCompletionSource = null;

        private void ResolveExitCompletionSource() => 
            _exitCompletionSource?.TrySetResult();
    }
}