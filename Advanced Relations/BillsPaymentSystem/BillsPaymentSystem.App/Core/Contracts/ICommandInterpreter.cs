namespace BillsPaymentSystem.App.Core.Contracts
{
    using Microsoft.EntityFrameworkCore;
    public interface ICommandInterpreter
    {
        string Read(string[] input, DbContextOptionsBuilder contextOptions);
    }
}