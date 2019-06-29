namespace VaporStore.DataProcessor
{
    using Data;
    using System.Linq;
    using VaporStore.Data.Models;

    public static class Bonus
	{
		public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
		{

            bool emailUnused = context.Users.FirstOrDefault(x => x.Email == newEmail) is null;
            if (!emailUnused)
            {
                return $"Email {newEmail} is already taken";
            }
            User user = context.Users.FirstOrDefault(x => x.Username == username);
            if (user is null)
            {
                return $"User {username} not found";
            }

            user.Email = newEmail;
            context.SaveChanges();
            return $"Changed {user.Username}'s email successfully";
        }
	}
}