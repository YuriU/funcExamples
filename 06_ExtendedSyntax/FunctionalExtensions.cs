using System;
using System.Collections.Generic;
using System.Text;
using static _06_ExtendedSyntax.F;

namespace _06_ExtendedSyntax
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

/*
        public static Option<RR> SelectMany<T, R, RR>(this Option<T> opt, Func<T, Option<R>> bind, Func<T, R, RR> project)
            => opt.Match(
                    () => None,
                    (t) => bind(t).Match(
                            () => None,
                            (r) => Some(project(t, r))));
 */
        public static Either<L, Res> Map<L, R, Res>(this Either<L, R> eithLR, Func<R, Res> f) 
            => eithLR.Match(
                l => (Either<L, Res>)Left(l), 
                r => (Either<L, Res>)Right(f(r)));

       public static Either<L, Res> Bind<L, R, Res>(this Either<L, R> eithLR, Func<R, Either<L, Res>> f) 
            => eithLR.Match(
                l => Left(l), 
                r => f(r));
        public static Either<L, Res> Select<L, R, Res>(this Either<L, R> eithLR, Func<R, Res> f) 
            => eithLR.Map(f);
       public static Either<L, Res> SelectMany<L, R, Res>(this Either<L, R> eithLR, Func<R, Either<L, Res>> f) 
            => eithLR.Bind(f);

       public static Either<L, Res> SelectMany<L, R, RR, Res>(this Either<L, R> eithLR, Func<R, Either<L, RR>> bind, Func<R, RR, Res> project)
                => eithLR.Match(
                    Left: (l) => (Either<L, Res>)Left(l),
                    Right:(r) => bind(r).Match(
                            Left:(l1) => (Either<L, Res>)Left(l1),
                            Right:(r1) => Right(project(r, r1))));
    }
}