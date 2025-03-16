namespace Code.UI.BaseWindow
{
    public abstract class BaseWindow : CommonWindow
    {
        public abstract void SetupOnInstantiate();
    }
    
    public abstract class BaseWindow<TArg> : CommonWindow
    {
        public abstract void SetupOnInstantiate(TArg arg);
    }
}