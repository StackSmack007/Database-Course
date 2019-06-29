namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    public class SeedPaymentsCommand : Command
    {
        public SeedPaymentsCommand(DbContextOptionsBuilder contextOptions) : base(contextOptions)
        {
        }

        public override string Execute(string[] input)
        {
            int usersCount = int.Parse(input[0]);
            return SeedPaymentMethods(usersCount);
        }

        private string SeedPaymentMethods(int n)
        {
            PaymentMethod[] paymentMenthods = new PaymentMethod[n];
            for (int i = 0; i < n; i++)
            {
                PaymentMethod pm = new PaymentMethod();

                paymentMenthods[i] = pm;
                if (i % 2 == 0)
                {
                    CreditCard newCreditCard = GenerateCreditCard();
                    pm.CreditCard = newCreditCard;
                }
                else
                {
                    BankAccount newBankAccount = GenerateBankAccount();
                    pm.BankAccount = newBankAccount;
                }
            }
            using (var context = contextOptions is null ?
          new BillsPaymentSystemContext() :
          new BillsPaymentSystemContext(contextOptions.Options))
            {
                User[] users = context.Users.ToArray();
                for (int i = 0; i < paymentMenthods.Length; i++)
                {
                    paymentMenthods[i].User = users[random.Next(0, users.Length)];
                }
                context.PaymentMethods.AddRange(paymentMenthods.Where(x => Validations.IsValid(x)));

                return $"{context.SaveChanges()} Rows Affected!";
            }
        }

        private CreditCard GenerateCreditCard()
        {
            CreditCard CreditCard = new CreditCard()
            {
                ExpirationDate = DateTime.Now.AddDays(random.Next(5, 365)),
                Limit = (decimal)random.Next(3000, 25000),
                MoneyOwed = (decimal)random.Next(1000, 3000)
            };
            return CreditCard;
        }

        private BankAccount GenerateBankAccount()
        {
            string[] bankNames = { "Tokuda", "FIB", "BNB", "Aliance", "KTB" };
            string[] SWIFTcodes = { "SWIFT_1A", "SWIFT_1AS2", "SWIFT_3DN5" };
            BankAccount BankAccount = new BankAccount()
            {
                Balance = (decimal)random.Next(3000, 25000),
                BankName = bankNames[random.Next(0, bankNames.Length)],
                SwiftCode = SWIFTcodes[random.Next(0, SWIFTcodes.Length)],

            };
            return BankAccount;
        }
               

    }
}
