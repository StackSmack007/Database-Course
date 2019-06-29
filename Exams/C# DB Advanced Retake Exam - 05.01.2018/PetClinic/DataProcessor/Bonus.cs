namespace PetClinic.DataProcessor
{
    using PetClinic.Data;
    using PetClinic.Models;
    using System.Linq;
    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            Vet vet = context.Vets.FirstOrDefault(x => x.PhoneNumber == phoneNumber);


            if (vet is null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }
            string oldProfesion = vet.Profession;
            vet.Profession = newProfession;
            context.SaveChanges();

            string resultMessage = $"{vet.Name}'s profession updated from {oldProfesion} to {newProfession}.";
            return resultMessage;
        }
    }
}