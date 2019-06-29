using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FluentValidationTest.Model
{
    public class Town
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string CountryName { get; set; }
        public long population { get; set; }

        public DateTime yearOfFounding { get; set; }
        public virtual ICollection<Person> residents { get; set; }
    }
}