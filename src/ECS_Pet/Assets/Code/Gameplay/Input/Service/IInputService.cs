using UnityEngine;

namespace Code.Gameplay.Input
{
    public interface IInputService
    {
        bool IsLeftMouseButtonPressed();
        Vector3 GetScreenPosition();
        bool IsEscapeKeyPressed();
        bool IsMouseOverUI();
    }
}