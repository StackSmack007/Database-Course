namespace FastFood.Models
{
using System.ComponentModel.DataAnnotations;
    public abstract   class BaseEntityIdentifiable
    {
        [Key]
        public int Id { get; set; }
    }
}