using Entitas;
using Code.StaticData;
using Code.Gameplay.Features.Combat;

namespace Code.Gameplay.Fighter
{
    public class FighterComponents
    {
        [Game] public class Fighter : IComponent {  }
        [Game] public class FighterTypeIdComponent : IComponent { public FighterTypeId Value; }
        [Game] public class FighterRequest : IComponent {  }
        [Game] public class Selected : IComponent {  }
        [Game] public class Placed : IComponent {  }
        
        [Game] public class BasicAbilityId : IComponent { public int Value; }
        [Game] public class SpecialAbilityId : IComponent { public int Value; }
    }
}