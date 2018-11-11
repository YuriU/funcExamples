using System;
using static _06_ExtendedSyntax.F;
using static System.Linq.Enumerable;

namespace _06_ExtendedSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //EnumerableExample();

            Either<string, int> correct = Right(3);
            MapExample(correct);

            Either<string, int> error = Left("Bad value");
            MapExample(error);


            BindExample();

            
        }

        public static void EnumerableExample()
        {
        var numbers = 
            from x in Range(1, 4)
            select x * 2;

            foreach(var n in numbers)
                Console.WriteLine(n);
        }

        public static void MapExample(Either<string, int> input)
        {
            var mul2 = from num in input
                       select num * 2;
                       
            var output = mul2.Match(
                Left:(l) => l,
                Right:(r) => r.ToString()
            );
            

             Console.WriteLine(output);
        }

        public static void BindExample()
        {
            var first = ReadConsole("Enter first number:");
            var second = ReadConsole("Enter second number:");

            var result =
            from firstStr in first
            from firstNumber in Parse(firstStr)
            from secondStr in second
            from secondNumber in Parse(secondStr)
            from r in SafeSqrtE(firstNumber + secondNumber)
            select r;

            var output = result.Match(
                Left:(l) => l,
                Right:(r) => r.ToString()
            );
            
             Console.WriteLine($"Result is: {output}");
        }

        public static Option<int> SafeSqrt(int number)
        {
            return number < 0 ? (Option<int>)None : Some((int)Math.Sqrt(number));
        }

        public static Either<string, int> Parse(string numberStr)
        {
            if(int.TryParse(numberStr, out var num))
            {
                return Right(num);
            }
            else
            {
                return Left("String cannot be parsed");
            }
        }

        public static Either<string, int> SafeSqrtE(int number)
        {
            return number < 0 ? (Either<string, int>)Left("Value is above zero") : Right((int)Math.Sqrt(number));
        }

        public static Either<string, string> ReadConsole(string label)
        {
            Console.WriteLine(label);

            var line = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(line))
            {
                return Left("Empty result");
            }
            else
            {
                return Right(line);
            }
        }
    }
}
