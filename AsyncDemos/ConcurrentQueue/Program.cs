using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace ConcurrentQueueDemo
{
    class Program
    {
        static Random rnd = new Random();
        static ConcurrentQueue<ulong> cq = new ConcurrentQueue<ulong>();

        static void Main(string[] args)
        {
            Thread threadFib = new Thread(new ThreadStart(GenerateFib));
            threadFib.IsBackground = false;
            threadFib.Start();

            Thread threadReader = new Thread(new ThreadStart(ReadFib));
            threadReader.IsBackground = false;
            threadReader.Start();
        }

        private static void ReadFib()
        {
            Console.WriteLine("Starting to read from the queue...");

            do
            {
                if (cq.TryDequeue(out ulong n))
                {
                    Console.Write("[Fib {0}]", n);
                }
                else
                {
                    //Console.Write(".");
                }
                Thread.Sleep(10);
            } while (true);
        }

        private static void GenerateFib()
        {
            for (ushort ix = 0; ix < 50; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                cq.Enqueue(Fibonacci(ix));
            }
        }

        private static ulong Fibonacci(ushort n)
        {
            return (n == 0) ? 0 : Fib(n);
        }
        private static ulong Fib(ushort n)
        {
            ulong a = 0;
            ulong b = 1;
            for (uint ix = 0; ix < n - 1; ix++)
            {
                ulong next = checked(a + b);
                a = b;
                b = next;
            }
            return b;
        }
    }
}
