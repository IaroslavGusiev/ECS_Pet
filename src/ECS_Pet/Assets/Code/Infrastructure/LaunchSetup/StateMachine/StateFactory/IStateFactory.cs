namespace Code.Infrastructure.StateMachineBase
{
    public interface IStateFactory
    {
        T GetState<T>() where T : class, IExitableState;
    }
}