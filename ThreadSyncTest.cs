using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSyncTest
{
    public static class ThreadSyncTest
    {
        private static int x = 0;

        #region Syncronized

        public static void RunNotSyncedThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => Count());
            }
        }

        public static void Count()
        {
            Console.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}");
            x = 1;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}  :  {x}");
                x++;
                Thread.Sleep(100);
            }
        }

        #endregion

        #region Lock

        public static object  Locker = new object();

        public static void RunSyncLockThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedCount());
            }
        }

        public static void SynchronizedCount()
        {
            Console.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}");
            x = 1;
            lock (Locker)
            {
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} : {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine($"Thread profit: {Thread.CurrentThread.ManagedThreadId} : {j}");
                Thread.Sleep(100);
            }
        }


        #endregion

        #region Monitor

        public static object Mon = new object();

        public static void RunSyncMonitorThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMonitorCount());
            }
        }

        public static void SynchronizedMonitorCount()
        {
            Console.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}");
            
            try
            {
                Monitor.Enter(Mon);
                x = 1;
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} : {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                Monitor.Exit(Mon);
            }
            
            
            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine($"Thread profit: {Thread.CurrentThread.ManagedThreadId} : {j}");
                Thread.Sleep(100);
            }
        }

        #endregion

        #region Mutex

        public static Mutex mutex = new Mutex();

        public static void RunSyncMutexThreads()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => SynchronizedMutexCount());
            }
        }

        public static void SynchronizedMutexCount()
        {
            Console.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}");

            mutex.WaitOne();

                x = 1;
                for (int i = 0; i < 9; i++)
                {
                    Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} : {x}");
                    x++;
                    Thread.Sleep(100);
                }

           mutex.ReleaseMutex();

            for (int j = 9; j >= 0; j--)
            {
                Console.WriteLine($"Thread profit: {Thread.CurrentThread.ManagedThreadId} : {j}");
                Thread.Sleep(100);
            }
        }


        #endregion

        #region Semaphore

        public static Semaphore semaphore = new Semaphore(0 , 4); // 0 - no threads in init,  4 - max count of threads

        public static void RunSyncSemaphoreThreads()
        {
            Task.Run(() => ReadFile()); // run anonymus func and execure method ReadFile()

            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => ProcessFile());
            }
        }

        public static void ReadFile() // ReadFile emulation
        {
            Console.WriteLine($"File read STARTED: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
            Console.WriteLine($"File read ENDED: {Thread.CurrentThread.ManagedThreadId}");
            semaphore.Release(3); //Allows do this work in 3 of 4 threads
        }

        public static void ProcessFile()
        {
            Console.WriteLine($"Waiting for data: {Thread.CurrentThread.ManagedThreadId}");

            semaphore.WaitOne();

            Console.WriteLine($"File processing started: {Thread.CurrentThread.ManagedThreadId}");

            x = 1;
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} : {x}");
                x++;
                Thread.Sleep(100);
            }

            Console.WriteLine($"File processing ended: {Thread.CurrentThread.ManagedThreadId}");
        }

        #endregion
    }
}
