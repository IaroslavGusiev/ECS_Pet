using Entitas;

namespace Code.GameplayEffects
{
    [Game] public class Effect : IComponent { }
    [Game] public class EffectValue : IComponent { public float Value; }
    [Game] public class DamageEffect : IComponent { }
    [Game] public class HealEffect : IComponent { }
}