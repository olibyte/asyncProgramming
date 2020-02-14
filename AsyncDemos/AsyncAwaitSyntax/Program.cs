using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitSyntax
{
    class Program
    {
        static string url = "https://go.microsoft.com/fwlink/p/?linkid=845299";
        static void Main(string[] args)
        {
            Download();
        }
        static async void Download()
        {
            var downloader = new WebClient();
            byte[] rawdata = await downloader.DownloadDataTaskAsync(url);
            Console.WriteLine(rawdata.Length);
        }
    }
}
