using FluentValidation;
using System.Linq;

namespace FluentValidationTest.Model.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName)

                .NotEmpty()
                .Length(2, 15);

            RuleFor(p => p.FirstName)
                            .Must(BeValidName)
                            .WithMessage("{PropertyName} must start with Uppercase and contain only leters spaces or dashes")
                            .When(n => (n.FirstName + n.LastName).Length < 20).Must((x) =>{return false; })
                            .WithMessage("The first and LastNames exceeds 20 symbols")
                            .When(n => n.FirstName.StartsWith('A')).Must((x) => { return false; })
                            .Must((x,y) => { return !string.IsNullOrEmpty(x.LastName); })
                            .WithMessage("Please provide valid FirstName!");

            RuleFor(p => p.Age).GreaterThan(12).LessThan(99);

        }


        private bool BeValidName(string name)
        {
            name = name.Replace(" ", string.Empty);
            name = name.Replace("-", string.Empty);
            return name.All(x => char.IsLetter(x)) && char.IsUpper(name[0]);
        }
    }
}