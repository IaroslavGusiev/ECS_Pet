using Zenject;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public class StateMachine : ITickable
    {
        private IExitableState _activeState;
        private readonly IStateFactory _stateFactory;

        protected StateMachine(IStateFactory stateFactory) => 
            _stateFactory = stateFactory;

        public void Tick()
        {
            if (_activeState is IUpdateable updateableState)
            {
                updateableState.Update();
            }
        }

        public UniTask<TState> Enter<TState>() where TState : class, IState => 
            RequestEnter<TState>();

        public UniTask<TState> Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload> => 
            RequestEnter<TState, TPayload>(payload);

        private async UniTask<TState> RequestEnter<TState>() where TState : class, IState
        {
            var state = await RequestChangeState<TState>();
            await EnterState(state);
            return state;
        }

        private async UniTask<TState> RequestEnter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = await RequestChangeState<TState>();
            await EnterPayloadState(state, payload);
            return state;
        }

        private async UniTask<TState> EnterState<TState>(TState state) where TState : class, IState
        {
            await state.Enter();
            _activeState = state;
            return state;
        }

        private async UniTask<TState> EnterPayloadState<TState, TPayload>(TState state, TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            await state.Enter(payload);
            _activeState = state;
            return state;
        }

        private async UniTask<TState> RequestChangeState<TState>() where TState : class, IExitableState
        {
            if (_activeState == null)
            {
                return ChangeState<TState>();
            }
            
            await _activeState.BeginExit();
            _activeState.EndExit();
            return ChangeState<TState>();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState => 
            _stateFactory.GetState<TState>();
    }
}