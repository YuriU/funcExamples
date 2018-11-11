using System;
using System.Threading.Tasks;
using static _07_Async.F;

namespace _07_Async
{
    class Program
    {
        static void Main(string[] args)
            => MainAsync(args).Wait();

        public static async Task MainAsync(string[] args)
        {
              var val = await GetString()
                        .Bind(Parse)
                        .Map(r => r * 3)
                        .Map(s => s.ToString());

            Console.WriteLine("Method sequence result: " + val);

            var resFluent = await
                from s in GetString()
                from n in Parse(s)
                from m3 in FunctionalExtensions.Async(n * 3)
                select m3.ToString();

            Console.WriteLine("Fluent result: " + resFluent);
        }

        public static async Task<string> GetString()
        {
            await Task.Delay(1000);
            return "3";
        }

        public static async Task<int> Parse(string str)
        {
            await Task.Delay(1000);
            
            int val = 0;
            int.TryParse(str, out val);
            return val;
        }
    }
}
