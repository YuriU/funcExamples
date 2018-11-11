using System;
using System.Collections.Generic;
using System.Text;
using static _02_Map.F;

namespace _02_Map
{
    public static class FunctionalExtensions
    {
        public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f) 
            => optT.Match(
                () => (Option<R>)None, 
                (t) => Some(f(t)));
    }
}