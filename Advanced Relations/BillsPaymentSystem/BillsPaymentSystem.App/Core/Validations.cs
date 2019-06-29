namespace BillsPaymentSystem.App.Core
{
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    public static class Validations
    {
        public static bool IsValid(object entity)
        {
            ValidationContext valContext = new ValidationContext(entity);

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool result= Validator.TryValidateObject(entity, valContext, validationResults, true);
            return result;
        }
    }
}