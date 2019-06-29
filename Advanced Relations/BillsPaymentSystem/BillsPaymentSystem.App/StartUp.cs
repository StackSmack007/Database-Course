namespace BillsPaymentSystem.App
{
    using BillsPaymentSystem.App.Core;
    using BillsPaymentSystem.App.Core.Contracts;
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine("1)SeedUsers [NumberOfUsers]");
            Console.WriteLine("2)SeedPayments [NumberOfPayments]");
            Console.WriteLine("3)UserInfo [UserId to look for]");
            Console.WriteLine("4)WithDraw [UserId] [Ammount]");
            Console.WriteLine("5)Deposit [UserId] [Ammount]");
            Console.WriteLine("6)Exit");
            Console.Write("Enable SQL ConsoleLogging Y/N?:");
            string loggingResponse = Console.ReadLine();
            bool IsLoggingEnabled = loggingResponse.ToLower() == "y" ? true : false;
            Console.WriteLine("================================");
            ICommandInterpreter ci = new CommandInterpreter();

            IEngine engine = new Engine(ci,IsLoggingEnabled);
            engine.Run();

        }
    }
}