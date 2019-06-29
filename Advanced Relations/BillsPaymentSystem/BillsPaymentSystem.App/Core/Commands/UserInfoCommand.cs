namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    public class UserInfoCommand : Command
    {
        public UserInfoCommand(DbContextOptionsBuilder contextOptions) : base(contextOptions)
        {
        }

        public override string Execute(string[] input)
        {
            int userId = int.Parse(input[0]);
            return GetInformationForUser(userId);
        }

        private string GetInformationForUser(int targetId)
        {
            StringBuilder sb = new StringBuilder();
            using (var context = contextOptions is null ?
         new BillsPaymentSystemContext() :
         new BillsPaymentSystemContext(contextOptions.Options))
            {
                var user = context.Users.Select(u => new
                {
                    u.UserId,
                    FullName = u.FirstName + " " + u.LastName,
                    BankAccounts = u.PaymentMethods.Where(x => x.BankAccount != null).Select(pm => pm.BankAccount)
                                                   .Select(ba => new
                                                   {
                                                       ba.BankAccountId,
                                                       ba.Balance,
                                                       ba.BankName,
                                                       ba.SwiftCode
                                                   }).ToList(),

                    CreditCards = u.PaymentMethods.Where(x => x.CreditCard != null).Select(pm => pm.CreditCard)
                                                  .Select(cc => new
                                                  {
                                                      cc.CreditCardId,
                                                      cc.Limit,
                                                      cc.MoneyOwed,
                                                      cc.LimitLeft,
                                                      cc.ExpirationDate
                                                  }).ToList(),
                }).FirstOrDefault(u => u.UserId == targetId);

                if (user is null) return $"User with id {targetId} not found!";

                sb.AppendLine($"User: {user.FullName}");
                sb.AppendLine("Bank Accounts:");
                foreach (var ba in user.BankAccounts)
                {
                    sb.AppendLine($"-- ID: {ba.BankAccountId}");
                    sb.AppendLine($"--- Balance: {ba.Balance:F2}");
                    sb.AppendLine($"--- Bank: {ba.BankName}");
                    sb.AppendLine($"--- SWIFT: {ba.SwiftCode}");
                }
                sb.AppendLine("Credit Cards:");
                foreach (var cc in user.CreditCards)
                {
                    sb.AppendLine($"-- ID: {cc.CreditCardId}");
                    sb.AppendLine($"--- Limit: {cc.Limit:F2}");
                    sb.AppendLine($"--- Money Owed: {cc.MoneyOwed:F2}");
                    sb.AppendLine($"--- Limit Left:: {cc.LimitLeft:F2}");
                    sb.AppendLine($"--- Expiration Date: {cc.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                }
                return sb.ToString().TrimEnd();
            }
        }

    }
}
