using System;
using Zenject;
using System.Threading;
using Cysharp.Threading.Tasks;
using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public abstract class Bootstrapper<T> : IInitializable, IDisposable where T : StateMachine
    {
        protected readonly T StateMachine;
        private CancellationTokenSource _tokenSource;

        protected Bootstrapper(T stateMachine) => 
            StateMachine = stateMachine;

        public void Initialize()
        {
            _tokenSource = new CancellationTokenSource();
            OnInitialize().Forget();
        }

        public void Dispose()
        {
            if (_tokenSource == null)
            {
                return;
            }
            
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        protected abstract UniTask OnInitialize();
    }
}