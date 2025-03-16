using Code.Common.View;
using Code.Gameplay.Input;
using Code.Common.Destruct;
using Code.Gameplay.Fighter;
using Code.Infrastructure.Systems;
using Code.Gameplay.FighterSelection;
using Code.Gameplay.Features.Movement;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay
{
    public sealed class GameFeature : Feature
    {
        public GameFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<InputFeature>());
            Add(systemFactory.Create<BindViewFeature>());

            Add(systemFactory.Create<GameBoardFeature>());
            Add(systemFactory.Create<FighterSelectionFeature>());
            Add(systemFactory.Create<MovementFeature>());
            Add(systemFactory.Create<FighterFeature>());
            
            Add(systemFactory.Create<ProcessDestructedFeature>());
        }
    }
}