using System;
using System.Linq;
using System.Collections.Generic;

namespace Code.Common.Extensions
{
    public static class EnumExtensions
    {
        public static T GetRandomEnumValue<T>(params T[] excludeValues) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            values = values.Except(excludeValues).ToArray();
            return values.PickRandom();
        }
        
        public static List<T> GetRandomEnumValues<T>(int count, params T[] excludeValues) where T : Enum
        {
            var values = new List<T>(collection: (T[]) Enum.GetValues(typeof(T)));
            values = values.Except(excludeValues).ToList();

            if (count > values.Count)
            {
                throw new ArgumentException($"Cannot get {count} random values from enum with only {values.Count} available values.");
            }

            return values
                .Shuffle()
                .Take(count)
                .ToList();
        }
    }
}