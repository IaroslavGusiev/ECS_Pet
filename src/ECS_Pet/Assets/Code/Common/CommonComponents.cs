using Entitas;
using Code.Common.View;

namespace Code.Common
{
    public class CommonComponents
    {
        [Game] public class View : IComponent { public IEntityView Value; }
        [Game] public class ViewPath : IComponent { public string Value; }
        
        [Game] public class Destructed : IComponent { }
    }
}