using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardComponents
    {
        [Game] public class GameBoardCell : IComponent {  }
        [Game] public class MaterialChangeRequest : IComponent { public Material Value; }
    }
}