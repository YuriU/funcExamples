using System;
using static _01_Options.F;

namespace _01_Options
{
    class Program
    {
        static void Main(string[] args)
        {
            Option<string> firstName = Some("John");

            Option<string> lastName = None;

            Print(firstName);

            Print(lastName);

            Console.Read();
        }

        public static void Print(Option<string> value)
        {
            var nameToPrint = value.Match(
                () => "No value provided", 
                s => s);

            Console.WriteLine(nameToPrint);
        }
    }
}
