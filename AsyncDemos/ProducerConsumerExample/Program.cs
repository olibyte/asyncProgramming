using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerExample
{
    class Program
    {
        static Random rnd = new Random();
        static BlockingCollection<ulong> numbers = new BlockingCollection<ulong>(10);

        static void Main(string[] args)
        {
            Thread threadFib = new Thread(new ThreadStart(GenerateFib));
            threadFib.IsBackground = false;
            threadFib.Start();

            Thread threadReader = new Thread(new ThreadStart(ReadFib));
            threadReader.IsBackground = false;
            threadReader.Start();
        }

        private static void GenerateFib()
        {
            for (ushort ix = 0; ix < 50; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                Console.WriteLine("Adding next Fib...");
                numbers.Add(Fibonacci(ix));
            }
        }
        private static void ReadFib()
        {
            Thread.Sleep(10000);
            do
            {
                var n = numbers.Take();
                Console.Write("[Fib {0}]", n);
            } while (true);
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
