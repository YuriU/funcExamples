using System;
using System.Threading.Tasks;

namespace _08_RealAppExample
{

    // Result<T> = Error | Value(R)
    public struct Result<T>
    {
        private readonly bool isValue;

        private readonly Results.Error error;

        private readonly T value;

        private Result(bool isValue, Results.Error error, T value)
        {
            this.isValue = isValue;
            this.error = error;
            this.value = value;
        }

        public static implicit operator Result<T>(Results.Error error)
            => new Result<T>(false, error, default(T));

        public static implicit operator Result<T>(Results.Value<T> value)
            => new Result<T>(true, default(Results.Error), value.value);

        public Res Match<Res>(Func<Results.Error, Res> Error, Func<T, Res> Value) 
            => isValue ? Value(value) : Error(error);
    }

    public static partial class F
    {
        public static Results.Error Error(string message) => new Results.Error(message);

        public static Results.Value<T> Value<T>(T value) => new Results.Value<T>(value);

        public static Task<Result<T>> AsyncResult<T>(Result<T> result)
            => Task.FromResult(result);

        public static Task<Result<T>> AsyncValue<T>(T t)
            => Task.FromResult((Result<T>)Value(t));

        public static Result<Unit> OK()
            => (Result<Unit>)Value(new Unit());

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

    namespace Results 
    {
        public struct Error
        {
            internal string Message { get; }

            internal Error(string message)
            {
                if(message == null)
                    throw new ArgumentNullException();

                Message = message;
            }
        }
        public struct Value<T>
        {
            internal T value { get; }

            internal Value(T value)
            {
                if(value == null)
                    throw new ArgumentNullException();

                this.value = value;
            }           
        }   
    }
}
