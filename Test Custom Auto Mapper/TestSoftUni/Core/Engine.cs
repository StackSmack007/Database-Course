namespace TestSoftUni.Core
{
    using System;
    using TestSoftUni.Core.Commands.Contracts;
    using TestSoftUni.Core.Contracts;
    public class Engine : IEngine
    {
        private ICommandInterpreter commandInterpreter;
        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            while (input[0].ToLower() != "exit")
            {
                try
                {
                    string result = commandInterpreter.ReadCommand(input);
                    Console.WriteLine(result);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
