namespace BillsPaymentSystem.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute:ValidationAttribute
    {
        private string xorTargetAttribute;
        public XorAttribute(string xorTargetAttribute)
        {
            this.xorTargetAttribute = xorTargetAttribute;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetAttribute = validationContext.ObjectType
                .GetProperty(xorTargetAttribute)
                .GetValue(validationContext.ObjectInstance);

            if ((targetAttribute is null && value !=null)||
                (targetAttribute!=null && value is null))
            {
                return ValidationResult.Success;
            }
            string errorMessage = "The two properties must have opposite values!";
            return new ValidationResult(errorMessage);
        }
    }
}