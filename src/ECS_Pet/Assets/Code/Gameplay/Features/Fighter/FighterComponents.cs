using Entitas;
using Code.StaticData;

namespace Code.Gameplay.Fighter
{
    public class FighterComponents
    {
        [Game] public class Fighter : IComponent {  }
        [Game] public class FighterTypeIdComponent : IComponent { public FighterTypeId Value; }
        [Game] public class FighterRequest : IComponent {  }
        [Game] public class Selected : IComponent {  }
    }
}