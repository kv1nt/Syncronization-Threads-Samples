using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadSyncTest.RunNotSyncedThreads();      // Not syncronized logic
            //ThreadSyncTest.RunSyncLockThreads();       // Use locks
            //ThreadSyncTest.RunSyncMonitorThreads();    // Use Monitor
            //ThreadSyncTest.RunSyncMutexThreads();      // Use Mutex
            ThreadSyncTest.RunSyncSemaphoreThreads();    // Use Semaphore 

            //bool existed;
            //string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString(); //create guid
            //Mutex mutexObj = new Mutex(true, guid, out existed);

            //if (existed)
            //{
            //    Console.WriteLine("APPLICATION IS WORKING");
            //    Console.WriteLine($"Guid:{guid}");
            //}
            //else
            //{
            //    Console.WriteLine("APPLICATION IS ALREADY RUNNING. Shoutdown in 3 seconds");
            //    Thread.Sleep(3000);
            //    return;
            //}

            Console.ReadKey();

        }

        private static void CancelTest()
        {
            CancellationTokenSource source = new CancellationTokenSource(); // This Class allows create tokens for cancelling some Threads
            CancellationToken token = source.Token;
            source.Cancel(true);

        }
    }
}
