using Entitas;
using Code.Gameplay.Features.Combat;

namespace Code.Gameplay.Combat
{ 
   [Game] public class Attacking : IComponent {  } 
   [Game] public class AttackRange : IComponent { public float Value; }
   [Game] public class TimeLeft : IComponent { public float Value; }
   
   [Game] public class StatsSliderHolderComponent : IComponent { public StatsSliderHolder Value; }
   [Game] public class CombatantAnimatorComponent : IComponent { public CombatantAnimator Value; }
   [Game] public class CombatantProjectileHolderComponent : IComponent { public CombatantProjectileHolder Value; }
}