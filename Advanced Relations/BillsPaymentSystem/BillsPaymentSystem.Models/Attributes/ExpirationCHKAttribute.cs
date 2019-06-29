namespace BillsPaymentSystem.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationCHKAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value<DateTime.Now)
            {
                return ValidationResult.Success;
            }
            string errorMessage = "The expiration date has expired already!";

            return new ValidationResult(errorMessage);
        }
    }
}