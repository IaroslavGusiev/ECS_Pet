using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input
{
    public class InputComponents
    {
        [Input] public class InputComponent : IComponent { }
        [Input] public class ClickInput : IComponent { public Vector3 Value; }
        [Input] public class EscKeyInputComponent : IComponent { }
    }
}