using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskForCleverence
{
    public static class MeServer
    {
        private static int count;
        private static ReaderWriterLock locker = new ReaderWriterLock();

        public static void AddToCount(int value)
        {
            locker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            count += value;
            locker.ReleaseWriterLock();
        }

        public static int GetCount()
        {
            locker.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            try
            {
                return count;
            }
            finally
            {
                locker.ReleaseReaderLock();
            }
        }
    }

    class Program
    {
        static void Main()
        {

            Console.ReadLine();
        }

    }
}
