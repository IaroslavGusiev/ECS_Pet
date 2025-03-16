namespace Code.Gameplay.Fighter
{
    public interface IFighterPlacementService
    {
        void RegisterFighter(int fighterId, int cellId);
        void UnregisterFighter(int cellId);
        bool IsFighterPlaced(int fighterId);
    }
}