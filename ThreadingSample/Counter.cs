namespace ThreadingSample
{
    class CounterContainer
    {
        private readonly object _counterLock = new object();
        private int _counter = 0;

        public int Counter => _counter;

        public void Increase()
        {
            lock (_counterLock)
            {
                _counter++;
            }
        }
    }
}