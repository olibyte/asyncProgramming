using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyPools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Current Thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            ThreadPool.QueueUserWorkItem(new WaitCallback(DoWork));

            Console.WriteLine("Is Backgound Thread? {0} ", Thread.CurrentThread.IsBackground);

            mre.WaitOne();
        }

        private static ManualResetEvent mre = new ManualResetEvent(false);

        private static void DoWork(object state)
        {
            Console.WriteLine("Current Thread: {0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Is Backgound Thread? {0} ", Thread.CurrentThread.IsBackground);

            mre.Set();
        }
    }
}
