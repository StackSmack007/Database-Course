namespace TestSoftUni.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;
    using TestSoftUni.Core.Commands.Contracts;
    using TestSoftUni.Core.Contracts;
    public class CommandIntepreter : ICommandInterpreter
    {
        private const string Suffix = "Command";
        public string ReadCommand(string[] command)
        {
            string commandName = command[0] + Suffix;
            string[] commandInputParameters = command.Skip(1).ToArray();

            Type commandType = Assembly
                                       .GetCallingAssembly()
                                       .GetTypes()
                                       .FirstOrDefault(x => x.Name == commandName);

            if (commandType is null)
            {
                throw new ArgumentException("Unrecognised Command!");
            }


            ConstructorInfo ctor = commandType.GetConstructors().First();
            Type[] ctorParameterTypes = ctor.GetParameters().Select(x => x.ParameterType).ToArray();
            object[] parameters = ctorParameterTypes.Select(ServicesBox.ServiceProvider.GetService).ToArray();
            
            ICommand foundCommand = (ICommand)ctor.Invoke(parameters);
            return foundCommand.Execute(commandInputParameters);
        }
    }
}
