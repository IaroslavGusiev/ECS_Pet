using Entitas;

namespace Code.Common.Extensions
{
    public static class EntitasExtensions
    {
        public static bool IsEmpty<T>(this IGroup<T> group) where T : class, IEntity
        {
            return group.count == 0;
        }
    }
}