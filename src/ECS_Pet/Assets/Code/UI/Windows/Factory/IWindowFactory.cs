using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.UI.BaseWindow
{
    public interface IWindowFactory
    {
        UniTask<TWindow> CreateWindow<TWindow>(AssetReference assetReference) where TWindow : BaseWindow;
        UniTask<TWindow> CreateWindow<TWindow, TArg>(AssetReference assetReference, TArg arg) where TWindow : BaseWindow<TArg>;
    }
}