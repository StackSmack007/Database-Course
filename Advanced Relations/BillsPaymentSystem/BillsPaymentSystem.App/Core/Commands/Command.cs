namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.App.Core.Commands.Contracts;
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public abstract class Command : ICommand
    {
        public abstract string Execute(string[] input);
        protected Random random = new Random();
        protected DbContextOptionsBuilder contextOptions;

        public Command(DbContextOptionsBuilder contextOptions)
        {
            this.contextOptions = contextOptions;
        }


        protected User GetUser(int userId)
        {
            User foundUser;
            using (var context = contextOptions is null?new BillsPaymentSystemContext():new BillsPaymentSystemContext(contextOptions.Options))
            {
                foundUser = context.Users.Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.BankAccount)
                .Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.CreditCard)
                .FirstOrDefault(u => u.UserId == userId);
            }
            return foundUser;
        }
    }
}