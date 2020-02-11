using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int ix = 0; ix < 100; ix++)
            //{
            //    Console.Write(ix);
            //}

            //Parallel.For(0, 100, (ix) => { Console.Write(ix); });

            var numbers = new List<int>();
            for (int ix = 0; ix < 100; ix++)
            {
                numbers.Add(ix);
            }
            Parallel.ForEach<int>(numbers, (ix) => { });
        }
    }
}
