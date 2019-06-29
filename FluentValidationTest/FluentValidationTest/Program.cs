using FluentValidationTest.Model;
using FluentValidationTest.Model.Validators;
using System;

namespace FluentValidationTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Person person = new Person();
            person.FirstName = "Aenetenba";
            person.LastName = "Tenetssssssssssssssssssssssssssssssssssssss";
            person.Age = 13;
            bool test = (person.FirstName + person.LastName).Length == 4;

               PersonValidator validator = new PersonValidator();

            var validationResultSet = validator.Validate(person);




            if (validationResultSet.IsValid)
            {
                Console.WriteLine("Success!");
                Environment.Exit(0);
            }
            Console.WriteLine("Failure");
            foreach (var error in validationResultSet.Errors)
            {
                Console.WriteLine($"{error.PropertyName} failed because : {error.ErrorMessage}");
            }

        }
    }
}