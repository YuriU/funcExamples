using System;
using static _04_ErrorHandling.F;

namespace _04_ErrorHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Print example
            PrintExample();

            // 2. Parse example
            MainLoop();
        }

        private static void PrintExample()
        {
            Either<string, int> numRes = Right(3);

            Either<string, string> error = Left("Unable to parse string");

            Print(numRes);

            Print(error);
        }

        public static void Print<L, R>(Either<L, R> either)
        {
            var nameToPrint = either.Match(
                left => left.ToString(), 
                right => right.ToString());

            Console.WriteLine(nameToPrint);
        }

        private static string ProcessInput(Either<string, string> input)
        {
            var res = input
            .Bind(ParseStringToInt)
            .Bind(GetSqrt)
            .Match(
                l => l,
                r => r.ToString());
                
            return res;
        }

        public static Either<string, int> ParseStringToInt(string line)
        {
            if (int.TryParse(line, out var number))
            {
                return Right(number);
            }
            else
            {
                return Left("The string you entered is not a number");
            }
        }

        public static Either<string, double> GetSqrt(int number)
        {
            if(number < 0)
            {
                return Left("The number is above zero");
            }
            else
            {
                return Right(Math.Sqrt(number));
            }
        }

        private static void MainLoop()
        {
            do
            {
                Console.WriteLine("Enter some value:");
                var line = Console.ReadLine();

                Either<string, string> input = !string.IsNullOrWhiteSpace(line) 
                            ? (Either<string, string>)Right(line) 
                            : Left("The string is empty");

                var output = ProcessInput(input);

                Console.WriteLine(output);

            } 
            while (true);
        }
    }
}
