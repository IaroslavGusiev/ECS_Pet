using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.StateMachineBase
{
    public interface IPayloadState<in TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}