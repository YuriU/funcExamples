using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using static _07_Async.F;

namespace _07_Async
{
    public static class FunctionalExtensions
    {
        public static Result<R> Map<T, R>(this Result<T> res, Func<T, R> f) 
            => res.Match(
                l => (Result<R>)l, 
                r => Value(f(r)));

       public static Result<R> Bind<T, R>(this Result<T> res, Func<T, Result<R>> f) 
            => res.Match(
                l => (Result<R>)l, 
                r => f(r));

       public static Result<R> Select<T, R>(this Result<T> res, Func<T, R> f) 
            => res.Map(f);
       public static Result<R> SelectMany<T, R>(this Result<T> res, Func<T, Result<R>> f) 
            => res.Bind(f);

       public static Result<RR> SelectMany<T, R, RR>(this Result<T> res, Func<T, Result<R>> bind, Func<T, R, RR> project)
                => res.Match(
                    Error: (l) => (Result<RR>)l,
                    Value: (r) => bind(r).Match(
                            Error:(l1) => (Result<RR>)l1,
                            Value:(r1) => Value(project(r, r1))));

       public static Task<T> Async<T>(T t)
                => Task.FromResult(t);

        
        

    }
}