namespace BillsPaymentSystem.App.Core
{
    using BillsPaymentSystem.App.Core.Contracts;
    using BillsPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Engine : IEngine
    {
        private ICommandInterpreter commandInterpreter;
        private DbContextOptionsBuilder contextOptions;
        private readonly Dictionary<string, string> commandsAllowed = new Dictionary<string, string>
        {
            ["1"] = "SeedUsers",
            ["2"] = "SeedPayments",
            ["3"] = "UserInfo",
            ["4"] = "WithDraw",
            ["5"] = "Deposit",
            ["6"] = "Exit"
        };
        public Engine(ICommandInterpreter commandInterpreter, bool LoggingEnabled = false)
        {
            this.commandInterpreter = commandInterpreter;
            if (LoggingEnabled)
            {
                LoggerFactory sqlCommandLogger = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category,level)=>level==LogLevel.Information             &&

                DbLoggerCategory.Database.Command.Name==category,true)});
                contextOptions = new DbContextOptionsBuilder<BillsPaymentSystemContext>();

                contextOptions.UseSqlServer(@"Server=ZEVS-PC\SQLEXPRESS;Database=BillsPaymentSystem;Integrated Security=true;")
                    .UseLoggerFactory(sqlCommandLogger)
                    .EnableSensitiveDataLogging();
            }
            else
            {
                contextOptions = null;
            }

        }

        public void Run()
        {
            string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            while (input[0] != "Exit")
            {
                if (!ValidateCommand(input))
                {
                    Console.WriteLine("Invalid Command\nType correct command:");
                    input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    continue;
                }
                try
                {
                    string result = commandInterpreter.Read(input, contextOptions);
                    Console.WriteLine(result);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            Console.WriteLine("GoodBye!");
        }

        private bool ValidateCommand(string[] input)
        {
            if (commandsAllowed.ContainsKey(input[0]))
            {
                input[0] = commandsAllowed[input[0]];
            }
            if (commandsAllowed.Values.Contains(input[0]))
            {
                return true;
            }
            return false;
        }
    }
}