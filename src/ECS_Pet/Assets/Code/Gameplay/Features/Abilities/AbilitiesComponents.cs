using Entitas;
using Code.StaticData;
using Entitas.CodeGeneration.Attributes;

namespace Code.Gameplay.Abilities
{
    [Game] public class Ability : IComponent {  }
    [Game] public class BasicAbility : IComponent {  }
    [Game] public class SpecialAbility : IComponent {  }
    
    [Game] public class OwnerLink : IComponent { [EntityIndex] public int Value; }
    [Game] public class AnimationDelay : IComponent {  public float Value; }
    [Game] public class AnimationDelayLeft : IComponent {  public float Value; }
    
    [Game] public class AbilityTypeIdComponent : IComponent { public AbilityTypeId Value; }
    [Game] public class MeleeAttackAbility : IComponent { }
}