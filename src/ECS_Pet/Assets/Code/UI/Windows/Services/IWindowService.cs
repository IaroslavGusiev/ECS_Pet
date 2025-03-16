using Cysharp.Threading.Tasks;

namespace Code.UI.BaseWindow
{
    public interface IWindowService
    {
        void Initialize();
        UniTask<TScreen> ShowWindow<TScreen>() where TScreen : BaseWindow;
        UniTask<TScreen> ShowWindow<TScreen, TArg>(TArg arg) where TScreen : BaseWindow<TArg>;
        T GetWindowFromActive<T>() where T : CommonWindow;
    }
}