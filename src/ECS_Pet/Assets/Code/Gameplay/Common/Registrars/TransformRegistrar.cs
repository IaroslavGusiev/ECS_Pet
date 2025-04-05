using Code.Common.View;

namespace Code.Gameplay.Common.Time
{
    public class TransformRegistrar : EntityComponentRegistrar
    {
        public override void RegisterComponents() => 
            Entity.AddTransform(transform);

        public override void UnregisterComponents()
        {
            if (Entity.hasTransform)
            {
                Entity.RemoveTransform();
            }
        }
    }
}