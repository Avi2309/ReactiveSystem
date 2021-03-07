using System;
using System.IO;

namespace ReactiveSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Reactive system !");
            string fileName = @$"{args[0]}";
            string initialSet = File.ReadAllText(fileName);
            var reactiveEngine = new ReactiveEngine(initialSet);

            while (true)
            {
                Console.WriteLine("\nMenu:\n" +
                    "a. Print current state\n" +
                    "b. Change a value");
                var option = Console.ReadLine();
                if (option.Equals("a"))
                {
                    Console.WriteLine(reactiveEngine.calculateCurrentState());
                }
                else if (option.Equals("b"))
                {
                    var input = Console.ReadLine();
                    var parts = input.Split(" ");
                    reactiveEngine.setNode(int.Parse(parts[0]), parts[1]);
                    Console.WriteLine($"Cell #{parts[0]} changed to {parts[1]}");
                }
            }
        }
    }
}
