namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AttributeValidator
    {
        public static bool IsValid(params object[] entities)
        {
            bool AllValid = true;
            foreach (var entity in entities)
            {
                ValidationContext vContext = new ValidationContext(entity);
                List<ValidationResult> vResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(entity, vContext, vResults, true))
                {
                    AllValid = false;
                    break;
                }

            }
            return AllValid;
        }
    }
}