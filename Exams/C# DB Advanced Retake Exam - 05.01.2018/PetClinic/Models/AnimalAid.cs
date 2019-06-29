﻿namespace PetClinic.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public  class AnimalAid:BaseId
    {
        public AnimalAid()
        {
            AnimalAidProcedures = new HashSet<ProcedureAnimalAid>();
        }

        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }

        [Range(typeof(decimal),"0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

       public virtual ICollection<ProcedureAnimalAid> AnimalAidProcedures { get; set; }
    }
}