namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public abstract  class BaseEntityId
    {
        [Key]
        public int Id { get; set; }
    }
}