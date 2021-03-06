﻿namespace PetClinic.DataProcessor
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class AttributeValidator
    {

        public static bool IsValid(object entity)
        {
            ValidationContext vContext = new ValidationContext(entity);
            List<ValidationResult> vResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(entity, vContext, vResults, true);
            return isValid;
        }
    }
}