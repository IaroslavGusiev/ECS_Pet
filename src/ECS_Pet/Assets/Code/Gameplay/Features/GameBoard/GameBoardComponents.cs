using Entitas;

namespace Code.Gameplay.Features.GameBoard
{
    [Game] public class GameBoardCell : IComponent {  }
    [Game] public class CellId : IComponent { public int Value; }
    [Game] public class PlacedCellId : IComponent { public int Value; }
    [Game] public class Occupied : IComponent {  }
    [Game] public class SuccessfulCellRequest : IComponent {  }
}
