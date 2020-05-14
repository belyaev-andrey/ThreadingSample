using System;
using System.Threading;

namespace ThreadingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoader loader = new DataLoader();
            NetworkTransfer transfer = new NetworkTransfer();
            Thread l = new Thread(loader.LoadData);
            Thread t = new Thread(transfer.TransferData);
            l.Start();
            t.Start();
        }
    }

    class DataLoader
    {
        public void LoadData()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Loading data: {0}%", i);
                Thread.Sleep(100);
            }
            Console.WriteLine("Loading data: Finished");
        }
    }

    class NetworkTransfer
    {
        public void TransferData()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Transferring data: {0}%", i);
                Thread.Sleep(100);
            }
            Console.WriteLine("Transferring data: Finished");
        }
    }
    
}