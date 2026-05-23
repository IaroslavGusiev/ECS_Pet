using TMPro;
using Zenject;
using UnityEngine;
using Code.UI.BaseWindow;

namespace Code.Gameplay.UI.Gold
{
    public class GoldBadgeWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI coinCountText;

        private IStorageUIService _storageUIService;

        [Inject]
        public void Construct(IStorageUIService storageUIService) =>
            _storageUIService = storageUIService;
        
        public override void SetupOnInstantiate()
        {
            _storageUIService.GoldChanged += RefreshGold;
            RefreshGold();
        }

        private void OnDestroy()
        {
            if (_storageUIService != null)
            {
                _storageUIService.GoldChanged -= RefreshGold;
            }
        }

        private void RefreshGold() => 
            coinCountText.text = _storageUIService.CurrentGold.ToString("0");
    }
}
