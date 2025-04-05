using System;
using Entitas;
using System.Linq;
using Code.Gameplay.Common.Time;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IIdProvider _idProvider;
        
        private readonly Dictionary<Type, string> _entityTypeMapping = new();
        private readonly Dictionary<string, IContext> _contextNameToContext;

        private readonly Dictionary<string, int> _contextToIdIndex = new()
        {
            { "Game", GameComponentsLookup.Id },
            { "Meta", MetaComponentsLookup.Id }
        };

        public EntityFactory(IIdProvider idProvider)
        {
            _idProvider = idProvider;
            
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
                AddIdComponent(entity, name);
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
        
        private void AddIdComponent(IEntity entity, string contextName)
        {
            if (_contextToIdIndex.TryGetValue(contextName, out int idIndex))
            {
                var idComponent = new CommonComponents.Id { Value = _idProvider.NextId() };
                entity.AddComponent(idIndex, idComponent);
            }
            else
            {
                throw new InvalidOperationException($"Id component index not found for context: {contextName}");
            }
        }
    }
}