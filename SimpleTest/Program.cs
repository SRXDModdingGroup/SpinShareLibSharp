using System;
using SpinShareLib;
using System.Threading.Tasks;

namespace SimpleTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var ssapi = new SSAPI();
            Task.Run(async () => {
                var thing = await ssapi.ping();
                Console.WriteLine(thing.status);
            }).GetAwaiter().GetResult();
        }
    }
}