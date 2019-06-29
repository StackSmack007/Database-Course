namespace BillsPaymentSystem.App.Core
{
    using BillsPaymentSystem.App.Core.Commands.Contracts;
    using BillsPaymentSystem.App.Core.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Reflection;
    public class CommandInterpreter : ICommandInterpreter
    {
        private const string suffix = "Command";
        public string Read(string[] input, DbContextOptionsBuilder contextOptions)
        {
            string commandName = input[0];

            Type commandType = Assembly.GetCallingAssembly().GetTypes().FirstOrDefault(x => x.Name == commandName + suffix);
            ICommand command = (ICommand)Activator.CreateInstance(commandType,new object[] { contextOptions});
            return command.Execute(input.Skip(1).ToArray());
        }
    }
}