namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Text;
    public class DepositCommand : Command
    {
        public DepositCommand(DbContextOptionsBuilder contextOptions) : base(contextOptions) { }
 

        public override string Execute(string[] input)
        {

            int userId = int.Parse(input[0]);
            decimal ammount = decimal.Parse(input[1]);
            return Deposit(userId, ammount);
        }


        private string Deposit(int userId, decimal amaunt)
        {
            StringBuilder sb = new StringBuilder();
       using (var context = contextOptions is null ?
                new BillsPaymentSystemContext() : 
                new BillsPaymentSystemContext(contextOptions.Options))
            {
                var user = GetUser(userId);

                if (user is null)
                {
                    return "User not exist";
                }

                BankAccount ba = user.PaymentMethods.Select(pm => pm.BankAccount).FirstOrDefault();
                CreditCard cc = user.PaymentMethods.Select(pm => pm.CreditCard).FirstOrDefault();

                if (ba != null)
                {
                    sb.AppendLine($"Bank: {ba.BankName}\nAccount ID: {ba.BankAccountId}\nBalance before transaction: {ba.Balance:F2}\nBalance after transaction: {ba.Balance + amaunt:F2}");
                    ba.Balance += amaunt;
                }
                else if (cc != null)
                {
                    sb.AppendLine($"CreditCard with ID: {cc.CreditCardId}\nLimit before transaction: {cc.Limit:F2}\nLimit after transaction: {cc.Limit + amaunt:F2}");
                    cc.Limit += amaunt;
                }
                else
                {
                    return "No BankAccounts or CreditCards to transfer money into!";
                }
                context.SaveChanges();
                return sb.ToString().Trim();
            }
        }
    }
}