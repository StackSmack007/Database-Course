namespace SoftJail.DataProcessor
{
    using Data;
    using SoftJail.Data.Models;
    using System;
    using System.Linq;

    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {

            Prisoner prisoner = context.Prisoners.FirstOrDefault(x => x.Id == prisonerId);
            if (prisoner.ReleaseDate is null)
            {
                return $"Prisoner {prisoner.FullName} is sentenced to life";
            }

            prisoner.ReleaseDate = DateTime.Now;
            prisoner.CellId = null;
            context.SaveChanges();
            return $"Prisoner {prisoner.FullName} released";
        }
    }
}