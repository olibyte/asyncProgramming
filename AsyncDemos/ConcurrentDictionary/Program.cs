using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictionaryTest
{
    class Program
    {
        static ConcurrentDictionary<int, string> cd = new ConcurrentDictionary<int, string>();
        static void Main()
        {
            //if (cd.TryAdd(1, "one"))
            //{
            //    Console.WriteLine("KVP 1:one Was Added!");
            //}

            //string val = cd.GetOrAdd(1, "ONE");
            //Console.WriteLine("Existing 1: {0}", val);

            //string val2 = cd.AddOrUpdate(1, "_ONE_",
            //    (int existingKey, string existingValue) => 
            //    {
            //        return existingValue.ToUpper();
            //    });
            //Console.WriteLine("Existing 1: {0}", val2);

            //if (cd.TryGetValue(1, out string val3))
            //{
            //    Console.WriteLine("Existing value: ", val3);
            //}

            //cd.GetOrAdd()

            string filename = @"d:\source\sp.txt";
            string[] lines = File.ReadAllLines(filename);

            Parallel.ForEach<string>(lines,
                (string line) =>
                {
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (string.IsNullOrWhiteSpace(word)) continue;

                        string canonicalWord = word.Trim(' ', ',', '.', '?', '!', ';','-', ':', '\'', '\"').ToLowerInvariant();

                        wordCount.AddOrUpdate(canonicalWord, 1, (k, currentCount) => { return currentCount + 1; });
                    }
                });

        }
        static ConcurrentDictionary<string, uint> wordCount = new ConcurrentDictionary<string, uint>();
    }
}
