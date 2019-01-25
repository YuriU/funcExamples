using System;
using static _05_MonadsLaws.F;

namespace _05_MonadsLaws
{
    class Program
    {
        static void Main(string[] args)
        {          
            // 1. m == m.Bind(Return)
            RightIdentity(Some(3));

            // 2. Return(t).Bind(f) == f(t)
            LeftIdentity(3);

            // 3. (a + b) + c == a + (b + c)
            Associativity(Some("5"));


            // And some more ...
            // 4. Multi argument
            MultiArgumentsExample();
        }

        // m == m.Bind(Return)
        public static void RightIdentity(Option<int> opt)
        {
            var areEqual = (opt == opt.Bind(Option<int>.Return));

            Console.WriteLine($"Right identity equality : {areEqual}");
        }

        // Return(t).Bind(f) == f(t)
        public static void LeftIdentity(int number)
        {
            var areEqual = (
                Option<int>.Return(number).Bind(MultipleBy2) == MultipleBy2(number)
                );
            
            Console.WriteLine($"Left identity equality : {areEqual}");
        }
        public static Option<int> MultipleBy2(int number)
        {
            return Some(number * 2);
        }

        // m.Bind(f).Bind(g) == m.Bind(x => f(x).Bind(g))
        public static void Associativity(Option<string> str)
        {
            var first = str
                    .Bind(Parse)
                    .Bind(SafeSqrt);

            var second = str
                        .Bind(x => Parse(x)
                                    .Bind(y => SafeSqrt(y)));

            var areEqual = first == second;

            Console.WriteLine($"Associativity equality : {areEqual}");
        }

        public static Option<double> Parse(string str)
        {
            if(Double.TryParse(str, out var number))
            {
                return Some(number);
            }
            else
            {
                return None;
            }
        }

        public static Option<double> SafeSqrt(double number)
        {
            return number < 0 ? (Option<double>)None : Some(Math.Sqrt(number));
        }

        
        public static void MultiArgumentsExample()
        {
            Option<double> result = MultiplicationWithBind("3", "8");

            var output = result.Match(
                                    () => "Value cannot be parsed",
                                    val => val.ToString()
            );

            Console.WriteLine(output);
        }

        public static Option<double> multiply(double x, double y)
                => (Option<double>)Some(x * y);

        static Option<double> MultiplicationWithBind(string strX, string strY)
                => Parse(strX)
                    .Bind(x => Parse(strY)
                                  .Bind(y => multiply(x, y)));
    }
}
