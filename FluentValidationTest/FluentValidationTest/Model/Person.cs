namespace FluentValidationTest.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public  class Person
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int TownId { get; set; }
        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; }
            
    }
}