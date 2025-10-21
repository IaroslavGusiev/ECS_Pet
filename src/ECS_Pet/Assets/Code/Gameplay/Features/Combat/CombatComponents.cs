using Entitas;

namespace Code.Gameplay.Combat
{ 
   [Game] public class Attacking : IComponent {  } 
   [Game] public class AttackRange : IComponent { public float Value; }
   [Game] public class TimeLeft : IComponent { public float Value; }
}