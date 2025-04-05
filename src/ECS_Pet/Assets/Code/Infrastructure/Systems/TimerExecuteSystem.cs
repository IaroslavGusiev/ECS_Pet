using Entitas;
using Code.Gameplay.Common.Time;

namespace Code.Infrastructure.Systems
{
    public abstract class TimerExecuteSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        
        private readonly float _executeIntervalSeconds;
        private float _timeToExecute;

        protected TimerExecuteSystem(float executeIntervalSeconds, ITimeService timeService)
        {
            _executeIntervalSeconds = executeIntervalSeconds;
            _timeService = timeService;
        }

        protected abstract void Execute();

        void IExecuteSystem.Execute()
        {
            _timeToExecute -= _timeService.DeltaTime;
            
            if (_timeToExecute > 0)
            {
                return;
            }
      
            _timeToExecute = _executeIntervalSeconds;
            Execute();
        }
    }
}