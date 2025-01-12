using System;
using Entitas;
using System.Linq;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class EntityFactory : IEntityFactory
    {
        private readonly Dictionary<Type, string> _entityTypeMapping = new();
        private readonly Dictionary<string, IContext> _contextNameToContext;

        public EntityFactory()
        {
            _contextNameToContext = Contexts.sharedInstance.allContexts
                .ToDictionary(context => context.contextInfo.name, context => context);
        }
        
        public TEntity CreateEntity<TEntity>(bool needToSetId = false) where TEntity : class, IEntity
        {
            string name = GetContextName(typeof(TEntity));
      
            var context = _contextNameToContext[name] as IContext<TEntity>;
      
            TEntity entity = context?.CreateEntity();
            
            if (needToSetId)
            {
                // entity?.AddComponent(new IdComponent { Value = _idProvider.NextId });
            }
            
            return entity;
        }
        
        private string GetContextName(Type type)
        {
            if (_entityTypeMapping.TryGetValue(type, out string name))
            {
                return name;
            }
            
            name = type.Name.Replace("Entity", "");
            _entityTypeMapping.Add(type, name);
            return name;
        }
    }
}