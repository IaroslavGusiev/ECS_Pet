using System;

namespace Code.Gameplay.UI.Gold
{
    public interface IStorageUIService
    {
        event Action GoldChanged;
        float CurrentGold { get; }
        void UpdateCurrentGold(float gold);
    }
}
