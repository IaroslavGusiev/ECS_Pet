using System;

namespace Code.Gameplay.UI.Gold
{
    public class StorageUIService : IStorageUIService
    {
        public event Action GoldChanged;
        
        private float _currentGold;
        
        public float CurrentGold => _currentGold;
        
        public void UpdateCurrentGold(float gold)
        {
            if (Math.Abs(gold - _currentGold) > float.Epsilon)
            {
                _currentGold = gold;
                GoldChanged?.Invoke();
            }
        }

        public void Cleanup()
        {
            _currentGold = 0f;
            GoldChanged = null;
        }
    }
}