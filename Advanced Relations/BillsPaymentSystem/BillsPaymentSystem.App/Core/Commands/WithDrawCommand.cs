namespace BillsPaymentSystem.App.Core.Commands
{
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
    public class WithDrawCommand : Command
    {
        public WithDrawCommand(DbContextOptionsBuilder contextOptions) : base(contextOptions)
        {
        }

        public override string Execute(string[] input)
        {
            int userId = int.Parse(input[0]);
            decimal ammount = decimal.Parse(input[1]);
          return  Withdraw(userId, ammount);
        }

        private  string Withdraw(int userId, decimal amaunt)
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

                BankAccount[] BAs = user.PaymentMethods.Select(pm => pm.BankAccount).Where(x => x != null).ToArray();
                CreditCard[] CCs = user.PaymentMethods.Select(pm => pm.CreditCard).Where(x => x != null).ToArray();

                decimal overallFUnds = BAs.Sum(x => x.Balance) + CCs.Sum(x => x.LimitLeft);
                if (overallFUnds < amaunt)
                {
                    return "Insufficient funds!";
                }
                if (BAs.Any())
                {
                    for (int i = 0; i < BAs.Length; i++)
                    {
                        if (amaunt == 0) break;

                        BankAccount currentBA = BAs[i];
                        if (currentBA.Balance == 0) continue;

                        if (currentBA.Balance >= amaunt)
                        {
                            sb.AppendLine($"Bank: {currentBA.BankName}\nAccount ID: {currentBA.BankAccountId}\nBalance before transaction: {currentBA.Balance:F2}\nBalance after transaction: {currentBA.Balance - amaunt:F2}");
                            currentBA.Balance -= amaunt;
                            amaunt = 0m;
                            break;
                        }
                        else
                        {
                            sb.AppendLine($"Bank: {currentBA.BankName}\nAccount ID: {currentBA.BankAccountId}\nBalance before transaction: {currentBA.Balance:F2}\nBalance after transaction: 0.00");
                            amaunt -= currentBA.Balance;
                            currentBA.Balance = 0.00m;
                        }
                    }
                }
                if (CCs.Any() && amaunt > 0)
                {
                    for (int i = 0; i < CCs.Length; i++)
                    {

                        CreditCard currentCC = CCs[i];
                        if (currentCC.LimitLeft == 0) continue;

                        if (currentCC.LimitLeft >= amaunt)
                        {
                            sb.AppendLine($"CreditCard with ID: {currentCC.CreditCardId}\nLimit before transaction: {currentCC.Limit:F2}\nLimit after transaction: {currentCC.Limit - amaunt:F2}");
                            currentCC.MoneyOwed += amaunt;
                            amaunt = 0m;
                            break;
                        }
                        else
                        {
                            sb.AppendLine($"CreditCard with ID: {currentCC.CreditCardId}\nLimit before transaction: {currentCC.Limit:F2}\nLimit after transaction: 0,00");
                            amaunt -= currentCC.LimitLeft;
                            currentCC.MoneyOwed += currentCC.LimitLeft;
                        }
                    }
                }
                context.SaveChanges();
                return sb.ToString().Trim();
            }
        }
    }
}
