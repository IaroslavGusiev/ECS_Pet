using Entitas;
using Code.StaticData;
using System.Collections.Generic;

namespace Code.Gameplay.Statuses
{
    [Game] public class Status : IComponent { }
    [Game] public class StatusTypeIdComponent : IComponent { public StatusTypeId Value; }
    [Game] public class StatusDuration : IComponent { public float Value; }
    [Game] public class StatusSetups : IComponent { public List<StatusSetup> Value; }

    [Game] public class StunStatus : IComponent { }
    [Game] public class Stunned : IComponent { }
    [Game] public class Affected : IComponent { }
    [Game] public class Unapplied : IComponent { }
}
