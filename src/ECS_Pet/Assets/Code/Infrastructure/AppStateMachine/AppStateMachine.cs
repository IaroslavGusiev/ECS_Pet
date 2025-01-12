using Code.Infrastructure.StateMachineBase;

namespace Code.Infrastructure
{
    public class AppStateMachine : StateMachine
    {
        public AppStateMachine(IStateFactory stateFactory) : base(stateFactory) { }
    }
}