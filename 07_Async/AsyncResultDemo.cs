using System;
using System.Threading.Tasks;
using static _07_Async.F;

namespace _07_Async
{
    namespace AsyncResultsDemo
    {
        class AsyncResultDemo
        {
            public static async Task Show()
            {
                await ChainedMethodsSequenceDemo();

                //await ExtendedSyntaxDemo1();

                //await ExtendedSyntaxDemo2();
            }

            private static async Task ChainedMethodsSequenceDemo()
            {
                var result = await ReadConsoleAsync("[Chain:] Please enter the number")
                    .Bind(Parse)
                    .Map(x => x * x);

                PrintResult(result);
            }

            private static async Task ExtendedSyntaxDemo1()
            {
                var result = await
                    from str in ReadConsoleAsync("[Extended:] Please enter the number")
                    from number in Parse(str)
                    select number * number;

                PrintResult(result);
            }

            private static async Task ExtendedSyntaxDemo2()
            {
                var result = await from numStr1 in ReadConsoleAsync("[Extended:] Please enter first number")
                                 from num1 in Parse(numStr1)
                                 from numStr2 in ReadConsoleAsync("[Extended:] Please enter secont number")
                                 from num2 in Parse(numStr2)
                                 select num1 * num2;

                PrintResult(result);
            }

            public static async Task<Result<int>> Parse(string str)
            {
                await Task.Delay(1000);

                int val = 0;
                if (int.TryParse(str, out val))
                {
                    return Value(val);
                }
                else
                {
                    return Error("String cannot be parsed to int");
                }
            }

            public static async Task<Result<string>> ReadConsoleAsync(string label)
            {
                Console.WriteLine(label);
                await Task.Delay(1000);

                var str = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(str))
                {
                    return Error("Input value is empty");
                }
                else
                {
                    return Value(str);
                }
            }


            private static void PrintResult<T>(Result<T> result)
            {
                var toPrint = result.Match(
                                Error: e => e.Message,
                                Value: e => e.ToString()
                              );

                Console.WriteLine(toPrint);
            }
        }

        public static class FunctionalExtensionsAsync
        {
            public static Task<Result<T>> Async<T>(T t)
                => Task.FromResult((Result<T>)Value(t));

            public static async Task<Result<R>> Map<T, R>(this Task<Result<T>> task, Func<T, R> f)
            {
                var t = await task;
                return t.Match(
                    Error: (l) => (Result<R>)l,
                    Value: (r) => Value(f(r)));
            }

            public static async Task<Result<R>> Bind<T, R>(this Task<Result<T>> task, Func<T, Task<Result<R>>> f)
            {
                var t = await task;
                var taskToReturn = t.Match(
                    Error: (l) => Task.FromResult((Result<R>)l),
                    Value: async (r) => await f(r)
                );

                return await taskToReturn;
            }

            public static Task<Result<R>> Select<T, R>(this Task<Result<T>> task, Func<T, R> f)
                => task.Map(f);

            public static Task<Result<R>> SelectMany<T, R>(this Task<Result<T>> task, Func<T, Task<Result<R>>> f)
                => task.Bind(f);

            public static async Task<Result<RR>> SelectMany<T, R, RR>(this Task<Result<T>> task, Func<T, Task<Result<R>>> bind, Func<T, R, RR> project)
            {
                var t = await task;

                var taskToWait = t.Match(
                    Error: (l) => Task.FromResult((Result<RR>)l),
                    Value: async (r) => (await bind(r))
                        .Match(
                            Error: (l1) => (Result<RR>)l1,
                            Value: (r1) => Value(project(r, r1))));

                return await taskToWait;
            }
        }
    }
}
