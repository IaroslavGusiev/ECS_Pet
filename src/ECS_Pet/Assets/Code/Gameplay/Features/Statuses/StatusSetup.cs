using System;
using Code.StaticData;

namespace Code.Gameplay.Statuses
{
    [Serializable]
    public class StatusSetup
    {
        public StatusTypeId StatusTypeId;
        public float Value;
        public float Duration;
        public float Period;
    }
}
