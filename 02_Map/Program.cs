using System;
using static _02_Map.F;

namespace _02_Map
{
    class Program
    {
        static void Main(string[] args)
        {
            MainLoop();
        }

        private static string ProcessInput(Option<string> input)
        {
            // 1. Match way
            //return MatchWay(input);

            // 2. Map way
            //return MapWay(input);

            // 3. Map problem
            return MapProblem(input);
        }

        #region MatchWay

        public static string MatchWay(Option<string> value)
        {
            var result = value.Match(
                () => "No value",
                s => AddQuotes(s.Length.ToString()));

            return result;
        }

        #endregion

        
        #region MapWay

        public static string MapWay(Option<string> value)
        {
            var result = value
                .Map(v => v.Length)
                .Map(v => v.ToString())
                .Map(AddQuotes)
                .Match(() => "No value", s => s);

            return result;
        }

        #endregion

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

        public static string AddQuotes(string line)
        {
            return $"\"{line}\"";
        }

        public static Option<int> ParseStringToInt(string line)
        {
            if (int.TryParse(line, out var number))
            {
                return Some(number);
            }
            else
            {
                return None;
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

            } while (true);
        }
        
    }
}
