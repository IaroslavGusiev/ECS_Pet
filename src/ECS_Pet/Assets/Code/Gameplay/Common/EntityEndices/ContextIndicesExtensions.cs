using Entitas;
using System.Collections.Generic;
using Code.Gameplay.CharacterStats;
using Code.Gameplay.CharacterStats.Indexing;

namespace Code.Gameplay.Common.Time.EntityIndices
{
    public static class ContextIndicesExtensions
    {
        public static HashSet<GameEntity> TargetStatChanges(this GameContext context, Stats stat, int targetId)
        {
            return ((EntityIndex<GameEntity, StatKey>) context.GetEntityIndex(GameEntityIndices.StatChangeKey))
                .GetEntities(new StatKey(targetId, stat));
        }
    }
}