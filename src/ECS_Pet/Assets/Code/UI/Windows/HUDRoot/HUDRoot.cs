using UnityEngine;

namespace Code.UI.BaseWindow
{
    public class HUDRoot : MonoBehaviour, IHUDRoot
    {
        [field: SerializeField] public Transform HudRoot { get; private set; }
    }
}