namespace BillsPaymentSystem.App.Core.Commands
{
    using BillsPaymentSystem.Data;
    using BillsPaymentSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Text;
    public class SeedUsersCommand : Command
    {
        public SeedUsersCommand(DbContextOptionsBuilder contextOptions) : base(contextOptions)
        {
        }

        public override string Execute(string[] input)
        {
            int usersCount = int.Parse(input[0]);
            return SeedDataInUsers(usersCount);
        }

        private string SeedDataInUsers(int n)
        {
            string[] fNames = { "Kiril", "Metodi", "Kardam", "Kubrat", "Avitohol", "Sava" };

            string[] lNames = { "Kirilov", "Metodiev", "Samuilov", "Tonev", "Tokmakov", "Stamatov", "Motkurov" };

            string[] mailDomains = { "@abv.bg", "@gmail.com", "@yahoo.com" };

            List<User> newUsers = new List<User>();
            for (int i = 0; i < n; i++)
            {
                string fName = fNames[random.Next(0, fNames.Length)];
                string lName = lNames[random.Next(0, fNames.Length)];
                string password = GeneratePassword();
                string email = fName.ToLower() + "_" + lName.ToLower() + mailDomains[random.Next(0, mailDomains.Length)];

                User user = new User()
                {
                    FirstName = fName,
                    LastName = lName,
                    Password = password,
                    Email = email
                };
                if (Validations.IsValid(user))
                {
                    newUsers.Add(user);
                }
            }

            using (var context = contextOptions is null ?
         new BillsPaymentSystemContext() :
         new BillsPaymentSystemContext(contextOptions.Options))
            {
                context.Users.AddRange(newUsers);
                return $"{context.SaveChanges()} Users Added!";
            }
        }

        private string GeneratePassword()
        {
            StringBuilder sb = new StringBuilder();
            int length = this.random.Next(4, 12);
            for (int i = 0; i < length; i++)
            {
                char symbol = (char)random.Next(33, 127);
                sb.Append(symbol);
            }
            return sb.ToString();
        }

    }
}