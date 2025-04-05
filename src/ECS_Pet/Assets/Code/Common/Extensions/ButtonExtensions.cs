using UnityEngine.UI;
using UnityEngine.Events;

namespace Code.Common.Extensions
{
    public static class ButtonExtensions
    {
        public static void AddClickListener(this Button button, UnityAction action) => 
            button.onClick.AddListener(action);

        public static void RemoveClickListener(this Button button, UnityAction action) => 
            button.onClick.RemoveListener(action);

        public static Button RemoveAllClickListeners(this Button button)
        {
            button.onClick.RemoveAllListeners();
            return button;
        }

        public static void ClearAndAddListener(this Button button, UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
        
        public static void AddOneTimeClickListener(this Button button, UnityAction action)
        {
            RemoveAllClickListeners(button);
            
            button.onClick.AddListener(() =>
            {
                button.RemoveAllClickListeners();
                action?.Invoke();
            });
        }
    }
}