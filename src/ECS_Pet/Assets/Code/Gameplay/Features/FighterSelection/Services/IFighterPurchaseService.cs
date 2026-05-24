using Code.Gameplay.Fighter;

namespace Code.Gameplay.FighterSelection
{
    public interface IFighterPurchaseService
    {
        bool CanPurchase(FighterConfig fighterConfig);
        bool TryPurchase(FighterConfig fighterConfig);
    }
}
