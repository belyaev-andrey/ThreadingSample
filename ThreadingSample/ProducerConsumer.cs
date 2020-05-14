using System;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace ThreadingSample
{

    public class ProducerConsumerDemo
    {
        public static void Demo()
        {
            Store store = new Store();
            Producer p = new Producer(store);
            Consumer c = new Consumer(store);
            
            Thread pThread = new Thread(p.Produce);
            pThread.Name = "Producer 1";
            Thread cThread = new Thread(c.Consume);
            cThread.Name = "Consumer 1";
            
            pThread.Start();
            cThread.Start();
        }
    }
    public class Store
    {
        private int _data;
        private bool _available = false;
        private readonly object _storeLock = new object();
        
        public void Put(int data)
        {
            Monitor.Enter(_storeLock);
            if (_available)
            {
                Monitor.Wait(_storeLock);
            }
            _data = data;
            Console.WriteLine("Put {0} by thread {1}", data, Thread.CurrentThread.Name);
            _available = true;
            Monitor.PulseAll(_storeLock);
            Monitor.Exit(_storeLock);
        }

        public int Get()
        {
            Monitor.Enter(_storeLock);
            if (!_available)
            {
                Monitor.Wait(_storeLock);
            }
            int i = _data;
            _available = false;
            Console.WriteLine("Get {0} by {1}", _data, Thread.CurrentThread.Name);
            Monitor.PulseAll(_storeLock);
            Monitor.Exit(_storeLock);
            return i;
        }
        
    }

    public class Producer
    {
        private Store _store;

        public Producer(Store store)
        {
            _store = store;
        }

        public void Produce()
        {
            for (int i = 0; i < 10; i++)
            {
                _store.Put(i);
            }
        }
    }

    public class Consumer
    {
        private Store _store;

        public Consumer(Store store)
        {
            _store = store;
        }

        public void Consume()
        {
            for (int i = 0; i < 10; i++)
            {
                _store.Get();
            }
        }
    }
}