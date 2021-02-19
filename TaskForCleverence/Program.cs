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

            Parallel.For(0, 50, i =>
            {
                if (i % 2 == 0)
                    MyServer.AddToCount(2);
                else
                    Console.WriteLine("GetCount " + MyServer.GetCount());
            });
            Console.WriteLine("GetCount " + MyServer.GetCount());
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

    }
}
