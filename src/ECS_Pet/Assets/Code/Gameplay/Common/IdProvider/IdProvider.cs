namespace Code.Gameplay.Common.Time
{
    public class IdProvider : IIdProvider
    {
        private int _lastId = 1;
    
        public int NextId() => 
            ++_lastId;
    }
}