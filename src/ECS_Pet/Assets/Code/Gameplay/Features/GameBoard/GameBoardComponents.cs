using Entitas;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardComponents
    {
        [Game] public class GameBoardCell : IComponent {  }
        [Game] public class Occupied : IComponent {  }
        [Game] public class SuccessfulCellRequest : IComponent {  }
    }
}