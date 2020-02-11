using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main()
        {
            Task t1 = new Task(() => { Console.WriteLine("Task One"); });
            Task t2 = t1.ContinueWith((tx) => { Console.WriteLine("Task Continued"); });
            t1.Start();

            Task.WaitAll(t1, t2);

            //TaskFactory tf = new TaskFactory();
            //tf.StartNew()
        }
    }
}
