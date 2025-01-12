using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{
    public class InputComponents
    {
        [Input] public class Input : IComponent { }
        [Input] public class ClickInput : IComponent { public Vector3 Value; }
    }
}