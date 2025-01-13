namespace Code.Common.View
{
    public abstract class EntityComponentRegistrar : EntityDependant
    {
        public abstract void RegisterComponents();
        public abstract void UnregisterComponents(); 
    }
}