using System;
using System.Threading;

namespace ThreadingSample
{

    public class DeadlockDemo
    {
        public static void Demo()
        {
            ResourceHolder res = new ResourceHolder();
            Thread t1 = new Thread(new Locker1(res).DoLock1);
            Thread t2 = new Thread(new Locker2(res).DoLock2);
            t1.Start();
            t2.Start();
        }
    }
    public class ResourceHolder
    {
        public object A = new object();
        public object B = new object();
    }

    public class Locker1
    {
        public ResourceHolder res;

        public Locker1(ResourceHolder res)
        {
            this.res = res;
        }

        public void DoLock1()
        {
            lock (res.A)
            {
                Console.WriteLine("Resource A is captured by One, commander!");
                Thread.Sleep(100);
                lock (res.B)
                {
                    Console.WriteLine("Resource B is captured by One, commander!");
                }
            }
        }
    }
    
    public class Locker2
    {
        public ResourceHolder res;

        public Locker2(ResourceHolder res)
        {
            this.res = res;
        }

        public void DoLock2()
        {
            lock (res.B)
            {
                Console.WriteLine("Resource B is captured by Two, commander!");
                lock (res.A)
                {
                    Console.WriteLine("Resource A is captured by Two, commander!");
                }
            }
        }
    }
    
}