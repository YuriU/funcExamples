using System;
using System.Threading.Tasks;

namespace _07_Async
{
    namespace TasksDemo
    {
        public static class TaskDemo
        {
            public static async Task Show()
            {
                var val = await ReadConsoleAsync("[CallsSequence]: Enter the number please")
                    .Bind(ParseOrZero)
                    .Map(r => r * 3)
                    .Map(s => s.ToString());

                Console.WriteLine("Method sequence result: " + val);

                var resFluent = await
                    from s in ReadConsoleAsync("[Extended]: Enter the number please")
                    from n in ParseOrZero(s)
                    from m3 in FunctionalExtensions.Async(n * 3)
                    select m3.ToString();

                Console.WriteLine("Fluent result: " + resFluent);
            }

            public static async Task<int> ParseOrZero(string str)
            {
                await Task.Delay(1000);

                int val = 0;
                int.TryParse(str, out val);
                return val;
            }

            public static async Task<string> ReadConsoleAsync(string label)
            {
                Console.WriteLine(label);
                await Task.Delay(1000);

                return Console.ReadLine();
            }
        }

        public static class FunctionalExtensionsTasks
        {
            public static Task<T> Async<T>(T t)
                => Task.FromResult(t);

            public static async Task<R> Map<T, R>(this Task<T> task, Func<T, R> f)
                => f(await task);

            public static async Task<R> Bind<T, R>(this Task<T> task, Func<T, Task<R>> f)
                => await f(await task);


            public static Task<R> Select<T, R>(this Task<T> task, Func<T, R> f)
                => task.Map(f);

            public static Task<R> SelectMany<T, R>(this Task<T> task, Func<T, Task<R>> f)
                     => task.Bind(f);

            public static async Task<RR> SelectMany<T, R, RR>(this Task<T> task, Func<T, Task<R>> bind, Func<T, R, RR> project)
            {
                var t = await task;
                var r = await bind(t);
                return project(t, r);
            }
        }
    }
}
