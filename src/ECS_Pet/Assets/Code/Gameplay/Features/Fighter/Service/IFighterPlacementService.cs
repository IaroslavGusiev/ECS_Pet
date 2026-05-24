namespace Code.Gameplay.Fighter
{
    public interface IFighterPlacementService
    {
        void RegisterFighter(int fighterId, int cellId);
        bool IsFighterPlaced(int fighterId);
    }
}