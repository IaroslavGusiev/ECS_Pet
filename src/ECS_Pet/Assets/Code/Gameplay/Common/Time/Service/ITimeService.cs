namespace Code.Gameplay.Common.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        void StopTime();
        void StartTime();
    }
}