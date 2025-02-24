using Entitas;

namespace Code.Gameplay.Target
{
    public class TargetComponents
    {
        [Game] public class TargetId : IComponent { public int Value; }
        [Game] public class ProducerId: IComponent { public int Value; }
    }
}