using Entitas;
using Code.StaticData;

namespace Code.Gameplay.Monster
{
    [Game] public class Monster : IComponent {  }
    [Game] public class MonsterTypeIdComponent : IComponent { public MonsterTypeId Value; }
}