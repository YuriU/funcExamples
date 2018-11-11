using System;

namespace _07_Async
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
    }

    // Sorry, I know the namespace is not grammatically correct
    // I just needed to tell it from Either class name
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
