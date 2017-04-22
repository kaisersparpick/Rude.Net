using System;

namespace RudeExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rude = new Rude();

            var arg = args.Length == 1 ? args[0] : "static";
            switch (arg)
            {
                case "static":
                    WriteTitle(SimpleStatic.Title);
                    SimpleStatic.AddRulesAndCheck(rude);
                    WriteResult(SimpleStatic.Result, rude.GetPath());
                    break;
                case "dynamic":
                    var example = new DynamicRules(rude, 5);
                    WriteTitle(example.Title);
                    example.CheckConditions();
                    WriteResult(example.Result, rude.GetPath());
                    break;
                default:
                    return;
            }
        }

        private static void WriteTitle(string title)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Rude.Net Example : " + title);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Now checking conditions...");
        }

        private static void WriteResult(string result, string path)
        {
            Console.WriteLine("The result is: " + result);
            Console.WriteLine("The path was: " + path);
        }
    }
}
