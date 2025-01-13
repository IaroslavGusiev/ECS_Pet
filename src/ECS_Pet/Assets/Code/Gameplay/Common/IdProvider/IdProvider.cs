namespace Code.Gameplay.Common
{
    public class IdProvider : IIdProvider
    {
        private int _lastId = 1;
    
        public int NextId() => 
            ++_lastId;
    }
}