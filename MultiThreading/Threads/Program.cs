using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting in the Main Method");
            var t1 = new Thread(new ThreadStart(AddToList));
            t1.Start();

            var t2 = new Thread(new ThreadStart(AddToList));
            t2.Start();

            var t3 = new Thread(new ThreadStart(AddToList));
            t3.Start();

            Console.WriteLine("Ending of the Main Method");
        }

        private static object myLock = new object();
        private static List<int> numbers = new List<int>();
        private static void AddToList()
        {
            for (int ix = 0; ix < 100; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                lock (myLock)
                {
                    numbers.Add(ix);
                }
            }
        }

        private static Random rnd = new Random();
        private static int total = 0;

        //private static void DoWork()
        //{
        //    Thread.Sleep(rnd.Next(1, 1500));
        //    int myTotal = total;
        //    Thread.Sleep(rnd.Next(1, 1500));
        //    total = myTotal + 1;
        //    Console.WriteLine("Managed Thread ID: {0} says {1}", Thread.CurrentThread.ManagedThreadId, total);
        //}
    }
}
