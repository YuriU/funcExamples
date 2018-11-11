using System;

namespace _04_ErrorHandling
{
    // type Option t = None | Some t
    public struct Option<T>
    {
        private readonly bool isSome;
        private readonly T value;

        private Option(T value)
        {
            this.isSome = true;
            this.value = value;
        }

        public static implicit operator Option<T>(Options.None _)
            => new Option<T>();

        public static implicit operator Option<T>(Options.Some<T> some)
            => new Option<T>(some.Value);

        public R Match<R>(Func<R> None, Func<T, R> Some) 
            => isSome ? Some(value) : None();
    }

    public static partial class F
    {
        public static Options.None None => Options.None.Default;

        public static Options.Some<T> Some<T>(T value) => new Options.Some<T>(value);
    }

    namespace Options
    {
        public struct None
        {
            internal static readonly None Default = new None();
        }

        public struct Some<T>
        {
            internal T Value { get; }

            internal Some(T value)
            {
                if(value == null)
                    throw new ArgumentNullException();

                Value = value;
            }           
        }   
    }
}
