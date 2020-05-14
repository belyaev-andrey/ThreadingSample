using System;
using System.Threading;

namespace ThreadingSample
{
    public class CounterContainer
    {
        private readonly object _counterLock = new object();
        public int Counter { get; private set; } = 0;

        public void Increase()
        {
            try
            {
                Monitor.Enter(_counterLock);
                Counter++;
            }
            finally
            {
                Monitor.Exit(_counterLock);
            }
        }
        
    }
    
    public class CounterIncreaser
    {
        private CounterContainer _container;

        public CounterIncreaser(CounterContainer container)
        {
            _container = container;
        }

        public void IncreaseCounter()
        {
            for (int i = 0; i < 1000000; i++)
            {
                _container.Increase();
            }
        }
    }

    public class CounterDemo
    {
        public static void Demo()
        {
            CounterContainer c = new CounterContainer();
            CounterIncreaser c1 = new CounterIncreaser(c);
            CounterIncreaser c2 = new CounterIncreaser(c);
            
            Thread t1 = new Thread(c1.IncreaseCounter);
            Thread t2 = new Thread(c2.IncreaseCounter);
            t1.Start();
            t2.Start();
            
            t1.Join();
            t2.Join();
            
            Console.WriteLine(c.Counter);
        }
    }
    
}