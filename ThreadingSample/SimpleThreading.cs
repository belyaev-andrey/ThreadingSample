using System;
using System.Threading;

namespace ThreadingSample
{
    class SimpleThreading
    {
        static void Main(string[] args)
        {
            //doSimpleThreading();
            //CounterDemo.Demo();
            //DeadlockDemo.Demo();
            //ProducerConsumerDemo.Demo();
        }

        private static void doSimpleThreading()
        {
            FileUploader uploader1 = new FileUploader(1);
            FileUploader uploader2 = new FileUploader(2);
            Thread uploadThread1 = new Thread(uploader1.uploadFile);
            Thread uploadThread2 = new Thread(uploader2.uploadFile);
            uploadThread1.Start();
            uploadThread2.Start();
        }
    }

    class FileUploader
    {
        private int _timeout;
        public FileUploader(int timeout)
        {
            _timeout = timeout;
        }

        public void uploadFile()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Uploading file with timeout {1}: {0}", i, _timeout);
                Thread.Sleep(_timeout);
            }
        }
    }
    
}