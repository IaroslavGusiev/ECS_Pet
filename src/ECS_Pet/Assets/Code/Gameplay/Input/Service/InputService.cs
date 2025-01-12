using UnityEngine;

namespace Code.Gameplay.Input
{
    public class InputService : IInputService
    {
        public bool IsLeftMouseButtonPressed() =>
            UnityEngine.Input.GetMouseButtonDown(0);

        public Vector3 GetClickPosition() => 
            UnityEngine.Input.mousePosition;
    }
}