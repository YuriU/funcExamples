using System;
using static _03_Bind.F;

namespace _03_Bind
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

    }
}