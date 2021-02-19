using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskForCleverence
{
    public static class MyServer
    {
        private static int count;
        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        public static void AddToCount(int value)
        {
            locker.EnterWriteLock();
            Console.WriteLine($"Add {value}");
            count += value;
            locker.ExitWriteLock();
        }

        public static int GetCount()
        {
            locker.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                locker.ExitReadLock();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            new Program().Run();
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private void Run()
        {
            EventHandler h = new EventHandler(MyEventHandler);
            AsyncCaller ac = new AsyncCaller(h);
            if (ac.Invoke(5000, this, EventArgs.Empty))
                Console.WriteLine("Completed successfully");
            else
                Console.WriteLine("Timeout occured");
        }

        private void MyEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Enter Handler");
            Thread.Sleep(6000);
            Console.WriteLine("Exit Handler");
        }
    }
}
