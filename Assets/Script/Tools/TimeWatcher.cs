namespace Script.Tools
{
    public class TimeWatcher
    {
        private readonly float _delay;
        public float LeftTime { get; private set; }
        public bool IsSpendTime => LeftTime<=0;
            
        public TimeWatcher(float delay)
        {
            _delay = delay;
            LeftTime = 0;
        }

        public void StartWithDelay(float leftTime)
        {
            LeftTime = leftTime;
        }
        public void ResetTime()
        {
            LeftTime = _delay;
        }

        public void Update(float deltaTime)
        {
            LeftTime -= deltaTime;
        }


        
    }
}