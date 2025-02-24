namespace Code.Gameplay.Fighter
{
    public interface IFighterPlacementService
    {
        void RegisterFighter(int towerId, int cellId);
        void UnregisterFighter(int cellId);
    }
}