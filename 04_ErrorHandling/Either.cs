using System;

namespace _04_ErrorHandling
{
    // Either<L, R> = Left(L) | Right(R)
    public struct Either<L, R>
    {
        private readonly bool isRight;

        private readonly L left;
        private readonly R right;

        private Either(bool isRight, L left, R right)
        {
            this.isRight = isRight;
            this.left = left;
            this.right = right;
        }

        public static implicit operator Either<L, R>(Eithers.Left<L> left)
            => new Either<L, R>(false, left.Value, default(R));

        public static implicit operator Either<L, R>(Eithers.Right<R> right)
            => new Either<L, R>(true, default(L), right.Value);

        public Res Match<Res>(Func<L, Res> Left, Func<R, Res> Right) 
            => isRight ? Right(right) : Left(left);
    }

    public static partial class F
    {
        public static Eithers.Left<T> Left<T>(T value) => new Eithers.Left<T>(value);

        public static Eithers.Right<T> Right<T>(T value) => new Eithers.Right<T>(value);
    }

    // Sorry, I know the namespace is not grammatically correct
    // I just needed to tell it from Either class name
    namespace Eithers 
    {
        public struct Left<T>
        {
            internal T Value { get; }

            internal Left(T value)
            {
                if(value == null)
                    throw new ArgumentNullException();

                Value = value;
            }
        }
        public struct Right<T>
        {
            internal T Value { get; }

            internal Right(T value)
            {
                if(value == null)
                    throw new ArgumentNullException();

                Value = value;
            }           
        }   
    }
}
