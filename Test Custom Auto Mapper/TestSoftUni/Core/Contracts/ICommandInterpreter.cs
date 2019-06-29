namespace TestSoftUni.Core.Contracts
{
    using TestSoftUni.Core.Commands.Contracts;
    public interface ICommandInterpreter
    {
        string ReadCommand(string[] command);
    }
}