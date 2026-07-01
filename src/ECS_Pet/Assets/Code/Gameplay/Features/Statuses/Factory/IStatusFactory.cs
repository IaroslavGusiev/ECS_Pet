namespace Code.Gameplay.Statuses.Factory
{
    public interface IStatusFactory
    {
        GameEntity CreateStatus(StatusSetup setup, int producerId, int targetId);
    }
}
