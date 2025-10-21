using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.TargetCollection
{
    [Game] public class TargetId : IComponent { public int Value; }
    [Game] public class ProducerId: IComponent { public int Value; }
    
    [Game] public class TargetBuffer : IComponent { public List<int> Value; }
    [Game] public class ReadyToCollectTargets : IComponent {  }
}