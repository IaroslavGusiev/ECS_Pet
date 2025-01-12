using Cysharp.Threading.Tasks;

namespace Code.UI.LoadingCurtain
{
    public interface ILoadingCurtain
    {
        UniTask Show();
        UniTask Hide();
    }
}