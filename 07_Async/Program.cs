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
                        .Bind(ParseOrZero)
                        .Map(r => r * 3)
                        .Map(s => s.ToString());

            Console.WriteLine("Method sequence result: " + val);

            var resFluent = await
                        from s in GetString()
                        from n in ParseOrZero(s)
                        from m3 in FunctionalExtensions.Async(n * 3)
                        select m3.ToString();

            Console.WriteLine("Fluent result: " + resFluent);


            //var res = await 
            //            from firstStr in ReadConsoleAsync("Enter first number:")
            //            from secondStr in ReadConsoleAsync("Enter second number:")
            //            //from firstInt in Parse(firstStr)
        }

        public static async Task<string> GetString()
        {
            await Task.Delay(1000);
            return "3";
        }

        public static async Task<int> ParseOrZero(string str)
        {
            await Task.Delay(1000);
            
            int val = 0;
            int.TryParse(str, out val);
            return val;
        }

        public static async Task<int> Parse(string str)
        {
            await Task.Delay(1000);
            
            int val = 0;
            int.TryParse(str, out val);
            return val;
        }

        public static async Task<Result<string>> ReadConsoleAsync(string label)
        {
            Console.WriteLine(label);
            await Task.Delay(1000);

            var str = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(str))
            {
                return Error("Input value is empty");
            }
            else
            {
                return Value(str);
            }
        }
    }
}
