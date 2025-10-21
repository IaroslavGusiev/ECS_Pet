using Code.Common.View;
using Code.Gameplay.Input;
using Code.Common.Destruct;
using Code.Gameplay.Abilities;
using Code.Gameplay.Combat;
using Code.GameplayEffects;
using Code.Gameplay.Fighter;
using Code.Gameplay.Monster;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.Systems;
using Code.Gameplay.CharacterStats;
using Code.Gameplay.FighterSelection;
using Code.Gameplay.TargetCollection;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay
{
    public sealed class GameFeature : Feature
    {
        private const float SimulationTickSeconds = 1f; 
        
        public GameFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<EmitTickSystem>(SimulationTickSeconds));
            
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<BindViewFeature>());

            Add(systemFactory.Create<GameBoardFeature>());
            Add(systemFactory.Create<FighterSelectionFeature>());
            Add(systemFactory.Create<MovementFeature>());
            Add(systemFactory.Create<CombatFeature>());
            Add(systemFactory.Create<AbilitiesFeature>());
            
            Add(systemFactory.Create<FighterFeature>());
            Add(systemFactory.Create<MonsterFeature>());
            Add(systemFactory.Create<StatsFeature>());

            Add(systemFactory.Create<CollectTargetsFeature>());
            Add(systemFactory.Create<EffectFeature>());
            
            Add(systemFactory.Create<CleanupTickSystem>());
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}