using UnityEngine;

namespace Code.Gameplay.Input
{
    public class InputService : IInputService
    {
        private readonly Camera _mainCamera = Camera.main;
        
        public bool IsMouseOverUI() =>
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        
        public bool IsLeftMouseButtonPressed() =>
            UnityEngine.Input.GetMouseButtonDown(0);

        public Vector3 GetScreenPosition() => 
            UnityEngine.Input.mousePosition;
        
        public bool IsEscapeKeyPressed() =>
            UnityEngine.Input.GetKeyDown(KeyCode.Escape);
    }
}