namespace VaporStore.Data.Models
{
using System.ComponentModel.DataAnnotations;
    public abstract class ClassWithIdBase
    {
        [Key]
        public int Id { get; set; }

    }
}