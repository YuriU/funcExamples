using System;
using static _03_Bind.F;

namespace _03_Bind
{
    class Program
    {
        static void Main(string[] args)
        {
            MainLoop();
        }

        private static string ProcessInput(Option<string> input)
        {

            // 1. Map problem
            //return MapProblem(input);

            // 2. Bind way
            return BindWay(input);
        }

        #region MapProblem

        public static string MapProblem(Option<string> value)
        {
            var result = value
                .Map(ParseStringToInt)
                .Match(() => "No value", 
                    s => s.Match(
                            () => "Not parsed", 
                            r => r.ToString())
                );

            return result;
        }

        #endregion

        #region BindWay

        public static string BindWay(Option<string> value)
        {
            var result = value
                .Bind(ParseStringToInt)
                .Bind(GetSqrt)
                .Match(
                    () => "Invalid value", 
                    s => s.ToString()
                );

            return result;
        }

        #endregion

        public static Option<int> ParseStringToInt(string line)
        {
            if (int.TryParse(line, out var number))
            {
                return Some(number);
            }
            else
            {
                return F.None;
            }
        }

        public static Option<double> GetSqrt(int number)
        {
            if(number < 0)
            {
                return None;
            }
            else
            {
                return Some(Math.Sqrt(number));
            }
        }

        private static void MainLoop()
        {
            do
            {
                Console.WriteLine("Enter some value:");
                var line = Console.ReadLine();

                Option<string> input = !string.IsNullOrWhiteSpace(line) 
                            ? Some<string>(line) : 
                            (Option<string>)None;

                var output = ProcessInput(input);

                Console.WriteLine(output);

            } 
            while (true);
        }
    }
}
