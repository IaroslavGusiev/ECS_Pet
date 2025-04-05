using System;
using System.Linq;
using System.Collections.Generic;

namespace Code.Gameplay.CharacterStats
{
    public enum Stats
    {
        Unknown = 0,
        MaxMana = 1,
        MaxHp = 2,
        Damage = 3,
        ManaRegen = 4
    }
    
    public static class InitStats
    {
        public static Dictionary<Stats, float> EmptyStatDictionary()
        {
            return Enum.GetValues(typeof(Stats))
                .Cast<Stats>()
                .Except(new[] { Stats.Unknown })
                .ToDictionary(stat => stat, _ => 0f);
        }
    }
}