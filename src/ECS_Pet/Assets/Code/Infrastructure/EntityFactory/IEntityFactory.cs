using Entitas;

namespace Code.Infrastructure
{
    public interface IEntityFactory
    {
        TEntity CreateEntity<TEntity>(bool needToSetId = false) where TEntity : class, IEntity;
    }
}