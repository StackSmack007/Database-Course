using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Positions
{
    public class CreatePositionInputModel
    {
        [Required]
        [MinLength(4),MaxLength(32)]
        public string PositionName { get; set; }
    }
}
