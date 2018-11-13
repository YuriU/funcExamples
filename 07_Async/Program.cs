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
            await TasksDemo.TaskDemo.Show();

            //await AsyncResultsDemo.AsyncResultDemo.Show();
        }

    }
}
