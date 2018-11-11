using System;
using System.Collections.Generic;
using System.Text;
using static _04_ErrorHandling.F;

namespace _04_ErrorHandling
{
    public static class FunctionalExtensions
    {
        public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f) 
            => optT.Match(
                () => (Option<R>)None, 
                (t) => Some(f(t)));

        public static Option<R> Bind<T, R>(this Option<T> optT, Func<T, Option<R>> f) 
            => optT.Match(
                () => (Option<R>)None, 
                (t) => f(t));

        public static Either<L, Res> Map<L, R, Res>(this Either<L, R> eithLR, Func<R, Res> f) 
            => eithLR.Match(
                l => (Either<L, Res>)Left(l), 
                r => (Either<L, Res>)Right(f(r)));

        public static Either<L, Res> Bind<L, R, Res>(this Either<L, R> eithLR, Func<R, Either<L, Res>> f) 
            => eithLR.Match(
                l => Left(l), 
                r => f(r));
    }
}